using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Color4 : IEquatable<Color4>, IFormattable
    {
        public static readonly Color4 Black;
        public static readonly Color4 White;
        public float Red;
        public float Green;
        public float Blue;
        public float Alpha;
        public Color4(float value)
        {
            this.Alpha = this.Red = this.Green = this.Blue = value;
        }

        public Color4(float red, float green, float blue, float alpha)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
            this.Alpha = alpha;
        }

        public Color4(Vector4 value)
        {
            this.Red = value.X;
            this.Green = value.Y;
            this.Blue = value.Z;
            this.Alpha = value.W;
        }

        public Color4(Vector3 value, float alpha)
        {
            this.Red = value.X;
            this.Green = value.Y;
            this.Blue = value.Z;
            this.Alpha = alpha;
        }

        public Color4(uint rgba)
        {
            this.Alpha = ((float)((rgba >> 0x18) & 0xff)) / 255f;
            this.Blue = ((float)((rgba >> 0x10) & 0xff)) / 255f;
            this.Green = ((float)((rgba >> 8) & 0xff)) / 255f;
            this.Red = ((float)(rgba & 0xff)) / 255f;
        }

        public Color4(int rgba)
        {
            this.Alpha = ((float)((rgba >> 0x18) & 0xff)) / 255f;
            this.Blue = ((float)((rgba >> 0x10) & 0xff)) / 255f;
            this.Green = ((float)((rgba >> 8) & 0xff)) / 255f;
            this.Red = ((float)(rgba & 0xff)) / 255f;
        }

        public Color4(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Color4.");
            }
            this.Red = values[0];
            this.Green = values[1];
            this.Blue = values[2];
            this.Alpha = values[3];
        }

        public Color4(Color3 color)
        {
            this.Red = color.Red;
            this.Green = color.Green;
            this.Blue = color.Blue;
            this.Alpha = 1f;
        }

        public Color4(Color3 color, float alpha)
        {
            this.Red = color.Red;
            this.Green = color.Green;
            this.Blue = color.Blue;
            this.Alpha = alpha;
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

                    case 3:
                        return this.Alpha;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Color4 run from 0 to 3, inclusive.");
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

                    case 3:
                        this.Alpha = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Color4 run from 0 to 3, inclusive.");
            }
        }
        public int ToBgra()
        {
            uint num = ((uint)(this.Alpha * 255f)) & 0xff;
            uint num2 = ((uint)(this.Red * 255f)) & 0xff;
            uint num3 = ((uint)(this.Green * 255f)) & 0xff;
            uint num4 = ((uint)(this.Blue * 255f)) & 0xff;
            uint num5 = num4;
            num5 |= num3 << 8;
            num5 |= num2 << 0x10;
            num5 |= num << 0x18;
            return (int)num5;
        }

        public void ToBgra(out byte r, out byte g, out byte b, out byte a)
        {
            a = (byte)(this.Alpha * 255f);
            r = (byte)(this.Red * 255f);
            g = (byte)(this.Green * 255f);
            b = (byte)(this.Blue * 255f);
        }

        public int ToRgba()
        {
            uint num = ((uint)(this.Alpha * 255f)) & 0xff;
            uint num2 = ((uint)(this.Red * 255f)) & 0xff;
            uint num3 = ((uint)(this.Green * 255f)) & 0xff;
            uint num4 = ((uint)(this.Blue * 255f)) & 0xff;
            uint num5 = num2;
            num5 |= num3 << 8;
            num5 |= num4 << 0x10;
            num5 |= num << 0x18;
            return (int)num5;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(this.Red, this.Green, this.Blue);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(this.Red, this.Green, this.Blue, this.Alpha);
        }

        public float[] ToArray()
        {
            return new float[] { this.Red, this.Green, this.Blue, this.Alpha };
        }

        public static void Add(ref Color4 left, ref Color4 right, out Color4 result)
        {
            result.Alpha = left.Alpha + right.Alpha;
            result.Red = left.Red + right.Red;
            result.Green = left.Green + right.Green;
            result.Blue = left.Blue + right.Blue;
        }

        public static Color4 Add(Color4 left, Color4 right)
        {
            return new Color4(left.Red + right.Red, left.Green + right.Green, left.Blue + right.Blue, left.Alpha + right.Alpha);
        }

        public static void Subtract(ref Color4 left, ref Color4 right, out Color4 result)
        {
            result.Alpha = left.Alpha - right.Alpha;
            result.Red = left.Red - right.Red;
            result.Green = left.Green - right.Green;
            result.Blue = left.Blue - right.Blue;
        }

        public static Color4 Subtract(Color4 left, Color4 right)
        {
            return new Color4(left.Red - right.Red, left.Green - right.Green, left.Blue - right.Blue, left.Alpha - right.Alpha);
        }

        public static void Modulate(ref Color4 left, ref Color4 right, out Color4 result)
        {
            result.Alpha = left.Alpha * right.Alpha;
            result.Red = left.Red * right.Red;
            result.Green = left.Green * right.Green;
            result.Blue = left.Blue * right.Blue;
        }

        public static Color4 Modulate(Color4 left, Color4 right)
        {
            return new Color4(left.Red * right.Red, left.Green * right.Green, left.Blue * right.Blue, left.Alpha * right.Alpha);
        }

        public static void Scale(ref Color4 value, float scale, out Color4 result)
        {
            result.Alpha = value.Alpha * scale;
            result.Red = value.Red * scale;
            result.Green = value.Green * scale;
            result.Blue = value.Blue * scale;
        }

        public static Color4 Scale(Color4 value, float scale)
        {
            return new Color4(value.Red * scale, value.Green * scale, value.Blue * scale, value.Alpha * scale);
        }

        public static void Negate(ref Color4 value, out Color4 result)
        {
            result.Alpha = 1f - value.Alpha;
            result.Red = 1f - value.Red;
            result.Green = 1f - value.Green;
            result.Blue = 1f - value.Blue;
        }

        public static Color4 Negate(Color4 value)
        {
            return new Color4(1f - value.Red, 1f - value.Green, 1f - value.Blue, 1f - value.Alpha);
        }

        public static void Clamp(ref Color4 value, ref Color4 min, ref Color4 max, out Color4 result)
        {
            float alpha = value.Alpha;
            alpha = (alpha > max.Alpha) ? max.Alpha : alpha;
            alpha = (alpha < min.Alpha) ? min.Alpha : alpha;
            float red = value.Red;
            red = (red > max.Red) ? max.Red : red;
            red = (red < min.Red) ? min.Red : red;
            float green = value.Green;
            green = (green > max.Green) ? max.Green : green;
            green = (green < min.Green) ? min.Green : green;
            float blue = value.Blue;
            blue = (blue > max.Blue) ? max.Blue : blue;
            blue = (blue < min.Blue) ? min.Blue : blue;
            result = new Color4(red, green, blue, alpha);
        }

        public static Color4 Clamp(Color4 value, Color4 min, Color4 max)
        {
            Color4 color;
            Clamp(ref value, ref min, ref max, out color);
            return color;
        }

        public static void Lerp(ref Color4 start, ref Color4 end, float amount, out Color4 result)
        {
            result.Alpha = start.Alpha + (amount * (end.Alpha - start.Alpha));
            result.Red = start.Red + (amount * (end.Red - start.Red));
            result.Green = start.Green + (amount * (end.Green - start.Green));
            result.Blue = start.Blue + (amount * (end.Blue - start.Blue));
        }

        public static Color4 Lerp(Color4 start, Color4 end, float amount)
        {
            return new Color4(start.Red + (amount * (end.Red - start.Red)), start.Green + (amount * (end.Green - start.Green)), start.Blue + (amount * (end.Blue - start.Blue)), start.Alpha + (amount * (end.Alpha - start.Alpha)));
        }

        public static void SmoothStep(ref Color4 start, ref Color4 end, float amount, out Color4 result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.Alpha = start.Alpha + ((end.Alpha - start.Alpha) * amount);
            result.Red = start.Red + ((end.Red - start.Red) * amount);
            result.Green = start.Green + ((end.Green - start.Green) * amount);
            result.Blue = start.Blue + ((end.Blue - start.Blue) * amount);
        }

        public static Color4 SmoothStep(Color4 start, Color4 end, float amount)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            return new Color4(start.Red + ((end.Red - start.Red) * amount), start.Green + ((end.Green - start.Green) * amount), start.Blue + ((end.Blue - start.Blue) * amount), start.Alpha + ((end.Alpha - start.Alpha) * amount));
        }

        public static void Max(ref Color4 left, ref Color4 right, out Color4 result)
        {
            result.Alpha = (left.Alpha > right.Alpha) ? left.Alpha : right.Alpha;
            result.Red = (left.Red > right.Red) ? left.Red : right.Red;
            result.Green = (left.Green > right.Green) ? left.Green : right.Green;
            result.Blue = (left.Blue > right.Blue) ? left.Blue : right.Blue;
        }

        public static Color4 Max(Color4 left, Color4 right)
        {
            Color4 color;
            Max(ref left, ref right, out color);
            return color;
        }

        public static void Min(ref Color4 left, ref Color4 right, out Color4 result)
        {
            result.Alpha = (left.Alpha < right.Alpha) ? left.Alpha : right.Alpha;
            result.Red = (left.Red < right.Red) ? left.Red : right.Red;
            result.Green = (left.Green < right.Green) ? left.Green : right.Green;
            result.Blue = (left.Blue < right.Blue) ? left.Blue : right.Blue;
        }

        public static Color4 Min(Color4 left, Color4 right)
        {
            Color4 color;
            Min(ref left, ref right, out color);
            return color;
        }

        public static void AdjustContrast(ref Color4 value, float contrast, out Color4 result)
        {
            result.Alpha = value.Alpha;
            result.Red = 0.5f + (contrast * (value.Red - 0.5f));
            result.Green = 0.5f + (contrast * (value.Green - 0.5f));
            result.Blue = 0.5f + (contrast * (value.Blue - 0.5f));
        }

        public static Color4 AdjustContrast(Color4 value, float contrast)
        {
            return new Color4(0.5f + (contrast * (value.Red - 0.5f)), 0.5f + (contrast * (value.Green - 0.5f)), 0.5f + (contrast * (value.Blue - 0.5f)), value.Alpha);
        }

        public static void AdjustSaturation(ref Color4 value, float saturation, out Color4 result)
        {
            float num = ((value.Red * 0.2125f) + (value.Green * 0.7154f)) + (value.Blue * 0.0721f);
            result.Alpha = value.Alpha;
            result.Red = num + (saturation * (value.Red - num));
            result.Green = num + (saturation * (value.Green - num));
            result.Blue = num + (saturation * (value.Blue - num));
        }

        public static Color4 AdjustSaturation(Color4 value, float saturation)
        {
            float num = ((value.Red * 0.2125f) + (value.Green * 0.7154f)) + (value.Blue * 0.0721f);
            return new Color4(num + (saturation * (value.Red - num)), num + (saturation * (value.Green - num)), num + (saturation * (value.Blue - num)), value.Alpha);
        }

        public static Color4 operator +(Color4 left, Color4 right)
        {
            return new Color4(left.Red + right.Red, left.Green + right.Green, left.Blue + right.Blue, left.Alpha + right.Alpha);
        }

        public static Color4 operator +(Color4 value)
        {
            return value;
        }

        public static Color4 operator -(Color4 left, Color4 right)
        {
            return new Color4(left.Red - right.Red, left.Green - right.Green, left.Blue - right.Blue, left.Alpha - right.Alpha);
        }

        public static Color4 operator -(Color4 value)
        {
            return new Color4(-value.Red, -value.Green, -value.Blue, -value.Alpha);
        }

        public static Color4 operator *(float scale, Color4 value)
        {
            return new Color4(value.Red * scale, value.Green * scale, value.Blue * scale, value.Alpha * scale);
        }

        public static Color4 operator *(Color4 value, float scale)
        {
            return new Color4(value.Red * scale, value.Green * scale, value.Blue * scale, value.Alpha * scale);
        }

        public static Color4 operator *(Color4 left, Color4 right)
        {
            return new Color4(left.Red * right.Red, left.Green * right.Green, left.Blue * right.Blue, left.Alpha * right.Alpha);
        }

        public static bool operator ==(Color4 left, Color4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color4 left, Color4 right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Color3(Color4 value)
        {
            return new Color3(value.Red, value.Green, value.Blue);
        }

        public static explicit operator Vector3(Color4 value)
        {
            return new Vector3(value.Red, value.Green, value.Blue);
        }

        public static explicit operator Vector4(Color4 value)
        {
            return new Vector4(value.Red, value.Green, value.Blue, value.Alpha);
        }

        public static explicit operator Color4(Vector3 value)
        {
            return new Color4(value.X, value.Y, value.Z, 1f);
        }

        public static explicit operator Color4(Vector4 value)
        {
            return new Color4(value.X, value.Y, value.Z, value.W);
        }

        public static explicit operator Color4(ColorBGRA value)
        {
            return new Color4((float)value.R, (float)value.G, (float)value.B, (float)value.A);
        }

        public static explicit operator ColorBGRA(Color4 value)
        {
            return new ColorBGRA(value.Red, value.Green, value.Blue, value.Alpha);
        }

        public static explicit operator int(Color4 value)
        {
            return value.ToRgba();
        }

        public static explicit operator Color4(int value)
        {
            return new Color4(value);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Alpha:{0} Red:{1} Green:{2} Blue:{3}", new object[] { (float)this.Alpha, (float)this.Red, (float)this.Green, (float)this.Blue });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Alpha:{0} Red:{1} Green:{2} Blue:{3}", new object[] { ((float)this.Alpha).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Red).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Green).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Blue).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "Alpha:{0} Red:{1} Green:{2} Blue:{3}", new object[] { (float)this.Alpha, (float)this.Red, (float)this.Green, (float)this.Blue });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "Alpha:{0} Red:{1} Green:{2} Blue:{3}", new object[] { ((float)this.Alpha).ToString(format, formatProvider), ((float)this.Red).ToString(format, formatProvider), ((float)this.Green).ToString(format, formatProvider), ((float)this.Blue).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (((((float)this.Alpha).GetHashCode() + ((float)this.Red).GetHashCode()) + ((float)this.Green).GetHashCode()) + ((float)this.Blue).GetHashCode());
        }

        public bool Equals(Color4 other)
        {
            return ((((this.Alpha == other.Alpha) && (this.Red == other.Red)) && (this.Green == other.Green)) && (this.Blue == other.Blue));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Color4)))
            {
                return false;
            }
            return this.Equals((Color4)value);
        }

        static Color4()
        {
            Black = new Color4(0f, 0f, 0f, 1f);
            White = new Color4(1f, 1f, 1f, 1f);
        }
    }
}
