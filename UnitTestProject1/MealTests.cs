using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whats4Dinner.ViewModels.DataStructure;
using static Whats4Dinner.ViewModels.DataStructure.Meal;

namespace UnitTestProject1
{
	[TestClass]
	public class MealTests
	{
		[TestMethod]
		public void ConstructorTest()
		{
			// Arrange
			MealType testMealType = MealType.Breakfast;

			// Act
			Meal testMeal = new Meal(testMealType);

			// Assert
			Assert.AreEqual(testMealType, testMeal.ThisMealType);
		}
	}
}
