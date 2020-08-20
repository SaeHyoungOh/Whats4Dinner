using Whats4Dinner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whats4Dinner.ViewModels;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View for the MenuPage; it is the Master page of the MasterDetailPage.
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		/// <summary>
		/// Root page is the MasterDetailPage
		/// </summary>
		MainPage RootPage { get => Application.Current.MainPage as MainPage; }
		MenuViewModel viewModel;

		/// <summary>
		/// Constructor for MenuPage
		/// </summary>
		public MenuPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new MenuViewModel();

			// start with the 7-Day View when the app starts
			ListViewMenu.SelectedItem = viewModel.MenuItems[(int)MenuItemType.SevenDayView];
		}

		/// <summary>
		/// When an item is tapped from the menu (ListView), call NavigateFromMenu() from the root page to navigate to the selected page. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ListViewMenu_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			HomeMenuItem selectedItem = ((ListView)sender).SelectedItem as HomeMenuItem;

			if (selectedItem == null)
				return;

			MenuItemType id = selectedItem.Id;
			await RootPage.NavigateFromMenu(id);
		}
	}
}