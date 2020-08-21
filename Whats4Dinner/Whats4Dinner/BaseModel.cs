using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Whats4Dinner.Models;

namespace Whats4Dinner
{
	/// <summary>
	/// The base class for all ViewModels which use INotifyPropertyChanged for properties to notify the Views
	/// </summary>
	public class BaseModel : INotifyPropertyChanged
	{
		/// <summary>
		/// List of categories a dish can have
		/// </summary>
		public Dictionary<string, string> DishCategories
		{
			get => dishCategories;
			set
			{
				SetProperty(ref dishCategories, value);
			}
		}
		protected Dictionary<string, string> dishCategories;

		/// <summary>
		/// The file name for storing the dish categories
		/// </summary>
		protected readonly string dishCategoriesFileName = "DishCategories.json";

		/// <summary>
		/// FileIO object to handle dish database I/O
		/// </summary>
		protected FileIO DishCategoriesIO { get; set; }

		/// <summary>
		/// A replacement for the default "set" method for class property
		/// reference: Microsoft Documents
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

		/// <summary>
		/// Required in INotifyPropertyChanged
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Helper method to update View using PropertyChanged
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
