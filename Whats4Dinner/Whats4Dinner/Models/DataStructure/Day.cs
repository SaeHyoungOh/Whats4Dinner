using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Whats4Dinner.Models.DataStructure.Meal;

namespace Whats4Dinner.Models.DataStructure
{
	/// <summary>
	/// A Day with Date and a list of Meals, and whether each Meal has dishes in it
	/// </summary>
	public class Day : BaseModel
	{
		/// <summary>
		/// The date of this day
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
		/// Day of the week to display on View as a string
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
		/// Date to display on View as a string
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
		/// List of meals in this day
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
		/// Whether there are any dishes in Breakfast Meal
		/// </summary>
		public bool BreakfastCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Breakfast)
				{
					SetProperty(ref breakfastCheck, Meals[(int)MealType.Breakfast].HasDishes);
				}
				else
				{
					SetProperty(ref breakfastCheck, false);
				}
				return breakfastCheck;
			}
			set
			{
				SetProperty(ref breakfastCheck, value);
			}
		}

		/// <summary>
		/// Whether there are any dishes in Lunch Meal
		/// </summary>
		public bool LunchCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Lunch)
				{
					SetProperty(ref lunchCheck, Meals[(int)MealType.Lunch].HasDishes);
				}
				else
				{
					SetProperty(ref lunchCheck, false);
				}
				return lunchCheck;
			}
			set
			{
				SetProperty(ref lunchCheck, value);
			}
		}

		/// <summary>
		/// Whether there are any dishes in Dinner Meal
		/// </summary>
		public bool DinnerCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Dinner)
				{
					SetProperty(ref dinnerCheck, Meals[(int)MealType.Dinner].HasDishes);
				}
				else
				{
					SetProperty(ref dinnerCheck, false);
				}
				return dinnerCheck;
			}
			set
			{
				SetProperty(ref dinnerCheck, value);
			}
		}

		/// <summary>
		/// Whether there are any dishes in Other Meal
		/// </summary>
		public bool OtherCheck
		{
			get
			{
				if (Meals.Count > (int)MealType.Other)
				{
					SetProperty(ref otherCheck, Meals[(int)MealType.Other].HasDishes);
				}
				else
				{
					SetProperty(ref otherCheck, false);
				}
				return otherCheck;
			}
			set
			{
				SetProperty(ref otherCheck, value);
			}
		}

		/// <summary>
		/// List of Dishes in Breakfast, to be displayed in View
		/// </summary>
		public string DisplayBreakfast
		{
			get
			{
				string mealString = "Breakfast: ";

				if (BreakfastCheck)
				{
					ObservableCollection<Dish> dishList = Meals[(int)MealType.Breakfast].Dishes;

					// add each dish to string
					foreach (Dish dish in dishList)
					{
						mealString += dish.Name;

						if (dish != dishList.Last())
						{
							mealString += ", ";
						}
					}
				}
				else
				{
					mealString += "(Not planned)";
				}
				SetProperty(ref displayBreakfast, mealString);
				return mealString;
			}
			set
			{
				SetProperty(ref displayBreakfast, value);
			}
		}
		/// <summary>
		/// List of Dishes in Lunch, to be displayed in View
		/// </summary>

		public string DisplayLunch
		{
			get
			{
				string mealString = "Lunch: ";

				if (LunchCheck)
				{
					ObservableCollection<Dish> dishList = Meals[(int)MealType.Lunch].Dishes;

					// add each dish to string
					foreach (Dish dish in dishList)
					{
						mealString += dish.Name;

						if (dish != dishList.Last())
						{
							mealString += ", ";
						}
					}
				}
				else
				{
					mealString += "(Not planned)";
				}
				SetProperty(ref displayLunch, mealString);
				return mealString;
			}
			set
			{
				SetProperty(ref displayLunch, value);
			}
		}

		/// <summary>
		/// List of Dishes in Dinner, to be displayed in View
		/// </summary>
		public string DisplayDinner
		{
			get
			{
				string mealString = "Dinner: ";

				if (DinnerCheck)
				{
					ObservableCollection<Dish> dishList = Meals[(int)MealType.Dinner].Dishes;
					// add each dish to string
					foreach (Dish dish in dishList)
					{
						mealString += dish.Name;

						if (dish != dishList.Last())
						{
							mealString += ", ";
						}
					}
				}
				else
				{
					mealString += "(Not planned)";
				}
				SetProperty(ref displayDinner, mealString);
				return mealString;
			}
			set
			{
				SetProperty(ref displayDinner, value);
			}
		}

		/// <summary>
		/// List of Dishes in Other, to be displayed in View
		/// </summary>
		public string DisplayOther
		{
			get
			{
				string mealString = "Other: ";

				if (OtherCheck)
				{
					ObservableCollection<Dish> dishList = Meals[(int)MealType.Other].Dishes;

					// add each dish to string
					foreach (Dish dish in dishList)
					{
						mealString += dish.Name;

						if (dish != dishList.Last())
						{
							mealString += ", ";
						}
					}
				}
				else
				{
					mealString += "(Not planned)";
				}
				SetProperty(ref displayOther, mealString);
				return mealString;
			}
			set
			{
				SetProperty(ref displayOther, value);
			}
		}

		/// <summary>
		/// Whether the day as any meals, used in FileIO
		/// </summary>
		public bool HasMeals
		{
			get
			{
				return BreakfastCheck || LunchCheck || DinnerCheck || OtherCheck;
			}
		}

		// fields for the properties above
		private DateTime thisDate;
		private string displayDayOfWeek;
		private string displayDate;
		private ObservableCollection<Meal> meals;
		private bool breakfastCheck;
		private bool lunchCheck;
		private bool dinnerCheck;
		private bool otherCheck;
		private string displayBreakfast;
		private string displayLunch;
		private string displayDinner;
		private string displayOther;

		/// <summary>
		/// Parameterless constructor for JSON deserialization
		/// </summary>
		public Day() { }

		/// <summary>
		/// Constructor for Day
		/// </summary>
		/// <param name="date"></param>
		public Day(DateTime date)
		{
			// initialize properties
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
