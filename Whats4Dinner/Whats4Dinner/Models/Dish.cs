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
			Grains,
			Veggies,
			Proteins,
			Condiments,
			Drinks,
			Other
		}

		private string name;
		private DishCategory thisDishCategory;

		/// <summary>
		/// Name of the Dish
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Category of the Dish, as listed in Dish.Categories
		/// </summary>
		public DishCategory ThisDishCategory { get; set; }

		/// <summary>
		/// Parameterless constructor for JSON deserialization
		/// </summary>
		public Dish() { }

		/// <summary>
		/// Constructor for Dish class
		/// </summary>
		/// <param name="name">Name of the Dish</param>
		/// <param name="category">Category of the Dish, as listed in Dish.Categories</param>
		public Dish(string name, DishCategory category)
		{
			// initialize properties
			Name = name;
			ThisDishCategory = category;
		}
	}
}
