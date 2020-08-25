using IX.Observable;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Whats4Dinner.Models;
using Xamarin.Forms;
using static Whats4Dinner.Models.DataStructure.Dish;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// ViewModel class for DishEditPage
	/// </summary>
	class DishCategoriesViewModel : BaseViewModel
	{
		public string Entry { get; set; }

		/// <summary>
		/// DishCategories for View
		/// </summary>
		public ObservableDictionary<string, string> DisplayDishCategories
		{
			get => displayDishCategories;
			set
			{
				SetProperty(ref displayDishCategories, value);
			}
		}
		private ObservableDictionary<string, string> displayDishCategories;

		/// <summary>
		/// Command to edit the text of a DishCategory
		/// </summary>
		public DelegateCommand<KeyValuePair<string, string>?> EditCommand { get; set; }
		/// <summary>
		/// Command to move the DishCategory up a number
		/// </summary>
		public DelegateCommand<KeyValuePair<string, string>?> MoveUpCommand { get; set; }
		/// <summary>
		/// Command to move the DishCategory Down a number
		/// </summary>
		public DelegateCommand<KeyValuePair<string, string>?> MoveDownCommand { get; set; }

		public DelegateCommand LoadDishCategoriesCommand { get; set; }

		/// <summary>
		/// Change the Value of the Selected Category to the Entry set from View
		/// </summary>
		/// <param name="SelectedCategory"></param>
		private void EditExecute(KeyValuePair<string, string>? SelectedCategory)
		{
			DishCategories[SelectedCategory?.Key] = Entry;
			DisplayDishCategories[SelectedCategory?.Key] = Entry;
			DishCategoriesIO.WriteDishCategoriesToJSON(DishCategories);
			// TODO: update DishDB, UserDays, and DisplayDays to reflect changes
			MessagingCenter.Send(this, "DishCategories updated");
		}

		/// <summary>
		/// Switch the Value of the selected category with the one at the target position
		/// </summary>
		/// <param name="SelectedCategory"></param>
		private void MoveUpExecute(KeyValuePair<string, string>? SelectedCategory)
		{
			// get the target position to siwtch the Values with
			string targetPosition = "";
			switch(SelectedCategory?.Key)
			{
				case "2":
					targetPosition = "1";
					break;
				case "3":
					targetPosition = "2";
					break;
				case "4":
					targetPosition = "3";
					break;
				case "5":
					targetPosition = "4";
					break;
			}

			// switch the values and save to file (and sort)
			SwitchCategories(SelectedCategory, targetPosition);
		}

		/// <summary>
		/// Whether MoveUpCommand can execute: only if the category is not at position "1"
		/// </summary>
		/// <param name="SelectedCategory"></param>
		/// <returns></returns>
		private bool MoveUpCanExecute(KeyValuePair<string, string>? SelectedCategory)
		{
			if (SelectedCategory?.Key == "1")
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Switch the Value of the selected category with the one at the target position
		/// </summary>
		/// <param name="SelectedCategory"></param>
		private void MoveDownExecute(KeyValuePair<string, string>? SelectedCategory)
		{
			// get the target position to siwtch the Values with
			string targetPosition = "";
			switch (SelectedCategory?.Key)
			{
				case "1":
					targetPosition = "2";
					break;
				case "2":
					targetPosition = "3";
					break;
				case "3":
					targetPosition = "4";
					break;
				case "4":
					targetPosition = "5";
					break;
			}

			// switch the values and save to file (and sort)
			SwitchCategories(SelectedCategory, targetPosition);
		}

		/// <summary>
		/// Whether MoveUpCommand can execute: only if the category is not at position "5"
		/// </summary>
		/// <param name="SelectedCategory"></param>
		/// <returns></returns>
		private bool MoveDownCanExecute(KeyValuePair<string, string>? SelectedCategory)
		{
			if (SelectedCategory?.Key == "5")
			{
				return false;
			}
			return true;
		}

		private void SwitchCategories(KeyValuePair<string, string>? SelectedCategory, string targetPosition)
		{
			string temp = DishCategories[targetPosition];
			DishCategories[targetPosition] = SelectedCategory?.Value;           // in DB
			DisplayDishCategories[targetPosition] = SelectedCategory?.Value;    // in View
			DishCategories[SelectedCategory?.Key] = temp;                       // in DB
			DisplayDishCategories[SelectedCategory?.Key] = temp;                // in View
			DishCategoriesIO.WriteDishCategoriesToJSON(DishCategories);
			// TODO: update DishDB, UserDays, and DisplayDays to reflect changes
		}

		/// <summary>
		/// Refreshes the DishCategories, read from the JSON file.
		/// </summary>
		private void ExecuteLoadDishCategoriesCommand()
		{
			IsBusy = true;

			try
			{
				DishCategories.Clear();
				DisplayDishCategories.Clear();

				// read data from JSON file
				DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();

				// add to display in View
				foreach (KeyValuePair<string, string> pair in DishCategories)
				{
					DisplayDishCategories.Add(pair);
				}
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

		/// <summary>
		/// Constructor for DishCategoriesViewModel
		/// </summary>
		public DishCategoriesViewModel(Dictionary<string, object> UserData)
		{
			// initialize properties
			Title = "Dish Categories";
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			if (UserData.ContainsKey("DishCategories")) { DishCategories = (Dictionary<string, string>)UserData["DishCategories"]; }
			DisplayDishCategories = new ObservableDictionary<string, string>();
			foreach (KeyValuePair<string, string> pair in DishCategories)
			{
				DisplayDishCategories.Add(pair);
			}

			// initialize commands
			EditCommand = new DelegateCommand<KeyValuePair<string, string>?>(EditExecute);
			MoveUpCommand = new DelegateCommand<KeyValuePair<string, string>?>(MoveUpExecute, MoveUpCanExecute);
			MoveDownCommand = new DelegateCommand<KeyValuePair<string, string>?>(MoveDownExecute, MoveDownCanExecute);
			LoadDishCategoriesCommand = new DelegateCommand(ExecuteLoadDishCategoriesCommand);
		}
	}
}
