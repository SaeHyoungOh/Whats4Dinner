using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Whats4Dinner.Models;
using static Whats4Dinner.Models.Dish;

namespace Whats4Dinner.ViewModels
{
	class MealViewModel : BaseViewModel
	{
		private ObservableCollection<DishList> displayDishes;
		public ObservableCollection<DishList> DisplayDishes
		{
			get => displayDishes;
			set
			{
				displayDishes = value;
				OnPropertyChanged();
			}
		}
		public MealViewModel(Meal selected, string previousTitle)
		{
			Title = selected.ThisMealType.ToString() + ", " + previousTitle.Replace(",", "");

			DisplayDishes = new ObservableCollection<DishList>();

			// set the display dish categories
			foreach (DishList dishList in selected.Dishes)
			{
				DisplayDishes.Add(dishList);
			}
		}
	}
}
