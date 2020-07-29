using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Whats4Dinner.ViewModels.DataStructure;

namespace Whats4Dinner.ViewModels
{
	class DayViewModel : BaseViewModel
	{
		/// <summary>
		/// detail of the meals to display on view
		/// </summary>
		private ObservableCollection<Meal> displayMeals;
		private Day selectedDay;

		public ObservableCollection<Meal> DisplayMeals
		{
			get => displayMeals;
			set
			{
				SetProperty(ref displayMeals, value);
			}
		}
		public Day SelectedDay
		{
			get => selectedDay;
			set
			{
				SetProperty(ref selectedDay, value);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="DisplayDays"></param>
		/// <param name="dayIndex"></param>
		public DayViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;

			Title = SelectedDay.DisplayDayOfWeek + ", " + SelectedDay.DisplayDate;

			DisplayMeals = SelectedDay.Meals;

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}
