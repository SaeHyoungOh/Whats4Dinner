using Prism.Commands;
using System.Collections.ObjectModel;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels.DataStructure;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace Whats4Dinner.ViewModels
{
	class MealViewModel : BaseViewModel
	{
		private ObservableCollection<DishGroup> displayDishLists;
		private Day selectedDay;
		private Meal selectedMeal;

		/// <summary>
		/// 
		/// </summary>
		public ObservableCollection<DishGroup> DisplayDishLists
		{
			get => displayDishLists;
			set
			{
				SetProperty(ref displayDishLists, value);
			}
		}
		public Day SelectedDay
		{
			get => selectedDay;
			set
			{
				SetProperty(ref selectedDay, value);
			}
		}
		public Meal SelectedMeal
		{
			get => selectedMeal;
			set
			{
				SetProperty(ref selectedMeal, value);
			}
		}

		/// <summary>
		/// delegate command for "delete" button
		/// </summary>
		public DelegateCommand AddClick { get; private set; }
		public DelegateCommand<Dish> DeleteClick { get; private set; }

		/// <summary>
		/// button click action for "delete" button
		/// </summary>
		/// <param name="content"></param>
		private void AddClickExecute()
		{
			SelectedMeal.AddDish("test add", DishCategory.Drinks);
			OnPropertyChanged("DisplayDishLists");
			UserDataIO.WriteToJSON(DisplayDays);
		}
		private void DeleteClickExecute(Dish content)
		{
			SelectedMeal.DeleteDish(content);
			OnPropertyChanged("DisplayDishLists");
			UserDataIO.WriteToJSON(DisplayDays);
		}

		/// <summary>
		/// returns whether the meal includes the passed dish
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		private bool DeleteClickCanExecute(Dish content)
		{
			foreach (DishGroup dishGroup in DisplayDishLists)
			{
				if (dishGroup.DishGroupCategory == content.ThisDishCategory)
				{
					if (dishGroup.Contains(content))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// constructor for MealViewModel
		/// </summary>
		/// <param name="selected">selected Meal from MealPage</param>
		/// <param name="previousTitle">Title from MealPage</param>
		public MealViewModel(ObservableCollection<Day> DisplayDays, Day SelectedDay, Meal SelectedMeal)
		{
			this.DisplayDays = DisplayDays;
			this.SelectedDay = SelectedDay;
			this.SelectedMeal = SelectedMeal;

			Title = SelectedMeal.ThisMealType.ToString() + ", " + SelectedDay.DisplayDayOfWeek + " " + SelectedDay.DisplayDate;
			UserDataIO = new FileIO(fileName);
			DisplayDishLists = SelectedMeal.Dishes;

			// assign delegate commands
			AddClick = new DelegateCommand(AddClickExecute);
			DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute);
			//DeleteClick = new DelegateCommand<Dish>(DeleteClickExecute, DeleteClickCanExecute);

			// for refreshing
			LoadItemsCommand = new DelegateCommand(ExecuteLoadItemsCommand);
		}
	}
}
