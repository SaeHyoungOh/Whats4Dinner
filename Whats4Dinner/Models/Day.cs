using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static Whats4Dinner.Models.Meal;

namespace Whats4Dinner.Models
{
	/// <summary>
	/// A Day with Date and a list of Meals
	/// </summary>
	public class Day
	{
		/// <summary>
		/// the date of this day
		/// </summary>
		public DateTime ThisDate { get; set; }

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

				return dayString;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string DisplayDate
		{
			get
			{
				return ThisDate.Date.ToString("MM/dd/yyyy");
			}
		}

		/// <summary>
		/// list of meals in this day
		/// </summary>
		public Dictionary<MealType, Meal> Meals { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool BreakfastCheck
		{
			get
			{
				return Meals[MealType.Breakfast].HasDishes;
			}
		}
		public bool LunchCheck
		{
			get
			{
				return Meals[MealType.Lunch].HasDishes;
			}
		}
		public bool DinnerCheck
		{
			get
			{
				return Meals[MealType.Dinner].HasDishes;
			}
		}
		public bool OtherCheck
		{
			get
			{
				return Meals[MealType.Other].HasDishes;
			}
		}


		/// <summary>
		/// constructor for Day
		/// </summary>
		/// <param name="date"></param>
		public Day(DateTime date)
		{
			ThisDate = date;
			Meals = new Dictionary<MealType, Meal>();

			// add type names to the Meals
			foreach (MealType mealType in (MealType[])Enum.GetValues(typeof(MealType)))
			{
				Meals.Add(mealType, new Meal(mealType));
			}
		}
	}
}
