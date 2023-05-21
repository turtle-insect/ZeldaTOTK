using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaTOTK
{
	internal class NameIDInfo
	{
		public String ID { get; set; } = "";
		public String Name
		{
			get
			{
				if (Names.Count == 0) return "";

				var index = Properties.Settings.Default.lang;
				if (index >= Names.Count) return "";

				var value = Names[index];
				if (String.IsNullOrEmpty(value)) value = Names[0];
				return value;
			}
		}
		private List<String> Names { get; set; } = new List<String>();

		public bool Line(String[] oneLine)
		{
			ID = oneLine[0];
			for (int i = 1; i < oneLine.Length; i++)
			{
				Names.Add(oneLine[i]);
			}
			return true;
		}
	}
}
