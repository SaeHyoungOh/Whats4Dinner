using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.ViewModels;
using Whats4Dinner.ViewModels.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DishDBPage : ContentPage
	{
		ObservableCollection<Day> DisplayDays;
		Day SelectedDay;
		Meal SelectedMeal;

		public DishDBPage(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;

			InitializeComponent();
			BindingContext = new DishDBViewModel(DisplayDays, SelectedDay, SelectedMeal);
		}

		/// <summary>
		/// When "Create New" button is clicked, navigate to new dish page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void CreateItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new DishPage(DisplayDays, SelectedDay, SelectedMeal)));
		}

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			Dish selectedDish = (Dish)((ListView)sender).SelectedItem;

			// call the command to add dish to meal
			DishDBViewModel viewModel = (DishDBViewModel)BindingContext;
			if (viewModel.AddDishCommand.CanExecute(selectedDish))
			{
				viewModel.AddDishCommand.Execute(selectedDish);
			}

			// close page
			Navigation.PopModalAsync();
		}
	}
}