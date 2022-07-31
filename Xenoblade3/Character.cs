using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Character
	{
		private readonly uint mAddress;

		public ObservableCollection<Class> Classes { get; private set; } = new ObservableCollection<Class>();

		public Character(uint address)
		{
			mAddress = address;

			for(uint i = 0; i < 23; i++)
			{
				Classes.Add(new Class(mAddress + 0x14 + i * 68));
			}
		}

		public uint Lv
		{
			get => SaveData.Instance().ReadNumber(mAddress, 1);
			set => Util.WriteNumber(mAddress, 1, 1, 99, value);
		}

		public uint Exp
		{
			get => SaveData.Instance().ReadNumber(mAddress + 4, 4);
			set => Util.WriteNumber(mAddress + 4, 4, 0, 99999999, value);
		}
	}
}
