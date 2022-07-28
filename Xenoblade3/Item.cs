using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Item
	{
		private readonly uint mAddress;

		public Item(uint address)
		{
			mAddress = address;
		}

		public uint ID
		{
			get => SaveData.Instance().ReadNumber(mAddress, 2);
			set => SaveData.Instance().WriteNumber(mAddress, 2, value);
		}

		public uint Index
		{
			get => SaveData.Instance().ReadNumber(mAddress + 2, 2);
			set => SaveData.Instance().WriteNumber(mAddress + 2, 2, value);
		}

		public uint Count
		{
			get => SaveData.Instance().ReadNumber(mAddress + 12, 2);
			set => Util.WriteNumber(mAddress + 12, 2, 0, 999, value);
		}
	}
}
