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
		public DelegateCommand<MenuItemType?> LoadMoreCommand { get; set; }

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
		private void ReloadItemsExecute(MenuItemType? menuItemType)
		{
			NumDays = 7;
			CanLoadMore = true;
			LoadItemsExecute(menuItemType);
		}

		/// <summary>
		/// Load 7 more days to DisplayDays, up to 28 days
		/// </summary>
		/// <param name="menuItemType"></param>
		private void LoadMoreExecute(MenuItemType? menuItemType)
		{
			NumDays += 7;
			if (NumDays > 23) CanLoadMore = false;

			// refill the week with changed days
			DisplayDays.Clear();
			FillDisplayDays(menuItemType);
		}

		/// <summary>
		/// Whether LoadMoreCommand can execute: true if NumDays is 3 weeks or less
		/// </summary>
		/// <param name="menuItemType"></param>
		/// <returns></returns>
		private bool LoadMoreCanExecute(MenuItemType? menuItemType)
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
			PageType = MenuItemType.SevenDayView;
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			DisplayDays = new ObservableCollection<Day>();
			UserDaysIO = new FileIO(userFileName);

			// fill the 7-day with days and add to UserData
			NumDays = 7;
			CanLoadMore = true;
			FillDisplayDays(PageType);
			UserData["DisplayDays"] = DisplayDays;

			// for refreshing
			LoadItemsCommand = new DelegateCommand<MenuItemType?>(ReloadItemsExecute);
			LoadMoreCommand = new DelegateCommand<MenuItemType?>(LoadMoreExecute, LoadMoreCanExecute);
		}
	}
}
