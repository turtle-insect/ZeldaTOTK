﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class ViewModel : INotifyPropertyChanged
	{
		public Info Info { get; private set; } = Info.Instance();
		public uint ItemCount { get; set; } = 999;
		public Basic? Basic { get; private set; } = new Basic();
		public ObservableCollection<Item> Armors { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Materials { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Foods { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> Capsules { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Item> KeyItems { get; private set; } = new ObservableCollection<Item>();
		public EquipmentInfo? Bows { get; private set; }
		public EquipmentInfo? Shields { get; private set; }
		public EquipmentInfo? Weapons { get; private set; }

		public CommandAction? FileOpenCommand { get; private set; }
		public CommandAction? FileSaveCommand { get; private set; }
		public CommandAction? IncrementLimitCountCommand { get; private set; }
		public CommandAction? ChangeAllItemCountCommand { get; private set; }
		public CommandAction? GetAllItemCommand { get; private set; }

		public ViewModel()
		{
			FileOpenCommand = new CommandAction(LoadFile);
			FileSaveCommand = new CommandAction(SaveFile);
			IncrementLimitCountCommand = new CommandAction(IncrementLimitCount);
			ChangeAllItemCountCommand = new CommandAction(ChangeAllItemCount);
			GetAllItemCommand = new CommandAction(GetAllItem);
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void LoadFile(Object? obj)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == false) return;
			SaveData.Instance().Open(dlg.FileName);

			Armors.Clear();
			Materials.Clear();
			Foods.Clear();
			Capsules.Clear();

			for (uint index = 0; index < 400; index++)
			{
				Item item = new Item(0, 0x61BF8 + index * 64);
				if (String.IsNullOrEmpty(item.Name)) break;
				Armors.Add(item);
			}

			uint count = SaveData.Instance().ReadNumber(0x477DC, 4);
			for (uint index = 0; index < count; index++)
			{
				Item item = new Item(0x477E0 + index * 4, 0xAFC30 + index * 64);
				if (item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;

				Materials.Add(item);
			}

			count = SaveData.Instance().ReadNumber(0x4E9BC, 4);
			for (uint index = 0; index < count; index++)
			{
				Item item = new Item(0x4E9C0 + index * 4, 0x87CE0 + index * 64);
				if (item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;

				Foods.Add(item);
			}

			count = SaveData.Instance().ReadNumber(0x46180, 4);
			for (uint index = 0; index < count; index++)
			{
				Item item = new Item(0x46184 + index * 4, 0x9CBAC + index * 64);
				if (item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;

				Capsules.Add(item);
			}

			count = SaveData.Instance().ReadNumber(0x4EBD0, 4);
			for (uint index = 0; index < count; index++)
			{
				Item item = new Item(0x4EBD4 + index * 4, 0xB94C4 + index * 64);
				if (String.IsNullOrWhiteSpace(item.Name) && item.Count == 0xFFFFFFFF) break;
				if (item.Count == 0) break;
				if (item.Count == 0xFFFFFFFF) continue;

				KeyItems.Add(item);
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

		private void GetAllItem(Object? obj)
		{
			var items = obj as ObservableCollection<Item>;
			if (items == null) return;
			if (items != Capsules) return;

			items.Clear();
			uint count = 0x46184;
			uint name = 0x9CBAC;
			foreach (var info in Info.Instance().Capsule)
			{
				var item = new Item(count, name);
				item.Count = ItemCount;
				item.Name = info.Key;
				items.Add(item);
				count += 4;
				name += 64;
			}
		}
	}
}
