using System.Collections.Generic;
using System.Collections.ObjectModel;
using Whats4Dinner.ViewModels;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Whats4Dinner.Models;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View to display a Day; BindingContext: DayViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayPage : ContentPage
	{
		private Dictionary<string, object> UserData { get; set; }

		/// <summary>
		/// Cosntructor for DayPage; initializes properties
		/// </summary>
		public DayPage(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;

			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = new DayViewModel(UserData);
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
			UserData["SelectedMeal"] = (Meal)((ListView)sender).SelectedItem;
			await Navigation.PushAsync(new MealPage(UserData));

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}