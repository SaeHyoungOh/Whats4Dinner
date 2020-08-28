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
		public ObservableCollection<Day> DisplayDays
		{
			get => displayDays;
			set => SetProperty(ref displayDays, value);
		}
		private ObservableCollection<Day> displayDays;

		public string CurrentMonth
		{
			get
			{
				SetProperty(ref currentMonth, DateTime.Today.ToString("MMMM yyyy"));
				return currentMonth;
			}
			set => SetProperty(ref currentMonth, value);
		}
		private string currentMonth;

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

		public MonthlyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			Title = "Monthly View";
			NumDays = 6 * 7;
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			if (UserData.ContainsKey("DisplayDays")) DisplayDays = (ObservableCollection<Day>)UserData["DisplayDays"];
			else DisplayDays = new ObservableCollection<Day>();

			FillDisplayDays();
		}
	}
}
