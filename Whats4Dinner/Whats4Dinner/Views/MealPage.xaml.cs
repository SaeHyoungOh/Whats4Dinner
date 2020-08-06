using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using Whats4Dinner.ViewModels.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MealPage : ContentPage
	{
		ObservableCollection<Day> DisplayDays;
		Day SelectedDay;
		Meal SelectedMeal;

		public MealPage(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;

			InitializeComponent();
			BindingContext = new MealViewModel(DisplayDays, SelectedDay, SelectedMeal);
		}

		/// <summary>
		/// When the "Add" button is clicked, navigate to a new dish page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void AddItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new DishPage(DisplayDays, SelectedDay, SelectedMeal)));

		}

		/// <summary>
		/// When an item is tapped from the list of dishes, navigate to its edit page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void DishCategories_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Dish selectedDish = (Dish)((ListView)sender).SelectedItem;
			await Navigation.PushModalAsync(new NavigationPage(new DishPage(DisplayDays, SelectedDay, SelectedMeal, selectedDish)));

			// Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
