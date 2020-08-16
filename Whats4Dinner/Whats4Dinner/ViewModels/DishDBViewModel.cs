using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;

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

		/// <summary>
		/// Query string from the view to be searched in DishDB
		/// </summary>
		public string Query { get; set; }

		/// <summary>
		/// List of Dishes as the result of searching the query string in DishDB
		/// </summary>
		public ObservableCollection<Dish> SearchResult
		{
			get => searchResult;
			set
			{
				SetProperty(ref searchResult, value);
			}
		}

		// fields for the properties above
		private Day selectedDay;
		private Meal selectedMeal;
		private ObservableCollection<Dish> searchResult;

		/// <summary>
		/// Command to refresh the dish database in view
		/// </summary>
		public DelegateCommand LoadDishesCommand { get; set; }
		/// <summary>
		/// Command to add a dish to the meal, to be used in the View
		/// </summary>
		public DelegateCommand<Dish> AddDishCommand;
		/// <summary>
		/// Command to Delete a dish from the meal, to be used in the View
		/// </summary>
		public DelegateCommand<Dish> DeleteDishCommand;
		/// <summary>
		/// Command to execute the search in view
		/// </summary>
		public DelegateCommand SearchCommand;

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
			if (SelectedDish != null && !(SelectedMeal.Dishes.Select(dish => dish.Name).Contains(SelectedDish.Name)))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Delete the selected Dish from the meal and save to file
		/// </summary>
		/// <param name="SelectedDish"></param>
		private void DeleteDishExecute(Dish SelectedDish)
		{
			DishDB.Remove(SelectedDish);
			DishDBIO.WriteDishesToJSON(DishDB);
		}

		/// <summary>
		/// Whether DeleteDishCommand can execute
		/// </summary>
		/// <param name="SelectedDish"></param>
		/// <returns></returns>
		private bool DeleteDishCanExecute(Dish SelectedDish)
		{
			if (SelectedDish != null && DishDB.Contains(SelectedDish))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Searches the DishDB for the query string, and sets the SearchResult with the matching Dishes.
		/// An empty query string sets the whole DishDB to SearchResult.
		/// </summary>
		private void SearchExecute()
		{
			// empty query returns the whole database
			if (Query == "")
			{
				SearchResult = DishDB;
			}
			// build the search result based on the query string
			else
			{
				SearchResult = new ObservableCollection<Dish>();
				foreach (Dish dish in DishDB)
				{
					if (dish.Name.Contains(Query))
					{
						SearchResult.Add(dish);
					}
				}
			}
		}

		/// <summary>
		/// Refreshes the DishDB and SearchResult, read from the JSON file.
		/// </summary>
		private void ExecuteLoadDishesCommand()
		{
			IsBusy = true;

			try
			{
				SearchResult.Clear();
				DishDB.Clear();

				// read dish data from JSON file
				DishDB = DishDBIO.ReadDishesFromJSON();

				// redo the search
				SearchExecute();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
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
			Query = "";

			// get the Dish database from file
			DishDB = DishDBIO.ReadDishesFromJSON();
			SearchResult = DishDB;

			// initialize commands
			AddDishCommand = new DelegateCommand<Dish>(AddDishExecute, AddDishCanExecute);
			DeleteDishCommand = new DelegateCommand<Dish>(DeleteDishExecute, DeleteDishCanExecute);
			SearchCommand = new DelegateCommand(SearchExecute);
			LoadDishesCommand = new DelegateCommand(ExecuteLoadDishesCommand);
		}
	}
}
