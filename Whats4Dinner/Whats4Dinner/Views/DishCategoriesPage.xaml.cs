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
		private Dictionary<string, object> UserData;
		private DishCategoriesViewModel viewModel;

		public DishCategoriesPage(Dictionary<string, object> UserData)
		{
			this.UserData = UserData;

			// call InitializeComponent() and assign BindingContext
			InitializeComponent();
			BindingContext = viewModel = new DishCategoriesViewModel(UserData);
		}

		/// <summary>
		/// When an item is tapped from the list of dishes, prompt user for edit or moving the positions of the DishCategories
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			KeyValuePair<string, string> dishCategory = (KeyValuePair<string, string>)((ListView)sender).SelectedItem;

			// user prompt for action
			string action;
			if (dishCategory.Key == "1")
			{
				action = await DisplayActionSheet(dishCategory.Value, "Cancel", null, "Edit", "Move Down");
			}
			else if (dishCategory.Key == "5")
			{
				action = await DisplayActionSheet(dishCategory.Value, "Cancel", null, "Edit", "Move Up");
			}
			else
			{
				action = await DisplayActionSheet(dishCategory.Value, "Cancel", null, "Edit", "Move Up", "Move Down");
			}
			
			// edit the DishCategory
			if (action == "Edit")
			{
				viewModel.Entry = await DisplayPromptAsync("Edit Dish Category" + dishCategory.Key, "(max. length: 5)", "OK", "Cancel", "ex. Grain", 5, null, dishCategory.Value);
				viewModel.EditCommand.Execute(dishCategory);
			}
			// switch index with the previous one
			else if (action == "Move Up")
			{
				if (viewModel.MoveUpCommand.CanExecute(dishCategory))
				{
					viewModel.MoveUpCommand.Execute(dishCategory);
				}
			}
			// switch index with the next one
			else if (action == "Move Down")
			{
				if (viewModel.MoveDownCommand.CanExecute(dishCategory))
				{
					viewModel.MoveDownCommand.Execute(dishCategory);
				}
			}

			// Deselect Item
			((ListView)sender).SelectedItem = null;
		}
	}
}