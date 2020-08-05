using Prism.Commands;
using System.Collections.ObjectModel;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// Viewmodel to display MealPage, with a list of dishes
	/// Derived from BaseViewModel
	/// </summary>
	class MealViewModel : BaseViewModel
	{
		/// <summary>
		/// List of the Dishes in the Meal to display on View, categorized by DishGroup
		/// </summary>
		public ObservableCollection<DishGroup> DisplayDishLists
		{
			get => displayDishLists;
			set
			{
				SetProperty(ref displayDishLists, value);
			}
		}

		/// <summary>
		/// The currently selected Day that this meal belongs to
		/// </summary>
		public Day SelectedDay
		{
			get => selectedDay;
			set
			{
				SetProperty(ref selectedDay, value);
			}
		}

		/// <summary>
		/// The currently selected Meal
		/// </summary>
		public Meal SelectedMeal
		{
			get => selectedMeal;
			set
			{
				SetProperty(ref selectedMeal, value);
			}
		}

		// fields for the properties above
		private ObservableCollection<DishGroup> displayDishLists;
		private Day selectedDay;
		private Meal selectedMeal;

		/// <summary>
		/// Delegate command for the "Add" button
		/// </summary>
		public DelegateCommand AddClick { get; private set; }

		/// <summary>
		/// Delegate command for the "x" (Delete) button
		/// </summary>
		public DelegateCommand<Dish> DeleteClick { get; private set; }

		/// <summary>
		/// Button click action for "Add" button
		/// </summary>
		/// <param name="content"></param>
		private void AddClickExecute()
		{
			// for test only; delete for production
			SelectedMeal.AddDish("test add", DishCategory.Drinks);

			// TODO: create "AddPage" to add dishes to the meal, and remove this method

			OnPropertyChanged("DisplayDishLists");
			UserDataIO.WriteToJSON(DisplayDays);
		}

		/// <summary>
		/// Button click action for "x" (Delete) button
		/// </summary>
		/// <param name="content"></param>
		private void DeleteClickExecute(Dish content)
		{
			// delete the Dish from the Meal, then update the user data file
			SelectedMeal.DeleteDish(content);
			OnPropertyChanged("DisplayDishLists");
			UserDataIO.WriteToJSON(DisplayDays);
		}

		/// <summary>
		/// Check whether the meal includes the passed dish
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		private bool DeleteClickCanExecute(Dish content)
		{
			foreach (DishGroup dishGroup in DisplayDishLists)
			{
				if (dishGroup.DishGroupCategory == content.ThisDishCategory)
				{
					if (dishGroup.Contains(content))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Constructor for MealViewModel
		/// </summary>
		/// <param name="selected">selected Meal from MealPage</param>
		/// <param name="previousTitle">Title from MealPage</param>
		public MealViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			Title = SelectedMeal.ThisMealType.ToString() + ", " + SelectedDay.DisplayDayOfWeek + " " + SelectedDay.DisplayDate;
			UserDataIO = new FileIO(fileName);
			DisplayDishLists = SelectedMeal.Dishes;

			// assign delegate commands
			AddClick = new DelegateCommand(AddClickExecute);
			DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute, DeleteClickCanExecute);

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}
