using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;

namespace Whats4Dinner.ViewModels
{
	class DishDBViewModel : BaseViewModel
	{
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
		private Day selectedDay;
		private Meal selectedMeal;

		/// <summary>
		/// Command to add a dish to the meal, to be used in the View
		/// </summary>
		public DelegateCommand<Dish> AddDishCommand;

		/// <summary>
		/// Add the selected Dish to the meal and save to file
		/// </summary>
		/// <param name="SelectedDish"></param>
		private void AddDishExecute(Dish SelectedDish)
		{
			SelectedMeal.AddDish(SelectedDish.Name, SelectedDish.DishCategories);
			UserDataIO.WriteUserDataToJSON(DisplayDays);
		}

		/// <summary>
		/// Whether AddDishCommand can execute
		/// </summary>
		/// <param name="SelectedDish"></param>
		/// <returns></returns>
		private bool AddDishCanExecute(Dish SelectedDish)
		{
			if (SelectedDish != null)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Constructor for DishDBViewModel
		/// </summary>
		/// <param name="DisplayDays"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		public DishDBViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			// initialize properties
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;
			UserDataIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			Title = "Choose a Dish";

			// get the Dish database from file
			DishDB = DishDBIO.ReadDishesFromJSON();

			// initialize commands
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
			AddDishCommand = new DelegateCommand<Dish>(AddDishExecute, AddDishCanExecute);
		}
	}
}
