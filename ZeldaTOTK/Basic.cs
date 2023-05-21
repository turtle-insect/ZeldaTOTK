using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class Basic
	{
		public uint Heart
		{
			get => SaveData.Instance().ReadNumber(0x29A44, 4);
			set
			{
				if (value < 1) value = 1;
				if (value > 40) value = 40;
				SaveData.Instance().WriteNumber(0x29A44, 4, value);
			}
		}

		public float Stamina
		{
			get
			{
				return BitConverter.ToSingle(SaveData.Instance().ReadValue(0x34FA4, 4));
			}

			set
			{
				if(value < 1000) value = 1000;
				if (value > 3000) value = 3000;
				SaveData.Instance().WriteValue(0x34FA4, BitConverter.GetBytes(value));
			}
		}

		public uint Rupee
		{
			get => SaveData.Instance().ReadNumber(0x2BBDC, 4);
			set
			{
				if (value > 9999999) value = 9999999;
				SaveData.Instance().WriteNumber(0x2BBDC, 4, value);
			}
		}

		public float Battery
		{
			get
			{
				return BitConverter.ToSingle(SaveData.Instance().ReadValue(0x34FC4, 4));
			}

			set
			{
				if (value < 3000) value = 3000;
				if (value > 48000) value = 48000;
				SaveData.Instance().WriteValue(0x34FC4, BitConverter.GetBytes(value));
			}
		}

		public uint Arrow
		{
			get => SaveData.Instance().ReadNumber(0x47030, 4);
			set
			{
				if (value > 999) value = 999;
				SaveData.Instance().WriteNumber(0x47030, 4, value);
			}
		}
	}
}
