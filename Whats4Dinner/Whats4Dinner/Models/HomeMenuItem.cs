
namespace Whats4Dinner.Models
{
	/// <summary>
	/// Enum for each menu item type
	/// </summary>
	public enum MenuItemType
	{
		SevenDayView,
		WeeklyView,
		MonthlyView,
		DishDB,
		DishCategories,
		About
	}

	/// <summary>
	/// Class to hold ID and title of the menu item
	/// </summary>
	public class HomeMenuItem
	{
		/// <summary>
		/// MenuItemType of the item, in enum
		/// </summary>
		public MenuItemType Id { get; set; }

		/// <summary>
		/// Title of the menu item, initialized in the constructor
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Constructor for HomeMenuItem; it initializes the properties.
		/// </summary>
		/// <param name="id"></param>
		public HomeMenuItem(MenuItemType id)
		{
			Id = id;
			switch (id)
			{
				case MenuItemType.SevenDayView:
					Title = "7-Day View";
					break;
				case MenuItemType.WeeklyView:
					Title = "Weekly View";
					break;
				case MenuItemType.MonthlyView:
					Title = "Monthy View";
					break;
				case MenuItemType.DishDB:
					Title = "Dish Database";
					break;
				case MenuItemType.DishCategories:
					Title = "Dish Categories";
					break;
				case MenuItemType.About:
					Title = "About";
					break;
			}
		}
	}
}
