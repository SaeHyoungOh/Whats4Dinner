using System;
using System.Collections.ObjectModel;
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

		private void AddItem_Clicked(object sender, EventArgs e)
		{
			//if (e.Item == null)
			//	return;

			Dish selected = (Dish)((Button)sender).CommandParameter;
			string dishname = selected.Name;
			DisplayAlert("AddItem_Clicked", dishname + " was clicked", "Ok");

			//Deselect Item
			//((ListView)sender).SelectedItem = null;
		}

		private void DeleteItem_Clicked(object sender, EventArgs e)
		{
			Dish selected = (Dish)((Button)sender).CommandParameter;
			string dishname = selected.Name;
			DisplayAlert("DeleteItem_Clicked", dishname + " was clicked", "Ok");
			//ThisMeal.DeleteDish(selected);
		}
	}
}