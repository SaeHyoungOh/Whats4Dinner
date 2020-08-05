using System.Collections.ObjectModel;
using Whats4Dinner.ViewModels.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeekPage : ContentPage
	{
		public WeekPage()
		{
			InitializeComponent();
		}

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;
			// get the DisplayDays and the day index
			ObservableCollection<Day> DisplayDays = (ObservableCollection<Day>)((ListView)sender).ItemsSource;
			Day SelectedDay = (Day)((ListView)sender).SelectedItem;

			await Navigation.PushModalAsync(new NavigationPage(new DayPage(DisplayDays, SelectedDay)));

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}
