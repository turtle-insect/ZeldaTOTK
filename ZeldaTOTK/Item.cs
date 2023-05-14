using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class Item : INotifyPropertyChanged
	{
		private readonly uint mCountAddress;
		private readonly uint mNameAddress;

		public event PropertyChangedEventHandler? PropertyChanged;

		public Item(uint countAddress, uint nameAddress)
		{
			mCountAddress = countAddress;
			mNameAddress = nameAddress;
		}

		public uint Count
		{
			get => SaveData.Instance().ReadNumber(mCountAddress, 4);
			set
			{
				if(value < 0) value = 1;
				if (value > 999) value = 999;
				SaveData.Instance().WriteNumber(mCountAddress, 4, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			}
		}

		public String Name
		{
			get => SaveData.Instance().ReadText(mNameAddress, 64);
			set => SaveData.Instance().WriteText(mNameAddress, 64, value);
		}
	}
}
