using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Whats4Dinner.Models
{
	public class BaseModel : INotifyPropertyChanged
	{
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

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// helper method to update View using PropertyChanged
		/// </summary>
		/// <param name="propertyName"></param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
