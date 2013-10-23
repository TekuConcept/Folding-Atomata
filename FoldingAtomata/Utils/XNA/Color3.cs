using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Color3 : IEquatable<Color3>, IFormattable
    {
        public static readonly Color3 Black;
        public static readonly Color3 White;
        public float Red;
        public float Green;
        public float Blue;
        public Color3(float value)
        {
            this.Red = this.Green = this.Blue = value;
        }

        public Color3(float red, float green, float blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public Color3(Vector3 value)
        {
            this.Red = value.X;
            this.Green = value.Y;
            this.Blue = value.Z;
        }

        public Color3(int rgb)
        {
            this.Blue = ((float)((rgb >> 0x10) & 0xff)) / 255f;
            this.Green = ((float)((rgb >> 8) & 0xff)) / 255f;
            this.Red = ((float)(rgb & 0xff)) / 255f;
        }

        public Color3(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 3)
            {
                throw new ArgumentOutOfRangeException("values", "There must be three and only three input values for Color3.");
            }
            this.Red = values[0];
            this.Green = values[1];
            this.Blue = values[2];
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.Red;

                    case 1:
                        return this.Green;

                    case 2:
                        return this.Blue;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Color3 run from 0 to 2, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.Red = value;
                        return;

                    case 1:
                        this.Green = value;
                        return;

                    case 2:
                        this.Blue = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Color3 run from 0 to 2, inclusive.");
            }
        }
        public int ToRgba()
        {
            uint num = 0xff;
            uint num2 = ((uint)(this.Red * 255f)) & 0xff;
            uint num3 = ((uint)(this.Green * 255f)) & 0xff;
            uint num4 = ((uint)(this.Blue * 255f)) & 0xff;
            uint num5 = num2;
            num5 |= num3 << 8;
            num5 |= num4 << 0x10;
            num5 |= num << 0x18;
            return (int)num5;
        }

        public int ToBgra()
        {
            uint num = 0xff;
            uint num2 = ((uint)(this.Red * 255f)) & 0xff;
            uint num3 = ((uint)(this.Green * 255f)) & 0xff;
            uint num4 = ((uint)(this.Blue * 255f)) & 0xff;
            uint num5 = num4;
            num5 |= num3 << 8;
            num5 |= num2 << 0x10;
            num5 |= num << 0x18;
            return (int)num5;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(this.Red, this.Green, this.Blue);
        }

        public float[] ToArray()
        {
            return new float[] { this.Red, this.Green, this.Blue };
        }

        public static void Add(ref Color3 left, ref Color3 right, out Color3 result)
        {
            result.Red = left.Red + right.Red;
            result.Green = left.Green + right.Green;
            result.Blue = left.Blue + right.Blue;
        }

        public static Color3 Add(Color3 left, Color3 right)
        {
            return new Color3(left.Red + right.Red, left.Green + right.Green, left.Blue + right.Blue);
        }

        public static void Subtract(ref Color3 left, ref Color3 right, out Color3 result)
        {
            result.Red = left.Red - right.Red;
            result.Green = left.Green - right.Green;
            result.Blue = left.Blue - right.Blue;
        }

        public static Color3 Subtract(Color3 left, Color3 right)
        {
            return new Color3(left.Red - right.Red, left.Green - right.Green, left.Blue - right.Blue);
        }

        public static void Modulate(ref Color3 left, ref Color3 right, out Color3 result)
        {
            result.Red = left.Red * right.Red;
            result.Green = left.Green * right.Green;
            result.Blue = left.Blue * right.Blue;
        }

        public static Color3 Modulate(Color3 left, Color3 right)
        {
            return new Color3(left.Red * right.Red, left.Green * right.Green, left.Blue * right.Blue);
        }

        public static void Scale(ref Color3 value, float scale, out Color3 result)
        {
            result.Red = value.Red * scale;
            result.Green = value.Green * scale;
            result.Blue = value.Blue * scale;
        }

        public static Color3 Scale(Color3 value, float scale)
        {
            return new Color3(value.Red * scale, value.Green * scale, value.Blue * scale);
        }

        public static void Negate(ref Color3 value, out Color3 result)
        {
            result.Red = 1f - value.Red;
            result.Green = 1f - value.Green;
            result.Blue = 1f - value.Blue;
        }

        public static Color3 Negate(Color3 value)
        {
            return new Color3(1f - value.Red, 1f - value.Green, 1f - value.Blue);
        }

        public static void Clamp(ref Color3 value, ref Color3 min, ref Color3 max, out Color3 result)
        {
            float red = value.Red;
            red = (red > max.Red) ? max.Red : red;
            red = (red < min.Red) ? min.Red : red;
            float green = value.Green;
            green = (green > max.Green) ? max.Green : green;
            green = (green < min.Green) ? min.Green : green;
            float blue = value.Blue;
            blue = (blue > max.Blue) ? max.Blue : blue;
            blue = (blue < min.Blue) ? min.Blue : blue;
            result = new Color3(red, green, blue);
        }

        public static Color3 Clamp(Color3 value, Color3 min, Color3 max)
        {
            Color3 color;
            Clamp(ref value, ref min, ref max, out color);
            return color;
        }

        public static void Lerp(ref Color3 start, ref Color3 end, float amount, out Color3 result)
        {
            result.Red = start.Red + (amount * (end.Red - start.Red));
            result.Green = start.Green + (amount * (end.Green - start.Green));
            result.Blue = start.Blue + (amount * (end.Blue - start.Blue));
        }

        public static Color3 Lerp(Color3 start, Color3 end, float amount)
        {
            return new Color3(start.Red + (amount * (end.Red - start.Red)), start.Green + (amount * (end.Green - start.Green)), start.Blue + (amount * (end.Blue - start.Blue)));
        }

        public static void SmoothStep(ref Color3 start, ref Color3 end, float amount, out Color3 result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.Red = start.Red + ((end.Red - start.Red) * amount);
            result.Green = start.Green + ((end.Green - start.Green) * amount);
            result.Blue = start.Blue + ((end.Blue - start.Blue) * amount);
        }

        public static Color3 SmoothStep(Color3 start, Color3 end, float amount)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            return new Color3(start.Red + ((end.Red - start.Red) * amount), start.Green + ((end.Green - start.Green) * amount), start.Blue + ((end.Blue - start.Blue) * amount));
        }

        public static void Max(ref Color3 left, ref Color3 right, out Color3 result)
        {
            result.Red = (left.Red > right.Red) ? left.Red : right.Red;
            result.Green = (left.Green > right.Green) ? left.Green : right.Green;
            result.Blue = (left.Blue > right.Blue) ? left.Blue : right.Blue;
        }

        public static Color3 Max(Color3 left, Color3 right)
        {
            Color3 color;
            Max(ref left, ref right, out color);
            return color;
        }

        public static void Min(ref Color3 left, ref Color3 right, out Color3 result)
        {
            result.Red = (left.Red < right.Red) ? left.Red : right.Red;
            result.Green = (left.Green < right.Green) ? left.Green : right.Green;
            result.Blue = (left.Blue < right.Blue) ? left.Blue : right.Blue;
        }

        public static Color3 Min(Color3 left, Color3 right)
        {
            Color3 color;
            Min(ref left, ref right, out color);
            return color;
        }

        public static void AdjustContrast(ref Color3 value, float contrast, out Color3 result)
        {
            result.Red = 0.5f + (contrast * (value.Red - 0.5f));
            result.Green = 0.5f + (contrast * (value.Green - 0.5f));
            result.Blue = 0.5f + (contrast * (value.Blue - 0.5f));
        }

        public static Color3 AdjustContrast(Color3 value, float contrast)
        {
            return new Color3(0.5f + (contrast * (value.Red - 0.5f)), 0.5f + (contrast * (value.Green - 0.5f)), 0.5f + (contrast * (value.Blue - 0.5f)));
        }

        public static void AdjustSaturation(ref Color3 value, float saturation, out Color3 result)
        {
            float num = ((value.Red * 0.2125f) + (value.Green * 0.7154f)) + (value.Blue * 0.0721f);
            result.Red = num + (saturation * (value.Red - num));
            result.Green = num + (saturation * (value.Green - num));
            result.Blue = num + (saturation * (value.Blue - num));
        }

        public static Color3 AdjustSaturation(Color3 value, float saturation)
        {
            float num = ((value.Red * 0.2125f) + (value.Green * 0.7154f)) + (value.Blue * 0.0721f);
            return new Color3(num + (saturation * (value.Red - num)), num + (saturation * (value.Green - num)), num + (saturation * (value.Blue - num)));
        }

        public static Color3 operator +(Color3 left, Color3 right)
        {
            return new Color3(left.Red + right.Red, left.Green + right.Green, left.Blue + right.Blue);
        }

        public static Color3 operator +(Color3 value)
        {
            return value;
        }

        public static Color3 operator -(Color3 left, Color3 right)
        {
            return new Color3(left.Red - right.Red, left.Green - right.Green, left.Blue - right.Blue);
        }

        public static Color3 operator -(Color3 value)
        {
            return new Color3(-value.Red, -value.Green, -value.Blue);
        }

        public static Color3 operator *(float scale, Color3 value)
        {
            return new Color3(value.Red * scale, value.Green * scale, value.Blue * scale);
        }

        public static Color3 operator *(Color3 value, float scale)
        {
            return new Color3(value.Red * scale, value.Green * scale, value.Blue * scale);
        }

        public static Color3 operator *(Color3 left, Color3 right)
        {
            return new Color3(left.Red * right.Red, left.Green * right.Green, left.Blue * right.Blue);
        }

        public static bool operator ==(Color3 left, Color3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color3 left, Color3 right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Color4(Color3 value)
        {
            return new Color4(value.Red, value.Green, value.Blue, 1f);
        }

        public static explicit operator Vector3(Color3 value)
        {
            return new Vector3(value.Red, value.Green, value.Blue);
        }

        public static explicit operator Color3(Vector3 value)
        {
            return new Color3(value.X, value.Y, value.Z);
        }

        public static explicit operator Color3(int value)
        {
            return new Color3(value);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Red:{1} Green:{2} Blue:{3}", new object[] { (float)this.Red, (float)this.Green, (float)this.Blue });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Red:{1} Green:{2} Blue:{3}", new object[] { ((float)this.Red).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Green).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Blue).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "Red:{1} Green:{2} Blue:{3}", new object[] { (float)this.Red, (float)this.Green, (float)this.Blue });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "Red:{0} Green:{1} Blue:{2}", new object[] { ((float)this.Red).ToString(format, formatProvider), ((float)this.Green).ToString(format, formatProvider), ((float)this.Blue).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return ((((float)this.Red).GetHashCode() + ((float)this.Green).GetHashCode()) + ((float)this.Blue).GetHashCode());
        }

        public bool Equals(Color3 other)
        {
            return (((this.Red == other.Red) && (this.Green == other.Green)) && (this.Blue == other.Blue));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Color3)))
            {
                return false;
            }
            return this.Equals((Color3)value);
        }

        static Color3()
        {
            Black = new Color3(0f, 0f, 0f);
            White = new Color3(1f, 1f, 1f);
        }
    }
}
