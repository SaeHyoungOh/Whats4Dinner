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
		public SevenDayViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			Title = "7-Day View";
			PageType = MenuItemType.SevenDayView;
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			DisplayDays = new ObservableCollection<Day>();
			UserDaysIO = new FileIO(userFileName);

			// fill the 7-day with days and add to UserData
			FillDisplayDays(PageType);
			UserData["DisplayDays"] = DisplayDays;

			// for refreshing
			LoadItemsCommand = new DelegateCommand<MenuItemType?>(ExecuteLoadItemsCommand);
		}
	}
}
