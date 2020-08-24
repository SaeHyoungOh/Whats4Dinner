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
	/// <summary>
	/// ViewModel class for DishDBPage
	/// </summary>
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
		/// Query string from the view to be searched in DishDB
		/// </summary>
		public string Query { get; set; }

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
			SelectedMeal.AddDish(SelectedDish.Name, SelectedDish.ThisDishCategories);
			if (!UserDays.Select(day => day.ThisDate).Contains(SelectedDay.ThisDate))
			{
				UserDays.Add(SelectedDay);
			}
			UserDaysIO.WriteUserDaysToJSON(UserDays);
		}

		/// <summary>
		/// Whether AddDishCommand can execute
		/// </summary>
		/// <param name="SelectedDish"></param>
		/// <returns></returns>
		private bool AddDishCanExecute(Dish SelectedDish)
		{
			// make sure the meal does not already contain it
			if (SelectedDish != null && !(SelectedMeal.Dishes.Select(dish => dish.Name).Contains(SelectedDish.Name)))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Delete the selected Dish from the DishDB and save to file
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
			// make sure the DishDB contains the dish
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
			// empty query sets the whole database to DishDB
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
		/// <param name="UserDays"></param>
		/// <param name="SelectedDay"></param>
		/// <param name="SelectedMeal"></param>
		public DishDBViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			if (UserData.ContainsKey("SelectedDay")) { SelectedDay = (Day)UserData["SelectedDay"]; }
			if (UserData.ContainsKey("SelectedMeal")) { SelectedMeal = (Meal)UserData["SelectedMeal"]; }
			UserDaysIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			Title = "Choose a Dish";
			Query = "";

			// get the Dish database from file
			DishDB = DishDBIO.ReadDishesFromJSON();
			UserData["DishDB"] = DishDB;
			SearchResult = DishDB;

			// initialize commands
			AddDishCommand = new DelegateCommand<Dish>(AddDishExecute, AddDishCanExecute);
			DeleteDishCommand = new DelegateCommand<Dish>(DeleteDishExecute, DeleteDishCanExecute);
			SearchCommand = new DelegateCommand(SearchExecute);
			LoadDishesCommand = new DelegateCommand(ExecuteLoadDishesCommand);

			// use MessagingCenter to be notified when the Dish DB is updated, to also update SearchResult
			MessagingCenter.Subscribe<DishEditViewModel>(this, "DB updated", (sender) =>
			{
				SearchExecute();
			});
		}
	}
}
