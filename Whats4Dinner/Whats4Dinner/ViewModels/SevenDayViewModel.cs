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
			PageType = MenuItemType.SevenDayView;

			// read user's data from JSON file
			UserDaysIO = new FileIO(userFileName);
			UserDays = UserDaysIO.ReadUserDaysFromJSON();

			// fill the 7-day with days
			DisplayDays = new ObservableCollection<Day>();
			FillDisplayDays(PageType);

			// fill data parameter
			UserData = new Dictionary<string, object>
			{
				{ "UserDays", UserDays },
				{ "DisplayDays", DisplayDays }
			};

			// for refreshing
			LoadItemsCommand = new DelegateCommand<MenuItemType?>(ExecuteLoadItemsCommand);
		}
	}
}
