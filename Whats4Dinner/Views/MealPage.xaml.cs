using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.Models;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MealPage : ContentPage
	{
		public MealPage(Meal selected, string previousTitle)
		{
			InitializeComponent();
			BindingContext = new MealViewModel(selected, previousTitle);
		}
	}
}