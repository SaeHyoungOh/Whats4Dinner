using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Whats4Dinner.Models;
using Whats4Dinner.Services;

namespace Whats4Dinner.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		protected static string FilePath { get; set; }

		string title = string.Empty;
		/// <summary>
		/// title of the page
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		/// <summary>
		/// a replacement for the default "set" method for class property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="backingStore">the property in question</param>
		/// <param name="value">new value for the property</param>
		/// <param name="propertyName">property name in view</param>
		/// <param name="onChanged"></param>
		/// <returns>whether the property has changed</returns>
		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// helper method to update View using PropertyChanged
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
