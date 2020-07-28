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
		public MealPage(Meal selected, string previousTitle)
		{
			InitializeComponent();
			BindingContext = new MealViewModel(selected, previousTitle);
		}

		private void AddItem_Clicked(object sender, EventArgs e)
		{
			//if (e.Item == null)
			//	return;

			//Meal selected = (Meal)((ListView)sender).SelectedItem;
			//await Navigation.PushModalAsync(new NavigationPage(new MealPage(selected, Title)));
			DisplayAlert("AddItem_Clicked", "AddItem_Clicked was clicked", "Ok");

			//Deselect Item
			//((ListView)sender).SelectedItem = null;
		}

		//private void DeleteItem_Clicked(object sender, EventArgs e)
		//{
		//	Dish selected = (Dish)((Button)sender).CommandParameter;
		//	string dishname = selected.Name;
		//	DisplayAlert("DeleteItem_Clicked", dishname + " was clicked", "Ok");
		//}
	}
}