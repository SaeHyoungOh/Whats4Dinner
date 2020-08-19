
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
		DishCategories
	}

	/// <summary>
	/// Class to hold ID and title of the menu item
	/// </summary>
	public class HomeMenuItem
	{
		public MenuItemType Id { get; set; }

		public string Title { get; set; }
	}
}
