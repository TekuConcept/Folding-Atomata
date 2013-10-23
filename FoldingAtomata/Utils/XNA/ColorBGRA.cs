using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct ColorBGRA : IEquatable<ColorBGRA>, IFormattable
    {
        public byte B;
        public byte G;
        public byte R;
        public byte A;
        public ColorBGRA(byte value)
        {
            this.A = this.R = this.G = this.B = value;
        }

        public ColorBGRA(float value)
        {
            this.A = this.R = this.G = this.B = ToByte(value);
        }

        public ColorBGRA(byte red, byte green, byte blue, byte alpha)
        {
            this.R = red;
            this.G = green;
            this.B = blue;
            this.A = alpha;
        }

        public ColorBGRA(float red, float green, float blue, float alpha)
        {
            this.R = ToByte(red);
            this.G = ToByte(green);
            this.B = ToByte(blue);
            this.A = ToByte(alpha);
        }

        public ColorBGRA(Vector4 value)
        {
            this.R = ToByte(value.X);
            this.G = ToByte(value.Y);
            this.B = ToByte(value.Z);
            this.A = ToByte(value.W);
        }

        public ColorBGRA(Vector3 value, float alpha)
        {
            this.R = ToByte(value.X);
            this.G = ToByte(value.Y);
            this.B = ToByte(value.Z);
            this.A = ToByte(alpha);
        }

        public ColorBGRA(uint bgra)
        {
            this.A = (byte)((bgra >> 0x18) & 0xff);
            this.R = (byte)((bgra >> 0x10) & 0xff);
            this.G = (byte)((bgra >> 8) & 0xff);
            this.B = (byte)(bgra & 0xff);
        }

        public ColorBGRA(int bgra)
        {
            this.A = (byte)((bgra >> 0x18) & 0xff);
            this.R = (byte)((bgra >> 0x10) & 0xff);
            this.G = (byte)((bgra >> 8) & 0xff);
            this.B = (byte)(bgra & 0xff);
        }

        public ColorBGRA(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for ColorBGRA.");
            }
            this.B = ToByte(values[0]);
            this.G = ToByte(values[1]);
            this.R = ToByte(values[2]);
            this.A = ToByte(values[3]);
        }

        public ColorBGRA(byte[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for ColorBGRA.");
            }
            this.B = values[0];
            this.G = values[1];
            this.R = values[2];
            this.A = values[3];
        }

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.B;

                    case 1:
                        return this.G;

                    case 2:
                        return this.R;

                    case 3:
                        return this.A;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for ColorBGRA run from 0 to 3, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.B = value;
                        return;

                    case 1:
                        this.G = value;
                        return;

                    case 2:
                        this.R = value;
                        return;

                    case 3:
                        this.A = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for ColorBGRA run from 0 to 3, inclusive.");
            }
        }
        public int ToBgra()
        {
            int num = this.B | (this.G << 8);
            num |= this.R << 0x10;
            return (num | (this.A << 0x18));
        }

        public int ToRgba()
        {
            int num = this.R | (this.G << 8);
            num |= this.B << 0x10;
            return (num | (this.A << 0x18));
        }

        public Vector3 ToVector3()
        {
            return new Vector3(((float)this.R) / 255f, ((float)this.G) / 255f, ((float)this.B) / 255f);
        }

        public Color3 ToColor3()
        {
            return new Color3(((float)this.R) / 255f, ((float)this.G) / 255f, ((float)this.B) / 255f);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(((float)this.R) / 255f, ((float)this.G) / 255f, ((float)this.B) / 255f, ((float)this.A) / 255f);
        }

        public byte[] ToArray()
        {
            return new byte[] { this.B, this.G, this.R, this.A };
        }

        public float GetBrightness()
        {
            float num = ((float)this.R) / 255f;
            float num2 = ((float)this.G) / 255f;
            float num3 = ((float)this.B) / 255f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            return ((num4 + num5) / 2f);
        }

        public float GetHue()
        {
            if ((this.R == this.G) && (this.G == this.B))
            {
                return 0f;
            }
            float num = ((float)this.R) / 255f;
            float num2 = ((float)this.G) / 255f;
            float num3 = ((float)this.B) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            float num6 = num4 - num5;
            if (num == num4)
            {
                num7 = (num2 - num3) / num6;
            }
            else if (num2 == num4)
            {
                num7 = 2f + ((num3 - num) / num6);
            }
            else if (num3 == num4)
            {
                num7 = 4f + ((num - num2) / num6);
            }
            num7 *= 60f;
            if (num7 < 0f)
            {
                num7 += 360f;
            }
            return num7;
        }

        public float GetSaturation()
        {
            float num = ((float)this.R) / 255f;
            float num2 = ((float)this.G) / 255f;
            float num3 = ((float)this.B) / 255f;
            float num7 = 0f;
            float num4 = num;
            float num5 = num;
            if (num2 > num4)
            {
                num4 = num2;
            }
            if (num3 > num4)
            {
                num4 = num3;
            }
            if (num2 < num5)
            {
                num5 = num2;
            }
            if (num3 < num5)
            {
                num5 = num3;
            }
            if (num4 == num5)
            {
                return num7;
            }
            float num6 = (num4 + num5) / 2f;
            if (num6 <= 0.5)
            {
                return ((num4 - num5) / (num4 + num5));
            }
            return ((num4 - num5) / ((2f - num4) - num5));
        }

        public static ColorBGRA FromBgra(int color)
        {
            return new ColorBGRA(color);
        }

        public static ColorBGRA FromBgra(uint color)
        {
            return new ColorBGRA(color);
        }

        public static ColorBGRA FromRgba(int color)
        {
            return new ColorBGRA((byte)(color & 0xff), (byte)((color >> 8) & 0xff), (byte)((color >> 0x10) & 0xff), (byte)((color >> 0x18) & 0xff));
        }

        public static ColorBGRA FromRgba(uint color)
        {
            return FromRgba((int)color);
        }

        public static void Add(ref ColorBGRA left, ref ColorBGRA right, out ColorBGRA result)
        {
            result.A = (byte)(left.A + right.A);
            result.R = (byte)(left.R + right.R);
            result.G = (byte)(left.G + right.G);
            result.B = (byte)(left.B + right.B);
        }

        public static ColorBGRA Add(ColorBGRA left, ColorBGRA right)
        {
            return new ColorBGRA((float)(left.R + right.R), (float)(left.G + right.G), (float)(left.B + right.B), (float)(left.A + right.A));
        }

        public static void Subtract(ref ColorBGRA left, ref ColorBGRA right, out ColorBGRA result)
        {
            result.A = (byte)(left.A - right.A);
            result.R = (byte)(left.R - right.R);
            result.G = (byte)(left.G - right.G);
            result.B = (byte)(left.B - right.B);
        }

        public static ColorBGRA Subtract(ColorBGRA left, ColorBGRA right)
        {
            return new ColorBGRA((float)(left.R - right.R), (float)(left.G - right.G), (float)(left.B - right.B), (float)(left.A - right.A));
        }

        public static void Modulate(ref ColorBGRA left, ref ColorBGRA right, out ColorBGRA result)
        {
            result.A = (byte)(((float)(left.A * right.A)) / 255f);
            result.R = (byte)(((float)(left.R * right.R)) / 255f);
            result.G = (byte)(((float)(left.G * right.G)) / 255f);
            result.B = (byte)(((float)(left.B * right.B)) / 255f);
        }

        public static ColorBGRA Modulate(ColorBGRA left, ColorBGRA right)
        {
            return new ColorBGRA((float)((left.R * right.R) >> 8), (float)((left.G * right.G) >> 8), (float)((left.B * right.B) >> 8), (float)((left.A * right.A) >> 8));
        }

        public static void Scale(ref ColorBGRA value, float scale, out ColorBGRA result)
        {
            result.A = (byte)(value.A * scale);
            result.R = (byte)(value.R * scale);
            result.G = (byte)(value.G * scale);
            result.B = (byte)(value.B * scale);
        }

        public static ColorBGRA Scale(ColorBGRA value, float scale)
        {
            return new ColorBGRA((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }

        public static void Negate(ref ColorBGRA value, out ColorBGRA result)
        {
            result.A = (byte)(0xff - value.A);
            result.R = (byte)(0xff - value.R);
            result.G = (byte)(0xff - value.G);
            result.B = (byte)(0xff - value.B);
        }

        public static ColorBGRA Negate(ColorBGRA value)
        {
            return new ColorBGRA((float)(0xff - value.R), (float)(0xff - value.G), (float)(0xff - value.B), (float)(0xff - value.A));
        }

        public static void Clamp(ref ColorBGRA value, ref ColorBGRA min, ref ColorBGRA max, out ColorBGRA result)
        {
            byte a = value.A;
            a = (a > max.A) ? max.A : a;
            a = (a < min.A) ? min.A : a;
            byte r = value.R;
            r = (r > max.R) ? max.R : r;
            r = (r < min.R) ? min.R : r;
            byte g = value.G;
            g = (g > max.G) ? max.G : g;
            g = (g < min.G) ? min.G : g;
            byte b = value.B;
            b = (b > max.B) ? max.B : b;
            b = (b < min.B) ? min.B : b;
            result = new ColorBGRA(r, g, b, a);
        }

        public static ColorBGRA Clamp(ColorBGRA value, ColorBGRA min, ColorBGRA max)
        {
            ColorBGRA rbgra;
            Clamp(ref value, ref min, ref max, out rbgra);
            return rbgra;
        }

        public static void Lerp(ref ColorBGRA start, ref ColorBGRA end, float amount, out ColorBGRA result)
        {
            result.A = (byte)(start.A + (amount * (end.A - start.A)));
            result.R = (byte)(start.R + (amount * (end.R - start.R)));
            result.G = (byte)(start.G + (amount * (end.G - start.G)));
            result.B = (byte)(start.B + (amount * (end.B - start.B)));
        }

        public static ColorBGRA Lerp(ColorBGRA start, ColorBGRA end, float amount)
        {
            return new ColorBGRA((byte)(start.R + (amount * (end.R - start.R))), (byte)(start.G + (amount * (end.G - start.G))), (byte)(start.B + (amount * (end.B - start.B))), (byte)(start.A + (amount * (end.A - start.A))));
        }

        public static void SmoothStep(ref ColorBGRA start, ref ColorBGRA end, float amount, out ColorBGRA result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.A = (byte)(start.A + ((end.A - start.A) * amount));
            result.R = (byte)(start.R + ((end.R - start.R) * amount));
            result.G = (byte)(start.G + ((end.G - start.G) * amount));
            result.B = (byte)(start.B + ((end.B - start.B) * amount));
        }

        public static ColorBGRA SmoothStep(ColorBGRA start, ColorBGRA end, float amount)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            return new ColorBGRA((byte)(start.R + ((end.R - start.R) * amount)), (byte)(start.G + ((end.G - start.G) * amount)), (byte)(start.B + ((end.B - start.B) * amount)), (byte)(start.A + ((end.A - start.A) * amount)));
        }

        public static void Max(ref ColorBGRA left, ref ColorBGRA right, out ColorBGRA result)
        {
            result.A = (left.A > right.A) ? left.A : right.A;
            result.R = (left.R > right.R) ? left.R : right.R;
            result.G = (left.G > right.G) ? left.G : right.G;
            result.B = (left.B > right.B) ? left.B : right.B;
        }

        public static ColorBGRA Max(ColorBGRA left, ColorBGRA right)
        {
            ColorBGRA rbgra;
            Max(ref left, ref right, out rbgra);
            return rbgra;
        }

        public static void Min(ref ColorBGRA left, ref ColorBGRA right, out ColorBGRA result)
        {
            result.A = (left.A < right.A) ? left.A : right.A;
            result.R = (left.R < right.R) ? left.R : right.R;
            result.G = (left.G < right.G) ? left.G : right.G;
            result.B = (left.B < right.B) ? left.B : right.B;
        }

        public static ColorBGRA Min(ColorBGRA left, ColorBGRA right)
        {
            ColorBGRA rbgra;
            Min(ref left, ref right, out rbgra);
            return rbgra;
        }

        public static void AdjustContrast(ref ColorBGRA value, float contrast, out ColorBGRA result)
        {
            result.A = value.A;
            result.R = ToByte(0.5f + (contrast * ((((float)value.R) / 255f) - 0.5f)));
            result.G = ToByte(0.5f + (contrast * ((((float)value.G) / 255f) - 0.5f)));
            result.B = ToByte(0.5f + (contrast * ((((float)value.B) / 255f) - 0.5f)));
        }

        public static ColorBGRA AdjustContrast(ColorBGRA value, float contrast)
        {
            return new ColorBGRA(ToByte(0.5f + (contrast * ((((float)value.R) / 255f) - 0.5f))), ToByte(0.5f + (contrast * ((((float)value.G) / 255f) - 0.5f))), ToByte(0.5f + (contrast * ((((float)value.B) / 255f) - 0.5f))), value.A);
        }

        public static void AdjustSaturation(ref ColorBGRA value, float saturation, out ColorBGRA result)
        {
            float num = (((((float)value.R) / 255f) * 0.2125f) + ((((float)value.G) / 255f) * 0.7154f)) + ((((float)value.B) / 255f) * 0.0721f);
            result.A = value.A;
            result.R = ToByte(num + (saturation * ((((float)value.R) / 255f) - num)));
            result.G = ToByte(num + (saturation * ((((float)value.G) / 255f) - num)));
            result.B = ToByte(num + (saturation * ((((float)value.B) / 255f) - num)));
        }

        public static ColorBGRA AdjustSaturation(ColorBGRA value, float saturation)
        {
            float num = (((((float)value.R) / 255f) * 0.2125f) + ((((float)value.G) / 255f) * 0.7154f)) + ((((float)value.B) / 255f) * 0.0721f);
            return new ColorBGRA(ToByte(num + (saturation * ((((float)value.R) / 255f) - num))), ToByte(num + (saturation * ((((float)value.G) / 255f) - num))), ToByte(num + (saturation * ((((float)value.B) / 255f) - num))), value.A);
        }

        public static ColorBGRA operator +(ColorBGRA left, ColorBGRA right)
        {
            return new ColorBGRA((float)(left.R + right.R), (float)(left.G + right.G), (float)(left.B + right.B), (float)(left.A + right.A));
        }

        public static ColorBGRA operator +(ColorBGRA value)
        {
            return value;
        }

        public static ColorBGRA operator -(ColorBGRA left, ColorBGRA right)
        {
            return new ColorBGRA((float)(left.R - right.R), (float)(left.G - right.G), (float)(left.B - right.B), (float)(left.A - right.A));
        }

        public static ColorBGRA operator -(ColorBGRA value)
        {
            return new ColorBGRA((float)-value.R, (float)-value.G, (float)-value.B, (float)-value.A);
        }

        public static ColorBGRA operator *(float scale, ColorBGRA value)
        {
            return new ColorBGRA((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }

        public static ColorBGRA operator *(ColorBGRA value, float scale)
        {
            return new ColorBGRA((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }

        public static ColorBGRA operator *(ColorBGRA left, ColorBGRA right)
        {
            return new ColorBGRA((byte)(((float)(left.R * right.R)) / 255f), (byte)(((float)(left.G * right.G)) / 255f), (byte)(((float)(left.B * right.B)) / 255f), (byte)(((float)(left.A * right.A)) / 255f));
        }

        public static bool operator ==(ColorBGRA left, ColorBGRA right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ColorBGRA left, ColorBGRA right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Color3(ColorBGRA value)
        {
            return new Color3((float)value.R, (float)value.G, (float)value.B);
        }

        public static explicit operator Vector3(ColorBGRA value)
        {
            return new Vector3(((float)value.R) / 255f, ((float)value.G) / 255f, ((float)value.B) / 255f);
        }

        public static explicit operator Vector4(ColorBGRA value)
        {
            return new Vector4(((float)value.R) / 255f, ((float)value.G) / 255f, ((float)value.B) / 255f, ((float)value.A) / 255f);
        }

        public static explicit operator Color4(ColorBGRA value)
        {
            return new Color4(((float)value.R) / 255f, ((float)value.G) / 255f, ((float)value.B) / 255f, ((float)value.A) / 255f);
        }

        public static explicit operator ColorBGRA(Vector3 value)
        {
            return new ColorBGRA(value.X / 255f, value.Y / 255f, value.Z / 255f, 1f);
        }

        public static explicit operator ColorBGRA(Color3 value)
        {
            return new ColorBGRA(value.Red, value.Green, value.Blue, 1f);
        }

        public static explicit operator ColorBGRA(Vector4 value)
        {
            return new ColorBGRA(value.X, value.Y, value.Z, value.W);
        }

        public static explicit operator ColorBGRA(Color4 value)
        {
            return new ColorBGRA(value.Red, value.Green, value.Blue, value.Alpha);
        }

        public static implicit operator ColorBGRA(Color value)
        {
            return new ColorBGRA(value.R, value.G, value.B, value.A);
        }

        public static implicit operator Color(ColorBGRA value)
        {
            return new Color(value.R, value.G, value.B, value.A);
        }

        public static explicit operator int(ColorBGRA value)
        {
            return value.ToBgra();
        }

        public static explicit operator ColorBGRA(int value)
        {
            return new ColorBGRA(value);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "A:{0} R:{1} G:{2} B:{3}", new object[] { (byte)this.A, (byte)this.R, (byte)this.G, (byte)this.B });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "A:{0} R:{1} G:{2} B:{3}", new object[] { ((byte)this.A).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((byte)this.R).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((byte)this.G).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((byte)this.B).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "A:{0} R:{1} G:{2} B:{3}", new object[] { (byte)this.A, (byte)this.R, (byte)this.G, (byte)this.B });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "A:{0} R:{1} G:{2} B:{3}", new object[] { ((byte)this.A).ToString(format, formatProvider), ((byte)this.R).ToString(format, formatProvider), ((byte)this.G).ToString(format, formatProvider), ((byte)this.B).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (((((byte)this.A).GetHashCode() + ((byte)this.R).GetHashCode()) + ((byte)this.G).GetHashCode()) + ((byte)this.B).GetHashCode());
        }

        public bool Equals(ColorBGRA other)
        {
            return ((((this.R == other.R) && (this.G == other.G)) && (this.B == other.B)) && (this.A == other.A));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(ColorBGRA)))
            {
                return false;
            }
            return this.Equals((ColorBGRA)value);
        }

        private static byte ToByte(float component)
        {
            int num = (int)(component * 255f);
            return ((num < 0) ? ((byte)0) : ((num > 0xff) ? ((byte)0xff) : ((byte)num)));
        }
    }
}
