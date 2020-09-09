using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whats4Dinner.Models
{
	public class DBConnect
	{
		private string ConnectionString { get; set; } = AppSettings.LoadAppSettings().StorageConnectionString;

		private CloudTable Table { get; set; }

		public void ConnectToTable()
		{
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
			Table = tableClient.GetTableReference("UserData");
		}

		public async Task<UserDataEntity> InsertOrMergeEntityAsync(UserDataEntity entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}

			try
			{
				// Create the InsertOrReplace table operation
				TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

				// Execute the operation.
				TableResult result = await Table.ExecuteAsync(insertOrMergeOperation);
				UserDataEntity insertedCustomer = result.Result as UserDataEntity;

				if (result.RequestCharge.HasValue)
				{
					Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
				}

				return insertedCustomer;
			}
			catch (StorageException e)
			{
				Console.WriteLine(e.Message);
				Console.ReadLine();
				throw;
			}
		}

		public DBConnect()
		{
			
		}
	}
}
