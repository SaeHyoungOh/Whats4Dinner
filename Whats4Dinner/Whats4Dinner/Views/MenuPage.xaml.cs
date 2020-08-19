using Whats4Dinner.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whats4Dinner.ViewModels;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		MainPage RootPage { get => Application.Current.MainPage as MainPage; }
		MenuViewModel viewModel;

		public MenuPage()
		{
			InitializeComponent();

			BindingContext = viewModel = new MenuViewModel();

			ListViewMenu.SelectedItem = viewModel.MenuItems[(int)MenuItemType.SevenDayView];
		}

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