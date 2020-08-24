using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using System.Collections.ObjectModel;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View for the MasterDetailPage; sets up the navigation for the MenuPage.
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : MasterDetailPage
	{
		/// <summary>
		/// List of Pages user can navigate to from the MenuPage
		/// </summary>
		Dictionary<MenuItemType, NavigationPage> MenuPages = new Dictionary<MenuItemType, NavigationPage>();

		/// <summary>
		/// Constructor for MainPage
		/// </summary>
		public MainPage()
		{
			InitializeComponent();

			MasterBehavior = MasterBehavior.Popover;

			// the Detail of the MasterDetailPage displays the main content - set as the SevenDayPage
			Detail = new NavigationPage(new SevenDayPage());
			MenuPages.Add(MenuItemType.SevenDayView, (NavigationPage)Detail);
		}

		/// <summary>
		/// Navigate from the MenuPage
		/// </summary>
		/// <param name="id">The page to navigate to</param>
		/// <returns></returns>
		public async Task NavigateFromMenu(MenuItemType id)
		{
			// if the page has not already been created, create one and add to the MenuPages list
			if (!MenuPages.ContainsKey(id))
			{
				switch (id)
				{
					case MenuItemType.SevenDayView:
						MenuPages.Add(id, new NavigationPage(new SevenDayPage()));
						break;
					case MenuItemType.WeeklyView:
						MenuPages.Add(id, new NavigationPage(new AboutPage()));
						break;
					case MenuItemType.MonthlyView:
						MenuPages.Add(id, new NavigationPage(new AboutPage()));
						break;
					case MenuItemType.DishDB:
						FileIO UserDaysIO = new FileIO("UserDays.json");
						ObservableCollection<Day> UserDays = UserDaysIO.ReadUserDaysFromJSON();
						Dictionary<string, object> UserData = new Dictionary<string, object>
						{
							{ "UserDays", UserDays }
						};
						MenuPages.Add(id, new NavigationPage(new DishDBPage(UserData)));
						break;
					case MenuItemType.DishCategories:
						MenuPages.Add(id, new NavigationPage(new DishCategoriesPage()));
						break;
					case MenuItemType.About:
						MenuPages.Add(id, new NavigationPage(new AboutPage()));
						break;
				}
			}

			// change the Detail of the MasterDetailPage to the chosen Page
			NavigationPage newPage = MenuPages[id];

			if (newPage != null && Detail != newPage)
			{
				Detail = newPage;

				if (Device.RuntimePlatform == Device.Android)
					await Task.Delay(100);

			}
			IsPresented = false;	// close the menu
		}
	}
}