using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
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
		public DelegateCommand AddToMealCommand;
		public DelegateCommand EditDBCommand;
		public string EntryName { get; set; }
		public bool GrainCheckBox { get; set; }
		public bool VeggieCheckBox { get; set; }
		public bool ProteinCheckBox { get; set; }
		public bool CondimentCheckBox { get; set; }
		public bool DrinkCheckBox { get; set; }

		private List<DishCategory> InputDishCategories { get; set; }

		private Dish BeforeEdit { get; set; }

		public bool IsNew { get; set; }

		/// <summary>
		/// Gets the user entry for the name and the list of categories, then adds the dish to the meal, and saves it to file.
		/// It is called by the code behind in AddDishPage View.
		/// </summary>
		private void SaveButtonExecute()
		{
			// build the dish category list
			InputDishCategories = new List<DishCategory>();
			if (GrainCheckBox) InputDishCategories.Add(Dish.DishCategory.Grain);
			if (VeggieCheckBox) InputDishCategories.Add(Dish.DishCategory.Veggie);
			if (ProteinCheckBox) InputDishCategories.Add(Dish.DishCategory.Protein);
			if (CondimentCheckBox) InputDishCategories.Add(Dish.DishCategory.Condiment);
			if (DrinkCheckBox) InputDishCategories.Add(Dish.DishCategory.Drink);

			// add dish to the database
			if (SelectedDish == null)
			{
				DishDB.Add(new Dish(EntryName, InputDishCategories));
				DishDBIO.WriteDishesToJSON(DishDB);
			}
			// or edit the dish
			else
			{
				BeforeEdit = new Dish(SelectedDish.Name, SelectedDish.DishCategories);
				SelectedMeal.EditDish(SelectedDish, EntryName, InputDishCategories);
				UserDataIO.WriteUserDataToJSON(DisplayDays);
			}
		}

		/// <summary>
		/// Input validation whether to proceed with saving the dish
		/// </summary>
		/// <returns></returns>
		private bool SaveButtonCanExecute()
		{
			if (EntryName.Length > 0)
			{
				return true;
			}
			return false;
		}

		private void AddToMealExecute()
		{
			SelectedMeal.AddDish(EntryName, InputDishCategories);
			UserDataIO.WriteUserDataToJSON(DisplayDays);
		}

		private void EditDBExecute()
		{
			foreach (Dish dish in DishDB)
			{
				if (dish.Name == BeforeEdit.Name)
				{
					dish.Name = EntryName;
					dish.DishCategories = InputDishCategories;
				}
			}
			DishDBIO.WriteDishesToJSON(DishDB);
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
			UserDataIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			DishDB = DishDBIO.ReadDishesFromJSON();

			// new dish
			if (SelectedDish == null)
			{
				Title = "Add a Dish";
				EntryName = "";
				GrainCheckBox = VeggieCheckBox = ProteinCheckBox = CondimentCheckBox = DrinkCheckBox = false;
				IsNew = true;
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
				IsNew = false;
			}

			// initialize commands
			SaveButtonClick = new DelegateCommand(SaveButtonExecute, SaveButtonCanExecute);
			AddToMealCommand = new DelegateCommand(AddToMealExecute);
			EditDBCommand = new DelegateCommand(EditDBExecute);
		}
	}
}
