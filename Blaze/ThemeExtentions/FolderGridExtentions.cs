using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Blaze.Core.Extentions;

namespace Blaze.Extentions
{
    //Propriety extentions for the current projects grid

    class ImageSourceExtention : IExtention<ImageSource,  ImageSourceExtention>
    {
    }

    //Allows the convertion of the title to a 5 character initials text
    public class ToInitialsConverter : IValueConverter
    {
        private static bool IsText(object obj)
        {
            if (Equals(obj, null))
            {
                return false;
            }

            Type objType = obj.GetType();
            objType = Nullable.GetUnderlyingType(objType) ?? objType;

            return objType == typeof(string);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!IsText(value)) return null;
            else
            {
                string text = (string)value;

                string inititials = "";
                bool addNext = true;

                foreach (char c in text)
                {
                    if (addNext && c != ' ')
                    {
                        inititials += char.ToUpper(c, culture);
                    }

                    if (c == ' ')
                    {
                        addNext = true;
                    }
                    else
                    {
                        addNext = false;
                    }

                    if (inititials.Length == 5)
                    {
                        return inititials;
                    }
                }

                return inititials;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


    class LinkedProjectsNumber : IExtention<int, LinkedProjectsNumber>
    {
    }
}
