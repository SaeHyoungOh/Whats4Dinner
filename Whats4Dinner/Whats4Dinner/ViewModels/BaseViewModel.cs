using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// The base class for all ViewModels which use properties for the Views such as Title, IsBusy, FilIO, etc.
	/// This also includes the list of Days used throughout the app.
	/// </summary>
	public class BaseViewModel : BaseModel
	{
		/// <summary>
		/// The file name for storing the user data
		/// </summary>
		protected static readonly string userFileName = "UserDays.json";

		/// <summary>
		/// The file name for storing the dish database
		/// </summary>
		protected static readonly string dishFileName = "DishDB.json";

		/// <summary>
		/// DelegateCommand (from Prism) to use in View when refreshing
		/// </summary>
		public DelegateCommand<MenuItemType?> LoadItemsCommand { get; set; }

		/// <summary>
		/// FileIO object to handle user data I/O
		/// </summary>
		protected FileIO UserDaysIO { get; set; }

		/// <summary>
		/// FileIO object to handle dish database I/O
		/// </summary>
		protected FileIO DishDBIO { get; set; }

		/// <summary>
		/// MenuItemType of the current page
		/// </summary>
		public MenuItemType PageType { get; set; }
			
		/// <summary>
							/// The title of the page
							/// </summary>
		public string Title
		{
			get => title;
			set { SetProperty(ref title, value); }
		}

		/// <summary>
		/// Whether the page is actively refreshing in the RefreshView
		/// </summary>
		public bool IsBusy
		{
			get => isBusy;
			set { SetProperty(ref isBusy, value); }
		}

		/// <summary>
		/// List of all the meals in user data
		/// </summary>
		public ObservableCollection<Day> UserDays { get; set; }

		/// <summary>
		/// List of all the Days and their Meals and Dishes
		/// </summary>
		public ObservableCollection<Day> DisplayDays
		{
			get => displayDays;
			set
			{
				SetProperty(ref displayDays, value);
			}
		}

		/// <summary>
		/// List of Dishes already created before
		/// </summary>
		public ObservableCollection<Dish> DishDB
		{
			get => dishDB;
			set
			{
				SetProperty(ref dishDB, value);
			}
		}

		// fields for the properties above
		private string title = string.Empty;
		private bool isBusy = false;
		private ObservableCollection<Day> displayDays;
		private ObservableCollection<Dish> dishDB;

		public Dictionary<string, object> UserData { get; set; }

		/// <summary>
		/// Fill the DisplayDays from user data read from file
		/// </summary>
		/// <param name="UserDays"></param>
		protected void FillDisplayDays(MenuItemType? menuItemType)
		{
			if (UserDays == null)
			{
				return;
			}

			if (menuItemType == MenuItemType.SevenDayView)
			{
				DateTime today = DateTime.Today;
				int i = 0,	// number of days to fill (7 days)
					j = 0;	// UserDays index to iterate

				while (i < 7)
				{
					DateTime UserDaysDate, currentDate = today.AddDays(i);

					// prevent j from going out of bounds
					if (j < UserDays.Count)
					{
						UserDaysDate = UserDays[j].ThisDate;
					}
					else
					{
						UserDaysDate = today.AddDays(7);
					}

					// if we run out of data from file, fill days with blanks
					if (j > UserDays.Count - 1 || UserDaysDate > today.AddDays(6))
					{
						DisplayDays.Add(new Day(currentDate));
						i++;
					}
					// skip until today
					else if (UserDaysDate < currentDate)
					{
						j++;
					}
					// use the day if date matches
					else if (UserDaysDate == currentDate)
					{
						DisplayDays.Add(UserDays[j]);
						j++;
						i++;
					}
					// fill the between days with empty day
					else if (UserDaysDate <= today.AddDays(6))
					{
						int emptyDays = (UserDaysDate - currentDate).Days;
						for (int k = 0; k < emptyDays; k++)
						{
							DisplayDays.Add(new Day(currentDate));
							i++;
						}
					}
					// ignore all dates after the 7 days
					else
					{
						j = UserDays.Count;
					}
				}
			}
		}

		/// <summary>
		/// Replace the DisplayDays with new data from the file
		/// (to be made asyncronous when using database)
		/// </summary>
		protected void ExecuteLoadItemsCommand(MenuItemType? menuItemType)
		{
			IsBusy = true;

			try
			{
				UserDays.Clear();
				DisplayDays.Clear();

				// read user's data from JSON file
				UserDays = UserDaysIO.ReadUserDaysFromJSON();

				// fill the week with days
				FillDisplayDays(menuItemType);
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
	}
}
