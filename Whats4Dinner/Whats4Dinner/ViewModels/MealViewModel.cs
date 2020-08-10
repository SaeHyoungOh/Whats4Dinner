using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;

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
		/// Whether this meal has any dishes
		/// </summary>
		public bool HasDishes
		{
			get
			{
				SetProperty(ref hasDishes, DisplayDishes.Count != 0);
				return hasDishes;
			}
			set
			{
				SetProperty(ref hasDishes, value);
			}
		}

		/// <summary>
		/// Whether this meal has no dishes
		/// </summary>
		public bool NoDishes
		{
			get
			{
				SetProperty(ref noDishes, DisplayDishes.Count == 0);
				return noDishes;
			}
			set
			{
				SetProperty(ref noDishes, value);
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
		private bool hasDishes;
		private bool noDishes;
		private Day selectedDay;
		private Meal selectedMeal;

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

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}
