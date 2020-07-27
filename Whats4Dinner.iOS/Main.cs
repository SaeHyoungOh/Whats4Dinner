using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using UIKit;
using Whats4Dinner.ViewModels;

using Xamarin.Forms;

[assembly: Dependency(typeof(Whats4Dinner.iOS.FilePathService))]
namespace Whats4Dinner.iOS
{
	public class FilePathService : IFilePathService
	{
		public string GetFilePath(string fileName)
		{
			string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", fileName);

			return fullPath;
		}
	}

	public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
