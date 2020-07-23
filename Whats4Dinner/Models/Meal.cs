using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.Models
{
	/// <summary>
	/// A meal with Id, Type, and a list of Dishes
	/// </summary>
	public class Meal
	{
		/// <summary>
		/// List of types a dish can have
		/// </summary>
		//public static string[] MealType = { "Breakfast", "Lunch", "Dinner", "Other" };
		public enum MealType
		{
			Breakfast,
			Lunch,
			Dinner,
			Other = 99
		}

		/// <summary>
		/// Type of the Meal, such as breakfast, lunch, dinner, etc.
		/// </summary>
		public MealType ThisMealType { get; private set; }

		/// <summary>
		/// List of dishes in the meal, separated by categories, such as grains, veggies, proteins, etc.
		/// </summary>
		public Dictionary<DishCategory, List<Dish>> Dishes { get; private set; }
		
		/// <summary>
		/// constructor for Meal class
		/// </summary>
		/// <param name="mealType">Type of the Meal, such as breakfast, lunch, dinner, etc.</param>
		public Meal(MealType mealType)
		{
			ThisMealType = mealType;
			Dishes = new Dictionary<DishCategory, List<Dish>>();

			// add category names to the Dishes
			foreach (DishCategory cat in (DishCategory[])Enum.GetValues(typeof(DishCategory)))
			{
				Dishes.Add(cat, new List<Dish>());
			}
		}

		public void AddDish(string name, DishCategory cat)
		{
			Dishes[cat].Add(new Dish(name, cat));
		}
	}
}
