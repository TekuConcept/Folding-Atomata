using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct Color : IEquatable<Color>, IFormattable
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        public static readonly Color Zero;
        public static readonly Color Transparent;
        public static readonly Color AliceBlue;
        public static readonly Color AntiqueWhite;
        public static readonly Color Aqua;
        public static readonly Color Aquamarine;
        public static readonly Color Azure;
        public static readonly Color Beige;
        public static readonly Color Bisque;
        public static readonly Color Black;
        public static readonly Color BlanchedAlmond;
        public static readonly Color Blue;
        public static readonly Color BlueViolet;
        public static readonly Color Brown;
        public static readonly Color BurlyWood;
        public static readonly Color CadetBlue;
        public static readonly Color Chartreuse;
        public static readonly Color Chocolate;
        public static readonly Color Coral;
        public static readonly Color CornflowerBlue;
        public static readonly Color Cornsilk;
        public static readonly Color Crimson;
        public static readonly Color Cyan;
        public static readonly Color DarkBlue;
        public static readonly Color DarkCyan;
        public static readonly Color DarkGoldenrod;
        public static readonly Color DarkGray;
        public static readonly Color DarkGreen;
        public static readonly Color DarkKhaki;
        public static readonly Color DarkMagenta;
        public static readonly Color DarkOliveGreen;
        public static readonly Color DarkOrange;
        public static readonly Color DarkOrchid;
        public static readonly Color DarkRed;
        public static readonly Color DarkSalmon;
        public static readonly Color DarkSeaGreen;
        public static readonly Color DarkSlateBlue;
        public static readonly Color DarkSlateGray;
        public static readonly Color DarkTurquoise;
        public static readonly Color DarkViolet;
        public static readonly Color DeepPink;
        public static readonly Color DeepSkyBlue;
        public static readonly Color DimGray;
        public static readonly Color DodgerBlue;
        public static readonly Color Firebrick;
        public static readonly Color FloralWhite;
        public static readonly Color ForestGreen;
        public static readonly Color Fuchsia;
        public static readonly Color Gainsboro;
        public static readonly Color GhostWhite;
        public static readonly Color Gold;
        public static readonly Color Goldenrod;
        public static readonly Color Gray;
        public static readonly Color Green;
        public static readonly Color GreenYellow;
        public static readonly Color Honeydew;
        public static readonly Color HotPink;
        public static readonly Color IndianRed;
        public static readonly Color Indigo;
        public static readonly Color Ivory;
        public static readonly Color Khaki;
        public static readonly Color Lavender;
        public static readonly Color LavenderBlush;
        public static readonly Color LawnGreen;
        public static readonly Color LemonChiffon;
        public static readonly Color LightBlue;
        public static readonly Color LightCoral;
        public static readonly Color LightCyan;
        public static readonly Color LightGoldenrodYellow;
        public static readonly Color LightGray;
        public static readonly Color LightGreen;
        public static readonly Color LightPink;
        public static readonly Color LightSalmon;
        public static readonly Color LightSeaGreen;
        public static readonly Color LightSkyBlue;
        public static readonly Color LightSlateGray;
        public static readonly Color LightSteelBlue;
        public static readonly Color LightYellow;
        public static readonly Color Lime;
        public static readonly Color LimeGreen;
        public static readonly Color Linen;
        public static readonly Color Magenta;
        public static readonly Color Maroon;
        public static readonly Color MediumAquamarine;
        public static readonly Color MediumBlue;
        public static readonly Color MediumOrchid;
        public static readonly Color MediumPurple;
        public static readonly Color MediumSeaGreen;
        public static readonly Color MediumSlateBlue;
        public static readonly Color MediumSpringGreen;
        public static readonly Color MediumTurquoise;
        public static readonly Color MediumVioletRed;
        public static readonly Color MidnightBlue;
        public static readonly Color MintCream;
        public static readonly Color MistyRose;
        public static readonly Color Moccasin;
        public static readonly Color NavajoWhite;
        public static readonly Color Navy;
        public static readonly Color OldLace;
        public static readonly Color Olive;
        public static readonly Color OliveDrab;
        public static readonly Color Orange;
        public static readonly Color OrangeRed;
        public static readonly Color Orchid;
        public static readonly Color PaleGoldenrod;
        public static readonly Color PaleGreen;
        public static readonly Color PaleTurquoise;
        public static readonly Color PaleVioletRed;
        public static readonly Color PapayaWhip;
        public static readonly Color PeachPuff;
        public static readonly Color Peru;
        public static readonly Color Pink;
        public static readonly Color Plum;
        public static readonly Color PowderBlue;
        public static readonly Color Purple;
        public static readonly Color Red;
        public static readonly Color RosyBrown;
        public static readonly Color RoyalBlue;
        public static readonly Color SaddleBrown;
        public static readonly Color Salmon;
        public static readonly Color SandyBrown;
        public static readonly Color SeaGreen;
        public static readonly Color SeaShell;
        public static readonly Color Sienna;
        public static readonly Color Silver;
        public static readonly Color SkyBlue;
        public static readonly Color SlateBlue;
        public static readonly Color SlateGray;
        public static readonly Color Snow;
        public static readonly Color SpringGreen;
        public static readonly Color SteelBlue;
        public static readonly Color Tan;
        public static readonly Color Teal;
        public static readonly Color Thistle;
        public static readonly Color Tomato;
        public static readonly Color Turquoise;
        public static readonly Color Violet;
        public static readonly Color Wheat;
        public static readonly Color White;
        public static readonly Color WhiteSmoke;
        public static readonly Color Yellow;
        public static readonly Color YellowGreen;
        public Color(byte value)
        {
            this.A = this.R = this.G = this.B = value;
        }

        public Color(float value)
        {
            this.A = this.R = this.G = this.B = ToByte(value);
        }

        public Color(byte red, byte green, byte blue, byte alpha)
        {
            this.R = red;
            this.G = green;
            this.B = blue;
            this.A = alpha;
        }

        public Color(float red, float green, float blue, float alpha)
        {
            this.R = ToByte(red);
            this.G = ToByte(green);
            this.B = ToByte(blue);
            this.A = ToByte(alpha);
        }

        public Color(Vector4 value)
        {
            this.R = ToByte(value.X);
            this.G = ToByte(value.Y);
            this.B = ToByte(value.Z);
            this.A = ToByte(value.W);
        }

        public Color(Vector3 value, float alpha)
        {
            this.R = ToByte(value.X);
            this.G = ToByte(value.Y);
            this.B = ToByte(value.Z);
            this.A = ToByte(alpha);
        }

        public Color(uint rgba)
        {
            this.A = (byte)((rgba >> 0x18) & 0xff);
            this.B = (byte)((rgba >> 0x10) & 0xff);
            this.G = (byte)((rgba >> 8) & 0xff);
            this.R = (byte)(rgba & 0xff);
        }

        public Color(int rgba)
        {
            this.A = (byte)((rgba >> 0x18) & 0xff);
            this.B = (byte)((rgba >> 0x10) & 0xff);
            this.G = (byte)((rgba >> 8) & 0xff);
            this.R = (byte)(rgba & 0xff);
        }

        public Color(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Color.");
            }
            this.R = ToByte(values[0]);
            this.G = ToByte(values[1]);
            this.B = ToByte(values[2]);
            this.A = ToByte(values[3]);
        }

        public Color(byte[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Color.");
            }
            this.R = values[0];
            this.G = values[1];
            this.B = values[2];
            this.A = values[3];
        }

        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.R;

                    case 1:
                        return this.G;

                    case 2:
                        return this.B;

                    case 3:
                        return this.A;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Color run from 0 to 3, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.R = value;
                        return;

                    case 1:
                        this.G = value;
                        return;

                    case 2:
                        this.B = value;
                        return;

                    case 3:
                        this.A = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Color run from 0 to 3, inclusive.");
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
            return new byte[] { this.R, this.G, this.B, this.A };
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

        public static void Add(ref Color left, ref Color right, out Color result)
        {
            result.A = (byte)(left.A + right.A);
            result.R = (byte)(left.R + right.R);
            result.G = (byte)(left.G + right.G);
            result.B = (byte)(left.B + right.B);
        }

        public static Color Add(Color left, Color right)
        {
            return new Color((float)(left.R + right.R), (float)(left.G + right.G), (float)(left.B + right.B), (float)(left.A + right.A));
        }

        public static void Subtract(ref Color left, ref Color right, out Color result)
        {
            result.A = (byte)(left.A - right.A);
            result.R = (byte)(left.R - right.R);
            result.G = (byte)(left.G - right.G);
            result.B = (byte)(left.B - right.B);
        }

        public static Color Subtract(Color left, Color right)
        {
            return new Color((float)(left.R - right.R), (float)(left.G - right.G), (float)(left.B - right.B), (float)(left.A - right.A));
        }

        public static void Modulate(ref Color left, ref Color right, out Color result)
        {
            result.A = (byte)(((float)(left.A * right.A)) / 255f);
            result.R = (byte)(((float)(left.R * right.R)) / 255f);
            result.G = (byte)(((float)(left.G * right.G)) / 255f);
            result.B = (byte)(((float)(left.B * right.B)) / 255f);
        }

        public static Color Modulate(Color left, Color right)
        {
            return new Color((float)(left.R * right.R), (float)(left.G * right.G), (float)(left.B * right.B), (float)(left.A * right.A));
        }

        public static void Scale(ref Color value, float scale, out Color result)
        {
            result.A = (byte)(value.A * scale);
            result.R = (byte)(value.R * scale);
            result.G = (byte)(value.G * scale);
            result.B = (byte)(value.B * scale);
        }

        public static Color Scale(Color value, float scale)
        {
            return new Color((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }

        public static void Negate(ref Color value, out Color result)
        {
            result.A = (byte)(0xff - value.A);
            result.R = (byte)(0xff - value.R);
            result.G = (byte)(0xff - value.G);
            result.B = (byte)(0xff - value.B);
        }

        public static Color Negate(Color value)
        {
            return new Color((float)(0xff - value.R), (float)(0xff - value.G), (float)(0xff - value.B), (float)(0xff - value.A));
        }

        public static void Clamp(ref Color value, ref Color min, ref Color max, out Color result)
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
            result = new Color(r, g, b, a);
        }

        public static Color FromBgra(int color)
        {
            return new Color((byte)((color >> 0x10) & 0xff), (byte)((color >> 8) & 0xff), (byte)(color & 0xff), (byte)((color >> 0x18) & 0xff));
        }

        public static Color FromBgra(uint color)
        {
            return FromBgra((int)color);
        }

        public static Color FromRgba(int color)
        {
            return new Color(color);
        }

        public static Color FromRgba(uint color)
        {
            return new Color(color);
        }

        public static Color Clamp(Color value, Color min, Color max)
        {
            Color color;
            Clamp(ref value, ref min, ref max, out color);
            return color;
        }

        public static void Lerp(ref Color start, ref Color end, float amount, out Color result)
        {
            result.A = (byte)(start.A + (amount * (end.A - start.A)));
            result.R = (byte)(start.R + (amount * (end.R - start.R)));
            result.G = (byte)(start.G + (amount * (end.G - start.G)));
            result.B = (byte)(start.B + (amount * (end.B - start.B)));
        }

        public static Color Lerp(Color start, Color end, float amount)
        {
            return new Color((byte)(start.R + (amount * (end.R - start.R))), (byte)(start.G + (amount * (end.G - start.G))), (byte)(start.B + (amount * (end.B - start.B))), (byte)(start.A + (amount * (end.A - start.A))));
        }

        public static void SmoothStep(ref Color start, ref Color end, float amount, out Color result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.A = (byte)(start.A + ((end.A - start.A) * amount));
            result.R = (byte)(start.R + ((end.R - start.R) * amount));
            result.G = (byte)(start.G + ((end.G - start.G) * amount));
            result.B = (byte)(start.B + ((end.B - start.B) * amount));
        }

        public static Color SmoothStep(Color start, Color end, float amount)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            return new Color((byte)(start.R + ((end.R - start.R) * amount)), (byte)(start.G + ((end.G - start.G) * amount)), (byte)(start.B + ((end.B - start.B) * amount)), (byte)(start.A + ((end.A - start.A) * amount)));
        }

        public static void Max(ref Color left, ref Color right, out Color result)
        {
            result.A = (left.A > right.A) ? left.A : right.A;
            result.R = (left.R > right.R) ? left.R : right.R;
            result.G = (left.G > right.G) ? left.G : right.G;
            result.B = (left.B > right.B) ? left.B : right.B;
        }

        public static Color Max(Color left, Color right)
        {
            Color color;
            Max(ref left, ref right, out color);
            return color;
        }

        public static void Min(ref Color left, ref Color right, out Color result)
        {
            result.A = (left.A < right.A) ? left.A : right.A;
            result.R = (left.R < right.R) ? left.R : right.R;
            result.G = (left.G < right.G) ? left.G : right.G;
            result.B = (left.B < right.B) ? left.B : right.B;
        }

        public static Color Min(Color left, Color right)
        {
            Color color;
            Min(ref left, ref right, out color);
            return color;
        }

        public static void AdjustContrast(ref Color value, float contrast, out Color result)
        {
            result.A = value.A;
            result.R = ToByte(0.5f + (contrast * ((((float)value.R) / 255f) - 0.5f)));
            result.G = ToByte(0.5f + (contrast * ((((float)value.G) / 255f) - 0.5f)));
            result.B = ToByte(0.5f + (contrast * ((((float)value.B) / 255f) - 0.5f)));
        }

        public static Color AdjustContrast(Color value, float contrast)
        {
            return new Color(ToByte(0.5f + (contrast * ((((float)value.R) / 255f) - 0.5f))), ToByte(0.5f + (contrast * ((((float)value.G) / 255f) - 0.5f))), ToByte(0.5f + (contrast * ((((float)value.B) / 255f) - 0.5f))), value.A);
        }

        public static void AdjustSaturation(ref Color value, float saturation, out Color result)
        {
            float num = (((((float)value.R) / 255f) * 0.2125f) + ((((float)value.G) / 255f) * 0.7154f)) + ((((float)value.B) / 255f) * 0.0721f);
            result.A = value.A;
            result.R = ToByte(num + (saturation * ((((float)value.R) / 255f) - num)));
            result.G = ToByte(num + (saturation * ((((float)value.G) / 255f) - num)));
            result.B = ToByte(num + (saturation * ((((float)value.B) / 255f) - num)));
        }

        public static Color AdjustSaturation(Color value, float saturation)
        {
            float num = (((((float)value.R) / 255f) * 0.2125f) + ((((float)value.G) / 255f) * 0.7154f)) + ((((float)value.B) / 255f) * 0.0721f);
            return new Color(ToByte(num + (saturation * ((((float)value.R) / 255f) - num))), ToByte(num + (saturation * ((((float)value.G) / 255f) - num))), ToByte(num + (saturation * ((((float)value.B) / 255f) - num))), value.A);
        }

        public static Color operator +(Color left, Color right)
        {
            return new Color((float)(left.R + right.R), (float)(left.G + right.G), (float)(left.B + right.B), (float)(left.A + right.A));
        }

        public static Color operator +(Color value)
        {
            return value;
        }

        public static Color operator -(Color left, Color right)
        {
            return new Color((float)(left.R - right.R), (float)(left.G - right.G), (float)(left.B - right.B), (float)(left.A - right.A));
        }

        public static Color operator -(Color value)
        {
            return new Color((float)-value.R, (float)-value.G, (float)-value.B, (float)-value.A);
        }

        public static Color operator *(float scale, Color value)
        {
            return new Color((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }

        public static Color operator *(Color value, float scale)
        {
            return new Color((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }

        public static Color operator *(Color left, Color right)
        {
            return new Color((byte)(((float)(left.R * right.R)) / 255f), (byte)(((float)(left.G * right.G)) / 255f), (byte)(((float)(left.B * right.B)) / 255f), (byte)(((float)(left.A * right.A)) / 255f));
        }

        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Color3(Color value)
        {
            return new Color3((float)value.R, (float)value.G, (float)value.B);
        }

        public static explicit operator Vector3(Color value)
        {
            return new Vector3(((float)value.R) / 255f, ((float)value.G) / 255f, ((float)value.B) / 255f);
        }

        public static explicit operator Vector4(Color value)
        {
            return new Vector4(((float)value.R) / 255f, ((float)value.G) / 255f, ((float)value.B) / 255f, ((float)value.A) / 255f);
        }

        public static implicit operator Color4(Color value)
        {
            return new Color4(((float)value.R) / 255f, ((float)value.G) / 255f, ((float)value.B) / 255f, ((float)value.A) / 255f);
        }

        public static explicit operator Color(Vector3 value)
        {
            return new Color(value.X, value.Y, value.Z, 1f);
        }

        public static explicit operator Color(Color3 value)
        {
            return new Color(value.Red, value.Green, value.Blue, 1f);
        }

        public static explicit operator Color(Vector4 value)
        {
            return new Color(value.X, value.Y, value.Z, value.W);
        }

        public static explicit operator Color(Color4 value)
        {
            return new Color(value.Red, value.Green, value.Blue, value.Alpha);
        }

        public static explicit operator int(Color value)
        {
            return value.ToRgba();
        }

        public static explicit operator Color(int value)
        {
            return new Color(value);
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

        public bool Equals(Color other)
        {
            return ((((this.R == other.R) && (this.G == other.G)) && (this.B == other.B)) && (this.A == other.A));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Color)))
            {
                return false;
            }
            return this.Equals((Color)value);
        }

        private static byte ToByte(float component)
        {
            int num = (int)(component * 255f);
            return ((num < 0) ? ((byte)0) : ((num > 0xff) ? ((byte)0xff) : ((byte)num)));
        }

        static Color()
        {
            Zero = FromBgra(0);
            Transparent = FromBgra(0xffffff);
            AliceBlue = FromBgra((uint)0xfff0f8ff);
            AntiqueWhite = FromBgra((uint)0xfffaebd7);
            Aqua = FromBgra((uint)0xff00ffff);
            Aquamarine = FromBgra((uint)0xff7fffd4);
            Azure = FromBgra((uint)0xfff0ffff);
            Beige = FromBgra((uint)0xfff5f5dc);
            Bisque = FromBgra((uint)0xffffe4c4);
            Black = FromBgra((uint)0xff000000);
            BlanchedAlmond = FromBgra((uint)0xffffebcd);
            Blue = FromBgra((uint)0xff0000ff);
            BlueViolet = FromBgra((uint)0xff8a2be2);
            Brown = FromBgra((uint)0xffa52a2a);
            BurlyWood = FromBgra((uint)0xffdeb887);
            CadetBlue = FromBgra((uint)0xff5f9ea0);
            Chartreuse = FromBgra((uint)0xff7fff00);
            Chocolate = FromBgra((uint)0xffd2691e);
            Coral = FromBgra((uint)0xffff7f50);
            CornflowerBlue = FromBgra((uint)0xff6495ed);
            Cornsilk = FromBgra((uint)0xfffff8dc);
            Crimson = FromBgra((uint)0xffdc143c);
            Cyan = FromBgra((uint)0xff00ffff);
            DarkBlue = FromBgra((uint)0xff00008b);
            DarkCyan = FromBgra((uint)0xff008b8b);
            DarkGoldenrod = FromBgra((uint)0xffb8860b);
            DarkGray = FromBgra((uint)0xffa9a9a9);
            DarkGreen = FromBgra((uint)0xff006400);
            DarkKhaki = FromBgra((uint)0xffbdb76b);
            DarkMagenta = FromBgra((uint)0xff8b008b);
            DarkOliveGreen = FromBgra((uint)0xff556b2f);
            DarkOrange = FromBgra((uint)0xffff8c00);
            DarkOrchid = FromBgra((uint)0xff9932cc);
            DarkRed = FromBgra((uint)0xff8b0000);
            DarkSalmon = FromBgra((uint)0xffe9967a);
            DarkSeaGreen = FromBgra((uint)0xff8fbc8b);
            DarkSlateBlue = FromBgra((uint)0xff483d8b);
            DarkSlateGray = FromBgra((uint)0xff2f4f4f);
            DarkTurquoise = FromBgra((uint)0xff00ced1);
            DarkViolet = FromBgra((uint)0xff9400d3);
            DeepPink = FromBgra((uint)0xffff1493);
            DeepSkyBlue = FromBgra((uint)0xff00bfff);
            DimGray = FromBgra((uint)0xff696969);
            DodgerBlue = FromBgra((uint)0xff1e90ff);
            Firebrick = FromBgra((uint)0xffb22222);
            FloralWhite = FromBgra((uint)0xfffffaf0);
            ForestGreen = FromBgra((uint)0xff228b22);
            Fuchsia = FromBgra((uint)0xffff00ff);
            Gainsboro = FromBgra((uint)0xffdcdcdc);
            GhostWhite = FromBgra((uint)0xfff8f8ff);
            Gold = FromBgra((uint)0xffffd700);
            Goldenrod = FromBgra((uint)0xffdaa520);
            Gray = FromBgra((uint)0xff808080);
            Green = FromBgra((uint)0xff008000);
            GreenYellow = FromBgra((uint)0xffadff2f);
            Honeydew = FromBgra((uint)0xfff0fff0);
            HotPink = FromBgra((uint)0xffff69b4);
            IndianRed = FromBgra((uint)0xffcd5c5c);
            Indigo = FromBgra((uint)0xff4b0082);
            Ivory = FromBgra((uint)0xfffffff0);
            Khaki = FromBgra((uint)0xfff0e68c);
            Lavender = FromBgra((uint)0xffe6e6fa);
            LavenderBlush = FromBgra((uint)0xfffff0f5);
            LawnGreen = FromBgra((uint)0xff7cfc00);
            LemonChiffon = FromBgra((uint)0xfffffacd);
            LightBlue = FromBgra((uint)0xffadd8e6);
            LightCoral = FromBgra((uint)0xfff08080);
            LightCyan = FromBgra((uint)0xffe0ffff);
            LightGoldenrodYellow = FromBgra((uint)0xfffafad2);
            LightGray = FromBgra((uint)0xffd3d3d3);
            LightGreen = FromBgra((uint)0xff90ee90);
            LightPink = FromBgra((uint)0xffffb6c1);
            LightSalmon = FromBgra((uint)0xffffa07a);
            LightSeaGreen = FromBgra((uint)0xff20b2aa);
            LightSkyBlue = FromBgra((uint)0xff87cefa);
            LightSlateGray = FromBgra((uint)0xff778899);
            LightSteelBlue = FromBgra((uint)0xffb0c4de);
            LightYellow = FromBgra((uint)0xffffffe0);
            Lime = FromBgra((uint)0xff00ff00);
            LimeGreen = FromBgra((uint)0xff32cd32);
            Linen = FromBgra((uint)0xfffaf0e6);
            Magenta = FromBgra((uint)0xffff00ff);
            Maroon = FromBgra((uint)0xff800000);
            MediumAquamarine = FromBgra((uint)0xff66cdaa);
            MediumBlue = FromBgra((uint)0xff0000cd);
            MediumOrchid = FromBgra((uint)0xffba55d3);
            MediumPurple = FromBgra((uint)0xff9370db);
            MediumSeaGreen = FromBgra((uint)0xff3cb371);
            MediumSlateBlue = FromBgra((uint)0xff7b68ee);
            MediumSpringGreen = FromBgra((uint)0xff00fa9a);
            MediumTurquoise = FromBgra((uint)0xff48d1cc);
            MediumVioletRed = FromBgra((uint)0xffc71585);
            MidnightBlue = FromBgra((uint)0xff191970);
            MintCream = FromBgra((uint)0xfff5fffa);
            MistyRose = FromBgra((uint)0xffffe4e1);
            Moccasin = FromBgra((uint)0xffffe4b5);
            NavajoWhite = FromBgra((uint)0xffffdead);
            Navy = FromBgra((uint)0xff000080);
            OldLace = FromBgra((uint)0xfffdf5e6);
            Olive = FromBgra((uint)0xff808000);
            OliveDrab = FromBgra((uint)0xff6b8e23);
            Orange = FromBgra((uint)0xffffa500);
            OrangeRed = FromBgra((uint)0xffff4500);
            Orchid = FromBgra((uint)0xffda70d6);
            PaleGoldenrod = FromBgra((uint)0xffeee8aa);
            PaleGreen = FromBgra((uint)0xff98fb98);
            PaleTurquoise = FromBgra((uint)0xffafeeee);
            PaleVioletRed = FromBgra((uint)0xffdb7093);
            PapayaWhip = FromBgra((uint)0xffffefd5);
            PeachPuff = FromBgra((uint)0xffffdab9);
            Peru = FromBgra((uint)0xffcd853f);
            Pink = FromBgra((uint)0xffffc0cb);
            Plum = FromBgra((uint)0xffdda0dd);
            PowderBlue = FromBgra((uint)0xffb0e0e6);
            Purple = FromBgra((uint)0xff800080);
            Red = FromBgra((uint)0xffff0000);
            RosyBrown = FromBgra((uint)0xffbc8f8f);
            RoyalBlue = FromBgra((uint)0xff4169e1);
            SaddleBrown = FromBgra((uint)0xff8b4513);
            Salmon = FromBgra((uint)0xfffa8072);
            SandyBrown = FromBgra((uint)0xfff4a460);
            SeaGreen = FromBgra((uint)0xff2e8b57);
            SeaShell = FromBgra((uint)0xfffff5ee);
            Sienna = FromBgra((uint)0xffa0522d);
            Silver = FromBgra((uint)0xffc0c0c0);
            SkyBlue = FromBgra((uint)0xff87ceeb);
            SlateBlue = FromBgra((uint)0xff6a5acd);
            SlateGray = FromBgra((uint)0xff708090);
            Snow = FromBgra((uint)0xfffffafa);
            SpringGreen = FromBgra((uint)0xff00ff7f);
            SteelBlue = FromBgra((uint)0xff4682b4);
            Tan = FromBgra((uint)0xffd2b48c);
            Teal = FromBgra((uint)0xff008080);
            Thistle = FromBgra((uint)0xffd8bfd8);
            Tomato = FromBgra((uint)0xffff6347);
            Turquoise = FromBgra((uint)0xff40e0d0);
            Violet = FromBgra((uint)0xffee82ee);
            Wheat = FromBgra((uint)0xfff5deb3);
            White = FromBgra(uint.MaxValue);
            WhiteSmoke = FromBgra((uint)0xfff5f5f5);
            Yellow = FromBgra((uint)0xffffff00);
            YellowGreen = FromBgra((uint)0xff9acd32);
        }
    }
}
