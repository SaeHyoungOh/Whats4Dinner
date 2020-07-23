using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Whats4Dinner.Models;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// viewmodel to help display WeeklyPage, with a list of days
	/// </summary>
	public class WeekViewModel : BaseViewModel
	{
		private ObservableCollection<Day> displayDays;

		/// <summary>
		/// list of all the Days
		/// </summary>
		public ObservableCollection<Day> DisplayDays
		{
			get => displayDays;
			private set
			{
				displayDays = value;
				OnPropertyChanged();
			}
		}

		private void CreateJSON()
		{
			// create the object to save to JSON
			ObservableCollection<Day> sample = new ObservableCollection<Day>
			{
				new Day(DateTime.Today.AddDays(1)),
				new Day(DateTime.Today.AddDays(3))
			};
		}

		public WeekViewModel()
		{
			Title = "Week View";

			// populate the list with 7 days, starting today
			DisplayDays = new ObservableCollection<Day>();

			for (int i = 0; i < 7; i++)
			{
				DisplayDays.Add(new Day(DateTime.Today.AddDays(i)));
			}
		}

	}
}
