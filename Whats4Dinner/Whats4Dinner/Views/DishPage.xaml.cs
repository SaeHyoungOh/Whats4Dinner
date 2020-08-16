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
	public partial class DishPage : ContentPage
	{
		ObservableCollection<Day> DisplayDays;
		Day SelectedDay;
		Meal SelectedMeal;
		Dish SelectedDish;

		public DishPage(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal, Dish SelectedDish = null)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			this.SelectedDish = SelectedDish;

			InitializeComponent();

			// new dish page
			if (SelectedDish == null)
			{
				BindingContext = new DishViewModel(DisplayDays, SelectedDay, SelectedMeal);
			}
			// edit dish page
			else
			{
				BindingContext = new DishViewModel(DisplayDays, SelectedDay, SelectedMeal, SelectedDish);
			}
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
			DishViewModel viewModel = (DishViewModel)BindingContext;
			if (viewModel.SaveButtonClick.CanExecute())
			{
				viewModel.SaveButtonClick.Execute();

				// adding a new dish to the database
				if (viewModel.IsNew)
				{
					if (await DisplayAlert(null, "Also add to your meal?", "Yes", "No"))
					{
						viewModel.AddToMealCommand.Execute();
						// return to the meal page
						Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
					}
				}
				// editing a dish
				else
				{
					if (await DisplayAlert(null, "Also make changes to the database?", "Yes", "No"))
					{
						if (viewModel.EditDBCommand.CanExecute())
						{
							viewModel.EditDBCommand.Execute();
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
			// input validation - duplicate name
			else
			{
				await DisplayAlert(null, "The dish name already exists.", "Ok");
				return;
			}

			// close the page
			await Navigation.PopAsync();
		}
	}
}