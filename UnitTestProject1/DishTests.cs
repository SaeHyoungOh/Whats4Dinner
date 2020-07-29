using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whats4Dinner.ViewModels.DataStructure;
using static Whats4Dinner.ViewModels.DataStructure.Dish;

namespace UnitTestProject1
{
	[TestClass]
	public class DishTests
	{
		[TestMethod]
		public void ConstructorTest()
		{
			// Arrange
			string testName = "ketchup";
			DishCategory testCategory = DishCategory.Condiments;

			// Act
			Dish testDish = new Dish(testName, testCategory);

			// Assert
			Assert.AreEqual(testName, testDish.Name);
			Assert.AreEqual(testCategory, testDish.ThisDishCategory);
		}
	}
}
