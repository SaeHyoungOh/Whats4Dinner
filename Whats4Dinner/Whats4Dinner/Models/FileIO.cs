using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Whats4Dinner.ViewModels.DataStructure;
using Xamarin.Forms;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace Whats4Dinner.Models
{
	/// <summary>
	/// File Path interface to be implemented in each platform for dependency service
	/// </summary>
	public interface IFilePathService
	{
		string GetFilePath(string fileName);
	}

	/// <summary>
	/// Class to handle file I/O of the user data
	/// </summary>
	public class FileIO
	{
		/// <summary>
		/// Absolute file path used for file I/O
		/// </summary>
		private string FilePath { get; set; }

		/// <summary>
		/// JSON serializer options to enable usage of Dictionary data structure
		/// </summary>
		private JsonSerializerOptions serializeOptions = new JsonSerializerOptions();

		/// <summary>
		/// Create a sample json file for testing
		/// </summary>
		public void CreateSampleFile()
		{
			// create the object to save to JSON
			ObservableCollection<Day> sampleDays = new ObservableCollection<Day>
			{
				new Day(DateTime.Today.AddDays(1)),
				new Day(DateTime.Today.AddDays(3)),
				new Day(DateTime.Today.AddDays(5)),
				new Day(DateTime.Today.AddDays(6))
			};

			// list of sample data
			sampleDays[0].Meals[(int)Meal.MealType.Breakfast].AddDish("Blueberry Pancakes", new List<DishCategory> { DishCategory.Grain});
			sampleDays[1].Meals[(int)Meal.MealType.Lunch].AddDish("Ribeye Steak", new List<DishCategory> { DishCategory.Protein });
			sampleDays[2].Meals[(int)Meal.MealType.Dinner].AddDish("Green Salad", new List<DishCategory> { DishCategory.Veggie });
			sampleDays[3].Meals[(int)Meal.MealType.Breakfast].AddDish("Blueberry Pancakes", new List<DishCategory> { DishCategory.Grain });
			sampleDays[3].Meals[(int)Meal.MealType.Lunch].AddDish("Ribeye Steak", new List<DishCategory> { DishCategory.Protein });
			sampleDays[3].Meals[(int)Meal.MealType.Dinner].AddDish("Green Salad", new List<DishCategory> { DishCategory.Veggie });

			WriteToJSON(sampleDays);
		}

		/// <summary>
		/// Sort, convert DishGroup to DishGroupForJSON, and then write user's data to JSON file
		/// </summary>
		/// <param name="days"></param>
		public void WriteToJSON(ObservableCollection<Day> DisplayDays)
		{
			// convert ObservalbeCollection to List so it can sort
			List<Day> days = new List<Day>();
			foreach (Day day in DisplayDays)
			{
				days.Add(day);
			}

			// sort the list by day
			days = days.OrderBy(day => day.ThisDate).ToList();

			// save to file
			string jsonString = JsonSerializer.Serialize(days, serializeOptions);
			File.WriteAllText(FilePath, jsonString);
		}

		/// <summary>
		/// Read user's data from JSON file, covnert DishGroupForJSON to DishGroup, and return it
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>List<Day> object read from user's data file</Day></returns>
		public List<Day> ReadFromJSON()
		{
			string jsonString = File.ReadAllText(FilePath);
			List<Day> result = JsonSerializer.Deserialize<List<Day>>(jsonString, serializeOptions);

			return result;
		}

		public FileIO() { }

		/// <summary>
		/// Constructor for FilIO class; calculate and initialize FilePath from provided file name
		/// </summary>
		/// <param name="fileName"></param>
		public FileIO(string fileName)
		{
			// build file path
			IFilePathService service = DependencyService.Get<IFilePathService>();
			FilePath = service.GetFilePath(fileName);

			// add serializer options to be able to use Dictionary data structure in JSON
			serializeOptions.Converters.Add(new DictionaryTKeyEnumTValueConverter());
		}
	}
}
