using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace InitialProject.View.Tourist
{
    public class IntToTextConverter : MarkupExtension, IValueConverter
    {
        public IntToTextConverter(){ }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var v = (int)(double)value;
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var v = value as string;
            int ret = 0;
            if (int.TryParse(v, out ret))
            {
                return ret;
            }
            else
            {
                return value;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
