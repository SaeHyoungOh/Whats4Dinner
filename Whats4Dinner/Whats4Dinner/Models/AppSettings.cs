using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Whats4Dinner.Models
{
	public class AppSettings
	{
		public string StorageConnectionString { get; set; }

		public static AppSettings LoadAppSettings()
		{
			IConfigurationRoot configRoot = new ConfigurationBuilder()
				.AddJsonFile("Settings.json")
				.Build();
			AppSettings appSettings = configRoot.Get<AppSettings>();
			return appSettings;
		}
	}
}
