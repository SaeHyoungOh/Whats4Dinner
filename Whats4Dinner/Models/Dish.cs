using System;
using System.Collections.Generic;
using System.Text;
using Whats4Dinner.Models;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.Models
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish : BaseModel
	{
		private string name;
		private DishCategory thisDishCategory;

		/// <summary>
		/// Name of the Dish
		/// </summary>
		public string Name
		{
			get => name;
			set
			{
				SetProperty(ref name, value);
			}
		}

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
		public DishCategory ThisDishCategory
		{
			get => thisDishCategory;
			set
			{
				SetProperty(ref thisDishCategory, value);
			}
		}

		/// <summary>
		/// parameterless constructor for JSON deserialization
		/// </summary>
		public Dish() { }

		/// <summary>
		/// constructor for Dish class
		/// </summary>
		/// <param name="newName">Name of the Dish</param>
		/// <param name="cat">Category of the Dish, as listed in Dish.Categories</param>
		public Dish(string newName, DishCategory cat)
		{
			Name = newName;
			ThisDishCategory = cat;
		}
	}

	public class DishGroup : List<Dish>
	{
		public DishCategory DishGroupCategory { get; set; }

		public string DisplayDishCategory
		{
			get
			{
				return DishGroupCategory.ToString();
			}
		}

		public DishGroup() { }

		public DishGroup(DishCategory cat)
		{
			DishGroupCategory = cat;
		}
		public DishGroup(DishCategory cat, List<Dish> dishes)
		{
			DishGroupCategory = cat;
			Clear();
			foreach (Dish dish in dishes)
			{
				Add(dish);
			}
		}
	}
}

public class DishGroupForJSON
{
	public DishCategory DishGroupCategory { get; set; }

	public List<Dish> DishList { get; set; }

	public DishGroupForJSON() { }

	public DishGroupForJSON(DishCategory cat, List<Dish> dishes)
	{
		DishGroupCategory = cat;
		DishList = dishes;
	}
}
