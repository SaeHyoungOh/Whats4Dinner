using System;
using System.Collections.Generic;
using System.Text;
using Whats4Dinner.Models;
using static Whats4Dinner.Models.DataStructure.Dish;

namespace Whats4Dinner.ViewModels
{
	/// <summary>
	/// ViewModel class for DishEditPage
	/// </summary>
	class DishCategoriesViewModel : BaseViewModel
	{
		/// <summary>
		/// Constructor for DishCategoriesViewModel
		/// </summary>
		public DishCategoriesViewModel()
		{
			// initialize properties
			DishCategoriesIO = new FileIO(dishCategoriesFileName);
			DishCategories = DishCategoriesIO.ReadDishCategoriesFromJSON();
		}
	}
}
