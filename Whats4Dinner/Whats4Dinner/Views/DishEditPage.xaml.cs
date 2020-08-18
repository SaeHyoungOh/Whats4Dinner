using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DishEditPage : ContentPage
	{
		ObservableCollection<Day> DisplayDays;
		Day SelectedDay;
		Meal SelectedMeal;
		Dish SelectedDish;
		DishEditViewModel viewModel;
		bool IsFromDB;

		public DishEditPage(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal, Dish SelectedDish = null, bool IsFromDB = false, ObservableCollection<Dish> DishDB = null)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			this.SelectedDish = SelectedDish;
			this.IsFromDB = IsFromDB;

			InitializeComponent();
			BindingContext = viewModel = new DishEditViewModel(DisplayDays, SelectedDay, SelectedMeal, SelectedDish, IsFromDB, DishDB);
		}

		private async void Save_Clicked(object sender, EventArgs e)
		{
			// input validation - empty name
			if (newName.Text.Length == 0)
			{
				await DisplayAlert(null, "Please enter a dish Name.", "Ok");
				return;
			}

			// call the command
			if (viewModel.SaveButtonClick.CanExecute())
			{
				viewModel.SaveButtonClick.Execute();

				// if added a new dish to the database
				if (SelectedDish == null)
				{
					await AddDishToMealPrompt();
				}
				// if edited a dish
				else
				{
					await EditDishPrompt();
				}
			}
			// input validation - duplicate name
			else
			{
				await DisplayAlert(null, "The dish name already exists.", "Ok");
				return;
			}

			// close the page
			await Navigation.PopAsync();
		}

		private async Task AddDishToMealPrompt()
		{
			if (await DisplayAlert(null, "Also add to your meal?", "Yes", "No"))
			{
				viewModel.AddToMealCommand.Execute();
				// return to the meal page
				Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
			}
		}

		private async Task EditDishPrompt()
		{
			if (IsFromDB)
			{
				if (await DisplayAlert(null, "Also make changes to all the planned meals?", "Yes", "No"))
				{
					if (viewModel.AdditionalEditDishCommand.CanExecute())
					{
						viewModel.AdditionalEditDishCommand.Execute();
					}
				}
			}
			else
			{
				if (await DisplayAlert(null, "Also make changes to the database?", "Yes", "No"))
				{
					if (viewModel.AdditionalEditDishCommand.CanExecute())
					{
						viewModel.AdditionalEditDishCommand.Execute();
					}
					// if the dish no longer exists in the database
					else
					{
						if (await DisplayAlert(null, "The dish does not exist in the database. Add to the database?", "Yes", "No"))
						{
							viewModel.SaveButtonClick.Execute();
						}
					}
				}

			}
		}
	}
}