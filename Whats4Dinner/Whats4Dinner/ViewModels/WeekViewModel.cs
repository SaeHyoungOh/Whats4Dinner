﻿using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// Viewmodel to display WeekPage, with a list of days
	/// Derived from BaseViewModel
	/// </summary>
	public class WeekViewModel : BaseViewModel
	{
		/// <summary>
		/// Constructor for WeekViewModel class
		/// </summary>
		public WeekViewModel()
		{
			// initialize properties
			Title = "Week View";
			DisplayDays = new ObservableCollection<Day>();
			UserDataIO = new FileIO(fileName);

			// create a sample file. delete for production
			UserDataIO.CreateSampleFile();

			// read user's data from JSON file
			List<Day> dataFromFile = UserDataIO.ReadFromJSON();

			// fill the week with days
			FillDisplayDays(dataFromFile);

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}