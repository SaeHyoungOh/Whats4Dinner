using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Whats4Dinner.ViewModels;

namespace UnitTestProject1
{
	[TestClass]
	public class DailyViewModelTests
	{
		[TestMethod]
		public void ConstructorTest()
		{
			// Arrange

			// Act
			WeekViewModel testDailyViewModel = new WeekViewModel();

			// Assert
			Assert.AreEqual(7, testDailyViewModel.DisplayDays.Count);
		}

		[TestMethod]
		public void DaysTest()
		{
			// Arrange
			string today = DateTime.Today.Date.ToString();

			// Act
			WeekViewModel testDailyViewModel = new WeekViewModel();

			// Assert
			Assert.AreEqual(today, testDailyViewModel.DisplayDays[0].DisplayDayOfWeek);
		}
	}
}
