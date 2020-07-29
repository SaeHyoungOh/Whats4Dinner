using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;

namespace Whats4Dinner.ViewModels
{
	public class BaseViewModel : BaseModel
	{
		private string title = string.Empty;
		private bool isBusy = false;

		/// <summary>
		/// title of the page
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		protected readonly string fileName = "UserData.json";
		protected FileIO UserDataIO;

		/// <summary>
		/// list of all the Days
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
		public DelegateCommand LoadItemsCommand { get; set; }

		/// <summary>
		/// Fill the DisplayDays from user data read from file
		/// </summary>
		/// <param name="dataFromFile"></param>
		protected void FillDisplayDays(List<Day> dataFromFile)
		{
			DateTime today = DateTime.Today;
			int i = 0, j = 0;

			while (i < 7)
			{
				DateTime fileDate, currentDate = today.AddDays(i);

				// prevent j from going out of bounds
				if (j < dataFromFile.Count)
				{
					fileDate = dataFromFile[j].ThisDate;
				}
				else
				{
					fileDate = today.AddDays(7);
				}

				// if we run out of data from file, fill days with blanks
				if (j > dataFromFile.Count - 1)
				{
					DisplayDays.Add(new Day(currentDate));
					i++;
				}
				// skip until today
				else if (fileDate < currentDate)
				{
					j++;
				}
				// use the day if date matches
				else if (fileDate == currentDate)
				{
					DisplayDays.Add(dataFromFile[j]);
					j++;
					i++;
				}
				// fill the between days with empty day
				else if (fileDate <= currentDate.AddDays(6))
				{
					int emptyDays = (fileDate - currentDate).Days;
					for (int k = 0; k < emptyDays; k++)
					{
						DisplayDays.Add(new Day(currentDate));
						i++;
					}
				}
				// ignore all dates after the 7 days
				else
				{
					j = dataFromFile.Count - 1;
				}
			}
		}

		/// <summary>
		/// to be made asyncronous when using database
		/// </summary>
		protected void ExecuteLoadItemsCommand()
		{
			IsBusy = true;

			try
			{
				DisplayDays.Clear();

				// read user's data from JSON file
				List<Day> dataFromFile = UserDataIO.ReadFromJSON();

				// fill the week with days
				FillDisplayDays(dataFromFile);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
