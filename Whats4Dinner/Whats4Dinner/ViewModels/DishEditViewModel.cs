using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using static Whats4Dinner.Models.DataStructure.Dish;

namespace Whats4Dinner.ViewModels
{
	class DishEditViewModel : BaseViewModel
	{
		Day SelectedDay;
		Meal SelectedMeal;
		Dish SelectedDish;

		public DelegateCommand SaveButtonClick;
		public DelegateCommand AddToMealCommand;
		public DelegateCommand AdditionalEditDishCommand;
		public string EntryName { get; set; }
		public bool GrainCheckBox { get; set; }
		public bool VeggieCheckBox { get; set; }
		public bool ProteinCheckBox { get; set; }
		public bool CondimentCheckBox { get; set; }
		public bool DrinkCheckBox { get; set; }

		private List<DishCategory> InputDishCategories { get; set; }

		private string NameBeforeEdit { get; set; }

		private bool IsFromDB;

		/// <summary>
		/// Gets the user entry for the name and the list of categories, then adds the dish to the meal, and saves it to file.
		/// It is called by the code behind in DishEditPage View.
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
				NameBeforeEdit = string.Copy(SelectedDish.Name);	// save the dish name before change

				if (IsFromDB)
				{
					foreach (Dish dish in DishDB)
					{
						if (dish.Name == SelectedDish.Name)
						{
							dish.Name = EntryName;
							dish.DishCategories = InputDishCategories;
							DishDBIO.WriteDishesToJSON(DishDB);
							break;
						}
					}
				}
				else
				{
					SelectedMeal.EditDish(SelectedDish, EntryName, InputDishCategories);
					UserDataIO.WriteUserDataToJSON(DisplayDays);
				}
			}
		}

		/// <summary>
		/// Input validation whether to proceed with saving the dish
		/// </summary>
		/// <returns></returns>
		private bool SaveButtonCanExecute()
		{
			// if the entered name already exists, return false
			if (DishDB.Select(dish => dish.Name).Contains(EntryName))
			{
				if (SelectedDish == null || SelectedDish.Name != EntryName)
				{
					return false;
				}
			}

			return true;
		}

		private void AddToMealExecute()
		{
			SelectedMeal.AddDish(EntryName, InputDishCategories);
			UserDataIO.WriteUserDataToJSON(DisplayDays);
		}

		private void AdditionalEditDishExecute()
		{
			// change all cases of the dish in the user data, today or later
			if (IsFromDB)
			{
				foreach (Day day in DisplayDays)
				{
					if (day.ThisDate >= DateTime.Today)
					{
						foreach (Meal meal in day.Meals)
						{
							foreach (Dish dish in meal.Dishes)
							{
								if (dish.Name == NameBeforeEdit)
								{
									dish.Name = EntryName;
									dish.DishCategories = InputDishCategories;
								}
							}
						}
					}
				}
				UserDataIO.WriteUserDataToJSON(DisplayDays);
			}
			// change the dish in the database
			else
			{
				foreach (Dish dish in DishDB)
				{
					if (dish.Name == NameBeforeEdit)
					{
						dish.Name = EntryName;
						dish.DishCategories = InputDishCategories;
						break;
					}
				}
				DishDBIO.WriteDishesToJSON(DishDB);
			}
		}

		private bool AdditionalEditDishCanExecute()
		{
			if (IsFromDB)
			{
				return true;
			}
			else
			{
				if (DishDB.Select(dish => dish.Name).Contains(NameBeforeEdit))
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// Constructor for AddDishViewModel class
		/// </summary>
		/// <param name="DisplayDays"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		/// <param name="SelectedDish"></param>
		public DishEditViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal, Dish SelectedDish = null, bool IsFromDB = false)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			this.SelectedDish = SelectedDish;
			this.IsFromDB = IsFromDB;
			UserDataIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			DishDB = DishDBIO.ReadDishesFromJSON();

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
			AddToMealCommand = new DelegateCommand(AddToMealExecute);
			AdditionalEditDishCommand = new DelegateCommand(AdditionalEditDishExecute, AdditionalEditDishCanExecute);
		}
	}
}
