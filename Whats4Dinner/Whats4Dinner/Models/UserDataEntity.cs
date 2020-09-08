using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Whats4Dinner.Models
{
	public class UserDataEntity : TableEntity
	{
		public UserDataEntity()
		{
		}

		public UserDataEntity(string userID, string userGroup = null)
		{
			PartitionKey = userGroup;
			RowKey = userID;
		}

		public string UserDays { get; set; }

		public string Dishes { get; set; }

		public string DishCategories { get; set; }
	}
}
