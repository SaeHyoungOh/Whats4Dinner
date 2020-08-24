using System.Collections.ObjectModel;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View to display the 7-day page; BindingContext: SevenDayViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SevenDayPage : ContentPage
	{
		/// <summary>
		/// Cosntructor for SevenDayPage
		/// </summary>
		public SevenDayPage()
		{
			InitializeComponent();
		}

		/// <summary>
		/// When a Day is selected, navigate to its DayPage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;
			// get the data from ViewModel
			SevenDayViewModel viewModel = (SevenDayViewModel)BindingContext;
			viewModel.UserData["SelectedDay"] = (Day)((ListView)sender).SelectedItem;

			// Navigate to the DayPage
			await Navigation.PushAsync(new DayPage(viewModel.UserData));


			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
