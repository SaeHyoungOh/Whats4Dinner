using IX.StandardExtensions;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// 
	/// reference: https://forums.xamarin.com/discussion/45354/conditional-styling-based-on-data-binding-values
	/// </summary>
	public class TodayConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? -2 : 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (int)value == -2 ? true : false;
		}
	}

	public class CurrentMonthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? Color.Black : Color.LightGray;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (Color)value == Color.Black;
		}
	}

	/// <summary>
	/// ViewModel for the MonthlyPage
	/// </summary>
	public class MonthlyViewModel : BaseViewModel
	{
		/// <summary>
		/// Command to load the previous month
		/// </summary>
		public DelegateCommand PreviousMonthCommand { get; set; }

		/// <summary>
		/// Command to load this month
		/// </summary>
		public DelegateCommand TodayCommand { get; set; }

		/// <summary>
		/// Command to load the next month
		/// </summary>
		public DelegateCommand NextMonthCommand { get; set; }

		public ObservableCollection<Day> DisplayDays
		{
			get => displayDays;
			set => SetProperty(ref displayDays, value);
		}

		public bool IsNotThisMonth
		{
			get
			{
				SetProperty(ref isNotThisMonth, CurrentMonth != 0);
				return isNotThisMonth;
			}
			set
			{
				SetProperty(ref isNotThisMonth, value);
			}
		}

		public string CurrentMonthName
		{
			get
			{
				SetProperty(ref currentMonthName, DateTime.Today.AddMonths(CurrentMonth).ToString("MMMM yyyy"));
				return currentMonthName;
			}
			set => SetProperty(ref currentMonthName, value);
		}

		// fields for the above properties
		private ObservableCollection<Day> displayDays;
		private string currentMonthName;
		private bool isNotThisMonth;

		private int CurrentMonth { get; set; } = 0;

		/// <summary>
		/// Fills the DisplayDays with the previous month's days
		/// </summary>
		private void PreviousMonthExecute()
		{
			CurrentMonth--;
			FillDisplayDays();
			NotifyView();
		}

		/// <summary>
		/// Fills the DisplayDays with this month's days
		/// </summary>
		private void TodayExecute()
		{
			CurrentMonth = 0;
			FillDisplayDays();
			NotifyView();
		}

		/// <summary>
		/// Fills the DisplayDays with the next month's days
		/// </summary>
		private void NextMonthExecute()
		{
			CurrentMonth++;
			FillDisplayDays();
			NotifyView();
		}

		private void NotifyView()
		{
			OnPropertyChanged(nameof(IsNotThisMonth));
			OnPropertyChanged(nameof(CurrentMonthName));
			OnPropertyChanged(nameof(DisplayDays));
		}

		private void SetIsCurrentMonth()
		{
			foreach (Day day in DisplayDays)
			{
				day.IsCurrentMonth = day.ThisDate.Month == DateTime.Today.AddMonths(CurrentMonth).Month;
			}
		}

		/// <summary>
		/// Overriding the virtual method in base class; for Monthly View, always start on the Sunday of the first day of the month.
		/// </summary>
		/// <returns></returns>
		protected override DateTime DetermineFirstDay()
		{
			DateTime today = DateTime.Today;
			DateTime firstDayOfThisMonth = new DateTime(today.Year, today.Month, 1);
			DateTime firstDay = firstDayOfThisMonth.AddMonths(CurrentMonth);

			return firstDay.AddDays(DayOfWeek.Sunday - firstDay.DayOfWeek);
		}

		protected override void FillDays(DateTime firstDay)
		{
			base.FillDays(firstDay);
			SetIsCurrentMonth();
			NotifyView();
		}

		public MonthlyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			Title = "Monthly View";
			NumDays = 6 * 7;
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			if (UserData.ContainsKey("DisplayDays")) DisplayDays = (ObservableCollection<Day>)UserData["DisplayDays"];
			else
			{
				DisplayDays = new ObservableCollection<Day>();
				UserData["DisplayDays"] = DisplayDays;
			}

			FillDisplayDays();

			// initialize commands
			PreviousMonthCommand = new DelegateCommand(PreviousMonthExecute);
			TodayCommand = new DelegateCommand(TodayExecute);
			NextMonthCommand = new DelegateCommand(NextMonthExecute);
		}
	}
}
