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
		/// <summary>
		/// detail of the meals to display on view
		/// </summary>
		public ObservableCollection<Meal> DisplayMeals
		{
			get => displayMeals;
			set
			{
				SetProperty(ref displayMeals, value);
			}
		}
		private ObservableCollection<Meal> displayMeals;

		public DayViewModel(Day selected)
		{
			Title = selected.DisplayDayOfWeek + ", " + selected.DisplayDate;

			DisplayMeals = new ObservableCollection<Meal>();

			foreach (Meal meal in selected.Meals.Values)
			{
				DisplayMeals.Add(meal);
			}
		}
	}
}
