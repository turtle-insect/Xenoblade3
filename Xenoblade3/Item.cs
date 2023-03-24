﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private readonly uint mAddress;

		public Item(uint address)
		{
			mAddress = address;
		}

		public uint ID
		{
			get => SaveData.Instance().ReadNumber(mAddress, 2);
			set
			{
				SaveData.Instance().WriteNumber(mAddress, 2, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}

		public uint Index
		{
			get => SaveData.Instance().ReadNumber(mAddress + 2, 2);
			set => SaveData.Instance().WriteNumber(mAddress + 2, 2, value);
		}

		public uint Category
		{
			get => SaveData.Instance().ReadNumber(mAddress + 4, 1);
			set => SaveData.Instance().WriteNumber(mAddress + 4, 1, value);
		}

		public uint Count
		{
			get => SaveData.Instance().ReadNumber(mAddress + 12, 2);
			set
			{
				Util.WriteNumber(mAddress + 12, 2, 0, 999, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			}
		}

		public uint Status
		{
			get => SaveData.Instance().ReadNumber(mAddress + 14, 1);
			set => SaveData.Instance().WriteNumber(mAddress + 14, 1, value);
		}
	}
}
