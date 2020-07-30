using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace Whats4Dinner.ViewModels.DataStructure
{
	/// <summary>
	/// A meal with Id, Type, and a list of Dishes
	/// </summary>
	public class Meal : BaseModel
	{
		private MealType thisMealType;
		private ObservableCollection<DishGroup> dishes;
		private bool hasDishes;
		private string displayDishes;

		/// <summary>
		/// List of types a dish can have
		/// </summary>
		public enum MealType
		{
			Breakfast,
			Lunch,
			Dinner,
			Other
		}

		/// <summary>
		/// Type of the Meal, such as breakfast, lunch, dinner, etc.
		/// </summary>
		public MealType ThisMealType
		{
			get => thisMealType;
			set
			{
				SetProperty(ref thisMealType, value);
			}
		}

		/// <summary>
		/// List of dishes in the meal, separated by categories, such as grains, veggies, proteins, etc.
		/// </summary>
		public ObservableCollection<DishGroup> Dishes
		{
			get => dishes;
			set
			{
				SetProperty(ref dishes, value);
			}
		}
		public List<DishGroupForJSON> DishesJSON { get; set; }

		public bool HasDishes
		{
			get
			{
				foreach (DishGroup dishList in Dishes)
				{
					if (dishList.Count != 0)
					{
						SetProperty(ref hasDishes, true);
						return true;
					}
				}
				SetProperty(ref hasDishes, false);
				return false;
			}
			set
			{
				SetProperty(ref hasDishes, value);
			}
		}

		/// <summary>
		/// list of dishes for display in view
		/// </summary>
		public string DisplayDishes
		{
			get
			{
				string mealString = "";

				// for each dish category
				foreach (DishGroup dishCategories in Dishes)
				{
					// only add the categories with dishes in it
					if (dishCategories.Count == 0)
					{
						continue;
					}
					mealString += dishCategories.DishGroupCategory + ": ";

					// for each dish
					foreach (Dish dish in dishCategories)
					{
						mealString += dish.Name;
						if (dish != dishCategories.Last())
						{
							mealString += ", ";
						}
					}
					mealString += "\n";
				}
				SetProperty(ref displayDishes, mealString);

				return displayDishes;
			}
			set
			{
				SetProperty(ref displayDishes, value);
			}
		}

		/// <summary>
		/// parameterless constructor for JSON deserialization
		/// </summary>
		public Meal() { }

		/// <summary>
		/// constructor for Meal class
		/// </summary>
		/// <param name="mealType">Type of the Meal, such as breakfast, lunch, dinner, etc.</param>
		public Meal(MealType mealType)
		{
			ThisMealType = mealType;
			Dishes = new ObservableCollection<DishGroup>();
			DishesJSON = new List<DishGroupForJSON>();

			// add category names to the Dishes
			foreach (DishCategory cat in (DishCategory[])Enum.GetValues(typeof(DishCategory)))
			{
				Dishes.Add(new DishGroup(cat));
			}
		}

		/// <summary>
		/// add a dish to the list of dishes in the dish category
		/// </summary>
		/// <param name="name"></param>
		/// <param name="cat"></param>
		public void AddDish(string name, DishCategory cat)
		{
			foreach (DishGroup dishList in Dishes)
			{
				if (dishList.DishGroupCategory == cat)
				{
					dishList.Add(new Dish(name, cat));
					OnPropertyChanged("Dishes");
					break;
				}
			}
		}

		public void DeleteDish(Dish selected)
		{
			foreach (DishGroup dishGroup in Dishes)
			{
				if (dishGroup.DishGroupCategory == selected.ThisDishCategory)
				{
					dishGroup.Remove(selected);
					OnPropertyChanged("Dishes");
					break;
				}
			}
		}
	}
}
