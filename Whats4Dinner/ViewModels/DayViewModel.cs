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
		public string DisplayBreakfast { get; private set; }
		public string DisplayLunch { get; private set; }
		public string DisplayDinner { get; private set; }
		public string DisplayOther { get; private set; }

		private ObservableCollection<KeyValuePair<MealType, Meal>> displayMeals;
		public ObservableCollection<KeyValuePair<MealType, Meal>> DisplayMeals
		{
			get => displayMeals;
			private set
			{
				displayMeals = value;
				OnPropertyChanged();
			}
		}

		public DayViewModel(Day selected)
		{
			Title = selected.DisplayDayOfWeek + ", " + selected.DisplayDate;

			DisplayMeals = new ObservableCollection<KeyValuePair<MealType, Meal>>();

			foreach (var item in selected.Meals)
			{
				DisplayMeals.Add(item);
			}
		}
	}
}
