using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whats4Dinner.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Xamarin.Forms;

[assembly: Dependency(typeof(Whats4Dinner.UWP.FilePathService))]
namespace Whats4Dinner.UWP
{
	public class FilePathService : IFilePathService
	{
		public string GetFilePath(string fileName)
		{
			string fullPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, fileName);

			return fullPath;
		}
	}

	public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Whats4Dinner.App());
        }
    }
}
