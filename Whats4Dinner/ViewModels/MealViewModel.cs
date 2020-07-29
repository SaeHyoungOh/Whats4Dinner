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
				SetProperty(ref displayDishLists, value);
				OnPropertyChanged();
			}
		}
		private ObservableCollection<DishGroup> displayDishLists;

		/// <summary>
		/// delegate command for "delete" button
		/// </summary>
		public DelegateCommand AddClick { get; private set; }
		public DelegateCommand<Dish> DeleteClick { get; private set; }

		private void DeleteClickExecute(Dish content)
		{
			foreach (DishGroup dishGroup in DisplayDishLists)
			{
				if (dishGroup.DishGroupCategory == content.ThisDishCategory)
				{
					//dishGroup.Remove(content);
					dishGroup.Add(new Dish("test delete", DishCategory.Condiments));
					OnPropertyChanged("DisplayDishLists");
					break;
				}
			}
		}
		/// <summary>
		/// button click action for "delete" button
		/// </summary>
		/// <param name="content"></param>
		private void AddClickExecute()
		{
			DisplayDishLists[3].Add(new Dish("test add", DishCategory.Drinks));
			OnPropertyChanged("DisplayDishLists");
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
			AddClick = new DelegateCommand(AddClickExecute);
			DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute);
			//DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute, DeleteClickCanExecute);
		}
	}
}
