using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class Info
	{
		private static Info? mThis;
		public Dictionary<String, NameIDInfo> Capsule { get; private set; } = new Dictionary<String, NameIDInfo>();

		private Info() { }
		public static Info Instance()
		{
			if (mThis == null)
			{
				mThis = new Info();
				mThis.Init();
			}
			return mThis;
		}

		private void Init()
		{
			AppendList(@"info\capsule.txt", Capsule);
		}

		private void AppendList<Type>(String filename, Dictionary<String, Type> items)
			where Type : NameIDInfo, new()
		{
			if (!System.IO.File.Exists(filename)) return;
			String[] lines = System.IO.File.ReadAllLines(filename);
			foreach (String line in lines)
			{
				if (line.Length < 3) continue;
				if (line[0] == '#') continue;
				String[] values = line.Split('\t');
				if (values.Length < 2) continue;
				if (String.IsNullOrEmpty(values[0])) continue;
				if (String.IsNullOrEmpty(values[1])) continue;

				Type type = new Type();
				if (type.Line(values))
				{
					items.Add(type.ID, type);
				}
			}
		}
	}
}
