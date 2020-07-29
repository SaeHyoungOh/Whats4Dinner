using System.Collections.Generic;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace Whats4Dinner.ViewModels.DataStructure
{
	public class DishGroup : List<Dish>
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
		public DishGroup(DishCategory cat, List<Dish> dishes)
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

		public List<Dish> DishList { get; set; }

		public DishGroupForJSON() { }

		public DishGroupForJSON(DishCategory cat, List<Dish> dishes)
		{
			DishGroupCategory = cat;
			DishList = dishes;
		}
	}

}
