using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCoursework.Models
{
	class PriceModel : INotifyPropertyChanged
	{
		private int _ruble;
		private int _kopek;

		public int Ruble {
			get {
				return _ruble;
			}
			set {
				_ruble = value;
				OnPropertyChanged("Ruble");
			}
		}

		public int Kopek {
			get {
				return _kopek;
			}
			set {
				_kopek = value;
				OnPropertyChanged("Kopek");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName) {
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
