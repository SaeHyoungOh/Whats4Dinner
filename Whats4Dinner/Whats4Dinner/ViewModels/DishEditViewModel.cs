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
	/// <summary>
	/// ViewModel class for DishEditPage
	/// </summary>
	class DishEditViewModel : BaseViewModel
	{
		Day SelectedDay;
		Meal SelectedMeal;
		Dish SelectedDish;

		/// <summary>
		/// Command to execute when "Save" button is clicked
		/// </summary>
		public DelegateCommand SaveButtonClick;

		/// <summary>
		/// Command to add the dish to the meal
		/// </summary>
		public DelegateCommand AddToMealCommand;

		/// <summary>
		/// Command to perform additional edits: in the DishDB or in the meals, depending on the user's action
		/// </summary>
		public DelegateCommand AdditionalEditDishCommand;
		public string EntryName { get; set; }

		// DishCategory to display in View
		public string DishCategory1
		{
			get
			{
				SetProperty(ref dishCategory1, DishCategories["1"]);
				return dishCategory1;
			}
		}
		public string DishCategory2
		{
			get
			{
				SetProperty(ref dishCategory2, DishCategories["2"]);
				return dishCategory2;
			}
		}
		public string DishCategory3
		{
			get
			{
				SetProperty(ref dishCategory3, DishCategories["3"]);
				return dishCategory3;
			}
		}
		public string DishCategory4
		{
			get
			{
				SetProperty(ref dishCategory4, DishCategories["4"]);
				return dishCategory4;
			}
		}
		// fields for the properties above
		public string DishCategory5
		{
			get
			{
				SetProperty(ref dishCategory5, DishCategories["5"]);
				return dishCategory5;
			}
		}
		private string dishCategory1;
		private string dishCategory2;
		private string dishCategory3;
		private string dishCategory4;
		private string dishCategory5;

		// dish category checkboxes to display in the view, two-way
		public bool DishCategoryCheckBox1 { get; set; }
		public bool DishCategoryCheckBox2 { get; set; }
		public bool DishCategoryCheckBox3 { get; set; }
		public bool DishCategoryCheckBox4 { get; set; }
		public bool DishCategoryCheckBox5 { get; set; }

		/// <summary>
		/// List of Dish Categories built from the user input
		/// </summary>
		private List<string> InputDishCategories { get; set; }

		/// <summary>
		/// Name of the Dish before edit, for looking up
		/// </summary>
		private string NameBeforeEdit { get; set; }

		/// <summary>
		/// Whether the page is created from the DB; it determines the behavior of the commands.
		/// </summary>
		private bool IsFromDB;

		/// <summary>
		/// Gets the user entry for the name and the list of categories, then adds the dish to the meal, and saves it to file.
		/// It is called by the code behind in DishEditPage View.
		/// </summary>
		private void SaveButtonExecute()
		{
			// build the dish category list
			InputDishCategories = new List<string>();
			if (DishCategoryCheckBox1) InputDishCategories.Add(DishCategories["1"]);
			if (DishCategoryCheckBox2) InputDishCategories.Add(DishCategories["2"]);
			if (DishCategoryCheckBox3) InputDishCategories.Add(DishCategories["3"]);
			if (DishCategoryCheckBox4) InputDishCategories.Add(DishCategories["4"]);
			if (DishCategoryCheckBox5) InputDishCategories.Add(DishCategories["5"]);

			// if the page is opened to create a new dish, add the dish to the database
			if (SelectedDish == null)
			{
				DishDB.Add(new Dish(EntryName, InputDishCategories));
				DishDBIO.WriteDishesToJSON(DishDB);
				MessagingCenter.Send(this, "DB updated");	// refresh the search result
			}
			// otherwise edit the dish
			else
			{
				NameBeforeEdit = string.Copy(SelectedDish.Name);	// save the dish name before change

				// if editing from the DishDB, find the dish in DishDB and update it
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
				// if editing from the meal, update the dish in the meal
				else
				{
					SelectedMeal.EditDish(SelectedDish, EntryName, InputDishCategories);
					UserDataIO.WriteUserDataToJSON(UserData);
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

		/// <summary>
		/// Add the Dish as built in the page to the meal, and save to file
		/// </summary>
		private void AddToMealExecute()
		{
			SelectedMeal.AddDish(EntryName, InputDishCategories);
			if (!UserData.Select(day => day.ThisDate).Contains(SelectedDay.ThisDate))
			{
				UserData.Add(SelectedDay);
			}
			UserDataIO.WriteUserDataToJSON(UserData);
		}

		/// <summary>
		/// After editing a Dish and the initial saving is done, more action can be done after the prompt to the user:
		/// If the Dish in the DishDB was edited, the occurrences of the Dish in the meals in the future can be updated.
		/// If the Dish in the Meal was edited, the DishDB can be updated.
		/// </summary>
		private void AdditionalEditDishExecute()
		{
			// if DishDB is edited, update all cases of the dish in the user data, today and later
			if (IsFromDB)
			{
				foreach (Day day in UserData)
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
				UserDataIO.WriteUserDataToJSON(UserData);
			}
			// if the Dish in the Meal was edited, update the Dish in the database
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

		/// <summary>
		/// Whether AdditionalEditDish command can execute; 
		/// </summary>
		/// <returns></returns>
		private bool AdditionalEditDishCanExecute()
		{
			// if the DIsh in the DishDB was edited, always allow additional update to the meals
			if (IsFromDB)
			{
				return true;
			}
			// if the Dish in the Meal was edited, only update if the DishDB still contains that Dish.
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
		/// <param name="UserData"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		/// <param name="SelectedDish"></param>
		public DishEditViewModel(ObservableCollection<Day> UserData, Day SelectedDay, Meal SelectedMeal, Dish SelectedDish = null, bool IsFromDB = false)
		{
			// initialize properties
			this.UserData = UserData;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			this.SelectedDish = SelectedDish;
			this.IsFromDB = IsFromDB;
			UserDataIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			DishDB = DishDBIO.ReadDishesFromJSON();
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();

			// if creating a new dish, initialize an empty form
			if (SelectedDish == null)
			{
				Title = "Add a Dish";
				EntryName = "";
				DishCategoryCheckBox1 = DishCategoryCheckBox2 = DishCategoryCheckBox3 = DishCategoryCheckBox4 = DishCategoryCheckBox5 = false;
			}
			// if editing a dish, pre-fill the form with the existing data
			else
			{
				Title = "Edit Dish";
				EntryName = SelectedDish.Name;
				DishCategoryCheckBox1 = SelectedDish.HasDishCategory1;
				DishCategoryCheckBox2 = SelectedDish.HasDishCategory2;
				DishCategoryCheckBox3 = SelectedDish.HasDishCategory3;
				DishCategoryCheckBox4 = SelectedDish.HasDishCategory4;
				DishCategoryCheckBox5 = SelectedDish.HasDishCategory5;
			}

			// initialize commands
			SaveButtonClick = new DelegateCommand(SaveButtonExecute, SaveButtonCanExecute);
			AddToMealCommand = new DelegateCommand(AddToMealExecute);
			AdditionalEditDishCommand = new DelegateCommand(AdditionalEditDishExecute, AdditionalEditDishCanExecute);
		}
	}
}
