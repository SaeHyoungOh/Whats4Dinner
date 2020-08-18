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

		// DishCategory for View
		public string DishCategory0
		{
			get
			{
				SetProperty(ref dishCategory0, DishCategories[0]);
				return dishCategory0;
			}
		}
		public string DishCategory1
		{
			get
			{
				SetProperty(ref dishCategory1, DishCategories[1]);
				return dishCategory1;
			}
		}
		public string DishCategory2
		{
			get
			{
				SetProperty(ref dishCategory2, DishCategories[2]);
				return dishCategory2;
			}
		}
		public string DishCategory3
		{
			get
			{
				SetProperty(ref dishCategory3, DishCategories[3]);
				return dishCategory3;
			}
		}
		public string DishCategory4
		{
			get
			{
				SetProperty(ref dishCategory4, DishCategories[4]);
				return dishCategory4;
			}
		}
		// fields for the properties above
		private string dishCategory0;
		private string dishCategory1;
		private string dishCategory2;
		private string dishCategory3;
		private string dishCategory4;

		public bool DishCategoryCheckBox0 { get; set; }
		public bool DishCategoryCheckBox1 { get; set; }
		public bool DishCategoryCheckBox2 { get; set; }
		public bool DishCategoryCheckBox3 { get; set; }
		public bool DishCategoryCheckBox4 { get; set; }

		private List<string> InputDishCategories { get; set; }

		private string NameBeforeEdit { get; set; }

		private bool IsFromDB;

		/// <summary>
		/// Gets the user entry for the name and the list of categories, then adds the dish to the meal, and saves it to file.
		/// It is called by the code behind in DishEditPage View.
		/// </summary>
		private void SaveButtonExecute()
		{
			// build the dish category list
			InputDishCategories = new List<string>();
			if (DishCategoryCheckBox0) InputDishCategories.Add(DishCategories[0]);
			if (DishCategoryCheckBox1) InputDishCategories.Add(DishCategories[1]);
			if (DishCategoryCheckBox2) InputDishCategories.Add(DishCategories[2]);
			if (DishCategoryCheckBox3) InputDishCategories.Add(DishCategories[3]);
			if (DishCategoryCheckBox4) InputDishCategories.Add(DishCategories[4]);

			// add dish to the database
			if (SelectedDish == null)
			{
				DishDB.Add(new Dish(EntryName, InputDishCategories));
				DishDBIO.WriteDishesToJSON(DishDB);
				MessagingCenter.Send(this, "DB updated");	// refresh the search result
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
							dish.ThisDishCategories = InputDishCategories;
							DishDBIO.WriteDishesToJSON(DishDB);
							MessagingCenter.Send(this, "DB updated");	// refresh the search result
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
									dish.ThisDishCategories = InputDishCategories;
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
						dish.ThisDishCategories = InputDishCategories;
						break;
					}
				}
				DishDBIO.WriteDishesToJSON(DishDB);
				MessagingCenter.Send(this, "DB updated");   // refresh the search result
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
		public DishEditViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal, Dish SelectedDish = null, bool IsFromDB = false, ObservableCollection<Dish> DishDB = null)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			this.SelectedDish = SelectedDish;
			this.IsFromDB = IsFromDB;
			UserDataIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			this.DishDB = DishDB;

			// new dish
			if (SelectedDish == null)
			{
				Title = "Add a Dish";
				EntryName = "";
				DishCategoryCheckBox0 = DishCategoryCheckBox1 = DishCategoryCheckBox2 = DishCategoryCheckBox3 = DishCategoryCheckBox4 = false;
			}
			// edit dish
			else
			{
				Title = "Edit Dish";
				EntryName = SelectedDish.Name;
				DishCategoryCheckBox0 = SelectedDish.HasDishCategory0;
				DishCategoryCheckBox1 = SelectedDish.HasDishCategory1;
				DishCategoryCheckBox2 = SelectedDish.HasDishCategory2;
				DishCategoryCheckBox3 = SelectedDish.HasDishCategory3;
				DishCategoryCheckBox4 = SelectedDish.HasDishCategory4;
			}

			// initialize commands
			SaveButtonClick = new DelegateCommand(SaveButtonExecute, SaveButtonCanExecute);
			AddToMealCommand = new DelegateCommand(AddToMealExecute);
			AdditionalEditDishCommand = new DelegateCommand(AdditionalEditDishExecute, AdditionalEditDishCanExecute);
		}
	}
}
