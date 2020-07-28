﻿using Newtonsoft.Json;
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
		//public Dictionary<DishCategory, List<Dish>> Dishes { get; set; }
		public List<DishGroup> Dishes { get; set; }
		public List<DishGroupForJSON> DishesJSON { get; set; }

		public bool HasDishes
		{
			get
			{
				//foreach (List<Dish> dishList in Dishes.Values)
				foreach (DishGroup dishList in Dishes)
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
				//foreach (KeyValuePair<DishCategory, List<Dish>> dishCategories in Dishes)
				foreach (DishGroup dishCategories in Dishes)
				{
					// only add the categories with dishes in it
					//if (dishCategories.Value.Count == 0)
					if (dishCategories.Count == 0)
					{
						continue;
					}
					//mealString += dishCategories.Key.ToString() + ": ";
					mealString += dishCategories.DishGroupCategory + ": ";

					// for each dish
					//foreach (Dish dish in dishCategories.Value)
					foreach (Dish dish in dishCategories)
					{
						mealString += dish.Name;
						//if (dish != dishCategories.Value.Last())
						if (dish != dishCategories.Last())
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
		/// parameterless constructor for JSON deserialization
		/// </summary>
		public Meal() { }

		/// <summary>
		/// constructor for Meal class
		/// </summary>
		/// <param name="mealType">Type of the Meal, such as breakfast, lunch, dinner, etc.</param>
		public Meal(MealType mealType)
		{
			ThisMealType = mealType;
			//Dishes = new Dictionary<DishCategory, List<Dish>>();
			Dishes = new List<DishGroup>();
			DishesJSON = new List<DishGroupForJSON>();

			// add category names to the Dishes
			foreach (DishCategory cat in (DishCategory[])Enum.GetValues(typeof(DishCategory)))
			{
				//Dishes.Add(cat, new List<Dish>());
				Dishes.Add(new DishGroup(cat));
			}
		}

		/// <summary>
		/// add a dish to the list of dishes in the dish category
		/// </summary>
		/// <param name="name"></param>
		/// <param name="cat"></param>
		public void AddDish(string name, DishCategory cat)
		{
			//Dishes[cat].Add(new Dish(name, cat));
			foreach (DishGroup dishList in Dishes)
			{
				if (dishList.DishGroupCategory == cat)
				{
					dishList.Add(new Dish(name, cat));
					break;
				}
			}
		}
	}
}
