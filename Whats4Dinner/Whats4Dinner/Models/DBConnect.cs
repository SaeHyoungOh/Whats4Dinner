using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whats4Dinner.Models
{
	public class DBConnect
	{
		private string ConnectionString { get; set; } = "DefaultEndpointsProtocol=https;AccountName=76116ea8-0ee0-4-231-b9ee;AccountKey=W5kCTCR67MqOxjzilXBPlSQ9dyOgvh2kxCabeY5v7tGY7ch0wPL3g1oeLzI2jBecMDEpPxBxmJGubKvpapkvqQ==;TableEndpoint=https://76116ea8-0ee0-4-231-b9ee.table.cosmos.azure.com:443/;";

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
	}
}
