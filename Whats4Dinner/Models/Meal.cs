using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.Models
{
	public class DishList : List<Dish>
	{
		public DishCategory ThisDishCategory { get; set; }


		public string DisplayDishCategory
		{
			get
			{
				return ThisDishCategory.ToString();
			}
		}
		public List<Dish> Dishes => this;

		/// <summary>
		/// default constructor for deserialization
		/// </summary>
		public DishList() { }

		/// <summary>
		/// constructor to initialize ThisDishCategory
		/// </summary>
		/// <param name="cat"></param>
		public DishList(DishCategory cat)
		{
			ThisDishCategory = cat;
		}
	}

	/// <summary>
	/// A meal with Id, Type, and a list of Dishes
	/// </summary>
	public class Meal
	{
		/// <summary>
		/// List of types a dish can have
		/// </summary>
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
		public MealType ThisMealType { get; set; }

		/// <summary>
		/// List of dishes in the meal, separated by categories, such as grains, veggies, proteins, etc.
		/// </summary>
		public List<DishList> Dishes { get; set; }

		public bool HasDishes
		{
			get
			{
				foreach (DishList dishList in Dishes)
				{
					if (dishList.Count != 0)
					{
						return true;
					}
				}

				return false;
			}
		}

		/// <summary>
		/// list of dishes for display in view
		/// </summary>
		public string DisplayDishes
		{
			get
			{
				string mealString = "";

				// for each dish category
				foreach (DishList dishList in Dishes)
				{
					// only add the categories with dishes in it
					if (dishList.Count == 0)
					{
						continue;
					}
					mealString += dishList.ThisDishCategory + ": ";

					// for each dish
					foreach (Dish dish in dishList)
					{
						mealString += dish.Name;
						if (dish != dishList.Last())
						{
							mealString += ", ";
						}
					}
					mealString += "\n";
				}
				return mealString;
			}
		}
		
		/// <summary>
		/// constructor for Meal class
		/// </summary>
		/// <param name="mealType">Type of the Meal, such as breakfast, lunch, dinner, etc.</param>
		public Meal(MealType mealType)
		{
			ThisMealType = mealType;
			Dishes = new List<DishList>();

			// add category names to the Dishes
			foreach (DishCategory cat in (DishCategory[])Enum.GetValues(typeof(DishCategory)))
			{
				Dishes.Add(new DishList(cat));
			}
		}

		/// <summary>
		/// add a dish to the list of dishes in the dish category
		/// </summary>
		/// <param name="name"></param>
		/// <param name="cat"></param>
		public void AddDish(string name, DishCategory cat)
		{
			foreach (DishList dishList in Dishes)
			{
				if (dishList.ThisDishCategory == cat)
				{
					dishList.Add(new Dish(name, cat));
					break;
				}
			}
		}
	}
}
