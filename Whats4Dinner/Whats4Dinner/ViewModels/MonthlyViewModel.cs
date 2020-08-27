using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.ViewModels
{
	public class DayString : BaseModel
	{
		// each day to display in View
		public string Str
		{
			get => str;
			set => SetProperty(ref str, value);
		}
		private string str;
	}

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

		// fields for the properties above
		private DayString day00;
		private DayString day01;
		private DayString day02;
		private DayString day03;
		private DayString day04;
		private DayString day05;
		private DayString day06;
		private DayString day10;
		private DayString day11;
		private DayString day12;
		private DayString day13;
		private DayString day14;
		private DayString day15;
		private DayString day16;
		private DayString day20;
		private DayString day21;
		private DayString day22;
		private DayString day23;
		private DayString day24;
		private DayString day25;
		private DayString day26;
		private DayString day30;
		private DayString day31;
		private DayString day32;
		private DayString day33;
		private DayString day34;
		private DayString day35;
		private DayString day36;
		private DayString day40;
		private DayString day41;
		private DayString day42;
		private DayString day43;
		private DayString day44;
		private DayString day45;
		private DayString day46;

		/// <summary>
		/// MonthlyPage-specific DisplayDays; 5 x 7 2-dimensional list
		/// </summary>
		private List<List<Day>> DisplayDaysMonthly { get; set; }

		/// <summary>
		/// Collection of the strings used in View for processing; 5 x 7 2-dimensional list
		/// </summary>
		public ObservableCollection<ObservableCollection<DayString>> DayStringList
		{
			get => dayStringList;
			set => SetProperty(ref dayStringList, value);
		}
		private ObservableCollection<ObservableCollection<DayString>> dayStringList;

		/// <summary>
		/// Fills DisplayDaysString with the strings used in View for processing later
		/// </summary>
		private void InitializeDayStringList()
		{
			// instanciate each DayString object
			Day00 = new DayString();
			Day01 = new DayString();
			Day02 = new DayString();
			Day03 = new DayString();
			Day04 = new DayString();
			Day05 = new DayString();
			Day06 = new DayString();
			Day10 = new DayString();
			Day11 = new DayString();
			Day12 = new DayString();
			Day13 = new DayString();
			Day14 = new DayString();
			Day15 = new DayString();
			Day16 = new DayString();
			Day20 = new DayString();
			Day21 = new DayString();
			Day22 = new DayString();
			Day23 = new DayString();
			Day24 = new DayString();
			Day25 = new DayString();
			Day26 = new DayString();
			Day30 = new DayString();
			Day31 = new DayString();
			Day32 = new DayString();
			Day33 = new DayString();
			Day34 = new DayString();
			Day35 = new DayString();
			Day36 = new DayString();
			Day40 = new DayString();
			Day41 = new DayString();
			Day42 = new DayString();
			Day43 = new DayString();
			Day44 = new DayString();
			Day45 = new DayString();
			Day46 = new DayString();

			// assign each DayString object to the collection for processing
			DayStringList = new ObservableCollection<ObservableCollection<DayString>>
			{
				new ObservableCollection<DayString>
				{
					Day00, Day01, Day02, Day03, Day04, Day05, Day06
				},
				new ObservableCollection<DayString>
				{
					Day10, Day11, Day12, Day13, Day14, Day15, Day16
				},
				new ObservableCollection<DayString>
				{
					Day20, Day21, Day22, Day23, Day24, Day25, Day26
				},
				new ObservableCollection<DayString>
				{
					Day30, Day31, Day32, Day33, Day34, Day35, Day36
				},
				new ObservableCollection<DayString>
				{
					Day40, Day41, Day42, Day43, Day44, Day45, Day46
				}
			};
		}

		public MonthlyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			Title = "Monthly View";
			PageType = MenuItemType.MonthlyView;
			DisplayDaysMonthly = new List<List<Day>>();
			InitializeDayStringList();

			UserData["PageType"] = PageType;
			UserData["DisplayDaysMonthly"] = DisplayDaysMonthly;
			UserData["DayStringList"] = DayStringList;
			FillDisplayDays(UserData);
		}
	}
}
