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
		MainViewModel viewModel;
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

			BindingContext = viewModel = new MainViewModel();

			MasterBehavior = MasterBehavior.Popover;

			// the Detail of the MasterDetailPage displays the main content - set as the SevenDayPage
			Detail = new NavigationPage(new SevenDayPage(viewModel.UserData));
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
						MenuPages.Add(id, new NavigationPage(new SevenDayPage(viewModel.UserData)));
						break;
					case MenuItemType.WeeklyView:
						MenuPages.Add(id, new NavigationPage(new WeeklyPage(viewModel.UserData)));
						break;
					case MenuItemType.MonthlyView:
						MenuPages.Add(id, new NavigationPage(new MonthlyPage(viewModel.UserData)));
						break;
					case MenuItemType.DishDB:
						MenuPages.Add(id, new NavigationPage(new DishDBPage(viewModel.UserData)));
						break;
					case MenuItemType.DishCategories:
						MenuPages.Add(id, new NavigationPage(new DishCategoriesPage(viewModel.UserData)));
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
				BaseViewModel? viewModel = null;
				switch (id)
				{
					case MenuItemType.SevenDayView:
						viewModel = (SevenDayViewModel)newPage.Navigation.NavigationStack[0].BindingContext;
						break;
					case MenuItemType.WeeklyView:
						viewModel = (WeeklyViewModel)newPage.Navigation.NavigationStack[0].BindingContext;
						break;
					case MenuItemType.MonthlyView:
						viewModel = (MonthlyViewModel)newPage.Navigation.NavigationStack[0].BindingContext;
						break;
					case MenuItemType.DishDB:
						viewModel = (DishDBViewModel)newPage.Navigation.NavigationStack[0].BindingContext;
						break;
					case MenuItemType.DishCategories:
						viewModel = (DishCategoriesViewModel)newPage.Navigation.NavigationStack[0].BindingContext;
						break;
				}
				if (viewModel != null)
				{
					viewModel.UserData["PageType"] = id;
					viewModel.FillDisplayDays(viewModel.UserData);
				}

				Detail = newPage;

				if (Device.RuntimePlatform == Device.Android)
					await Task.Delay(100);

			}
			IsPresented = false;	// close the menu
		}
	}
}