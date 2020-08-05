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

		private async void AddItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new AddDishPage(DisplayDays, SelectedDay, SelectedMeal)));

		}

		private void DeleteItem_Clicked(object sender, EventArgs e)
		{
			Dish selected = (Dish)((ListView)sender).SelectedItem;
			SelectedMeal.DeleteDish(selected);
		}

		private void DishCategories_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			// Deselect Item
			((ListView)sender).SelectedItem = null;

			// delete item
			DeleteItem_Clicked(sender, e);
		}
	}
}
