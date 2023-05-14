using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class Equipment
	{
		private readonly uint mNameAddress;
		private readonly uint mEnduranceAddress;

		public Equipment(uint nameAddress, uint enduranceAddress)
		{
			mNameAddress = nameAddress;
			mEnduranceAddress = enduranceAddress;
		}

		public String Name
		{
			get => SaveData.Instance().ReadText(mNameAddress, 64);
			set => SaveData.Instance().WriteText(mNameAddress, 64, value);
		}

		public uint Endurance
		{
			get => SaveData.Instance().ReadNumber(mEnduranceAddress, 4);
			set => SaveData.Instance().WriteNumber(mEnduranceAddress, 4, value);
		}

		public void Init()
		{
			Name = "";
			Endurance = 1;
		}
	}
}
