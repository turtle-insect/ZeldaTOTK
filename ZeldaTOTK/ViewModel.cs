using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public uint ItemCount { get; set; } = 999;
		public Basic? Basic { get; private set; } = new Basic();
		public ObservableCollection<Item> Materials { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Foods { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Capsules { get; private set; } = new ObservableCollection<Item>();
		public EquipmentInfo? Bows { get; private set; }
		public EquipmentInfo? Shields { get; private set; }
		public EquipmentInfo? Weapons { get; private set; }

		public CommandAction? FileOpenCommand { get; private set; }
		public CommandAction? FileSaveCommand { get; private set; }
		public CommandAction? IncrementLimitCountCommand { get; private set; }
		public CommandAction? ChangeAllItemCountCommand { get; private set; }

		public ViewModel()
		{
			FileOpenCommand = new CommandAction(LoadFile);
			FileSaveCommand = new CommandAction(SaveFile);
			IncrementLimitCountCommand = new CommandAction(IncrementLimitCount);
			ChangeAllItemCountCommand = new CommandAction(ChangeAllItemCount);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void LoadFile(Object? obj)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;
			SaveData.Instance().Open(dlg.FileName);

			Materials.Clear();
			Foods.Clear();

			for(uint index = 0; index < 400; index++)
			{
				Item item = new Item(0x477E0 + index * 4, 0xAFC30 + index * 64);
				if (item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;

				Materials.Add(item);
			}

			for (uint index = 0; index < 400; index++)
			{
				Item item = new Item(0x4E9C0 + index * 4, 0x87CE0 + index * 64);
				if (item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;

				Foods.Add(item);
			}

			for (uint index = 0; index < 400; index++)
			{
				Item item = new Item(0x46184 + index * 4, 0x9CBAC + index * 64);
				if (item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;

				Capsules.Add(item);
			}

			Bows = new EquipmentInfo(0x4766C, 0x7B520, 0x4AAF4, 14);
			Shields = new EquipmentInfo(0x4D0BC, 0x7612C, 0x4A3EC, 20);
			Weapons = new EquipmentInfo(0x4AAA0, 0xC3B94, 0x4D1FC, 20);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Basic)));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bows)));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Shields)));
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weapons)));
		}

		private void SaveFile(Object? obj)
		{
			SaveData.Instance().Save();
		}

		private void IncrementLimitCount(Object? obj)
		{
			var equipment = obj as EquipmentInfo;
			if (equipment == null) return;

			equipment.IncrementLimitCount();
		}

		private void ChangeAllItemCount(Object? obj)
		{
			var items = obj as ObservableCollection<Item>;
			if (items == null) return;

			foreach (var item in items)
			{
				item.Count = ItemCount;
			}
		}
	}
}
