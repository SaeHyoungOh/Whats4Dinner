using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace Whats4Dinner.Models.DataStructure
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish : BaseModel
	{
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

		// fields for the properties above
		private string name;

		/// <summary>
		/// Category of the Dish, as listed in Dish.Categories
		/// </summary>
		public List<string> ThisDishCategories { get; set; }

		// DishCategory for display in View
		public string DishCategory1 { get => DishCategories["1"]; }
		public string DishCategory2 { get => DishCategories["2"]; }
		public string DishCategory3 { get => DishCategories["3"]; }
		public string DishCategory4 { get => DishCategories["4"]; }
		public string DishCategory5 { get => DishCategories["5"]; }

		// whether DishCategories contains each of the DishCategory, to display in View
		public bool HasDishCategory1 { get => ThisDishCategories.Contains(DishCategories["1"]); }
		public bool HasDishCategory2 { get => ThisDishCategories.Contains(DishCategories["2"]); }
		public bool HasDishCategory3 { get => ThisDishCategories.Contains(DishCategories["3"]); }
		public bool HasDishCategory4 { get => ThisDishCategories.Contains(DishCategories["4"]); }
		public bool HasDishCategory5 { get => ThisDishCategories.Contains(DishCategories["5"]); }

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
			ThisDishCategories = new List<string>();
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();
		}

		/// <summary>
		/// Constructor for Dish class, with name and a list of dish categories
		/// </summary>
		/// <param name="name">Name of the Dish</param>
		/// <param name="categories">Categories of the Dish, as listed in Dish.DishCategories</param>
		public Dish(string name, List<string> categories)
		{
			// initialize properties
			Name = name;
			ThisDishCategories = categories;
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();
		}
	}
}
