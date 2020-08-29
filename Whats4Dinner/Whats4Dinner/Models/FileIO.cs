using IX.Observable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Whats4Dinner.Models.DataStructure;
using Xamarin.Forms;

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
		/// Sort and write user's data to JSON file
		/// </summary>
		/// <param name="days"></param>
		public void WriteUserDaysToJSON(ObservableCollection<Day> UserDays)
		{
			// convert ObservalbeCollection to List so it can sort
			List<Day> days = new List<Day>();
			foreach (Day day in UserDays)
			{
				// only save the days with meals
				if (day.HasMeals)
				{
					days.Add(day);
				}
			}

			// sort the list by day
			days = days.OrderBy(day => day.ThisDate).ToList();

			// save to file
			string jsonString = JsonSerializer.Serialize(days, serializeOptions);
			File.WriteAllText(FilePath, jsonString);
		}

		/// <summary>
		/// Sort and write Dish Database to JSON file
		/// </summary>
		/// <param name="Dishes"></param>
		public void WriteDishesToJSON(ObservableCollection<Dish> Dishes)
		{
			// convert ObservalbeCollection to List so it can sort
			List<Dish> dishes = new List<Dish>();
			foreach (Dish day in Dishes)
			{
				dishes.Add(day);
			}

			// sort the list by name
			dishes = dishes.OrderBy(dish => dish.Name).ToList();

			// save to file
			string jsonString = JsonSerializer.Serialize(dishes, serializeOptions);
			File.WriteAllText(FilePath, jsonString);
		}

		/// <summary>
		/// Sort and write Dish Categories to JSON file
		/// </summary>
		/// <param name="DishCategories"></param>
		public void WriteDishCategoriesToJSON(Dictionary<string, string> DishCategories)
		{
			// sort the dictionary by Key
			// reference: https://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value
			DishCategories = DishCategories.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);

			// save to file
			string jsonString = JsonSerializer.Serialize(DishCategories, serializeOptions);
			File.WriteAllText(FilePath, jsonString);
		}

		/// <summary>
		/// Read user's data from JSON file and return it
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>List<Day> object read from user's data file</Day></returns>
		public ObservableCollection<Day> ReadUserDaysFromJSON()
		{
			ObservableCollection<Day> result;

			//File.Delete(FilePath);
			if (File.Exists(FilePath))
			{
				string jsonString = File.ReadAllText(FilePath);
				result = JsonSerializer.Deserialize<ObservableCollection<Day>>(jsonString, serializeOptions);
			}
			// return empty list if file does not exist
			else
			{
				result = new ObservableCollection<Day>();
			}

			return result;
		}

		/// <summary>
		/// Read Dish Database from JSON file and return it
		/// </summary>
		/// <returns></returns>
		public ObservableCollection<Dish> ReadDishesFromJSON()
		{
			ObservableCollection<Dish> result;

			if (File.Exists(FilePath))
			{
				string jsonString = File.ReadAllText(FilePath);
				result = JsonSerializer.Deserialize<ObservableCollection<Dish>>(jsonString, serializeOptions);
			}
			// return empty list if file does not exist
			else
			{
				result = new ObservableCollection<Dish>();
			}

			return result;
		}

		/// <summary>
		/// Read Dish Categories from JSON file and return it
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, string> ReadDishCategoriesFromJSON()
		{
			Dictionary<string, string> result;

			if (File.Exists(FilePath))
			{
				string jsonString = File.ReadAllText(FilePath);
				result = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString, serializeOptions);
			}
			// return a pre-made list if file does not exist
			else
			{
				result = new Dictionary<string, string>
				{
					{ "1", "Grain" },
					{ "2", "Prote" },
					{ "3", "Veggi" },
					{ "4", "Drink" },
					{ "5", "Other" }
				};
			}

			return result;
		}

		/// <summary>
		/// Parameterless constructor for JSON deserializer
		/// </summary>
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
