using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab3.ExtensionMethods;

namespace Lab3
{
    public class Angle : IFormattable
    {
        public const decimal pi = 3.1415926535897932384626434M;
        public const decimal twoPi = 2M * pi;
        private decimal _Value = 0M;
        private AngleUnits _Units = AngleUnits.Degrees;
        private static decimal[,] _ConversionFactors =
        {
            {     1M,  9M/10M,  180M/pi,  360M},
            { 10M/9M,      1M,  200M/pi,  400M},
            {pi/180M, pi/200M,       1M, twoPi},
            {1M/360M, 1M/400M, 1M/twoPi,    1M} };

        public decimal Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = Normalize(value, Units);
            }
        }
        public AngleUnits Units
        {
            get
            {
                return _Units;
            }
            set
            {
                _Value = ConvertAngleValue(Value, Units, value);
                _Units = value;
            }
        }
        private static decimal Normalize(decimal value, AngleUnits units)
        {
            if (units == AngleUnits.Degrees)
            {
                while (value < 0)
                {
                    value += 360;

                }
                while (value >= 360)
                {
                    value -= 360;

                }
            }

            if (units == AngleUnits.Gradians)
            {
                while (value < 0)
                {
                    value += 400;
                }

                while (value >= 400)
                {
                    value -= 400;
                }
            }

            if (units == AngleUnits.Radians)
            {
                while (value < 0)
                {
                    value += twoPi;
                }
                while (value >= twoPi)
                {
                    value -= twoPi;
                }
            }
            if (units == AngleUnits.Turns)
            {
                while (value < 0)
                {
                    value += 1;
                }
                while (value >= 1)
                {
                    value -= 1;
                }
            }
            return value;
        }
        private static decimal ConvertAngleValue(decimal angle, AngleUnits fromUnits, AngleUnits toUnits)
        {

            decimal factor = _ConversionFactors[(int)toUnits, (int)fromUnits];
                return Normalize(factor * angle, toUnits);

        }
        public Angle()
        {
            new Angle(0, AngleUnits.Degrees);
        }
        public Angle(Angle Other)
        {
            Value = Other.Value;
            Units = Other.Units;
        }
        public Angle(decimal value, AngleUnits units = AngleUnits.Degrees)
        {
            Units = units;
            Value = value;
        }

        public Angle ToDegrees()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Degrees), AngleUnits.Degrees);
        }
        public Angle ToGradians()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Gradians), AngleUnits.Gradians);
        }
        public Angle ToRadians()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Radians), AngleUnits.Radians);
        }
        public Angle ToTurns()
        {
            return new Angle(ConvertAngleValue(Value, Units, AngleUnits.Turns), AngleUnits.Turns);
        }
        public Angle ConvertAngle(AngleUnits targetUnits)
        {
            Angle s = new Angle();
            switch (targetUnits)
            {
                case AngleUnits.Degrees:
                    s = ToDegrees();
                    break;
                case AngleUnits.Gradians:
                    s = ToGradians();
                    break;
                case AngleUnits.Radians:
                    s = ToRadians();
                    break;
                case AngleUnits.Turns:
                    s = ToTurns();
                    break;
            }
            return s;
        }
        public static Angle operator +(Angle a1, Angle a2)
        {
            return new Angle(a1.Value + ConvertAngleValue(a2.Value, a2.Units, a1.Units), a1.Units);
        }
        public static Angle operator -(Angle a1, Angle a2)
        {
            return new Angle(a1.Value - ConvertAngleValue(a2.Value, a2.Units, a1.Units), a1.Units);
        }
        public static Angle operator +(Angle a, decimal scalar)
        {
            return new Angle(a.Value + scalar, a.Units);
        }
        public static Angle operator -(Angle a, decimal scalar)
        {
            return new Angle(a.Value - scalar, a.Units);
        }
        public static Angle operator *(Angle a, decimal scalar)
        {
            return new Angle(a.Value * scalar, a.Units);
        }
        public static Angle operator /(Angle a, decimal scalar)
        {
            return new Angle(a.Value / scalar, a.Units);
        }
        public static bool operator ==(Angle a, Angle b)
        {
            object o1 = (object)a;
            object o2 = (object)b;
            if (o1 == null && o2 == null)
            {
                return true;
            }
            else if (o1 == null && o2 != null || o1 != null && o2 == null)
            {
                return false;
            }
            else
            {
                return a.Value.ApproximatelyEquals(ConvertAngleValue(b.Value, b.Units, a.Units));
            }
        }
        public static bool operator !=(Angle a, Angle b)
        {
            object o1 = (object)a;
            object o2 = (object)b;
            if (o1 == null && o2 == null)
            {
                return true;
            }
            else if (o1 == null && o2 != null || o1 != null && o2 == null)
            {
                return false;
            }
            else
            {
                return !a.Value.ApproximatelyEquals(ConvertAngleValue(b.Value, b.Units, a.Units));
            }
        }
        public static bool operator <(Angle a, Angle b)
        {
            if (a == null && b == null)
            {
                return false;
            }
            else if (a == null && b != null)
            {
                return true;
            }
            else if (a != null && b == null)
            {
                return false;
            }
            else
            {
                return !a.Value.ApproximatelyEquals(b.Value) && (a.Value < b.Value);
            }
        }
        public static bool operator >(Angle a, Angle b)
        {
            return !(a == b || a < b);
        }
        public static bool operator <=(Angle a, Angle b)
        {
            return (a < b || a == b);
        }
        public static bool operator >=(Angle a, Angle b)
        {
            return (a > b || a == b);
        }
        public override bool Equals(object obj)
        {
            return this == obj as Angle;
        }
        public override int GetHashCode()
        {
            return ConvertAngleValue(Value, Units, AngleUnits.Degrees).GetHashCode();
        }
        public static explicit operator decimal(Angle a)
        {
            return a.Value;
        }
        public static explicit operator double(Angle a)
        {
            return (double)a.Value;
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return new AngleFormatter().Format(format, this, formatProvider);
        }
        public string ToString(string format)
        {
            AngleFormatter fmt = new AngleFormatter();
            return fmt.Format(format, this, fmt);
        }
        public override string ToString()
        {
            return ToString(string.Empty);
        }
    }
}
