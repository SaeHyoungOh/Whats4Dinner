using System.Collections.Generic;
using System.Dynamic;

namespace Whats4Dinner.Models
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish
	{
		/// <summary>
		/// List of categories a dish can have
		/// </summary>
		public enum DishCategory
		{
			Grain,
			Veggie,
			Protein,
			Condiment,
			Drink,
			Other
		}

		/// <summary>
		/// Name of the Dish
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Category of the Dish, as listed in Dish.Categories
		/// </summary>
		public List<DishCategory> DishCategories { get; set; }

		// whether DishCategories contains each of the DishCategory
		public bool HasGrain
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Grain))
				{
					return true;
				}
				return false;
			}
		}
		public bool HasVeggie
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Veggie))
				{
					return true;
				}
				return false;
			}
		}
		public bool HasProtein
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Protein))
				{
					return true;
				}
				return false;
			}
		}
		public bool HasCondiment
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Condiment))
				{
					return true;
				}
				return false;
			}
		}
		public bool HasDrink
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Drink))
				{
					return true;
				}
				return false;
			}
		}
		public bool HasOther
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Other))
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// Parameterless constructor for JSON deserialization
		/// </summary>
		public Dish() { }

		/// <summary>
		/// Constructor for Dish class, with name only; it creates a new empty List of DishCategory
		/// </summary>
		/// <param name="name"></param>
		public Dish(string name)
		{
			// initialize properties
			Name = name;
			DishCategories = new List<DishCategory>();
		}

		/// <summary>
		/// Constructor for Dish class, with name and a list of dish categories
		/// </summary>
		/// <param name="name">Name of the Dish</param>
		/// <param name="category">Category of the Dish, as listed in Dish.Categories</param>
		public Dish(string name, List<DishCategory> category)
		{
			// initialize properties
			Name = name;
			DishCategories = category;
		}
	}
}
