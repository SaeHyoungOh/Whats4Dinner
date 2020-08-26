using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.Models.DataStructure;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeeklyPage : ContentPage
	{
		private Dictionary<string, object> UserData;

		public WeeklyPage(Dictionary<string, object> UserData)
		{
			this.UserData = UserData;

			InitializeComponent();
			BindingContext = new WeeklyViewModel(UserData);
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
			// add the selected day to UserData
			UserData["SelectedDay"] = (Day)((ListView)sender).SelectedItem;

			// Navigate to the DayPage
			await Navigation.PushAsync(new DayPage(UserData));

			// Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}