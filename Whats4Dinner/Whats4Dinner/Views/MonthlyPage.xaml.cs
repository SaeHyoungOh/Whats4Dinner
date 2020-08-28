using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whats4Dinner.Models.DataStructure;
using Whats4Dinner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Whats4Dinner.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MonthlyPage : ContentPage
	{
		private Dictionary<string, object> UserData { get; set; }

		private MonthlyViewModel ViewModel { get; set; }

		public MonthlyPage(Dictionary<string, object> UserData)
		{
			this.UserData = UserData;

			InitializeComponent();
			BindingContext = ViewModel = new MonthlyViewModel(UserData);
			BuildCalendar();
		}

		/// <summary>
		/// build the 5 x 7 calendar in code-behind so that loops can be used
		/// </summary>
		private void BuildCalendar()
		{
			var DisplayDaysMonthly = (ObservableCollection<ObservableCollection<Day>>)UserData["DisplayDaysMonthly"];
			var DayStringList = (ObservableCollection<ObservableCollection<DayString>>)UserData["DayStringList"];

			// add the day of the week to the top row
			MonthlyView.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

			string[] days = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
			for (int i = 0; i < 7; i++)
			{
				MonthlyView.Children.Add(new BoxView(), i, 0);
				MonthlyView.Children.Add(new Label
				{
					Text = days[i],
					Style = (Style)Resources["ListItemTextStyle"]
				}, i, 0);
			}

			// add each day
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					string objName = "Day" + i.ToString() + j.ToString();

					// boxview for borders
					var borderBoxView = new BoxView();
					MonthlyView.Children.Add(borderBoxView, j, i + 1);

					// set up the tap gesture recognizer with binding
					var Tap = new TapGestureRecognizer();
					Tap.SetBinding(TapGestureRecognizer.CommandParameterProperty, objName + ".ThisDay");
					Tap.Tapped += (sender, e) => Day_Tapped(sender, e);

					// set up children grids
					Grid dayGrid = new Grid { GestureRecognizers = { Tap } };			// day grid with gesture recognizer
					dayGrid.SetBinding(Grid.BackgroundColorProperty, new Binding(objName + ".IsToday", converter: new TodayConverter()));
					Grid mealGrid = new Grid { Margin = new Thickness(0, 0, 0, 0) };	// meal grid inside day grid
					mealGrid.SetBinding(Grid.BackgroundColorProperty, new Binding(objName + ".IsToday", converter: new TodayConverter()));

					// add date Label with binding to day grid
					dayGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
					Label dateLabel = new Label { Style = (Style)Resources["ListItemDetailTextStyle"] };
					dateLabel.SetBinding(Label.TextProperty, objName + ".Date");
					dateLabel.SetBinding(Label.TextColorProperty, new Binding(objName + ".IsCurrentMonth", converter: new CurrentMonthConverter()));
					dayGrid.Children.Add(dateLabel);

					dayGrid.Children.Add(mealGrid, 0, 1);								// add the meal grid to the day grid

					// each meal indicator in meal grid
					Label breakfastLabel = new Label { Text = "B", Style = (Style)Resources["ListItemDetailTextStyle"] };   // breakfast
					breakfastLabel.SetBinding(Label.IsVisibleProperty, objName + ".Breakfast");
					breakfastLabel.SetBinding(Label.TextColorProperty, new Binding(objName + ".IsCurrentMonth", converter: new CurrentMonthConverter()));
					mealGrid.Children.Add(breakfastLabel, 0, 0);

					Label lunchLabel = new Label { Text = "L", Style = (Style)Resources["ListItemDetailTextStyle"] };       // lunch
					lunchLabel.SetBinding(Label.IsVisibleProperty, objName + ".Lunch");
					lunchLabel.SetBinding(Label.TextColorProperty, new Binding(objName + ".IsCurrentMonth", converter: new CurrentMonthConverter()));
					mealGrid.Children.Add(lunchLabel, 1, 0);

					Label dinnerLabel = new Label { Text = "D", Style = (Style)Resources["ListItemDetailTextStyle"] };      // dinner
					dinnerLabel.SetBinding(Label.IsVisibleProperty, objName + ".Dinner");
					dinnerLabel.SetBinding(Label.TextColorProperty, new Binding(objName + ".IsCurrentMonth", converter: new CurrentMonthConverter()));
					mealGrid.Children.Add(dinnerLabel, 0, 1);

					Label otherLabel = new Label { Text = "O", Style = (Style)Resources["ListItemDetailTextStyle"] };       // other
					otherLabel.SetBinding(Label.IsVisibleProperty, objName + ".Other");
					otherLabel.SetBinding(Label.TextColorProperty, new Binding(objName + ".IsCurrentMonth", converter: new CurrentMonthConverter()));
					mealGrid.Children.Add(otherLabel, 1, 1);

					MonthlyView.Children.Add(dayGrid, j, i + 1);						// add the day grid to calendar
				}
			}
		}

		private async void Day_Tapped(object sender, EventArgs e)
		{
			// add the selected day to UserData
			UserData["SelectedDay"] = (Day)((TapGestureRecognizer)sender).CommandParameter;

			// Navigate to the DayPage
			await Navigation.PushAsync(new DayPage(UserData));
		}
	}
}