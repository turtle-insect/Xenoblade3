using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Class
	{
		private readonly uint mAddress;

		public Class(uint address)
		{
			mAddress = address;
		}

		public uint Lv
		{
			get => SaveData.Instance().ReadNumber(mAddress + 6, 1);
			set => Util.WriteNumber(mAddress + 6, 1, 0, 16, value);
		}

		public uint Exp
		{
			get => SaveData.Instance().ReadNumber(mAddress, 4);
			set => Util.WriteNumber(mAddress, 4, 0, 99999999, value);
		}
	}
}
