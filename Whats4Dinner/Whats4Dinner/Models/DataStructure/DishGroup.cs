using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Whats4Dinner.Models;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.ViewModels.DataStructure
{
	/// <summary>
	/// Inherited class of ObservableCollection of Dish, so that it can be used in Grouped Listview in the XAML View
	/// </summary>
	public class DishGroup : ObservableCollection<Dish>, INotifyPropertyChanged
	{
		/// <summary>
		/// The category of the list of dishes
		/// </summary>
		public DishCategory DishGroupCategory { get; set; }

		public string DisplayDishCategory
		{
			get
			{
				return DishGroupCategory.ToString();
			}
		}

		/// <summary>
		/// Parameterless constructor for JSON deserialization
		/// </summary>
		public DishGroup() { }

		/// <summary>
		/// Constructor for a new empty list
		/// </summary>
		/// <param name="cat"></param>
		public DishGroup(DishCategory cat)
		{
			DishGroupCategory = cat;
		}

		/// <summary>
		/// Constructor to create a DishGroup list from an ObservableCollection of Dishes.
		/// </summary>
		/// <param name="cat"></param>
		/// <param name="dishes"></param>
		public DishGroup(DishCategory cat, ObservableCollection<Dish> dishes)
		{
			// initialize properties
			DishGroupCategory = cat;
			Clear();
			foreach (Dish dish in dishes)
			{
				Add(dish);
			}
		}
	}

	/// <summary>
	/// Class specifically for serializing a DishGroup object to JSON
	/// </summary>
	public class DishGroupForJSON
	{
		/// <summary>
		/// The category of the list of dishes
		/// </summary>
		public DishCategory DishGroupCategory { get; set; }

		/// <summary>
		/// The list of dishes copied from DishGroup
		/// </summary>
		public ObservableCollection<Dish> DishList { get; set; }

		/// <summary>
		/// Parameterless constructor for JSON deserialization
		/// </summary>
		public DishGroupForJSON() { }

		/// <summary>
		/// Constructor for DishGroupForJSON; it requires the dish category and the ObservableColletion of Dishes
		/// </summary>
		/// <param name="cat"></param>
		/// <param name="dishes"></param>
		public DishGroupForJSON(DishCategory cat, ObservableCollection<Dish> dishes)
		{
			// initialize properties
			DishGroupCategory = cat;
			DishList = dishes;
		}
	}

}
