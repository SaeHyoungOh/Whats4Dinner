using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using Whats4Dinner.ViewModels.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddDishPage : ContentPage
	{
		ObservableCollection<Day> DisplayDays;
		Day SelectedDay;
		Meal SelectedMeal;

		public AddDishPage(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;

			InitializeComponent();
			BindingContext = new AddDishViewModel(DisplayDays, SelectedDay, SelectedMeal);
		}

		private void Save_Clicked(object sender, EventArgs e)
		{
			// input validation
			if (newName.Text.Length == 0)
			{
				DisplayAlert("", "Please enter a Dish Name.", "Ok");
				return;
			}

			// call the command and close the page
			var viewModel = (AddDishViewModel)BindingContext;
			if (viewModel.SaveButtonClick.CanExecute())
			{
				viewModel.SaveButtonClick.Execute();
			}
			Navigation.PopModalAsync();
		}
	}
}