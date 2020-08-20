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
			MenuItems = new ObservableCollection<HomeMenuItem>();
			foreach (MenuItemType menuItemType in Enum.GetValues(typeof(MenuItemType)))
			{
				MenuItems.Add(new HomeMenuItem(menuItemType));
			}
		}
	}
}
