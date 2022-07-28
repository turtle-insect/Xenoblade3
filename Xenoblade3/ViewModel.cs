using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public General General { get; private set; } = new General();
		public ObservableCollection<Character> Characters { get; private set; } = new ObservableCollection<Character>();
		public ObservableCollection<Item> Collections { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Accessories { get; private set; } = new ObservableCollection<Item>();
		public CommandAction FileOpenCommand { get; private set; }
		public CommandAction FileSaveCommand { get; private set; }

		public ViewModel()
		{
			FileOpenCommand = new CommandAction(FileOpen);
			FileSaveCommand = new CommandAction(FileSave);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void Init()
		{
			Characters.Clear();
			Collections.Clear();
			Accessories.Clear();

			for (uint i = 0; i < 8; i++)
			{
				Characters.Add(new Character(0xE3A0 + 8888 * i));
			}

			for (uint i = 0; i < 2300; i++)
			{
				var item = new Item(0x55060 + 16 * i);
				if (item.ID == 0) continue;

				Collections.Add(item);
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(General)));
		}

		private void FileOpen(Object? obj)
		{
			var dlg = new Microsoft.Win32.OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Open(dlg.FileName);
			Init();
		}

		private void FileSave(Object? obj)
		{
			SaveData.Instance().Save();
		}
	}
}
