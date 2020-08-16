using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Whats4Dinner.ViewModels;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DishDBPage : ContentPage
	{
		ObservableCollection<Day> DisplayDays;
		Day SelectedDay;
		Meal SelectedMeal;
		DishDBViewModel viewModel;

		public DishDBPage(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;

			InitializeComponent();
			BindingContext = viewModel = new DishDBViewModel(DisplayDays, SelectedDay, SelectedMeal);
		}

		/// <summary>
		/// When "Create New" button is clicked, navigate to new dish page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void CreateItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new DishEditPage(DisplayDays, SelectedDay, SelectedMeal));
		}

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Dish selectedDish = (Dish)((ListView)sender).SelectedItem;

			// call the command to add dish to meal
			if (viewModel.AddDishCommand.CanExecute(selectedDish))
			{
				viewModel.AddDishCommand.Execute(selectedDish);

				// close page
				Navigation.PopAsync();
			}
			// selected dish is already in the meal
			else
			{
				DisplayAlert(null, selectedDish.Name + " is already included in this meal.", "Ok");
				((ListView)sender).SelectedItem = null;	// deselect
			}
		}

		private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			((DishDBViewModel)BindingContext).SearchCommand.Execute();
		}

		private async void DeleteItem_Clicked(object sender, EventArgs e)
		{
			Button thisButton = (Button)sender;
			Dish selectedDish = (Dish)thisButton.BindingContext;

			if (await DisplayAlert(null, "Delete " + selectedDish.Name + " from the database?", "Delete", "Cancel"))
			{
				// call the command to delete dish from the meal
				if (viewModel.DeleteDishCommand.CanExecute(selectedDish))
				{
					viewModel.DeleteDishCommand.Execute(selectedDish);
				}
			}
		}
	}
}