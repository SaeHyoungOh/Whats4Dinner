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
		/// The currently selected Day that this meal belongs to
		/// </summary>
		public Day SelectedDay
		{
			get => selectedDay;
			set
			{
				SetProperty(ref selectedDay, value);
			}
		}
		private Day selectedDay;

		/// <summary>
		/// Constructor for DayViewModel class
		/// </summary>
		public DayViewModel(Dictionary<string, object> UserData)
		{
			// initialize the properties
			this.UserData = UserData;
			SelectedDay = (Day)UserData["SelectedDay"];
			Title = SelectedDay.DisplayDayOfWeek + ", " + SelectedDay.DisplayDate;
		}
	}
}
