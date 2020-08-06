using System.Collections.Generic;
using System.Dynamic;

namespace Whats4Dinner.ViewModels.DataStructure
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish : BaseModel
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
		public List<DishCategory> DishCategories
		{
			get => dishCategories;
			set
			{
				SetProperty(ref dishCategories, value);
			}
		}

		// whether DishCategories contains each of the DishCategory
		public bool HasGrain
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Grain))
				{
					SetProperty(ref hasGrain, true);
					return true;
				}
				SetProperty(ref hasGrain, false);
				return false;
			}
		}
		public bool HasVeggie
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Veggie))
				{
					SetProperty(ref hasVeggie, true);
					return true;
				}
				SetProperty(ref hasVeggie, false);
				return false;
			}
		}
		public bool HasProtein
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Protein))
				{
					SetProperty(ref hasProtein, true);
					return true;
				}
				SetProperty(ref hasProtein, false);
				return false;
			}
		}
		public bool HasCondiment
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Condiment))
				{
					SetProperty(ref hasCondiment, true);
					return true;
				}
				SetProperty(ref hasCondiment, false);
				return false;
			}
		}
		public bool HasDrink
		{
			get
			{
				if (DishCategories.Contains(DishCategory.Drink))
				{
					SetProperty(ref hasDrink, true);
					return true;
				}
				SetProperty(ref hasDrink, false);
				return false;
			}
		}

		// fields for the properties above
		private string name;
		private List<DishCategory> dishCategories;
		private bool hasGrain;
		private bool hasVeggie;
		private bool hasProtein;
		private bool hasCondiment;
		private bool hasDrink;

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
