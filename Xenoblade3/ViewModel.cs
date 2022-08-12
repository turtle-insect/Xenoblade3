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
		public CommandAction AppendAccessoriesCommand { get; private set; }
		public CommandAction ChoiceCollectiblesCommand { get; private set; }
		public CommandAction ChoiceGemsCommand { get; private set; }
		public CommandAction ChoiceKeyItemsCommand { get; private set; }
		public CommandAction ChoicePinnedItemsCommand { get; private set; }
		public CommandAction AllAdd100CollectiblesCommand { get; private set; }

		private Dictionary<ChoiceWindow.ItemType, ItemInfo> mItemInfo = new Dictionary<ChoiceWindow.ItemType, ItemInfo>();

		public ViewModel()
		{
			FileOpenCommand = new CommandAction(FileOpen);
			FileSaveCommand = new CommandAction(FileSave);
			ChoiceAccessoriesCommand = new CommandAction(ChoiceAccessories);
			AppendAccessoriesCommand = new CommandAction(AppendAccessories);
			ChoiceCollectiblesCommand = new CommandAction(ChoiceCollectibles);
			ChoiceGemsCommand = new CommandAction(ChoiceGems);
			ChoiceKeyItemsCommand = new CommandAction(ChoiceKeyItems);
			ChoicePinnedItemsCommand = new CommandAction(ChoicePinnedItems);
			AllAdd100CollectiblesCommand = new CommandAction(Add100Collectibles);

			mItemInfo.Add(ChoiceWindow.ItemType.eGems, new ItemInfo() { Items = Gems, BaseAddress = 0x53DA0, MaxCount = 300 });
			mItemInfo.Add(ChoiceWindow.ItemType.eCollectibles, new ItemInfo() { Items = Collectibles, BaseAddress = 0x55060, MaxCount = 2300 });
			mItemInfo.Add(ChoiceWindow.ItemType.eAccessories, new ItemInfo() { Items = Accessories, BaseAddress = 0x5E020, MaxCount = 1500 });
			mItemInfo.Add(ChoiceWindow.ItemType.eKeyItems, new ItemInfo() { Items = KeyItems, BaseAddress = 0x63DE0, MaxCount = 280 });
			mItemInfo.Add(ChoiceWindow.ItemType.ePinnedItems, new ItemInfo() { Items = PinnedItems, BaseAddress = 0x55060, MaxCount = 0 });
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

			for (uint i = 0; i < 30; i++)
			{
				Characters.Add(new Character(0xE3A0 + 4444 * i));
			}

			foreach(var info in mItemInfo.Values)
			{
				for(uint index = 0; index < info.MaxCount; index++)
				{
					Item item = new Item(info.BaseAddress + 16 * index);
					if (item.ID == 0) break;

					info.Items.Add(item);
				}
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(General)));
		}

		private void FileOpen(Object? obj)
		{
			var dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "sav|bf3game*.sav";
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

		private void AppendAccessories(Object? obj)
		{
			AppendItem(ChoiceWindow.ItemType.eAccessories);
		}

		private void ChoiceCollectibles(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eCollectibles);
		}

		private void AppendCollectibles(Object? obj)
		{
			AppendItem(ChoiceWindow.ItemType.eCollectibles);
		}

		private void ChoiceGems(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eGems);
		}

		private void AppendGems(Object? obj)
		{
			AppendItem(ChoiceWindow.ItemType.eGems);
		}

		private void ChoiceKeyItems(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.eKeyItems);
		}

		private void AppendKeyItems(Object? obj)
		{
			AppendItem(ChoiceWindow.ItemType.eKeyItems);
		}

		private void ChoicePinnedItems(Object? obj)
		{
			ChoiceItem(obj, ChoiceWindow.ItemType.ePinnedItems);
		}

		private void Add100Collectibles(Object? obj)
		{
			AddItem(ChoiceWindow.ItemType.eCollectibles, 100);
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

		private void AppendItem(ChoiceWindow.ItemType type)
		{
			var info = mItemInfo[type];
			if(info == null) return;
			if (info.Items.Count >= info.MaxCount) return;

			uint index = (uint)info.Items.Count;
			Item item = new Item(info.BaseAddress + 16 * index);
			item.Index = index;

			ChoiceItem(item, type);
			if (item.ID == 0) return;
			item.Count = 1;
			item.Confirm = 5;
			item.Unknown = 5;
			info.Items.Add(item);
		}

		private void AddItem(ChoiceWindow.ItemType type, uint count)
		{
			var info = mItemInfo[type];
			if (info == null) return;
			for (int i = 0; i < info.Items.Count; i++)
			{
				info.Items[i].Count += count;
			}
		}
	}
}
