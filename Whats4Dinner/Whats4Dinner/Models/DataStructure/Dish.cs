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

		/// <summary>
		/// Category of the Dish, as listed in Dish.Categories
		/// </summary>
		public List<string> ThisDishCategories { get; set; }

		// DishCategory for display in View
		public string DishCategory1
		{
			get
			{
				SetProperty(ref dishCategory1, DishCategories["1"]);
				return dishCategory1;
			}
		}
		public string DishCategory2
		{
			get
			{
				SetProperty(ref dishCategory2, DishCategories["2"]);
				return dishCategory2;
			}
		}
		public string DishCategory3
		{
			get
			{
				SetProperty(ref dishCategory3, DishCategories["3"]);
				return dishCategory3;
			}
		}
		public string DishCategory4
		{
			get
			{
				SetProperty(ref dishCategory4, DishCategories["4"]);
				return dishCategory4;
			}
		}
		public string DishCategory5
		{
			get
			{
				SetProperty(ref dishCategory5, DishCategories["5"]);
				return dishCategory5;
			}
		}

		// whether DishCategories contains each of the DishCategory, to display in View
		public bool HasDishCategory1
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories["1"]))
				{
					SetProperty(ref hasDishCategory1, true);
					return true;
				}
				SetProperty(ref hasDishCategory1, false);
				return false;
			}
		}
		public bool HasDishCategory2
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories["2"]))
				{
					SetProperty(ref hasDishCategory2, true);
					return true;
				}
				SetProperty(ref hasDishCategory2, false);
				return false;
			}
		}
		public bool HasDishCategory3
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories["3"]))
				{
					SetProperty(ref hasDishCategory3, true);
					return true;
				}
				SetProperty(ref hasDishCategory3, false);
				return false;
			}
		}
		public bool HasDishCategory4
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories["4"]))
				{
					SetProperty(ref hasDishCategory4, true);
					return true;
				}
				SetProperty(ref hasDishCategory4, false);
				return false;
			}
		}
		public bool HasDishCategory5
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories["5"]))
				{
					SetProperty(ref hasDishCategory5, true);
					return true;
				}
				SetProperty(ref hasDishCategory5, false);
				return false;
			}
		}

		// fields for the properties above
		private string name;
		private string dishCategory1;
		private string dishCategory2;
		private string dishCategory3;
		private string dishCategory4;
		private string dishCategory5;
		private bool hasDishCategory1;
		private bool hasDishCategory2;
		private bool hasDishCategory3;
		private bool hasDishCategory4;
		private bool hasDishCategory5;

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
