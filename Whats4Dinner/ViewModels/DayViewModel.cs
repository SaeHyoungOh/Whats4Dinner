using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Whats4Dinner.Models;
using static Whats4Dinner.Models.Meal;

namespace Whats4Dinner.ViewModels
{
	class DayViewModel : BaseViewModel
	{
		private ObservableCollection<Tuple<MealType, string>> displayMeals;
		/// <summary>
		/// detail of the meals to display on view
		/// </summary>
		public ObservableCollection<Tuple<MealType, string>> DisplayMeals
		{
			get => displayMeals;
			set
			{
				displayMeals = value;
				OnPropertyChanged();
			}
		}

		public DayViewModel(Day selected)
		{
			Title = selected.DisplayDayOfWeek + ", " + selected.DisplayDate;

			DisplayMeals = new ObservableCollection<Tuple<MealType, string>>();

			// build the display string for each meal
			foreach (KeyValuePair<MealType, Meal> mealType in selected.Meals)
			{
				string mealString = "";

				// skip empty meals
				if (mealType.Value == null)
				{
					DisplayMeals.Add(new Tuple<MealType, string>(mealType.Key, null));
					continue;
				}

				// for each dish category
				foreach (KeyValuePair<Dish.DishCategory, List<Dish>> dishCategory in mealType.Value.Dishes)
				{
					mealString += dishCategory.Key + ": ";

					// for each dish
					foreach (Dish dish in dishCategory.Value)
					{
						mealString += dish.Name;
						if (dish != dishCategory.Value.Last())
						{
							mealString += ", ";
						}
					}
					mealString += "\n";
				}
				DisplayMeals.Add(new Tuple<MealType, string>(mealType.Key, mealString));
			}
		}
	}
}
