using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xenoblade3
{
    class ItemInfo
    {
        public ObservableCollection<Item> Items { get; set; }
		public uint Category { get; set; }
		public uint BaseAddress { get; set; }
        public uint MaxCount { get; set; }
    }
}
