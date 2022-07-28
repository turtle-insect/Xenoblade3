using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class General
	{
		public uint Money
		{
			get => SaveData.Instance().ReadNumber(0x20, 4);
			set => Util.WriteNumber(0x20, 4, 0, 99999999, value);
		}
	}
}
