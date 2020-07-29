using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Whats4Dinner.Models;

namespace UnitTestProject1
{
	[TestClass]
	public class DayTests
	{
		[TestMethod]
		public void ConstructorTest()
		{
			// Arrange
			DateTime testDate = DateTime.Today;

			// Act
			Day testDay = new Day(testDate);

			// Assert
			Assert.AreEqual(testDate, testDay.ThisDate);
		}
	}
}
