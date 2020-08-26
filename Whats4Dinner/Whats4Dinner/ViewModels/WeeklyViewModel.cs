﻿using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Whats4Dinner.Models;
using Whats4Dinner.Models.DataStructure;

namespace Whats4Dinner.ViewModels
{
	public class WeeklyViewModel : BaseViewModel
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

		/// <summary>
		/// Constructor for WeeklyViewModel class
		/// </summary>
		public WeeklyViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			this.UserData = UserData;
			Title = "Weekly View";
			UserDaysIO = new FileIO(userFileName);
			if (UserData.ContainsKey("UserDays")) UserDays = (ObservableCollection<Day>)UserData["UserDays"];
			if (UserData.ContainsKey("DisplayDays")) DisplayDays = (ObservableCollection<Day>)UserData["DisplayDays"];
			PageType = MenuItemType.WeeklyView;
			UserData["PageType"] = PageType;

			// fill the week with days
			NumDays = 7;
			FillDisplayDays(UserData);

			// initialize commands
			LoadItemsCommand = new DelegateCommand<Dictionary<string, object>>(LoadItemsExecute);
		}
	}
}
