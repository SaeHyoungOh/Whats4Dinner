using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeekPage : ContentPage
	{
		//MainPage RootPage { get => Application.Current.MainPage as MainPage; }

		public WeekPage()
		{
			InitializeComponent();
		}

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			Day selected = (Day)((ListView)sender).SelectedItem;
			await Navigation.PushModalAsync(new NavigationPage(new DayPage(selected)));

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
