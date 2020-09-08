using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;

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

		// whether DishCategories contains each of the DishCategory, to display in View
		public bool HasDishCategory1
		{
			get
			{
				SetProperty(ref hasDishCategory1, ThisDishCategories.Contains(DishCategories["1"]));
				return hasDishCategory1;

			}
			set
			{
				SetProperty(ref hasDishCategory1, value);
			}
		}
		public bool HasDishCategory2
		{
			get
			{
				SetProperty(ref hasDishCategory2, ThisDishCategories.Contains(DishCategories["2"]));
				return hasDishCategory2;

			}
			set
			{
				SetProperty(ref hasDishCategory2, value);
			}
		}
		public bool HasDishCategory3
		{
			get
			{
				SetProperty(ref hasDishCategory3, ThisDishCategories.Contains(DishCategories["3"]));
				return hasDishCategory3;

			}
			set
			{
				SetProperty(ref hasDishCategory3, value);
			}
		}
		public bool HasDishCategory4
		{
			get
			{
				SetProperty(ref hasDishCategory4, ThisDishCategories.Contains(DishCategories["4"]));
				return hasDishCategory4;

			}
			set
			{
				SetProperty(ref hasDishCategory4, value);
			}
		}
		public bool HasDishCategory5
		{
			get
			{
				SetProperty(ref hasDishCategory5, ThisDishCategories.Contains(DishCategories["5"]));
				return hasDishCategory5;

			}
			set
			{
				SetProperty(ref hasDishCategory5, value);
			}
		}

		// fields for the properties above
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
		/// Constructor for Dish class, with name and a list of dish categories
		/// </summary>
		/// <param name="name">Name of the Dish</param>
		/// <param name="categories">Categories of the Dish, as listed in Dish.DishCategories</param>
		public Dish(string name, List<string> categories, Dictionary<string, object> UserData)
		{
			// initialize properties
			Name = name;
			ThisDishCategories = categories;
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			DishCategories = (Dictionary<string, string>)UserData["DishCategories"];
		}
	}
}
