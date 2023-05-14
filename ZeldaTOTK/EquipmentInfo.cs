using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZeldaTOTK
{
	internal class EquipmentInfo
	{
		private readonly uint mCountAddress;
		private readonly uint mNameAddress;
		private readonly uint mEnduranceAddress;
		private readonly uint mLimitCount;

		public ObservableCollection<Equipment> Equipments { get; private set; } = new ObservableCollection<Equipment>();

		public EquipmentInfo(uint countAddress, uint nameAddress, uint enduranceAddress, uint limitCount)
		{
			mCountAddress = countAddress;
			mNameAddress = nameAddress;
			mEnduranceAddress = enduranceAddress;
			mLimitCount = limitCount;
			for (uint index = 0; index < Count; index++)
			{
				var equipment = new Equipment(nameAddress + index * 64, enduranceAddress + index * 4);
				Equipments.Add(equipment);
			}
		}

		public uint Count
		{
			get { return SaveData.Instance().ReadNumber(mCountAddress, 4); }
			set { SaveData.Instance().WriteNumber(mCountAddress, 4, value); }
		}

		public void IncrementLimitCount()
		{
			if (Count >= mLimitCount) return;

			var equipment = new Equipment(mNameAddress + Count * 64, mEnduranceAddress + Count * 4);
			equipment.Init();
			Equipments.Add(equipment);
			Count++;
		}
	}
}
