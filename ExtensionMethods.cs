using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public static class ExtensionMethods
    {
        public static bool ApproximatelyEquals(this decimal v1, decimal v2, decimal precision = 0.0000000001M)
        {
            decimal dif = Math.Abs(v1 - v2);
            if (dif < precision)
            {
                return true;
            }
            else
            { 
                return false;
            }

        }
        public static int Constrain(this int value, int min, int max)
        {
            if(value<min)
            { 
                value = min;
            }
            if(value>max)
            {
                value = max;
            }
            return value;
        }
        public static string ToSymbol(this AngleUnits units)
        {
            string s = string.Empty;

            switch ((units)
)
            {
                case AngleUnits.Degrees:
                    s = "°";
                    break;
                case AngleUnits.Gradians:
                    s = "g";
                    break;
                case AngleUnits.Radians:
                    s = "rad";
                    break;
                case AngleUnits.Turns:
                    s = "tr";
                    break;
                    
            }
            return s;
        }
    }
}