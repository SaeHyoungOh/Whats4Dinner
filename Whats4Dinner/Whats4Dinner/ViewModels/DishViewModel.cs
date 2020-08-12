using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace Whats4Dinner.ViewModels
{
	class DishViewModel : BaseViewModel
	{
		Day SelectedDay;
		Meal SelectedMeal;
		Dish SelectedDish;

		public DelegateCommand SaveButtonClick;
		public string EntryName { get; set; }
		public bool GrainCheckBox { get; set; }
		public bool VeggieCheckBox { get; set; }
		public bool ProteinCheckBox { get; set; }
		public bool CondimentCheckBox { get; set; }
		public bool DrinkCheckBox { get; set; }

		/// <summary>
		/// Gets the user entry for the name and the list of categories, then adds the dish to the meal, and saves it to file.
		/// It is called by the code behind in AddDishPage View.
		/// </summary>
		public void SaveButtonExecute()
		{
			// build the dish category list
			List<DishCategory> inputDishCategory = new List<DishCategory>();
			if (GrainCheckBox) inputDishCategory.Add(Dish.DishCategory.Grain);
			if (VeggieCheckBox) inputDishCategory.Add(Dish.DishCategory.Veggie);
			if (ProteinCheckBox) inputDishCategory.Add(Dish.DishCategory.Protein);
			if (CondimentCheckBox) inputDishCategory.Add(Dish.DishCategory.Condiment);
			if (DrinkCheckBox) inputDishCategory.Add(Dish.DishCategory.Drink);

			// add dish to the meal
			if (SelectedDish == null)
			{
				SelectedMeal.AddDish(EntryName, inputDishCategory);
			}
			// or edit the dish
			else
			{
				SelectedMeal.EditDish(SelectedDish, EntryName, inputDishCategory);
			}
			// and save to file
			UserDataIO.WriteUserDataToJSON(DisplayDays);
		}

		/// <summary>
		/// Input validation whether to proceed with saving the dish
		/// </summary>
		/// <returns></returns>
		public bool SaveButtonCanExecute()
		{
			if (EntryName.Length > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Constructor for AddDishViewModel class
		/// </summary>
		/// <param name="DisplayDays"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		/// <param name="SelectedDish"></param>
		public DishViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal, Dish SelectedDish = null)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			this.SelectedDish = SelectedDish;
			UserDataIO = new FileIO(fileName);

			// TODO: change the add button to add to the dish database, and ask whether to also add to the meal
			// TODO: change the edit button to edit the meal, and ask whether to also update the database

			// new dish
			if (SelectedDish == null)
			{
				Title = "Add a Dish";
				EntryName = "";
				GrainCheckBox = VeggieCheckBox = ProteinCheckBox = CondimentCheckBox = DrinkCheckBox = false;
			}
			// edit dish
			else
			{
				Title = "Edit Dish";
				EntryName = SelectedDish.Name;
				GrainCheckBox = SelectedDish.HasGrain;
				VeggieCheckBox = SelectedDish.HasVeggie;
				ProteinCheckBox = SelectedDish.HasProtein;
				CondimentCheckBox = SelectedDish.HasCondiment;
				DrinkCheckBox = SelectedDish.HasDrink;
			}

			// initialize commands
			SaveButtonClick = new DelegateCommand(SaveButtonExecute, SaveButtonCanExecute);
		}
	}
}
