using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Character
	{
		private readonly uint mAddress;

		public Character(uint address)
		{
			mAddress = address;
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
