using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.ViewModels
{
	class MainViewModel : BaseViewModel
	{
		public MainViewModel()
		{
			// read user's data from JSON file
			UserDaysIO = new FileIO(userFileName);
			UserDays = UserDaysIO.ReadUserDaysFromJSON();

			// get dish categories from JSON file
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();

			// get Dish Database from JSON file
			DishDBIO = new FileIO(dishFileName);
			DishDB = DishDBIO.ReadDishesFromJSON();

			// fill data parameter
			UserData = new Dictionary<string, object>
			{
				{ "UserDays", UserDays },
				{ "DishCategories", DishCategories },
				{ "DishDB", DishDB },
				{ "DisplayDays", new ObservableCollection<Day>() }
			};

			// initialize commands
			LoadItemsCommand = new DelegateCommand<Dictionary<string, object>>(LoadItemsExecute);
		}
	}
}
