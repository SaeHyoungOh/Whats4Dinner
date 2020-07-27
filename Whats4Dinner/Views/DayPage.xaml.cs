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
	public partial class DayPage : ContentPage
	{
		public DayPage(Day selected)
		{
			InitializeComponent();
			BindingContext = new DayViewModel(selected);
		}
		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			Meal selected = (Meal)((ListView)sender).SelectedItem;
			await Navigation.PushModalAsync(new NavigationPage(new MealPage(selected)));

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}

	}
}