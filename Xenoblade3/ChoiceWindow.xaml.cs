using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Xenoblade3
{
	/// <summary>
	/// ChoiceWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class ChoiceWindow : Window
	{
		public uint ID { get; set; } = 0;
		public ItemType Type { get; set; } = ItemType.eAccessories;

		public enum ItemType
		{
			eCollectibles,
			eAccessories,
			eGems,
			eKeyItems,
			ePinnedItems,
		}

		public ChoiceWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			CreateItemList("");
			TextBoxFilter.Focus();
		}

		private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
		{
			CreateItemList(TextBoxFilter.Text);
		}

		private void ListBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ButtonDecision.IsEnabled = ListBoxItem.SelectedIndex >= 0;
		}

		private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			ChoiceItem();
		}

		private void ButtonDecision_Click(object sender, RoutedEventArgs e)
		{
			ChoiceItem();
		}

		private void ChoiceItem()
		{
			var item = ListBoxItem.SelectedItem as NameValueInfo;
			if (item == null) return;

			ID = item.Value;
			Close();
		}

		private void CreateItemList(String filter)
		{
			ListBoxItem.Items.Clear();
			var items = Info.Instance().Accessories;
			if(Type == ItemType.eCollectibles) items = Info.Instance().Collectibles;
			else if (Type == ItemType.eGems) items = Info.Instance().Gems;
			else if (Type == ItemType.eKeyItems) items = Info.Instance().KeyItems;
			else if (Type == ItemType.ePinnedItems) items = Info.Instance().PinnedItems;

			foreach (var item in items)
			{
				if (String.IsNullOrEmpty(filter) || item.Name.IndexOf(filter) >= 0)
				{
					ListBoxItem.Items.Add(item);
				}
			}
		}
	}
}
