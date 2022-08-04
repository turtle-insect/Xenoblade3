using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
	internal class Info
	{
		private static Info mThis;
		public List<NameValueInfo> Item { get; private set; } = new List<NameValueInfo>();
		public List<NameValueInfo> Collectibles { get; private set; } = new List<NameValueInfo>();
		public List<NameValueInfo> Accessories { get; private set; } = new List<NameValueInfo>();
		public List<NameValueInfo> Gems { get; private set; } = new List<NameValueInfo>();
		public List<NameValueInfo> KeyItems { get; private set; } = new List<NameValueInfo>();
		public List<NameValueInfo> PinnedItems { get; private set; } = new List<NameValueInfo>();

		private Info() { }

		public static Info Instance()
		{
			if (mThis == null)
			{
				mThis = new Info();
				mThis.Init();
			}
			return mThis;
		}

		public NameValueInfo? Search<Type>(List<Type> list, uint id)
			where Type : NameValueInfo, new()
		{
			int min = 0;
			int max = list.Count;
			for (; min < max;)
			{
				int mid = (min + max) / 2;
				if (list[mid].Value == id) return list[mid];
				else if (list[mid].Value > id) max = mid;
				else min = mid + 1;
			}
			return null;
		}

		private void Init()
		{
			AppendList("info\\accessories.txt", Accessories);
			AppendList("info\\collectibles.txt", Collectibles);
			AppendList("info\\gems.txt", Gems);
			AppendList("info\\keyItems.txt", KeyItems);
			AppendList("info\\pinnedItems.txt", PinnedItems);

			AppendList("info\\accessories.txt", Item);
			AppendList("info\\collectibles.txt", Item);
			AppendList("info\\gems.txt", Item);
			AppendList("info\\keyItems.txt", Item);
			AppendList("info\\pinnedItems.txt", Item);
		}

		private void AppendList<Type>(String filename, List<Type> items)
			where Type : ILineAnalysis, new()
		{
			if (!System.IO.File.Exists(filename)) return;
			String[] lines = System.IO.File.ReadAllLines(filename);
			foreach (String line in lines)
			{
				if (line.Length < 3) continue;
				if (line[0] == '#') continue;
				String[] values = line.Split('\t');
				if (values.Length < 2) continue;
				if (String.IsNullOrEmpty(values[0])) continue;
				if (String.IsNullOrEmpty(values[1])) continue;

				Type type = new Type();
				if (type.Line(values))
				{
					items.Add(type);
				}
			}

			items.Sort();
		}
	}
}
