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
	/// <summary>
	/// View to display Dish Editing; BindingContext: DishEditViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DishEditPage : ContentPage
	{
		private Dictionary<string, object> UserData;
		private DishEditViewModel viewModel;
		/// <summary>
		/// Whether this page is created from the DishDB
		/// </summary>
		bool IsFromDB;

		/// <summary>
		/// Cosntructor for DishEditPage; initializes properties
		/// </summary>
		/// <param name="UserDays"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		/// <param name="SelectedDish"></param>
		/// <param name="IsFromDB"></param>
		/// <param name="DishDB"></param>
		public DishEditPage(Dictionary<string, object> UserData, bool IsFromDB = false)
		{
			// initialize properties
			this.UserData = UserData;
			this.IsFromDB = IsFromDB;

			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = viewModel = new DishEditViewModel(UserData, IsFromDB);
		}

		/// <summary>
		/// When "Save" button is clicked, the Dish is saved to its appropriate locations, with additional updates depending on the user prompt
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void Save_Clicked(object sender, EventArgs e)
		{
			// input validation - empty name
			if (newName.Text.Length == 0)
			{
				await DisplayAlert(null, "Please enter a dish Name.", "Ok");
				return;
			}

			// call the command to save the Dish and prompt for additional updates
			if (viewModel.SaveButtonClick.CanExecute())
			{
				viewModel.SaveButtonClick.Execute();

				// if added a new dish to the database from a meal view
				if (UserData.ContainsKey("SelectedMeal") && !UserData.ContainsKey("SelectedDish"))
				{
					await AddDishToMealPrompt();
				}
				// if edited a dish
				else if (UserData.ContainsKey("SelectedDish"))
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

		/// <summary>
		/// User prompt whether to add the Dish to the Meal, and return to the MealPage upon closing
		/// </summary>
		/// <returns></returns>
		private async Task AddDishToMealPrompt()
		{
			if (await DisplayAlert(null, "Also add to your meal?", "Yes", "No"))
			{
				viewModel.AddToMealCommand.Execute();
				// return to the meal page
				Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
			}
		}

		/// <summary>
		/// User prompts whether to do additional updates to the Dish Database or the Meal, depending on whether it is the DishDB or the Meal being edited.
		/// </summary>
		/// <returns></returns>
		private async Task EditDishPrompt()
		{
			// if editing the Dish in the DishDB, prompt to update all the planned meals
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
			// if editing the Dish in the Meal, prompt to update the DishDB
			else if (UserData["SelectedMeal"] != null)
			{
				if (await DisplayAlert(null, "Also make changes to the database?", "Yes", "No"))
				{
					if (viewModel.AdditionalEditDishCommand.CanExecute())
					{
						viewModel.AdditionalEditDishCommand.Execute();
					}
					// if the dish no longer exists in the database, prompt to create on in the DishDB
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