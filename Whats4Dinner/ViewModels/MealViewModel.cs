using Prism.Commands;
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
		/// <summary>
		/// 
		/// </summary>
		public ObservableCollection<DishGroup> DisplayDishLists
		{
			get => displayDishLists;
			set
			{
				displayDishLists = value;
				OnPropertyChanged();
			}
		}
		private ObservableCollection<DishGroup> displayDishLists;

		/// <summary>
		/// delegate command for "delete" button
		/// </summary>
		public DelegateCommand<Dish> DeleteClick { get; private set; }

		/// <summary>
		/// button click action for "delete" button
		/// </summary>
		/// <param name="content"></param>
		private void DeleteClickExecute(Dish content)
		{
			foreach (DishGroup dishGroup in DisplayDishLists)
			{
				if (dishGroup.DishGroupCategory == content.ThisDishCategory)
				{
					dishGroup.Remove(content);
				}
			}
		}

		/// <summary>
		/// returns whether the meal includes the passed dish
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		private bool DeleteClickCanExecute(Dish content)
		{
			foreach (DishGroup dishGroup in DisplayDishLists)
			{
				if (dishGroup.DishGroupCategory == content.ThisDishCategory)
				{
					if (dishGroup.Contains(content))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// constructor for MealViewModel
		/// </summary>
		/// <param name="selected">selected Meal from MealPage</param>
		/// <param name="previousTitle">Title from MealPage</param>
		public MealViewModel(Meal selected, string previousTitle)
		{
			Title = selected.ThisMealType.ToString() + ", " + previousTitle.Replace(",", "");

			DisplayDishLists = new ObservableCollection<DishGroup>();

			// set the display dish categories
			foreach (DishGroup dishList in selected.Dishes)
			{
				DisplayDishLists.Add(dishList);
			}

			// assign delegate commands
			DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute, DeleteClickCanExecute);
		}
	}
}
