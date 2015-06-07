using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AlphaCoursework.Model;
using AlphaCoursework.Models;
using AlphaCoursework.Command;

namespace AlphaCoursework.ViewModel
{
	class GoodsViewModel : INotifyPropertyChanged
	{
		private static readonly StreamWriter _Writer = new StreamWriter("Data.log", true);
		
		/// <summary>
		/// Create three models
		/// </summary>
		public PriceModel Price { get; private set; }
		public ObservableCollection<FirmModel> Firms { get; private set; }
		public ObservableCollection<GoodModel> Goods { get; private set; }
		public FirmModel firm { get; private set; }
		public GoodModel good { get; private set; }

		
		//public PriceModel Price { get; private set; }

		/// <summary>
		/// Create defaul ctor that initializes firms, products and price.
		/// </summary>
		public GoodsViewModel() {
			Firms = new ObservableCollection<FirmModel>
			{
				new FirmModel {Name = "ООО Молоко"},
				new FirmModel {Name = "Залесский фермер"},
				new FirmModel {Name = "ООО Парус"},
				new FirmModel {Name = "ЧП Дмитров"},
				new FirmModel {Name = "ИП Александрия"},
				new FirmModel {Name = "ОАО Конкордия"}
			};

			Goods = new ObservableCollection<GoodModel>
			{
				new GoodModel {Name = "Морковь"},
				new GoodModel {Name = "Молоко"},
				new GoodModel {Name = "Баклажаны"},
				new GoodModel {Name = "Яблоки"},
				new GoodModel {Name = "Персики"},
				new GoodModel {Name = "Апельсины"}
			};

			Price = new PriceModel();
			Price.Ruble = 0;
			Price.Kopek = 0;
		}

		private ICommand _writeDataToFileCommand;

		public ICommand WriteDataToFileCommand {
			get
			{
				if (_writeDataToFileCommand == null) {
					_writeDataToFileCommand = new GoodsCommand(this.WriteDataToFile);
				}
				return _writeDataToFileCommand;
			}
		}

		private void  WriteDataToFile() {
			double price = Price.Ruble + (double)Price.Kopek / 100.0;
			_Writer.WriteLine("{0}", DateTime.Now.ToString("HH:mm:ss"));
			_Writer.WriteLine("{0}", firm.Name);
			_Writer.WriteLine("{0}", good.Name);
			_Writer.WriteLine("{0}", price);
			_Writer.WriteLine();
			PlaySound();
		}

		private void PlaySound() {
			SoundPlayer player = new SoundPlayer("C:\\Users\\Василий\\Desktop\\All Projects\\ForCursach\\AlphaCoursework\\bin\\Debug\\1.wav");
			System.Media.SystemSounds.Question.Play();
			//player.Play();
			//player.Stream.Position = 0;
		}

		/// <summary>
		/// Command depends on button "Выйти" 
		/// </summary>
		private ICommand _closeWindowCommand;

		public ICommand CloseWindowCommand {
			get
			{
				if (_closeWindowCommand == null) {
					_closeWindowCommand = new GoodsCommand(this.CloseWindow);
				}
				return _closeWindowCommand;
			}
		}
		/// <summary>
		/// Close application
		/// </summary>
		private void CloseWindow() {
			_Writer.Close();
			App.Current.MainWindow.Close();
		}

		
		/// <summary>
		/// INotify property changed signals that model has been changed
		/// </summary>
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
