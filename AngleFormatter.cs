using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class AngleFormatter : IFormatProvider , ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (typeof(ICustomFormatter).Equals(formatType))
            {
                return this;
            }
            return null;
        }
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            string result = string.Empty;
            if (arg == null)
            {
                throw new NullReferenceException("arg");
            }







            if (arg is Angle)
            {
                Angle a = arg as Angle;
                int c;
                if (string.IsNullOrWhiteSpace(format) || format.StartsWith("c") || format.StartsWith("C"))
                {
                    
                    switch (a.Units)
                    {
                        case AngleUnits.Degrees:
                            result = "d";
                            break;
                        case AngleUnits.Gradians:
                            result = "g";
                            break;
                        case AngleUnits.Radians:
                            result = "p";
                            break;
                        case AngleUnits.Turns:
                            result = "t";
                            break;
                    }
                    format = result;
                }
                if (format.Length > 1)
                {
                   
                    if (int.TryParse(format.Substring(1, format.Length - 1), out int num))
                    {
                        c = ExtensionMethods.Constrain(num, 0, 9);
                    }
                    else if (a.Units == AngleUnits.Radians)
                    {
                        c = 5;
                    }
                    else
                    {
                        c = 2;
                    }
                    
                }
                switch (char.ToLower(format[0]))
                {
                    case 'd':
                        result = String.Format("{0, 0:F{c}}", a.ToDegrees()) + AngleUnits.Degrees.ToSymbol();
                        break;
                    case 'g':
                        result = String.Format("{0, 0:F{c}}", a.ToGradians()) + AngleUnits.Gradians.ToSymbol();
                        break;
                    case 'p':
                        decimal resval = a.ToRadians().Value / (decimal)3.1415926535897932384626434;
                        result = String.Format("{0, 0:F{c}}", resval.) + "\u03A0" + AngleUnits.Radians.ToSymbol();
                        break;
                    case 't':
                        result = String.Format("{0, 0:F{c}}", a.ToRadians()) + AngleUnits.Turns.ToSymbol();
                        break;
                }





           
                return result;






            }
            else 
            { 
                if(arg is IFormattable)
                {
                    return ((IFormattable)arg).ToString(format, formatProvider);
                }
                return arg.ToString();
            }
            
            
                

        }

    }
}
