
namespace Whats4Dinner.ViewModels.DataStructure
{
	/// <summary>
	/// A dish with Id, Name, and Category
	/// </summary>
	public class Dish : BaseModel
	{
		/// <summary>
		/// List of categories a dish can have
		/// </summary>
		public enum DishCategory
		{
			Grains,
			Veggies,
			Proteins,
			Condiments,
			Drinks,
			Other
		}

		private string name;
		private DishCategory thisDishCategory;

		/// <summary>
		/// Name of the Dish
		/// </summary>
		public string Name
		{
			get => name;
			set
			{
				SetProperty(ref name, value);
			}
		}

		/// <summary>
		/// Category of the Dish, as listed in Dish.Categories
		/// </summary>
		public DishCategory ThisDishCategory
		{
			get => thisDishCategory;
			set
			{
				SetProperty(ref thisDishCategory, value);
			}
		}

		/// <summary>
		/// parameterless constructor for JSON deserialization
		/// </summary>
		public Dish() { }

		/// <summary>
		/// constructor for Dish class
		/// </summary>
		/// <param name="name">Name of the Dish</param>
		/// <param name="category">Category of the Dish, as listed in Dish.Categories</param>
		public Dish(string name, DishCategory category)
		{
			Name = name;
			ThisDishCategory = category;
		}
	}
}
