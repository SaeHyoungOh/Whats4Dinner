using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whats4Dinner.Models.DataStructure;
using static Whats4Dinner.Models.DataStructure.Dish;

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
			DishCategory testCategory = DishCategory.Condiment;

			// Act
			//Dish testDish = new Dish(testName, testCategory);

			// Assert
			//Assert.AreEqual(testName, testDish.Name);
			//Assert.AreEqual(testCategory, testDish.ThisDishCategory);
		}
	}
}
