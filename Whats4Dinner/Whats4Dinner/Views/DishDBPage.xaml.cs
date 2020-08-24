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
	/// <summary>
	/// View to display Dish Database. BindingContext: DishDBViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DishDBPage : ContentPage
	{
		private Dictionary<string, object> UserData;
		private DishDBViewModel viewModel;

		/// <summary>
		/// Cosntructor for DishDBPage; initializes properties
		/// </summary>
		public DishDBPage(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;

			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = viewModel = new DishDBViewModel(UserData);
		}

		/// <summary>
		/// Listener for when the text is changed in the SearchBar; it executes SearchCommand
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			viewModel.SearchCommand.Execute();
		}

		/// <summary>
		/// When "Create New" button is clicked, navigate to new dish page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new DishEditPage(UserData, true));
		}

		/// <summary>
		/// When a ListView item is tapped, prompt user for actions: add, edit, and delete.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Dish selectedDish = (Dish)((ListView)sender).SelectedItem;
			UserData["SelectedDish"] = selectedDish;

			// ask for action
			string action;
			if (UserData["SelectedMeal"] == null)
			{
				action = await DisplayActionSheet(selectedDish.Name, "Cancel", null, "Edit", "Delete");
			}
			else
			{
				action = await DisplayActionSheet(selectedDish.Name, "Cancel", null, "Add to meal", "Edit", "Delete");
			}

			// add the selected Dish to the Meal
			if (action == "Add to meal")
			{
				AddToMealTapped(selectedDish, sender);
			}
			// edit the selected Dish in the DishDB
			else if (action == "Edit")
			{
				await Navigation.PushAsync(new DishEditPage(UserData, true));
			}
			// delete the selected Dish from the DishDB
			else if (action == "Delete")
			{
				DeleteDishTapped(selectedDish);
			}

			((ListView)sender).SelectedItem = null; // deselect
		}

		/// <summary>
		/// Add the selected Dish to the Meal
		/// </summary>
		/// <param name="selectedDish"></param>
		/// <param name="sender"></param>
		private void AddToMealTapped(Dish selectedDish, object sender)
		{
			// call the command to add dish to meal
			if (viewModel.AddDishCommand.CanExecute(selectedDish))
			{
				viewModel.AddDishCommand.Execute(selectedDish);

				// close page
				Navigation.PopAsync();
			}
			// if selected dish is already in the meal
			else
			{
				DisplayAlert(null, selectedDish.Name + " is already included in this meal.", "Ok");
				((ListView)sender).SelectedItem = null; // deselect
			}
		}

		/// <summary>
		/// Delete the selected Dish from the DishDB
		/// </summary>
		/// <param name="selectedDish"></param>
		private async void DeleteDishTapped(Dish selectedDish)
		{
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