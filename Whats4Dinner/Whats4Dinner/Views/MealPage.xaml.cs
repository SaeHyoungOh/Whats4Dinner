using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View to display a Meal; BindingContext: MealViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MealPage : ContentPage
	{
		ObservableCollection<Day> UserData;
		Day SelectedDay;
		Meal SelectedMeal;

		/// <summary>
		/// Cosntructor for MealPage; initializes properties
		/// </summary>
		/// <param name="UserData"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		public MealPage(ObservableCollection<Day> UserData, Day SelectedDay, Meal SelectedMeal)
		{
			// initialize properties
			this.UserData = UserData;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;

			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = new MealViewModel(UserData, SelectedDay, SelectedMeal);
		}

		/// <summary>
		/// When the "Add" button is clicked, navigate to a new dish page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void AddItem_Clicked(object sender, EventArgs e)
		{
			// navigate to dish db page
			await Navigation.PushAsync(new DishDBPage(UserData, SelectedDay, SelectedMeal));
		}

		/// <summary>
		/// When an item is tapped from the list of dishes, navigate to its edit page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void DishCategories_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Dish selectedDish = (Dish)((ListView)sender).SelectedItem;

			// ask for action
			string action = await DisplayActionSheet(selectedDish.Name, "Cancel", null, "Remove", "Edit");

			if (action == "Edit")
			{
				await Navigation.PushAsync(new DishEditPage(UserData, SelectedDay, SelectedMeal, selectedDish));
			}
			else if (action == "Remove")
			{
				// confirm delete
				if (await DisplayAlert(null, "Remove this Dish?", "Remove", "Cancel"))
				{
					// call the command
					MealViewModel viewModel = (MealViewModel)BindingContext;
					if (viewModel.RemoveButtonClick.CanExecute(selectedDish))
					{
						viewModel.RemoveButtonClick.Execute(selectedDish);
					}
				}
			}

			// Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
