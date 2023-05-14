using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class NameObject
	{
		private readonly uint mAddress;

		public NameObject(uint address)
		{
			mAddress = address;
		}

		public String Name
		{
			get { return SaveData.Instance().ReadText(mAddress, 64); }
			set
			{
				SaveData.Instance().WriteText(mAddress, 64, value);
			}
		}
	}
}
