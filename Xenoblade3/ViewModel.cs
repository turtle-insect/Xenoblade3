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
		public Info Info { get; set; } = Info.Instance();
		public General General { get; private set; } = new General();
		public ObservableCollection<Character> Characters { get; private set; } = new ObservableCollection<Character>();
		public ObservableCollection<Item> Collectibles { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Accessories { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Gems { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> KeyItems { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> PinnedItems { get; private set; } = new ObservableCollection<Item>();
		public CommandAction FileOpenCommand { get; private set; }
		public CommandAction FileSaveCommand { get; private set; }
		public CommandAction ChoiceAccessoriesCommand { get; private set; }
		public CommandAction ChoiceCollectiblesCommand { get; private set; }
		public CommandAction ChoiceGemsCommand { get; private set; }
		public CommandAction ChoiceKeyItemsCommand { get; private set; }
		public CommandAction ChoicePinnedItemsCommand { get; private set; }

		public ViewModel()
		{
			FileOpenCommand = new CommandAction(FileOpen);
			FileSaveCommand = new CommandAction(FileSave);
			ChoiceAccessoriesCommand = new CommandAction(ChoiceAccessories);
			ChoiceCollectiblesCommand = new CommandAction(ChoiceCollectibles);
			ChoiceGemsCommand = new CommandAction(ChoiceGems);
			ChoiceKeyItemsCommand = new CommandAction(ChoiceKeyItems);
			ChoicePinnedItemsCommand = new CommandAction(ChoicePinnedItems);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void Init()
		{
			Characters.Clear();
			Collectibles.Clear();
			Accessories.Clear();
			Gems.Clear();
			KeyItems.Clear();
			PinnedItems.Clear();

			for (uint i = 0; i < 8; i++)
			{
				Characters.Add(new Character(0xE3A0 + 8888 * i));
			}

			for (uint i = 0; i < 2300; i++)
			{
				var item = new Item(0x55060 + 16 * i);
				if (item.ID == 0) continue;

				Collectibles.Add(item);
			}

			for (uint i = 0; i < 1500; i++)
			{
				var item = new Item(0x5E020 + 16 * i);
				if (item.ID == 0) continue;

				Accessories.Add(item);
			}

			for (uint i = 0; i < 300; i++)
			{
				var item = new Item(0x53DA0 + 16 * i);
				if (item.ID == 0) continue;

				Gems.Add(item);
			}

			for (uint i = 0; i < 280; i++)
			{
				var item = new Item(0x63DE0 + 16 * i);
				if (item.ID == 0) continue;

				KeyItems.Add(item);
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

		private void ChoiceAccessories(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eAccessories);
		}

		private void ChoiceCollectibles(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eCollectibles);
		}

		private void ChoiceGems(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eGems);
		}

		private void ChoiceKeyItems(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eKeyItems);
		}

		private void ChoicePinnedItems(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.ePinnedItems);
		}

		private void ChoiceItem(Object? obj, ChoiceWindow.ItemType type)
		{
			if (obj == null) return;
			var item = obj as Item;
			if (item == null) return;

			var dlg = new ChoiceWindow();
			dlg.Type = type;
			dlg.ID = item.ID;
			dlg.ShowDialog();
			item.ID = dlg.ID;
		}
	}
}
