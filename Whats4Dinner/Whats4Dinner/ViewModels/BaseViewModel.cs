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
		public DelegateCommand<Dictionary<string, object>> LoadItemsCommand { get; set; }

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
		/// List of all the meals in user data
		/// </summary>
		public ObservableCollection<Day> UserDays { get; set; }

		/// <summary>
		/// All-encompassing data structure for passing between pages and classes, in order to keep a single instance of the data
		/// </summary>
		public Dictionary<string, object> UserData { get; set; }

		public Dictionary<string, object> CommandParams { get; set; }

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
		/// Number of days to fill the DisplayDays
		/// </summary>
		protected int NumDays
		{
			get => numDays;
			set
			{
				SetProperty(ref numDays, value);
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
		private int numDays;
		private ObservableCollection<Dish> dishDB;

		/// <summary>
		/// Fill the DisplayDays from user data read from file
		/// </summary>
		/// <param name="UserDays"></param>
		protected void FillDisplayDays(Dictionary<string, object> CommandParams)
		{
			if (UserDays == null)
			{
				return;
			}

			MenuItemType pageType = (MenuItemType)CommandParams["PageType"];
			ObservableCollection<Day> displayDays = (ObservableCollection<Day>)CommandParams["DisplayDays"];

			if (pageType == MenuItemType.SevenDayView)
			{
				DateTime today = DateTime.Today;
				int i = 0,  // number of days to fill (NumDays days)
					j = 0;  // UserDays index to iterate

				while (i < NumDays)
				{
					DateTime UserDaysDate, currentDate = today.AddDays(i);

					// prevent j from going out of bounds
					if (j < UserDays.Count)
					{
						UserDaysDate = UserDays[j].ThisDate;
					}
					else
					{
						UserDaysDate = today.AddDays(NumDays);
					}

					// if we run out of data from file, fill days with blanks
					if (j > UserDays.Count - 1 || UserDaysDate > today.AddDays(6))
					{
						displayDays.Add(new Day(currentDate, UserData));
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
						displayDays.Add(UserDays[j]);
						j++;
						i++;
					}
					// fill the between days with empty day
					else if (UserDaysDate <= today.AddDays(6))
					{
						int emptyDays = (UserDaysDate - currentDate).Days;
						for (int k = 0; k < emptyDays; k++)
						{
							displayDays.Add(new Day(currentDate.AddDays(k), UserData));
							i++;
						}
					}
					// ignore all dates after the NumDays days
					else
					{
						j = UserDays.Count;
					}
				}
			}
			else if (pageType == MenuItemType.WeeklyView)
			{
				DateTime today = DateTime.Today;
				DateTime firstDay;
				switch (today.DayOfWeek)
				{
					case DayOfWeek.Monday:
						firstDay = today.AddDays(-1);
						break;
					case DayOfWeek.Tuesday:
						firstDay = today.AddDays(-2);
						break;
					case DayOfWeek.Wednesday:
						firstDay = today.AddDays(-3);
						break;
					case DayOfWeek.Thursday:
						firstDay = today.AddDays(-4);
						break;
					case DayOfWeek.Friday:
						firstDay = today.AddDays(-5);
						break;
					case DayOfWeek.Saturday:
						firstDay = today.AddDays(-6);
						break;
					default:
						firstDay = today;
						break;
				}
				int i = 0,  // number of days to fill (NumDays days)
					j = 0;  // UserDays index to iterate

				while (i < NumDays)
				{
					DateTime UserDaysDate, currentDate = firstDay.AddDays(i);

					// prevent j from going out of bounds
					if (j < UserDays.Count)
					{
						UserDaysDate = UserDays[j].ThisDate;
					}
					else
					{
						UserDaysDate = firstDay.AddDays(NumDays);
					}

					// if we run out of data from file, fill days with blanks
					if (j > UserDays.Count - 1 || UserDaysDate > firstDay.AddDays(6))
					{
						displayDays.Add(new Day(currentDate, UserData));
						i++;
					}
					// skip until firstDay
					else if (UserDaysDate < currentDate)
					{
						j++;
					}
					// use the day if date matches
					else if (UserDaysDate == currentDate)
					{
						displayDays.Add(UserDays[j]);
						j++;
						i++;
					}
					// fill the between days with empty day
					else if (UserDaysDate <= firstDay.AddDays(6))
					{
						int emptyDays = (UserDaysDate - currentDate).Days;
						for (int k = 0; k < emptyDays; k++)
						{
							displayDays.Add(new Day(currentDate.AddDays(k), UserData));
							i++;
						}
					}
					// ignore all dates after the NumDays days
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
		protected void LoadItemsExecute(Dictionary<string, object> CommandParams)
		{
			IsBusy = true;

			try
			{
				UserDays.Clear();
				((ObservableCollection<Day>)CommandParams["DisplayDays"]).Clear();

				// read user's data from JSON file
				UserDays = UserDaysIO.ReadUserDaysFromJSON();

				// fill the week with days
				FillDisplayDays(CommandParams);
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
