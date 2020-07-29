using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static Whats4Dinner.ViewModels.DataStructure.Meal;

namespace Whats4Dinner.ViewModels.DataStructure
{
	/// <summary>
	/// A Day with Date and a list of Meals
	/// </summary>
	public class Day : BaseModel
	{
		private DateTime thisDate;
		private string displayDayOfWeek;
		private string displayDate;
		private ObservableCollection<Meal> meals;
		private bool breakfastCheck;
		private bool lunchCheck;
		private bool dinnerCheck;
		private bool otherCheck;

		/// <summary>
		/// the date of this day
		/// </summary>
		public DateTime ThisDate
		{
			get => thisDate;
			set
			{
				SetProperty(ref thisDate, value);
			}
		}

		/// <summary>
		/// Date to display on View as a string
		/// </summary>
		public string DisplayDayOfWeek
		{
			get
			{
				string dayString;

				dayString = ThisDate.Date.ToString("dddd");

				// add "(Today)" to the today's date
				if (ThisDate == DateTime.Today)
				{
					dayString += " (Today)";
				}
				SetProperty(ref displayDayOfWeek, dayString);

				return displayDayOfWeek;
			}
			set
			{
				SetProperty(ref displayDayOfWeek, value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string DisplayDate
		{
			get
			{
				SetProperty(ref displayDate, ThisDate.Date.ToString("MM/dd/yyyy"));

				return displayDate;
			}
			set
			{
				SetProperty(ref displayDate, value);
			}
		}

		/// <summary>
		/// list of meals in this day
		/// </summary>
		public ObservableCollection<Meal> Meals
		{
			get => meals;
			set
			{
				SetProperty(ref meals, value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool BreakfastCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Breakfast)
				{
					SetProperty(ref breakfastCheck, Meals[(int)MealType.Breakfast].HasDishes);
				}

				return breakfastCheck;
			}
			set
			{
				SetProperty(ref breakfastCheck, value);
			}
		}
		public bool LunchCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Lunch)
				{
					SetProperty(ref lunchCheck, Meals[(int)MealType.Lunch].HasDishes);
				}

				return lunchCheck;
			}
			set
			{
				SetProperty(ref lunchCheck, value);
			}
		}
		public bool DinnerCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Dinner)
				{
					SetProperty(ref dinnerCheck, Meals[(int)MealType.Dinner].HasDishes);
				}

				return dinnerCheck;
			}
			set
			{
				SetProperty(ref dinnerCheck, value);
			}
		}
		public bool OtherCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Other)
				{
					SetProperty(ref otherCheck, Meals[(int)MealType.Other].HasDishes);
				}

				return otherCheck;
			}
			set
			{
				SetProperty(ref otherCheck, value);
			}
		}

		/// <summary>
		/// parameterless constructor for JSON deserialization
		/// </summary>
		public Day() { }

		/// <summary>
		/// constructor for Day
		/// </summary>
		/// <param name="date"></param>
		public Day(DateTime date)
		{
			ThisDate = date;
			Meals = new ObservableCollection<Meal>();

			// add type names to the Meals
			foreach (MealType mealType in (MealType[])Enum.GetValues(typeof(MealType)))
			{
				Meals.Add(new Meal(mealType));
			}
		}
	}
}
