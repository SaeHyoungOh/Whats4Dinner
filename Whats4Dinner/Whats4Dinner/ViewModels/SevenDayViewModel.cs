using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// Viewmodel to display SevenDayPage, with a list of days
	/// Derived from BaseViewModel
	/// </summary>
	public class SevenDayViewModel : BaseViewModel
	{
		/// <summary>
		/// Constructor for SevenDayViewModel class
		/// </summary>
		public SevenDayViewModel()
		{
			// initialize properties
			Title = "7-Day View";
			DisplayDays = new ObservableCollection<Day>();
			UserDataIO = new FileIO(userFileName);

			// read user's data from JSON file
			ObservableCollection<Day> dataFromFile = UserDataIO.ReadUserDataFromJSON();

			// fill the 7-day with days
			FillDisplayDays(dataFromFile);

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}
