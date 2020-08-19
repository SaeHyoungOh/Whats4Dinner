using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Whats4Dinner.Models;

namespace Whats4Dinner.ViewModels
{
	class MenuViewModel : BaseViewModel
	{
		public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

		public MenuViewModel()
		{
			MenuItems = new ObservableCollection<HomeMenuItem>
			{
				new HomeMenuItem {Id = MenuItemType.SevenDayView, Title="7-Day View" },
				new HomeMenuItem {Id = MenuItemType.WeeklyView, Title="Weekly View" },
				new HomeMenuItem {Id = MenuItemType.MonthlyView, Title="Monthy View" },
				new HomeMenuItem {Id = MenuItemType.DishDB, Title="Dish Database" },
				new HomeMenuItem {Id = MenuItemType.DishCategories, Title="Dish Categories" }
			};
		}
	}
}
