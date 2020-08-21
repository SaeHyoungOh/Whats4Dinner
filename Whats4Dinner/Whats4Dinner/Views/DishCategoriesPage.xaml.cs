using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	/// <summary>
	/// View to display and edit Dish Categories; BindingContext: DishCategoriesViewModel
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DishCategoriesPage : ContentPage
	{
		public DishCategoriesPage()
		{
			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = new DishCategoriesViewModel();
		}

		/// <summary>
		/// When an item is tapped from the list of dishes, prompt user for edit or moving the positions of the DishCategories
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			KeyValuePair<int, string> dishCategory = (KeyValuePair<int, string>)((ListView)sender).SelectedItem;

			// user prompt for action
			string action;
			if (dishCategory.Key == 1)
			{
				action = await DisplayActionSheet(null, "Cancel", null, "Edit", "Move Down");
			}
			else if (dishCategory.Key == 5)
			{
				action = await DisplayActionSheet(null, "Cancel", null, "Edit", "Move Up");
			}
			else
			{
				action = await DisplayActionSheet(null, "Cancel", null, "Edit", "Move Up", "Move Down");
			}
			
			// edit the DishCategory
			if (action == "Edit")
			{

			}
			// switch index with the previous one
			else if (action == "Move Up")
			{

			}
			// switch index with the next one
			else if (action == "Move Down")
			{

			}

			// Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}