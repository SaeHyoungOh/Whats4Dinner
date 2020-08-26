using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Whats4Dinner.Models;

namespace Whats4Dinner.ViewModels
{
	public class MonthlyViewModel : BaseViewModel
	{
		public MonthlyViewModel(Dictionary<string, object> UserData)
		{
			this.UserData = UserData;
			Title = "Monthly View";
			PageType = MenuItemType.MonthlyView;
		}
	}
}
