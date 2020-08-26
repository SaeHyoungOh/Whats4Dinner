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

		// DishCategory for display in View
		public string DishCategory1
		{
			get
			{
				SetProperty(ref dishCategory1, DishCategories["1"]);
				return dishCategory1;
			}
			set
			{
				SetProperty(ref dishCategory1, value);
			}
		}
		public string DishCategory2
		{
			get
			{
				SetProperty(ref dishCategory2, DishCategories["2"]);
				return dishCategory2;
			}
			set
			{
				SetProperty(ref dishCategory2, value);
			}
		}
		public string DishCategory3
		{
			get
			{
				SetProperty(ref dishCategory3, DishCategories["3"]);
				return dishCategory3;
			}
			set
			{
				SetProperty(ref dishCategory3, value);
			}
		}
		public string DishCategory4
		{
			get
			{
				SetProperty(ref dishCategory4, DishCategories["4"]);
				return dishCategory4;
			}
			set
			{
				SetProperty(ref dishCategory4, value);
			}
		}
		public string DishCategory5
		{
			get
			{
				SetProperty(ref dishCategory5, DishCategories["5"]);
				return dishCategory5;
			}
			set
			{
				SetProperty(ref dishCategory5, value);
			}
		}
		public bool DishCategory1Check
		{
			get => dishCategory1Check;
			set => SetProperty(ref dishCategory1Check, value);
		}
		public bool DishCategory2Check
		{
			get => dishCategory2Check;
			set => SetProperty(ref dishCategory2Check, value);
		}
		public bool DishCategory3Check
		{
			get => dishCategory3Check;
			set => SetProperty(ref dishCategory3Check, value);
		}
		public bool DishCategory4Check
		{
			get => dishCategory4Check;
			set => SetProperty(ref dishCategory4Check, value);
		}
		public bool DishCategory5Check
		{
			get => dishCategory5Check;
			set => SetProperty(ref dishCategory5Check, value);
		}

		// fields for the properties above
		private Day selectedDay;
		private Meal selectedMeal;
		private ObservableCollection<Dish> searchResult;
		private string dishCategory1;
		private string dishCategory2;
		private string dishCategory3;
		private string dishCategory4;
		private string dishCategory5;
		private bool dishCategory1Check;
		private bool dishCategory2Check;
		private bool dishCategory3Check;
		private bool dishCategory4Check;
		private bool dishCategory5Check;

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
		public DelegateCommand<Dish> AddDishCommand { get; set; }
		/// <summary>
		/// Command to Delete a dish from the meal, to be used in the View
		/// </summary>
		public DelegateCommand<Dish> DeleteDishCommand { get; set; }
		/// <summary>
		/// Command to execute the search in view
		/// </summary>
		public DelegateCommand SearchCommand { get; set; }
		/// <summary>
		/// Command to toggle a search filter on or off
		/// </summary>
		public DelegateCommand<string> ToggleFilterCommand { get; set; }

		/// <summary>
		/// Add the selected Dish to the meal and save to file
		/// </summary>
		/// <param name="SelectedDish"></param>
		private void AddDishExecute(Dish SelectedDish)
		{
			SelectedMeal.AddDish(SelectedDish.Name, SelectedDish.ThisDishCategories, UserData);
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
		/// Searches the DishDB for the query string and the filters, and sets the SearchResult with the matching Dishes.
		/// An empty query string with no filters sets the whole DishDB to SearchResult.
		/// </summary>
		private void SearchExecute()
		{
			// if empty filters
			if (!DishCategory1Check && !DishCategory2Check && !DishCategory3Check && !DishCategory4Check && !DishCategory5Check)
			{
				// if also empty Query string, set the entire DishDB to SearchResult
				if (Query == "")
				{
					SearchResult = DishDB;
				}
				// if there is a Query string, search the DishDB for it
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
			// if the filters are set, search the DishDB for them
			else
			{
				SearchResult = new ObservableCollection<Dish>();
				foreach (Dish dish in DishDB)
				{
					if ((DishCategory1Check && dish.ThisDishCategories.Contains(DishCategories["1"])) ||
						(DishCategory2Check && dish.ThisDishCategories.Contains(DishCategories["2"])) ||
						(DishCategory3Check && dish.ThisDishCategories.Contains(DishCategories["3"])) ||
						(DishCategory4Check && dish.ThisDishCategories.Contains(DishCategories["4"])) ||
						(DishCategory5Check && dish.ThisDishCategories.Contains(DishCategories["5"])))
					{
						// only add if the Query string is empty or the Name contains the Query string
						if (Query == "" || dish.Name.Contains(Query))
						{
							SearchResult.Add(dish);
						}
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
		/// Depending on the name of the search filter, toggle the appropriate filter on or off
		/// </summary>
		/// <param name="text"></param>
		private void ToggleFilterExecute(string text)
		{
			if (text == DishCategories["1"])
				DishCategory1Check = !DishCategory1Check;
			else if (text == DishCategories["2"])
				DishCategory2Check = !DishCategory2Check;
			else if (text == DishCategories["3"])
				DishCategory3Check = !DishCategory3Check;
			else if (text == DishCategories["4"])
				DishCategory4Check = !DishCategory4Check;
			else if (text == DishCategories["5"])
				DishCategory5Check = !DishCategory5Check;

			SearchExecute();
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
			if (UserData.ContainsKey("UserDays")) { UserDays = (ObservableCollection<Day>)UserData["UserDays"]; }
			if (UserData.ContainsKey("SelectedDay")) { SelectedDay = (Day)UserData["SelectedDay"]; }
			if (UserData.ContainsKey("SelectedMeal")) { SelectedMeal = (Meal)UserData["SelectedMeal"]; }
			if (UserData.ContainsKey("DishDB")) { DishDB = (ObservableCollection<Dish>)UserData["DishDB"]; }
			if (UserData.ContainsKey("DishCategories")) { DishCategories = (Dictionary<string, string>)UserData["DishCategories"]; }
			UserDaysIO = new FileIO(userFileName);
			DishDBIO = new FileIO(dishFileName);
			Title = "Choose a Dish";
			Query = "";
			SearchResult = DishDB;
			DishCategory1Check = DishCategory2Check = DishCategory3Check = DishCategory4Check = DishCategory5Check = false;

			// initialize commands
			AddDishCommand = new DelegateCommand<Dish>(AddDishExecute, AddDishCanExecute);
			DeleteDishCommand = new DelegateCommand<Dish>(DeleteDishExecute, DeleteDishCanExecute);
			SearchCommand = new DelegateCommand(SearchExecute);
			LoadDishesCommand = new DelegateCommand(ExecuteLoadDishesCommand);
			ToggleFilterCommand = new DelegateCommand<string>(ToggleFilterExecute);

			// use MessagingCenter to be notified when the Dish DB is updated, to also update SearchResult
			MessagingCenter.Subscribe<DishEditViewModel>(this, "DB updated", (sender) =>
			{
				SearchExecute();
			});
		}
	}
}
