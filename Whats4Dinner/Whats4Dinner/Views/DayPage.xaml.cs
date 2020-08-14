using System.Collections.Generic;
using System.Collections.ObjectModel;
using Whats4Dinner.ViewModels;
using Whats4Dinner.ViewModels.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayPage : ContentPage
	{
		private ObservableCollection<Day> DisplayDays;
		private Day SelectedDay;

		public DayPage(ObservableCollection<Day> DisplayDays, Day SelectedDay)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;

			InitializeComponent();
			BindingContext = new DayViewModel(DisplayDays, SelectedDay);
		}
		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			// get the DisplayDays, the day index, and the Meal index
			Meal SelectedMeal = (Meal)((ListView)sender).SelectedItem;
			await Navigation.PushAsync(new MealPage(DisplayDays, SelectedDay, SelectedMeal));

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}