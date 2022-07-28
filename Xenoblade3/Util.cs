using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Util
	{
		public static void WriteNumber(uint address, uint size, uint min, uint max, uint value)
		{
			if(value < min)value = min;
			if(value > max)value = max;
			SaveData.Instance().WriteNumber(address, size, value);
		}
	}
}
