using System.Collections.Generic;
using System.Dynamic;

namespace Whats4Dinner.Models.DataStructure
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish : BaseModel
	{
		/// <summary>
		/// Static (shared) FileIO object for reading/writing Dish Categories
		/// </summary>
		private static FileIO DishCategoriesIO { get; set; }

		/// <summary>
		/// JSON file name for Dish Categories
		/// </summary>
		private static string DishCategoriesFileName = "DishCategories.json";

		/// <summary>
		/// List of categories a dish can have
		/// </summary>
		public static List<string> DishCategories { get; set; }

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
		public List<string> ThisDishCategories
		{
			get => dishCategories;
			set
			{
				SetProperty(ref dishCategories, value);
			}
		}

		// DishCategory for display in View
		public string DishCategory0
		{
			get
			{
				SetProperty(ref dishCategory0, DishCategories[0]);
				return dishCategory0;
			}
		}
		public string DishCategory1
		{
			get
			{
				SetProperty(ref dishCategory1, DishCategories[1]);
				return dishCategory1;
			}
		}
		public string DishCategory2
		{
			get
			{
				SetProperty(ref dishCategory2, DishCategories[2]);
				return dishCategory2;
			}
		}
		public string DishCategory3
		{
			get
			{
				SetProperty(ref dishCategory3, DishCategories[3]);
				return dishCategory3;
			}
		}
		public string DishCategory4
		{
			get
			{
				SetProperty(ref dishCategory4, DishCategories[4]);
				return dishCategory4;
			}
		}

		// whether DishCategories contains each of the DishCategory, to display in View
		public bool HasDishCategory0
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories[0]))
				{
					SetProperty(ref hasDishCategory0, true);
					return true;
				}
				SetProperty(ref hasDishCategory0, false);
				return false;
			}
		}
		public bool HasDishCategory1
		{
			get
			{
				if (ThisDishCategories.Contains(DishCategories[1]))
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
				if (ThisDishCategories.Contains(DishCategories[2]))
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
				if (ThisDishCategories.Contains(DishCategories[3]))
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
				if (ThisDishCategories.Contains(DishCategories[4]))
				{
					SetProperty(ref hasDishCategory4, true);
					return true;
				}
				SetProperty(ref hasDishCategory4, false);
				return false;
			}
		}

		// fields for the properties above
		private string name;
		private List<string> dishCategories;
		private string dishCategory0;
		private string dishCategory1;
		private string dishCategory2;
		private string dishCategory3;
		private string dishCategory4;
		private bool hasDishCategory0;
		private bool hasDishCategory1;
		private bool hasDishCategory2;
		private bool hasDishCategory3;
		private bool hasDishCategory4;

		/// <summary>
		/// Static constructor for the shared property DishCategoriesIO
		/// Also initialize static DishCategories collection
		/// </summary>
		static Dish()
		{
			DishCategoriesIO = new FileIO(DishCategoriesFileName);
			DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();
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
			ThisDishCategories = new List<string>();
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
		}
	}
}
