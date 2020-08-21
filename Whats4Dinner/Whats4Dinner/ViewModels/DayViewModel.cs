using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// Viewmodel to display DayPage, with a list of meals
	/// Derived from BaseViewModel
	/// </summary>
	class DayViewModel : BaseViewModel
	{
		/// <summary>
		/// The currently selected Day
		/// </summary>
		public Day SelectedDay
		{
			get => selectedDay;
			set
			{
				SetProperty(ref selectedDay, value);
			}
		}

		/// <summary>
		/// List of the Meals in the Day to display on View
		/// </summary>
		public ObservableCollection<Meal> DisplayMeals
		{
			get => displayMeals;
			set
			{
				SetProperty(ref displayMeals, value);
			}
		}

		// fields for the properties above
		private Day selectedDay;
		private ObservableCollection<Meal> displayMeals;

		/// <summary>
		/// Constructor for DayViewModel class
		/// </summary>
		/// <param name="UserData">The entirety of the DisplayDays for the week</param>
		/// <param name="SelectedDay">The currently selected Day</param>
		public DayViewModel(ObservableCollection<Day> UserData, Day SelectedDay)
		{
			// initialize the properties
			this.UserData = UserData;
			this.SelectedDay = SelectedDay;
			Title = SelectedDay.DisplayDayOfWeek + ", " + SelectedDay.DisplayDate;
			DisplayMeals = SelectedDay.Meals;
		}
	}
}
