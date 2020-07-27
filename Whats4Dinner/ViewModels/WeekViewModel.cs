using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Whats4Dinner.Models;
using Xamarin.Forms;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// File Path interface to be implemented in each platform for dependency service
	/// </summary>
	public interface IFilePathService
	{
		string GetFilePath(string fileName);
	}

	/// <summary>
	/// viewmodel to help display WeeklyPage, with a list of days
	/// </summary>
	public class WeekViewModel : BaseViewModel
	{
		private string sampleFileName = "SampleDays.json";

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

		/// <summary>
		/// create a sample json file for testing
		/// </summary>
		private void CreateSampleFile()
		{
			// create the object to save to JSON
			List<Day> sampleDays = new List<Day>
			{
				new Day(DateTime.Today.AddDays(1)),
				new Day(DateTime.Today.AddDays(3)),
				new Day(DateTime.Today.AddDays(5)),
				new Day(DateTime.Today.AddDays(6))
			};
			
			sampleDays[0].Meals[Meal.MealType.Breakfast].AddDish("Blueberry Pancakes", Dish.DishCategory.Grains);

			sampleDays[1].Meals[Meal.MealType.Lunch].AddDish("Ribeye Steak", Dish.DishCategory.Proteins);

			sampleDays[2].Meals[Meal.MealType.Dinner].AddDish("Green Salad", Dish.DishCategory.Veggies);

			sampleDays[3].Meals[Meal.MealType.Breakfast].AddDish("Blueberry Pancakes", Dish.DishCategory.Grains);
			sampleDays[3].Meals[Meal.MealType.Lunch].AddDish("Ribeye Steak", Dish.DishCategory.Proteins);
			sampleDays[3].Meals[Meal.MealType.Dinner].AddDish("Green Salad", Dish.DishCategory.Veggies);

			WriteToJSON(sampleDays);
		}

		/// <summary>
		/// sort, and then write user's data to JSON file
		/// </summary>
		/// <param name="days"></param>
		private void WriteToJSON(List<Day> days)
		{
			// sort first
			days = days.OrderBy(day => day.ThisDate).ToList();
			
			// save to file
			string jsonString = JsonConvert.SerializeObject(days);
			File.WriteAllText(FilePath, jsonString);
		}

		/// <summary>
		/// read user's data from JSON file
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>List<Day> object read from user's data file</Day></returns>
		private List<Day> ReadFromJSON()
		{
			string jsonString = File.ReadAllText(FilePath);
			List<Day> result = JsonConvert.DeserializeObject<List<Day>>(jsonString);

			return result;
		}

		/// <summary>
		/// Fill the DisplayDays from user data read from file
		/// </summary>
		/// <param name="dataFromFile"></param>
		private void FillDisplayDays(List<Day> dataFromFile)
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

		public WeekViewModel()
		{
			Title = "Week View";
			DisplayDays = new ObservableCollection<Day>();

			// build file path
			IFilePathService service = DependencyService.Get<IFilePathService>();
			FilePath = service.GetFilePath(sampleFileName);
			//string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			//FilePath = Path.Combine(documentsPath, sampleFileName);

			// create a sample file
			CreateSampleFile();

			// read user's data from JSON file
			List<Day> dataFromFile = ReadFromJSON();

			// fill the week with days
			FillDisplayDays(dataFromFile);
		}
	}
}
