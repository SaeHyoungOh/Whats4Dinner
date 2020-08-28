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
			return (bool)value ? Color.FromHex("eefaff") : Color.White;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (Color)value == Color.FromHex("eefaff");
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
		// each Day to display in View
		public Day Day00
		{
			get => day00;
			set => SetProperty(ref day00, value);
		}
		public Day Day01
		{
			get => day01;
			set => SetProperty(ref day01, value);
		}
		public Day Day02
		{
			get => day02;
			set => SetProperty(ref day02, value);
		}
		public Day Day03
		{
			get => day03;
			set => SetProperty(ref day03, value);
		}
		public Day Day04
		{
			get => day04;
			set => SetProperty(ref day04, value);
		}
		public Day Day05
		{
			get => day05;
			set => SetProperty(ref day05, value);
		}
		public Day Day06
		{
			get => day06;
			set => SetProperty(ref day06, value);
		}
		public Day Day10
		{
			get => day10;
			set => SetProperty(ref day10, value);
		}
		public Day Day11
		{
			get => day11;
			set => SetProperty(ref day11, value);
		}
		public Day Day12
		{
			get => day12;
			set => SetProperty(ref day12, value);
		}
		public Day Day13
		{
			get => day13;
			set => SetProperty(ref day13, value);
		}
		public Day Day14
		{
			get => day14;
			set => SetProperty(ref day14, value);
		}
		public Day Day15
		{
			get => day15;
			set => SetProperty(ref day15, value);
		}
		public Day Day16
		{
			get => day16;
			set => SetProperty(ref day16, value);
		}
		public Day Day20
		{
			get => day20;
			set => SetProperty(ref day20, value);
		}
		public Day Day21
		{
			get => day21;
			set => SetProperty(ref day21, value);
		}
		public Day Day22
		{
			get => day22;
			set => SetProperty(ref day22, value);
		}
		public Day Day23
		{
			get => day23;
			set => SetProperty(ref day23, value);
		}
		public Day Day24
		{
			get => day24;
			set => SetProperty(ref day24, value);
		}
		public Day Day25
		{
			get => day25;
			set => SetProperty(ref day25, value);
		}
		public Day Day26
		{
			get => day26;
			set => SetProperty(ref day26, value);
		}
		public Day Day30
		{
			get => day30;
			set => SetProperty(ref day30, value);
		}
		public Day Day31
		{
			get => day31;
			set => SetProperty(ref day31, value);
		}
		public Day Day32
		{
			get => day32;
			set => SetProperty(ref day32, value);
		}
		public Day Day33
		{
			get => day33;
			set => SetProperty(ref day33, value);
		}
		public Day Day34
		{
			get => day34;
			set => SetProperty(ref day34, value);
		}
		public Day Day35
		{
			get => day35;
			set => SetProperty(ref day35, value);
		}
		public Day Day36
		{
			get => day36;
			set => SetProperty(ref day36, value);
		}
		public Day Day40
		{
			get => day40;
			set => SetProperty(ref day40, value);
		}
		public Day Day41
		{
			get => day41;
			set => SetProperty(ref day41, value);
		}
		public Day Day42
		{
			get => day42;
			set => SetProperty(ref day42, value);
		}
		public Day Day43
		{
			get => day43;
			set => SetProperty(ref day43, value);
		}
		public Day Day44
		{
			get => day44;
			set => SetProperty(ref day44, value);
		}
		public Day Day45
		{
			get => day45;
			set => SetProperty(ref day45, value);
		}
		public Day Day46
		{
			get => day46;
			set => SetProperty(ref day46, value);
		}
		public Day Day50
		{
			get => day50;
			set => SetProperty(ref day50, value);
		}
		public Day Day51
		{
			get => day51;
			set => SetProperty(ref day51, value);
		}
		public Day Day52
		{
			get => day52;
			set => SetProperty(ref day52, value);
		}
		public Day Day53
		{
			get => day53;
			set => SetProperty(ref day53, value);
		}
		public Day Day54
		{
			get => day54;
			set => SetProperty(ref day54, value);
		}
		public Day Day55
		{
			get => day55;
			set => SetProperty(ref day55, value);
		}
		public Day Day56
		{
			get => day56;
			set => SetProperty(ref day56, value);
		}

		// fields for the properties above
		private Day day00;
		private Day day01;
		private Day day02;
		private Day day03;
		private Day day04;
		private Day day05;
		private Day day06;
		private Day day10;
		private Day day11;
		private Day day12;
		private Day day13;
		private Day day14;
		private Day day15;
		private Day day16;
		private Day day20;
		private Day day21;
		private Day day22;
		private Day day23;
		private Day day24;
		private Day day25;
		private Day day26;
		private Day day30;
		private Day day31;
		private Day day32;
		private Day day33;
		private Day day34;
		private Day day35;
		private Day day36;
		private Day day40;
		private Day day41;
		private Day day42;
		private Day day43;
		private Day day44;
		private Day day45;
		private Day day46;
		private Day day50;
		private Day day51;
		private Day day52;
		private Day day53;
		private Day day54;
		private Day day55;
		private Day day56;

		//private Day day00 = new Day();
		//private Day day01 = new Day();
		//private Day day02 = new Day();
		//private Day day03 = new Day();
		//private Day day04 = new Day();
		//private Day day05 = new Day();
		//private Day day06 = new Day();
		//private Day day10 = new Day();
		//private Day day11 = new Day();
		//private Day day12 = new Day();
		//private Day day13 = new Day();
		//private Day day14 = new Day();
		//private Day day15 = new Day();
		//private Day day16 = new Day();
		//private Day day20 = new Day();
		//private Day day21 = new Day();
		//private Day day22 = new Day();
		//private Day day23 = new Day();
		//private Day day24 = new Day();
		//private Day day25 = new Day();
		//private Day day26 = new Day();
		//private Day day30 = new Day();
		//private Day day31 = new Day();
		//private Day day32 = new Day();
		//private Day day33 = new Day();
		//private Day day34 = new Day();
		//private Day day35 = new Day();
		//private Day day36 = new Day();
		//private Day day40 = new Day();
		//private Day day41 = new Day();
		//private Day day42 = new Day();
		//private Day day43 = new Day();
		//private Day day44 = new Day();
		//private Day day45 = new Day();
		//private Day day46 = new Day();
		//private Day day50 = new Day();
		//private Day day51 = new Day();
		//private Day day52 = new Day();
		//private Day day53 = new Day();
		//private Day day54 = new Day();
		//private Day day55 = new Day();
		//private Day day56 = new Day();

		/// <summary>
		/// MonthlyPage-specific DisplayDays; 5 x 7 2-dimensional list
		/// </summary>
		public ObservableCollection<ObservableCollection<Day>> DisplayDaysMonthly
		{
			get => displayDaysMonthly;
			set => SetProperty(ref displayDaysMonthly, value);
		}
		private ObservableCollection<ObservableCollection<Day>> displayDaysMonthly;

		public string CurrentMonth
		{
			get => currentMonth;
			set => SetProperty(ref currentMonth, value);
		}
		private string currentMonth = DateTime.Today.ToString("MMMM yyyy");

		/// <summary>
		/// Fills DisplayDaysString with the strings used in View for processing later
		/// </summary>
		private void InitializeDisplayDaysMonthly()
		{
			// assign each DayString object to the collection for processing
			DisplayDaysMonthly = new ObservableCollection<ObservableCollection<Day>>
			{
				new ObservableCollection<Day> { Day00, Day01, Day02, Day03, Day04, Day05, Day06 },
				new ObservableCollection<Day> { Day10, Day11, Day12, Day13, Day14, Day15, Day16 },
				new ObservableCollection<Day> { Day20, Day21, Day22, Day23, Day24, Day25, Day26 },
				new ObservableCollection<Day> { Day30, Day31, Day32, Day33, Day34, Day35, Day36 },
				new ObservableCollection<Day> { Day40, Day41, Day42, Day43, Day44, Day45, Day46 },
				new ObservableCollection<Day> { Day50, Day51, Day52, Day53, Day54, Day55, Day56 }
			};
		}

		private void AssignDays()
		{
			Day00 = DisplayDaysMonthly[0][0];
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
			// fill each week of DisplayDaysMonthly
			DisplayDaysMonthly = new ObservableCollection<ObservableCollection<Day>>();
			for (int i = 0; i < 6; i++)
			{
				if (DisplayDaysMonthly.Count < i + 1)
				{
					DisplayDaysMonthly.Add(new ObservableCollection<Day>());
				}
				base.FillDays(firstDay.AddDays(i * 7), DisplayDaysMonthly[i]);
			}
		}

		public MonthlyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			Title = "Monthly View";
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];

			//InitializeDisplayDaysMonthly();
			FillDisplayDays();

			UserData["DisplayDaysMonthly"] = DisplayDaysMonthly;
		}
	}
}
