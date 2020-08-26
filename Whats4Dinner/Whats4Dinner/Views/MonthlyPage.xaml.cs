using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonthlyPage : ContentPage
	{
		public MonthlyPage(Dictionary<string, object> UserData)
		{
			InitializeComponent();
			BindingContext = new MonthlyViewModel(UserData);
		}
	}
}