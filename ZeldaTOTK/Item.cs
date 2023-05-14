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

		public event PropertyChangedEventHandler? PropertyChanged;

		public NameObject Name { get; private set; }

		public Item(uint countAddress, uint nameAddress)
		{
			mCountAddress = countAddress;
			Name = new NameObject(nameAddress);
		}

		public uint Count
		{
			get { return SaveData.Instance().ReadNumber(mCountAddress, 4); }
			set
			{
				if(value < 0) value = 1;
				if (value > 999) value = 999;
				SaveData.Instance().WriteNumber(mCountAddress, 4, value);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			}
		}
	}
}
