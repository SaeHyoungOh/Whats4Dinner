using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MealPage : ContentPage
	{
		//Meal ThisMeal;

		public MealPage(Meal selected, string previousTitle)
		{
			InitializeComponent();
			//ThisMeal = selected;
			BindingContext = new MealViewModel(selected, previousTitle);
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