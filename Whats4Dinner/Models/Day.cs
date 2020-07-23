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
		public DateTime ThisDate { get; private set; }

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
			private set
			{
				DisplayDayOfWeek = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string DisplayDate
		{
			get
			{
				string dateString;

				dateString = ThisDate.Date.ToString("MM/dd/yyyy");

				return dateString;
			}
			private set
			{
				DisplayDayOfWeek = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		//public ObservableCollection<string> DisplayMealTypes = new ObservableCollection<string>(Meal.MealTypes);

		/// <summary>
		/// list of meals in this day
		/// </summary>
		public Dictionary<MealType, Meal> Meals { get; private set; }

		public bool BreakfastCheck { get; private set; }
		public bool LunchCheck { get; private set; }
		public bool DinnerCheck { get; private set; }
		public bool OtherCheck { get; private set; }

		//public ObservableCollection<string> DisplayMeals
		//{
		//	get
		//	{
		//		ObservableCollection<string> toReturn = new ObservableCollection<string>();



		//		return toReturn;
		//	}
		//}

		/// <summary>
		/// constructor for Day
		/// </summary>
		/// <param name="date"></param>
		public Day(DateTime date)
		{
			ThisDate = date;
			BreakfastCheck = LunchCheck = DinnerCheck = OtherCheck = false;
			Meals = new Dictionary<MealType, Meal>();

			// add type names to the Meals
			foreach (MealType mealType in (MealType[])Enum.GetValues(typeof(MealType)))
			{
				Meals.Add(mealType, null);
			}
		}

		public void AddMeal(MealType mealType)
		{
			switch (mealType)
			{
				case MealType.Breakfast: BreakfastCheck = true;
					break;
				case MealType.Lunch: LunchCheck = true;
					break;
				case MealType.Dinner: DinnerCheck = true;
					break;
				case MealType.Other: OtherCheck = true;
					break;
				default:
					Meals[mealType] = new Meal(mealType);
					break;
			}
		}
	}
}
