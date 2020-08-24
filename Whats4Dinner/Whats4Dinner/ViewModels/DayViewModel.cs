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
		private ObservableCollection<Meal> displayMeals;

		/// <summary>
		/// Constructor for DayViewModel class
		/// </summary>
		public DayViewModel(Dictionary<string, object> UserData)
		{
			// initialize the properties
			this.UserData = UserData;
			Day SelectedDay = (Day)UserData["SelectedDay"];
			Title = SelectedDay.DisplayDayOfWeek + ", " + SelectedDay.DisplayDate;
			DisplayMeals = SelectedDay.Meals;
		}
	}
}
