using Prism.Commands;
using System.Collections.Generic;
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
		public ObservableCollection<Dish> DisplayDishes
		{
			get => displayDishes;
			set
			{
				SetProperty(ref displayDishes, value);
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
		private ObservableCollection<Dish> displayDishes;
		private Day selectedDay;
		private Meal selectedMeal;

		/// <summary>
		/// Delegate command for the "x" (Delete) button
		/// </summary>
		public DelegateCommand<Dish> DeleteClick { get; private set; }

		/// <summary>
		/// Button click action for "x" (Delete) button
		/// </summary>
		/// <param name="content"></param>
		private void DeleteClickExecute(Dish content)
		{
			// delete the Dish from the Meal, then update the user data file
			SelectedMeal.DeleteDish(content);
			OnPropertyChanged("DisplayDishes");
			UserDataIO.WriteToJSON(DisplayDays);
		}

		/// <summary>
		/// Check whether the meal includes the passed dish
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		private bool DeleteClickCanExecute(Dish content)
		{
			if (content != null && SelectedMeal.Dishes.Contains(content))
			{
				return true;
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
			DisplayDishes = SelectedMeal.Dishes;

			// assign delegate commands
			DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute, DeleteClickCanExecute);

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}
