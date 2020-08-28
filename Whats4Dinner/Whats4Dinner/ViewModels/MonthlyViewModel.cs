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
	/// Class object to be bound to View
	/// </summary>
	public class DayString : BaseModel
	{
		/// <summary>
		/// Date to display in View
		/// </summary>
		public string Date
		{
			get => date;
			set => SetProperty(ref date, value);
		}

		/// <summary>
		/// Detail (meals) to display in View
		/// </summary>
		public bool Breakfast
		{
			get => breakfast;
			set => SetProperty(ref breakfast, value);
		}
		public bool Lunch
		{
			get => lunch;
			set => SetProperty(ref lunch, value);
		}
		public bool Dinner
		{
			get => dinner;
			set => SetProperty(ref dinner, value);
		}
		public bool Other
		{
			get => other;
			set => SetProperty(ref other, value);
		}

		/// <summary>
		/// Whether it is current month
		/// </summary>
		public bool IsCurrentMonth
		{
			get => isCurrentMonth;
			set => SetProperty(ref isCurrentMonth, value);
		}

		public bool IsToday
		{
			get => isToday;
			set => SetProperty(ref isToday, value);
		}

		public Day ThisDay
		{
			get => thisDay;
			set => SetProperty(ref thisDay, value);
		}

		// fields for the above properties
		private string date;
		private bool breakfast;
		private bool lunch;
		private bool dinner;
		private bool other;
		private bool isCurrentMonth;
		private bool isToday;
		private Day thisDay;
	}

	/// <summary>
	/// 
	/// reference: https://forums.xamarin.com/discussion/45354/conditional-styling-based-on-data-binding-values
	/// </summary>
	public class TodayConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? Color.FromHex("eefaff") : Color.White;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Not implemented.");
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
			throw new NotImplementedException("Not implemented.");
		}
	}

	/// <summary>
	/// ViewModel for the MonthlyPage
	/// </summary>
	public class MonthlyViewModel : BaseViewModel
	{
		// each day to display in View
		public DayString Day00
		{
			get => day00;
			set => SetProperty(ref day00, value);
		}
		public DayString Day01
		{
			get => day01;
			set => SetProperty(ref day01, value);
		}
		public DayString Day02
		{
			get => day02;
			set => SetProperty(ref day02, value);
		}
		public DayString Day03
		{
			get => day03;
			set => SetProperty(ref day03, value);
		}
		public DayString Day04
		{
			get => day04;
			set => SetProperty(ref day04, value);
		}
		public DayString Day05
		{
			get => day05;
			set => SetProperty(ref day05, value);
		}
		public DayString Day06
		{
			get => day06;
			set => SetProperty(ref day06, value);
		}
		public DayString Day10
		{
			get => day10;
			set => SetProperty(ref day10, value);
		}
		public DayString Day11
		{
			get => day11;
			set => SetProperty(ref day11, value);
		}
		public DayString Day12
		{
			get => day12;
			set => SetProperty(ref day12, value);
		}
		public DayString Day13
		{
			get => day13;
			set => SetProperty(ref day13, value);
		}
		public DayString Day14
		{
			get => day14;
			set => SetProperty(ref day14, value);
		}
		public DayString Day15
		{
			get => day15;
			set => SetProperty(ref day15, value);
		}
		public DayString Day16
		{
			get => day16;
			set => SetProperty(ref day16, value);
		}
		public DayString Day20
		{
			get => day20;
			set => SetProperty(ref day20, value);
		}
		public DayString Day21
		{
			get => day21;
			set => SetProperty(ref day21, value);
		}
		public DayString Day22
		{
			get => day22;
			set => SetProperty(ref day22, value);
		}
		public DayString Day23
		{
			get => day23;
			set => SetProperty(ref day23, value);
		}
		public DayString Day24
		{
			get => day24;
			set => SetProperty(ref day24, value);
		}
		public DayString Day25
		{
			get => day25;
			set => SetProperty(ref day25, value);
		}
		public DayString Day26
		{
			get => day26;
			set => SetProperty(ref day26, value);
		}
		public DayString Day30
		{
			get => day30;
			set => SetProperty(ref day30, value);
		}
		public DayString Day31
		{
			get => day31;
			set => SetProperty(ref day31, value);
		}
		public DayString Day32
		{
			get => day32;
			set => SetProperty(ref day32, value);
		}
		public DayString Day33
		{
			get => day33;
			set => SetProperty(ref day33, value);
		}
		public DayString Day34
		{
			get => day34;
			set => SetProperty(ref day34, value);
		}
		public DayString Day35
		{
			get => day35;
			set => SetProperty(ref day35, value);
		}
		public DayString Day36
		{
			get => day36;
			set => SetProperty(ref day36, value);
		}
		public DayString Day40
		{
			get => day40;
			set => SetProperty(ref day40, value);
		}
		public DayString Day41
		{
			get => day41;
			set => SetProperty(ref day41, value);
		}
		public DayString Day42
		{
			get => day42;
			set => SetProperty(ref day42, value);
		}
		public DayString Day43
		{
			get => day43;
			set => SetProperty(ref day43, value);
		}
		public DayString Day44
		{
			get => day44;
			set => SetProperty(ref day44, value);
		}
		public DayString Day45
		{
			get => day45;
			set => SetProperty(ref day45, value);
		}
		public DayString Day46
		{
			get => day46;
			set => SetProperty(ref day46, value);
		}
		public DayString Day50
		{
			get => day50;
			set => SetProperty(ref day50, value);
		}
		public DayString Day51
		{
			get => day51;
			set => SetProperty(ref day51, value);
		}
		public DayString Day52
		{
			get => day52;
			set => SetProperty(ref day52, value);
		}
		public DayString Day53
		{
			get => day53;
			set => SetProperty(ref day53, value);
		}
		public DayString Day54
		{
			get => day54;
			set => SetProperty(ref day54, value);
		}
		public DayString Day55
		{
			get => day55;
			set => SetProperty(ref day55, value);
		}
		public DayString Day56
		{
			get => day56;
			set => SetProperty(ref day56, value);
		}

		// fields for the properties above
		private DayString day00 = new DayString();
		private DayString day01 = new DayString();
		private DayString day02 = new DayString();
		private DayString day03 = new DayString();
		private DayString day04 = new DayString();
		private DayString day05 = new DayString();
		private DayString day06 = new DayString();
		private DayString day10 = new DayString();
		private DayString day11 = new DayString();
		private DayString day12 = new DayString();
		private DayString day13 = new DayString();
		private DayString day14 = new DayString();
		private DayString day15 = new DayString();
		private DayString day16 = new DayString();
		private DayString day20 = new DayString();
		private DayString day21 = new DayString();
		private DayString day22 = new DayString();
		private DayString day23 = new DayString();
		private DayString day24 = new DayString();
		private DayString day25 = new DayString();
		private DayString day26 = new DayString();
		private DayString day30 = new DayString();
		private DayString day31 = new DayString();
		private DayString day32 = new DayString();
		private DayString day33 = new DayString();
		private DayString day34 = new DayString();
		private DayString day35 = new DayString();
		private DayString day36 = new DayString();
		private DayString day40 = new DayString();
		private DayString day41 = new DayString();
		private DayString day42 = new DayString();
		private DayString day43 = new DayString();
		private DayString day44 = new DayString();
		private DayString day45 = new DayString();
		private DayString day46 = new DayString();
		private DayString day50 = new DayString();
		private DayString day51 = new DayString();
		private DayString day52 = new DayString();
		private DayString day53 = new DayString();
		private DayString day54 = new DayString();
		private DayString day55 = new DayString();
		private DayString day56 = new DayString();

		/// <summary>
		/// MonthlyPage-specific DisplayDays; 5 x 7 2-dimensional list
		/// </summary>
		private ObservableCollection<ObservableCollection<Day>> DisplayDaysMonthly { get; set; }

		/// <summary>
		/// Collection of the strings used in View for processing; 5 x 7 2-dimensional list
		/// </summary>
		public ObservableCollection<ObservableCollection<DayString>> DayStringList
		{
			get => dayStringList;
			set => SetProperty(ref dayStringList, value);
		}
		private ObservableCollection<ObservableCollection<DayString>> dayStringList;

		public string CurrentMonth
		{
			get => currentMonth;
			set => SetProperty(ref currentMonth, value);
		}
		private string currentMonth = DateTime.Today.ToString("MMMM yyyy");

		/// <summary>
		/// Fills DisplayDaysString with the strings used in View for processing later
		/// </summary>
		private void InitializeDayStringList()
		{
			// assign each DayString object to the collection for processing
			DayStringList = new ObservableCollection<ObservableCollection<DayString>>
			{
				new ObservableCollection<DayString> { Day00, Day01, Day02, Day03, Day04, Day05, Day06 },
				new ObservableCollection<DayString> { Day10, Day11, Day12, Day13, Day14, Day15, Day16 },
				new ObservableCollection<DayString> { Day20, Day21, Day22, Day23, Day24, Day25, Day26 },
				new ObservableCollection<DayString> { Day30, Day31, Day32, Day33, Day34, Day35, Day36 },
				new ObservableCollection<DayString> { Day40, Day41, Day42, Day43, Day44, Day45, Day46 },
				new ObservableCollection<DayString> { Day50, Day51, Day52, Day53, Day54, Day55, Day56 }
			};
		}

		/// <summary>
		/// Overriding the virtual method in base class; for Monthly View, always start on the Sunday of the first day of the month.
		/// </summary>
		/// <returns></returns>
		protected override DateTime DetermineFirstDay()
		{
			DateTime today = DateTime.Today;
			DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

			return firstDayOfMonth.AddDays(DayOfWeek.Sunday - firstDayOfMonth.DayOfWeek);
		}

		/// <summary>
		/// Overriding the virtual method in base class; fill the month with Days
		/// </summary>
		/// <param name="firstDay"></param>
		protected override void FillDays(DateTime firstDay, ObservableCollection<Day> DisplayDays = null)
		{
			DateTime today = DateTime.Today;

			// initialize DisplayDaysMonthly
			DisplayDaysMonthly = new ObservableCollection<ObservableCollection<Day>>();

			for (int i = 0; i < DayStringList.Count; i++)
			{
				// fill each week of DisplayDaysMonthly
				if (DisplayDaysMonthly.Count < i + 1)
				{
					DisplayDaysMonthly.Add(new ObservableCollection<Day>());
				}
				base.FillDays(firstDay.AddDays(i * 7), DisplayDaysMonthly[i]);

				// fill detail of each day in DayStringList
				for (int j = 0; j < DayStringList[0].Count; j++)
				{
					DateTime currentDay = firstDay.AddDays((i * 7) + j);
					
					// add the date
					DayStringList[i][j].Date = currentDay.Day.ToString();

					// add each meal's presence
					DayStringList[i][j].Breakfast = DisplayDaysMonthly[i][j].BreakfastCheck;
					DayStringList[i][j].Lunch = DisplayDaysMonthly[i][j].LunchCheck;
					DayStringList[i][j].Dinner = DisplayDaysMonthly[i][j].DinnerCheck;
					DayStringList[i][j].Other = DisplayDaysMonthly[i][j].OtherCheck;

					// whether it is current month or day, for Style in View
					DayStringList[i][j].IsCurrentMonth = today.Month.Equals(currentDay.Month);
					DayStringList[i][j].IsToday = today.Date.Equals(currentDay.Date);

					// add the Day for navigating to the Day
					DayStringList[i][j].ThisDay = DisplayDaysMonthly[i][j];
				}
			}
		}

		public MonthlyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			Title = "Monthly View";
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];

			InitializeDayStringList();
			FillDisplayDays();

			UserData["DisplayDaysMonthly"] = DisplayDaysMonthly;
			UserData["DayStringList"] = DayStringList;
		}
	}
}
