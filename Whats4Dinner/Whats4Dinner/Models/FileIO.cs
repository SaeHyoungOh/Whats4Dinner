﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Whats4Dinner.ViewModels.DataStructure;
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
			sampleDays[0].Meals[(int)Meal.MealType.Breakfast].AddDish("Blueberry Pancakes", Dish.DishCategory.Grains);
			sampleDays[1].Meals[(int)Meal.MealType.Lunch].AddDish("Ribeye Steak", Dish.DishCategory.Proteins);
			sampleDays[2].Meals[(int)Meal.MealType.Dinner].AddDish("Green Salad", Dish.DishCategory.Veggies);
			sampleDays[3].Meals[(int)Meal.MealType.Breakfast].AddDish("Blueberry Pancakes", Dish.DishCategory.Grains);
			sampleDays[3].Meals[(int)Meal.MealType.Lunch].AddDish("Ribeye Steak", Dish.DishCategory.Proteins);
			sampleDays[3].Meals[(int)Meal.MealType.Dinner].AddDish("Green Salad", Dish.DishCategory.Veggies);

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

			// convert DishGroup to DishGroupForJSON
			foreach (Day day in days)
			{
				foreach (Meal meal in day.Meals)
				{
					meal.DishesJSON.Clear();
					foreach (DishGroup dishGroup in meal.Dishes)
					{
						meal.DishesJSON.Add(new DishGroupForJSON(dishGroup.DishGroupCategory, dishGroup));
					}
				}
			}

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

			// convert DishGroupForJSON to DishGroup
			foreach (Day day in result)
			{
				foreach (Meal meal in day.Meals)
				{
					meal.Dishes.Clear();
					foreach (DishGroupForJSON dishGroupForJSON in meal.DishesJSON)
					{
						meal.Dishes.Add(new DishGroup(dishGroupForJSON.DishGroupCategory, dishGroupForJSON.DishList));
					}
				}
			}

			return result;
		}

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
