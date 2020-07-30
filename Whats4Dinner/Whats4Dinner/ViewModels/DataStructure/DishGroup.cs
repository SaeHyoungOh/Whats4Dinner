using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace Whats4Dinner.ViewModels.DataStructure
{
	public class DishGroup : ObservableCollection<Dish>, INotifyPropertyChanged
	{
		public DishCategory DishGroupCategory { get; set; }

		public string DisplayDishCategory
		{
			get
			{
				return DishGroupCategory.ToString();
			}
		}

		public DishGroup() { }

		public DishGroup(DishCategory cat)
		{
			DishGroupCategory = cat;
		}
		public DishGroup(DishCategory cat, ObservableCollection<Dish> dishes)
		{
			DishGroupCategory = cat;
			Clear();
			foreach (Dish dish in dishes)
			{
				Add(dish);
			}
		}
	}

	public class DishGroupForJSON
	{
		public DishCategory DishGroupCategory { get; set; }

		public ObservableCollection<Dish> DishList { get; set; }

		public DishGroupForJSON() { }

		public DishGroupForJSON(DishCategory cat, ObservableCollection<Dish> dishes)
		{
			DishGroupCategory = cat;
			DishList = dishes;
		}
	}

}
