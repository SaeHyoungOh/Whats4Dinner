using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
		/// List of all the Days and their Meals and Dishes for View
		/// </summary>
		public ObservableCollection<Day> DisplayDays
		{
			get => displayDays;
			set
			{
				SetProperty(ref displayDays, value);
			}
		}
		private ObservableCollection<Day> displayDays;

		public DelegateCommand<Dictionary<string, object>> LoadMoreCommand { get; set; }

		/// <summary>
		/// Whether loading more days has reached its limits, to be used in View
		/// </summary>
		public bool CanLoadMore
		{
			get => canLoadMore;
			set
			{
				SetProperty(ref canLoadMore, value);
			}
		}
		private bool canLoadMore;

		/// <summary>
		/// Reload the UserDays from file and fill DisplayDays with 7 days.
		/// </summary>
		/// <param name="menuItemType"></param>
		private void ReloadItemsExecute(Dictionary<string, object> CommandParams)
		{
			NumDays = 7;
			CanLoadMore = true;
			LoadItemsExecute(CommandParams);
		}

		/// <summary>
		/// Load 7 more days to DisplayDays, up to 28 days
		/// </summary>
		/// <param name="menuItemType"></param>
		private void LoadMoreExecute(Dictionary<string, object> CommandParams)
		{
			NumDays += 7;
			if (NumDays > 23) CanLoadMore = false;

			// refill the week with changed days
			DisplayDays.Clear();
			FillDisplayDays(CommandParams);
		}

		/// <summary>
		/// Whether LoadMoreCommand can execute: true if NumDays is 3 weeks or less
		/// </summary>
		/// <param name="menuItemType"></param>
		/// <returns></returns>
		private bool LoadMoreCanExecute(Dictionary<string, object> CommandParams)
		{
			if (NumDays <= 21) return true;
			else return false;
		}

		/// <summary>
		/// Constructor for SevenDayViewModel class
		/// </summary>
		public SevenDayViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			Title = "7-Day View";
			UserDaysIO = new FileIO(userFileName);
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			PageType = MenuItemType.SevenDayView;
			DisplayDays = new ObservableCollection<Day>();
			CommandParams = new Dictionary<string, object>
			{
				{ "PageType", PageType },
				{ "DisplayDays", DisplayDays }
			};
			UserData["DisplayDays"] = DisplayDays;

			// fill the 7-day with days
			NumDays = 7;
			CanLoadMore = true;
			FillDisplayDays(CommandParams);

			// initialize commands
			LoadItemsCommand = new DelegateCommand<Dictionary<string, object>>(ReloadItemsExecute);
			LoadMoreCommand = new DelegateCommand<Dictionary<string, object>>(LoadMoreExecute, LoadMoreCanExecute);
		}
	}
}
