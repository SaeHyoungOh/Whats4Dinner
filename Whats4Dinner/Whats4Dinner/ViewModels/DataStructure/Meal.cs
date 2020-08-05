using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Whats4Dinner.Models;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.ViewModels.DataStructure
{
	/// <summary>
	/// A meal with MealType, a list of Dishes, a string of dishes to display on View
	/// </summary>
	public class Meal : BaseModel
	{
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
		public ObservableCollection<Dish> Dishes
		{
			get => dishes;
			set
			{
				SetProperty(ref dishes, value);
			}
		}

		/// <summary>
		/// Whether this meal has any dishes
		/// </summary>
		public bool HasDishes
		{
			get
			{
				if (Dishes.Count != 0)
				{
					SetProperty(ref hasDishes, true);
					return true;
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
		/// List of dishes for display in view
		/// </summary>
		public string DisplayDishes
		{
			get
			{
				string mealString = "";

				// for each dish
				foreach (Dish dish in Dishes)
				{
					mealString += dish.Name + "(";

					// also add the dish categories
					foreach (DishCategory dishCategory in dish.DishCategories)
					{
						mealString += dishCategory.ToString();
						if (dishCategory != dish.DishCategories.Last())
						{
							mealString += ", ";
						}
					}
					mealString += ")\n";
				}
				SetProperty(ref displayDishes, mealString);
				return displayDishes;
			}
			set
			{
				SetProperty(ref displayDishes, value);
			}
		}

		// fields for the properties above
		private MealType thisMealType;
		private ObservableCollection<Dish> dishes;
		private bool hasDishes;
		private string displayDishes;

		/// <summary>
		/// Parameterless constructor for JSON deserialization
		/// </summary>
		public Meal() { }

		/// <summary>
		/// Constructor for Meal class
		/// </summary>
		/// <param name="mealType">Type of the Meal, such as breakfast, lunch, dinner, etc.</param>
		public Meal(MealType mealType)
		{
			// initialize properties
			ThisMealType = mealType;
			Dishes = new ObservableCollection<Dish>();
			
		}

		/// <summary>
		/// Add a dish to the list of dishes in the provided dish category
		/// </summary>
		/// <param name="name"></param>
		/// <param name="cat"></param>
		public void AddDish(string name, List<DishCategory> cat)
		{
			Dishes.Add(new Dish(name, cat));
			OnPropertyChanged("DisplayDishes");
			OnPropertyChanged("Dishes");
		}
		
		/// <summary>
		/// Delete a dish from the list of dishes
		/// </summary>
		/// <param name="selected"></param>
		public void DeleteDish(Dish selected)
		{
			Dishes.Remove(selected);
			OnPropertyChanged("DisplayDishes");
			OnPropertyChanged("Dishes");
		}
	}
}
