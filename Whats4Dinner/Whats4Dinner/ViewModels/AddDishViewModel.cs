using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.ViewModels
{
	class AddDishViewModel : BaseViewModel
	{
		Day SelectedDay;
		Meal SelectedMeal;
		public DelegateCommand SaveButtonClick;
		public string EntryName { get; set; }
		public bool GrainCheckBox { get; set; }
		public bool VeggieCheckBox { get; set; }
		public bool ProteinCheckBox { get; set; }
		public bool CondimentCheckBox { get; set; }
		public bool DrinkCheckBox { get; set; }

		public void SaveButtonExecute()
		{
			List<DishCategory> inputDishCategory = new List<DishCategory>();

			if (GrainCheckBox) inputDishCategory.Add(Dish.DishCategory.Grain);
			if (VeggieCheckBox) inputDishCategory.Add(Dish.DishCategory.Veggie);
			if (ProteinCheckBox) inputDishCategory.Add(Dish.DishCategory.Protein);
			if (CondimentCheckBox) inputDishCategory.Add(Dish.DishCategory.Condiment);
			if (DrinkCheckBox) inputDishCategory.Add(Dish.DishCategory.Drink);

			SelectedMeal.AddDish(EntryName, inputDishCategory);
			UserDataIO.WriteToJSON(DisplayDays);
		}

		public bool SaveButtonCanExecute()
		{
			if (EntryName.Length > 0)
			{
				return true;
			}
			return false;
		}

		public AddDishViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			UserDataIO = new FileIO(fileName);
			Title = "Add a Dish";
			EntryName = "";
			GrainCheckBox = VeggieCheckBox = ProteinCheckBox = CondimentCheckBox = DrinkCheckBox = false;

			SaveButtonClick = new DelegateCommand(SaveButtonExecute, SaveButtonCanExecute);
		}
	}
}
