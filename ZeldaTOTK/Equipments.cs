using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class Equipments
	{
		private readonly uint mCountAddress;
		private readonly uint mNameAddress;
		private readonly uint mLimitCount;

		public ObservableCollection<NameObject> Names { get; private set; } = new ObservableCollection<NameObject>();

		public Equipments(uint countAddress, uint nameAddress, uint limitCount)
		{
			mCountAddress = countAddress;
			mNameAddress = nameAddress;
			mLimitCount = limitCount;
			for (uint index = 0; index < Count; index++)
			{
				Names.Add(new NameObject(nameAddress + index * 64));
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

			Names.Add(new NameObject(mNameAddress + Count * 64));
			Count++;
		}
	}
}
