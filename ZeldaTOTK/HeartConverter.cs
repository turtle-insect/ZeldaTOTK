using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ZeldaTOTK
{
	internal class HeartConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var count = (uint)value / 4;
			return count;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			uint count = 0;
			if (!uint.TryParse(value.ToString(), out count)) return 0;

			return count * 4;
		}
	}
}
