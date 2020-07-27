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
		//private ObservableCollection<KeyValuePair<DishCategory, List<Dish>>> displayDishLists;
		private ObservableCollection<DishList> displayDishLists;
		//public ObservableCollection<KeyValuePair<DishCategory, List<Dish>>> DisplayDishLists
		public ObservableCollection<DishList> DisplayDishLists
		{
			get => displayDishLists;
			set
			{
				displayDishLists = value;
				OnPropertyChanged();
			}
		}
		public MealViewModel(Meal selected, string previousTitle)
		{
			Title = selected.ThisMealType.ToString() + ", " + previousTitle.Replace(",", "");

			//DisplayDishLists = new ObservableCollection<KeyValuePair<DishCategory, List<Dish>>>();
			DisplayDishLists = new ObservableCollection<DishList>();

			// set the display dish categories
			//foreach (KeyValuePair<DishCategory, List<Dish>> dishList in selected.Dishes)
			foreach (DishList dishList in selected.Dishes)
			{
				DisplayDishLists.Add(dishList);
			}
		}
	}
}
