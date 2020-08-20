using System.Collections.Generic;
using System.Collections.ObjectModel;
using Whats4Dinner.ViewModels;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View to display a Day; BindingContext: DayViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayPage : ContentPage
	{
		private ObservableCollection<Day> DisplayDays;
		private Day SelectedDay;

		/// <summary>
		/// Cosntructor for DayPage; initializes properties
		/// </summary>
		/// <param name="DisplayDays"></param>
		/// <param name="SelectedDay"></param>
		public DayPage(ObservableCollection<Day> DisplayDays, Day SelectedDay)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;

			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = new DayViewModel(DisplayDays, SelectedDay);
		}

		/// <summary>
		/// When a Meal is selected, navigate to its MealPage.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			// get the selected Meal, and navigate to its MealPage
			Meal SelectedMeal = (Meal)((ListView)sender).SelectedItem;
			await Navigation.PushAsync(new MealPage(DisplayDays, SelectedDay, SelectedMeal));

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}