using System;
using System.Collections.Generic;
using System.Text;

namespace Whats4Dinner.Models
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish
	{
		/// <summary>
		/// Name of the Dish
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// List of categories a dish can have
		/// </summary>
		public enum DishCategory
		{
			Grains,
			Veggies,
			Proteins,
			Condiments,
			Drinks,
			Other = 99
		}

		/// <summary>
		/// Category of the Dish, as listed in Dish.Categories
		/// </summary>
		public DishCategory ThisDishCategory { get; set; }

		/// <summary>
		/// parameterless constructor for JSON deserialization
		/// </summary>
		public Dish() { }

		/// <summary>
		/// constructor for Dish class
		/// </summary>
		/// <param name="name">Name of the Dish</param>
		/// <param name="category">Category of the Dish, as listed in Dish.Categories</param>
		public Dish(string name, DishCategory category)
		{
			Name = name;
			ThisDishCategory = category;
		}
	}
}
