using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.ViewModels
{
	public class WeeklyViewModel : BaseViewModel
	{
		/// <summary>
		/// List of all the Days and their Meals and Dishes for View
		/// </summary>
		public ObservableCollection<Day> DisplayDays
		{
			get => displayDays;
			set
			{
				SetProperty(ref displayDays, value);
			}
		}
		private ObservableCollection<Day> displayDays;

		/// <summary>
		/// Counter for keeping track of which week it is currently at. 0 means this week.
		/// </summary>
		private int CurrentWeek { get; set; } = 0;

		/// <summary>
		/// Whether current week is NOT this week. True if NOT this week. Used in View
		/// </summary>
		public bool IsNotThisWeek
		{
			get
			{
				SetProperty(ref isNotThisWeek, CurrentWeek != 0);
				return isNotThisWeek;
			}
			set
			{
				SetProperty(ref isNotThisWeek, value);
			}
		}
		private bool isNotThisWeek;

		/// <summary>
		/// Command to load the previous week
		/// </summary>
		public DelegateCommand PreviousWeekCommand { get; set; }

		/// <summary>
		/// Command to load this week
		/// </summary>
		public DelegateCommand TodayCommand { get; set; }

		/// <summary>
		/// Command to load the next week
		/// </summary>
		public DelegateCommand NextWeekCommand { get; set; }

		/// <summary>
		/// Fills the DisplayDays with the previous week's days
		/// </summary>
		private void PreviousWeekExecute()
		{
			CurrentWeek--;
			UserData["CurrentWeek"] = CurrentWeek;
			FillDisplayDays();
			OnPropertyChanged("IsNotThisWeek");
		}

		/// <summary>
		/// Fills the DisplayDays with this week's days
		/// </summary>
		private void TodayExecute()
		{
			CurrentWeek = 0;
			UserData["CurrentWeek"] = CurrentWeek;
			FillDisplayDays();
			OnPropertyChanged("IsNotThisWeek");
		}

		/// <summary>
		/// Fills the DisplayDays with the next week's days
		/// </summary>
		private void NextWeekExecute()
		{
			CurrentWeek++;
			UserData["CurrentWeek"] = CurrentWeek;
			FillDisplayDays();
			OnPropertyChanged("IsNotThisWeek");
		}

		/// <summary>
		/// Overriding virtual method in base class; for WeeklyView, always start on a Sunday
		/// </summary>
		/// <param name="today"></param>
		/// <param name="firstDay"></param>
		/// <returns></returns>
		protected override DateTime DetermineFirstDay()
		{
			DateTime today = DateTime.Today;

			return today.AddDays(DayOfWeek.Sunday - today.DayOfWeek);
		}

		/// <summary>
		/// Constructor for WeeklyViewModel class
		/// </summary>
		public WeeklyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			Title = "Weekly View";
			UserDaysIO = new FileIO(userFileName);
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			if (UserData.ContainsKey("DisplayDays")) DisplayDays = (ObservableCollection<Day>)UserData["DisplayDays"];

			// fill the week with days
			FillDisplayDays();

			// initialize commands
			LoadItemsCommand = new DelegateCommand(LoadItemsExecute);
			PreviousWeekCommand = new DelegateCommand(PreviousWeekExecute);
			TodayCommand = new DelegateCommand(TodayExecute);
			NextWeekCommand = new DelegateCommand(NextWeekExecute);
		}
	}
}
