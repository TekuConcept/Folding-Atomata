using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix5x4 : IEquatable<Matrix5x4>, IFormattable
    {
        public static readonly int SizeInBytes;
        public static readonly Matrix5x4 Zero;
        public static readonly Matrix5x4 Identity;
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;
        public float M51;
        public float M52;
        public float M53;
        public float M54;
        public Matrix5x4(float value)
        {
            this.M11 = this.M12 = this.M13 = this.M14 = this.M21 = this.M22 = this.M23 = this.M24 = this.M31 = this.M32 = this.M33 = this.M34 = this.M41 = this.M42 = this.M43 = this.M44 = this.M51 = this.M52 = this.M53 = this.M54 = value;
        }

        public Matrix5x4(float M11, float M12, float M13, float M14, float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44, float M51, float M52, float M53, float M54)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M14 = M14;
            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M24 = M24;
            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
            this.M34 = M34;
            this.M41 = M41;
            this.M42 = M42;
            this.M43 = M43;
            this.M44 = M44;
            this.M51 = M51;
            this.M52 = M52;
            this.M53 = M53;
            this.M54 = M54;
        }

        public Matrix5x4(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 20)
            {
                throw new ArgumentOutOfRangeException("values", "There must be 20 input values for Matrix5x4.");
            }
            this.M11 = values[0];
            this.M12 = values[1];
            this.M13 = values[2];
            this.M14 = values[3];
            this.M21 = values[4];
            this.M22 = values[5];
            this.M23 = values[6];
            this.M24 = values[7];
            this.M31 = values[8];
            this.M32 = values[9];
            this.M33 = values[10];
            this.M34 = values[11];
            this.M41 = values[12];
            this.M42 = values[13];
            this.M43 = values[14];
            this.M44 = values[15];
            this.M51 = values[0x10];
            this.M52 = values[0x11];
            this.M53 = values[0x12];
            this.M54 = values[0x13];
        }

        public Vector4 Row1
        {
            get
            {
                return new Vector4(this.M11, this.M12, this.M13, this.M14);
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
                this.M14 = value.W;
            }
        }
        public Vector4 Row2
        {
            get
            {
                return new Vector4(this.M21, this.M22, this.M23, this.M24);
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
                this.M24 = value.W;
            }
        }
        public Vector4 Row3
        {
            get
            {
                return new Vector4(this.M31, this.M32, this.M33, this.M34);
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
                this.M34 = value.W;
            }
        }
        public Vector4 Row4
        {
            get
            {
                return new Vector4(this.M41, this.M42, this.M43, this.M44);
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
                this.M44 = value.W;
            }
        }
        public Vector4 Row5
        {
            get
            {
                return new Vector4(this.M51, this.M52, this.M53, this.M54);
            }
            set
            {
                this.M51 = value.X;
                this.M52 = value.Y;
                this.M53 = value.Z;
                this.M54 = value.W;
            }
        }
        public Vector4 TranslationVector
        {
            get
            {
                return new Vector4(this.M51, this.M52, this.M53, this.M54);
            }
            set
            {
                this.M51 = value.X;
                this.M52 = value.Y;
                this.M53 = value.Z;
                this.M54 = value.W;
            }
        }
        public Vector4 ScaleVector
        {
            get
            {
                return new Vector4(this.M11, this.M22, this.M33, this.M44);
            }
            set
            {
                this.M11 = value.X;
                this.M22 = value.Y;
                this.M33 = value.Z;
                this.M44 = value.W;
            }
        }
        public bool IsIdentity
        {
            get
            {
                return this.Equals(Identity);
            }
        }
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.M11;

                    case 1:
                        return this.M12;

                    case 2:
                        return this.M13;

                    case 3:
                        return this.M14;

                    case 4:
                        return this.M21;

                    case 5:
                        return this.M22;

                    case 6:
                        return this.M23;

                    case 7:
                        return this.M24;

                    case 8:
                        return this.M31;

                    case 9:
                        return this.M32;

                    case 10:
                        return this.M33;

                    case 11:
                        return this.M34;

                    case 12:
                        return this.M41;

                    case 13:
                        return this.M42;

                    case 14:
                        return this.M43;

                    case 15:
                        return this.M44;

                    case 0x10:
                        return this.M51;

                    case 0x11:
                        return this.M52;

                    case 0x12:
                        return this.M53;

                    case 0x13:
                        return this.M54;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Matrix5x4 run from 0 to 19, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.M11 = value;
                        return;

                    case 1:
                        this.M12 = value;
                        return;

                    case 2:
                        this.M13 = value;
                        return;

                    case 3:
                        this.M14 = value;
                        return;

                    case 4:
                        this.M21 = value;
                        return;

                    case 5:
                        this.M22 = value;
                        return;

                    case 6:
                        this.M23 = value;
                        return;

                    case 7:
                        this.M24 = value;
                        return;

                    case 8:
                        this.M31 = value;
                        return;

                    case 9:
                        this.M32 = value;
                        return;

                    case 10:
                        this.M33 = value;
                        return;

                    case 11:
                        this.M34 = value;
                        return;

                    case 12:
                        this.M41 = value;
                        return;

                    case 13:
                        this.M42 = value;
                        return;

                    case 14:
                        this.M43 = value;
                        return;

                    case 15:
                        this.M44 = value;
                        return;

                    case 0x10:
                        this.M51 = value;
                        return;

                    case 0x11:
                        this.M52 = value;
                        return;

                    case 0x12:
                        this.M53 = value;
                        return;

                    case 0x13:
                        this.M54 = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Matrix5x4 run from 0 to 19, inclusive.");
            }
        }
        public float this[int row, int column]
        {
            get
            {
                if ((row < 0) || (row > 4))
                {
                    throw new ArgumentOutOfRangeException("row", "Rows for matrices run from 0 to 4, inclusive.");
                }
                if ((column < 0) || (column > 3))
                {
                    throw new ArgumentOutOfRangeException("column", "Columns for matrices run from 0 to 3, inclusive.");
                }
                return this[(row * 4) + column];
            }
            set
            {
                if ((row < 0) || (row > 4))
                {
                    throw new ArgumentOutOfRangeException("row", "Rows for matrices run from 0 to 4, inclusive.");
                }
                if ((column < 0) || (column > 3))
                {
                    throw new ArgumentOutOfRangeException("column", "Columns for matrices run from 0 to 3, inclusive.");
                }
                this[(row * 4) + column] = value;
            }
        }
        public static void Add(ref Matrix5x4 left, ref Matrix5x4 right, out Matrix5x4 result)
        {
            result.M11 = left.M11 + right.M11;
            result.M12 = left.M12 + right.M12;
            result.M13 = left.M13 + right.M13;
            result.M14 = left.M14 + right.M14;
            result.M21 = left.M21 + right.M21;
            result.M22 = left.M22 + right.M22;
            result.M23 = left.M23 + right.M23;
            result.M24 = left.M24 + right.M24;
            result.M31 = left.M31 + right.M31;
            result.M32 = left.M32 + right.M32;
            result.M33 = left.M33 + right.M33;
            result.M34 = left.M34 + right.M34;
            result.M41 = left.M41 + right.M41;
            result.M42 = left.M42 + right.M42;
            result.M43 = left.M43 + right.M43;
            result.M44 = left.M44 + right.M44;
            result.M51 = left.M51 + right.M51;
            result.M52 = left.M52 + right.M52;
            result.M53 = left.M53 + right.M53;
            result.M54 = left.M54 + right.M54;
        }

        public static Matrix5x4 Add(Matrix5x4 left, Matrix5x4 right)
        {
            Matrix5x4 matrixx;
            Add(ref left, ref right, out matrixx);
            return matrixx;
        }

        public static void Subtract(ref Matrix5x4 left, ref Matrix5x4 right, out Matrix5x4 result)
        {
            result.M11 = left.M11 - right.M11;
            result.M12 = left.M12 - right.M12;
            result.M13 = left.M13 - right.M13;
            result.M14 = left.M14 - right.M14;
            result.M21 = left.M21 - right.M21;
            result.M22 = left.M22 - right.M22;
            result.M23 = left.M23 - right.M23;
            result.M24 = left.M24 - right.M24;
            result.M31 = left.M31 - right.M31;
            result.M32 = left.M32 - right.M32;
            result.M33 = left.M33 - right.M33;
            result.M34 = left.M34 - right.M34;
            result.M41 = left.M41 - right.M41;
            result.M42 = left.M42 - right.M42;
            result.M43 = left.M43 - right.M43;
            result.M44 = left.M44 - right.M44;
            result.M51 = left.M51 - right.M51;
            result.M52 = left.M52 - right.M52;
            result.M53 = left.M53 - right.M53;
            result.M54 = left.M54 - right.M54;
        }

        public static Matrix5x4 Subtract(Matrix5x4 left, Matrix5x4 right)
        {
            Matrix5x4 matrixx;
            Subtract(ref left, ref right, out matrixx);
            return matrixx;
        }

        public static void Multiply(ref Matrix5x4 left, float right, out Matrix5x4 result)
        {
            result.M11 = left.M11 * right;
            result.M12 = left.M12 * right;
            result.M13 = left.M13 * right;
            result.M14 = left.M14 * right;
            result.M21 = left.M21 * right;
            result.M22 = left.M22 * right;
            result.M23 = left.M23 * right;
            result.M24 = left.M24 * right;
            result.M31 = left.M31 * right;
            result.M32 = left.M32 * right;
            result.M33 = left.M33 * right;
            result.M34 = left.M34 * right;
            result.M41 = left.M41 * right;
            result.M42 = left.M42 * right;
            result.M43 = left.M43 * right;
            result.M44 = left.M44 * right;
            result.M51 = left.M51 * right;
            result.M52 = left.M52 * right;
            result.M53 = left.M53 * right;
            result.M54 = left.M54 * right;
        }

        public static void Divide(ref Matrix5x4 left, float right, out Matrix5x4 result)
        {
            float num = 1f / right;
            result.M11 = left.M11 * num;
            result.M12 = left.M12 * num;
            result.M13 = left.M13 * num;
            result.M14 = left.M14 * num;
            result.M21 = left.M21 * num;
            result.M22 = left.M22 * num;
            result.M23 = left.M23 * num;
            result.M24 = left.M24 * num;
            result.M31 = left.M31 * num;
            result.M32 = left.M32 * num;
            result.M33 = left.M33 * num;
            result.M34 = left.M34 * num;
            result.M41 = left.M41 * num;
            result.M42 = left.M42 * num;
            result.M43 = left.M43 * num;
            result.M44 = left.M44 * num;
            result.M51 = left.M51 * num;
            result.M52 = left.M52 * num;
            result.M53 = left.M53 * num;
            result.M54 = left.M54 * num;
        }

        public static void Negate(ref Matrix5x4 value, out Matrix5x4 result)
        {
            result.M11 = -value.M11;
            result.M12 = -value.M12;
            result.M13 = -value.M13;
            result.M14 = -value.M14;
            result.M21 = -value.M21;
            result.M22 = -value.M22;
            result.M23 = -value.M23;
            result.M24 = -value.M24;
            result.M31 = -value.M31;
            result.M32 = -value.M32;
            result.M33 = -value.M33;
            result.M34 = -value.M34;
            result.M41 = -value.M41;
            result.M42 = -value.M42;
            result.M43 = -value.M43;
            result.M44 = -value.M44;
            result.M51 = -value.M51;
            result.M52 = -value.M52;
            result.M53 = -value.M53;
            result.M54 = -value.M54;
        }

        public static Matrix5x4 Negate(Matrix5x4 value)
        {
            Matrix5x4 matrixx;
            Negate(ref value, out matrixx);
            return matrixx;
        }

        public static void Lerp(ref Matrix5x4 start, ref Matrix5x4 end, float amount, out Matrix5x4 result)
        {
            result.M11 = start.M11 + ((end.M11 - start.M11) * amount);
            result.M12 = start.M12 + ((end.M12 - start.M12) * amount);
            result.M13 = start.M13 + ((end.M13 - start.M13) * amount);
            result.M14 = start.M14 + ((end.M14 - start.M14) * amount);
            result.M21 = start.M21 + ((end.M21 - start.M21) * amount);
            result.M22 = start.M22 + ((end.M22 - start.M22) * amount);
            result.M23 = start.M23 + ((end.M23 - start.M23) * amount);
            result.M24 = start.M24 + ((end.M24 - start.M24) * amount);
            result.M31 = start.M31 + ((end.M31 - start.M31) * amount);
            result.M32 = start.M32 + ((end.M32 - start.M32) * amount);
            result.M33 = start.M33 + ((end.M33 - start.M33) * amount);
            result.M34 = start.M34 + ((end.M34 - start.M34) * amount);
            result.M41 = start.M41 + ((end.M41 - start.M41) * amount);
            result.M42 = start.M42 + ((end.M42 - start.M42) * amount);
            result.M43 = start.M43 + ((end.M43 - start.M43) * amount);
            result.M44 = start.M44 + ((end.M44 - start.M44) * amount);
            result.M51 = start.M51 + ((end.M51 - start.M51) * amount);
            result.M52 = start.M52 + ((end.M52 - start.M52) * amount);
            result.M53 = start.M53 + ((end.M53 - start.M53) * amount);
            result.M54 = start.M54 + ((end.M54 - start.M54) * amount);
        }

        public static Matrix5x4 Lerp(Matrix5x4 start, Matrix5x4 end, float amount)
        {
            Matrix5x4 matrixx;
            Lerp(ref start, ref end, amount, out matrixx);
            return matrixx;
        }

        public static void SmoothStep(ref Matrix5x4 start, ref Matrix5x4 end, float amount, out Matrix5x4 result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.M11 = start.M11 + ((end.M11 - start.M11) * amount);
            result.M12 = start.M12 + ((end.M12 - start.M12) * amount);
            result.M13 = start.M13 + ((end.M13 - start.M13) * amount);
            result.M14 = start.M14 + ((end.M14 - start.M14) * amount);
            result.M21 = start.M21 + ((end.M21 - start.M21) * amount);
            result.M22 = start.M22 + ((end.M22 - start.M22) * amount);
            result.M23 = start.M23 + ((end.M23 - start.M23) * amount);
            result.M24 = start.M24 + ((end.M24 - start.M24) * amount);
            result.M31 = start.M31 + ((end.M31 - start.M31) * amount);
            result.M32 = start.M32 + ((end.M32 - start.M32) * amount);
            result.M33 = start.M33 + ((end.M33 - start.M33) * amount);
            result.M34 = start.M34 + ((end.M34 - start.M34) * amount);
            result.M41 = start.M41 + ((end.M41 - start.M41) * amount);
            result.M42 = start.M42 + ((end.M42 - start.M42) * amount);
            result.M43 = start.M43 + ((end.M43 - start.M43) * amount);
            result.M44 = start.M44 + ((end.M44 - start.M44) * amount);
            result.M51 = start.M51 + ((end.M51 - start.M51) * amount);
            result.M52 = start.M52 + ((end.M52 - start.M52) * amount);
            result.M53 = start.M53 + ((end.M53 - start.M53) * amount);
            result.M54 = start.M54 + ((end.M54 - start.M54) * amount);
        }

        public static Matrix5x4 SmoothStep(Matrix5x4 start, Matrix5x4 end, float amount)
        {
            Matrix5x4 matrixx;
            SmoothStep(ref start, ref end, amount, out matrixx);
            return matrixx;
        }

        public static void Scaling(ref Vector4 scale, out Matrix5x4 result)
        {
            Scaling(scale.X, scale.Y, scale.Z, scale.W, out result);
        }

        public static Matrix5x4 Scaling(Vector4 scale)
        {
            Matrix5x4 matrixx;
            Scaling(ref scale, out matrixx);
            return matrixx;
        }

        public static void Scaling(float x, float y, float z, float w, out Matrix5x4 result)
        {
            result = Identity;
            result.M11 = x;
            result.M22 = y;
            result.M33 = z;
            result.M44 = w;
        }

        public static Matrix5x4 Scaling(float x, float y, float z, float w)
        {
            Matrix5x4 matrixx;
            Scaling(x, y, z, w, out matrixx);
            return matrixx;
        }

        public static void Scaling(float scale, out Matrix5x4 result)
        {
            result = Identity;
            result.M11 = result.M22 = result.M33 = result.M44 = scale;
        }

        public static Matrix5x4 Scaling(float scale)
        {
            Matrix5x4 matrixx;
            Scaling(scale, out matrixx);
            return matrixx;
        }

        public static void Translation(ref Vector4 value, out Matrix5x4 result)
        {
            Translation(value.X, value.Y, value.Z, value.W, out result);
        }

        public static Matrix5x4 Translation(Vector4 value)
        {
            Matrix5x4 matrixx;
            Translation(ref value, out matrixx);
            return matrixx;
        }

        public static void Translation(float x, float y, float z, float w, out Matrix5x4 result)
        {
            result = Identity;
            result.M51 = x;
            result.M52 = y;
            result.M53 = z;
            result.M54 = w;
        }

        public static Matrix5x4 Translation(float x, float y, float z, float w)
        {
            Matrix5x4 matrixx;
            Translation(x, y, z, w, out matrixx);
            return matrixx;
        }

        public static Matrix5x4 operator +(Matrix5x4 left, Matrix5x4 right)
        {
            Matrix5x4 matrixx;
            Add(ref left, ref right, out matrixx);
            return matrixx;
        }

        public static Matrix5x4 operator +(Matrix5x4 value)
        {
            return value;
        }

        public static Matrix5x4 operator -(Matrix5x4 left, Matrix5x4 right)
        {
            Matrix5x4 matrixx;
            Subtract(ref left, ref right, out matrixx);
            return matrixx;
        }

        public static Matrix5x4 operator -(Matrix5x4 value)
        {
            Matrix5x4 matrixx;
            Negate(ref value, out matrixx);
            return matrixx;
        }

        public static Matrix5x4 operator *(float left, Matrix5x4 right)
        {
            Matrix5x4 matrixx;
            Multiply(ref right, left, out matrixx);
            return matrixx;
        }

        public static Matrix5x4 operator *(Matrix5x4 left, float right)
        {
            Matrix5x4 matrixx;
            Multiply(ref left, right, out matrixx);
            return matrixx;
        }

        public static Matrix5x4 operator /(Matrix5x4 left, float right)
        {
            Matrix5x4 matrixx;
            Divide(ref left, right, out matrixx);
            return matrixx;
        }

        public static bool operator ==(Matrix5x4 left, Matrix5x4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix5x4 left, Matrix5x4 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M3:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}] [M51:{16} M52:{17} M53:{18} M54:{19}]", new object[] { 
            (float) this.M11, (float) this.M12, (float) this.M13, (float) this.M14, (float) this.M21, (float) this.M22, (float) this.M23, (float) this.M24, (float) this.M31, (float) this.M32, (float) this.M33, (float) this.M34, (float) this.M41, (float) this.M42, (float) this.M43, (float) this.M44, 
            (float) this.M51, (float) this.M52, (float) this.M53, (float) this.M54
         });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format(format, (object[])new object[] { 
            CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M3:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}] [M51:{16} M52:{17} M53:{18} M54:{19}]", ((float) this.M11).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M12).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M13).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M14).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M21).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M22).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M23).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M24).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M31).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M32).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M33).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M34).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M41).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M42).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), 
            ((float) this.M43).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M44).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M51).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M52).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M53).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M54).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture)
         });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M3:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}] [M51:{16} M52:{17} M53:{18} M54:{19}]", new object[] { 
            ((float) this.M11).ToString(formatProvider), ((float) this.M12).ToString(formatProvider), ((float) this.M13).ToString(formatProvider), ((float) this.M14).ToString(formatProvider), ((float) this.M21).ToString(formatProvider), ((float) this.M22).ToString(formatProvider), ((float) this.M23).ToString(formatProvider), ((float) this.M24).ToString(formatProvider), ((float) this.M31).ToString(formatProvider), ((float) this.M32).ToString(formatProvider), ((float) this.M33).ToString(formatProvider), ((float) this.M34).ToString(formatProvider), ((float) this.M41).ToString(formatProvider), ((float) this.M42).ToString(formatProvider), ((float) this.M43).ToString(formatProvider), ((float) this.M44).ToString(formatProvider), 
            ((float) this.M51).ToString(formatProvider), ((float) this.M52).ToString(formatProvider), ((float) this.M53).ToString(formatProvider), ((float) this.M54).ToString(formatProvider)
         });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(format, (object[])new object[] { 
            formatProvider, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M3:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}] [M51:{16} M52:{17} M53:{18} M54:{19}]", ((float) this.M11).ToString(format, formatProvider), ((float) this.M12).ToString(format, formatProvider), ((float) this.M13).ToString(format, formatProvider), ((float) this.M14).ToString(format, formatProvider), ((float) this.M21).ToString(format, formatProvider), ((float) this.M22).ToString(format, formatProvider), ((float) this.M23).ToString(format, formatProvider), ((float) this.M24).ToString(format, formatProvider), ((float) this.M31).ToString(format, formatProvider), ((float) this.M32).ToString(format, formatProvider), ((float) this.M33).ToString(format, formatProvider), ((float) this.M34).ToString(format, formatProvider), ((float) this.M41).ToString(format, formatProvider), ((float) this.M42).ToString(format, formatProvider), 
            ((float) this.M43).ToString(format, formatProvider), ((float) this.M44).ToString(format, formatProvider), ((float) this.M51).ToString(format, formatProvider), ((float) this.M52).ToString(format, formatProvider), ((float) this.M53).ToString(format, formatProvider), ((float) this.M54).ToString(format, formatProvider)
         });
        }

        public override int GetHashCode()
        {
            return (((((((((((((((((((((float)this.M11).GetHashCode() + ((float)this.M12).GetHashCode()) + ((float)this.M13).GetHashCode()) + ((float)this.M14).GetHashCode()) + ((float)this.M21).GetHashCode()) + ((float)this.M22).GetHashCode()) + ((float)this.M23).GetHashCode()) + ((float)this.M24).GetHashCode()) + ((float)this.M31).GetHashCode()) + ((float)this.M32).GetHashCode()) + ((float)this.M33).GetHashCode()) + ((float)this.M34).GetHashCode()) + ((float)this.M41).GetHashCode()) + ((float)this.M42).GetHashCode()) + ((float)this.M43).GetHashCode()) + ((float)this.M44).GetHashCode()) + ((float)this.M51).GetHashCode()) + ((float)this.M52).GetHashCode()) + ((float)this.M53).GetHashCode()) + ((float)this.M54).GetHashCode());
        }

        public bool Equals(Matrix5x4 other)
        {
            return (((((((Math.Abs((float)(other.M11 - this.M11)) < 1E-06f) && (Math.Abs((float)(other.M12 - this.M12)) < 1E-06f)) && ((Math.Abs((float)(other.M13 - this.M13)) < 1E-06f) && (Math.Abs((float)(other.M14 - this.M14)) < 1E-06f))) && (((Math.Abs((float)(other.M21 - this.M21)) < 1E-06f) && (Math.Abs((float)(other.M22 - this.M22)) < 1E-06f)) && ((Math.Abs((float)(other.M23 - this.M23)) < 1E-06f) && (Math.Abs((float)(other.M24 - this.M24)) < 1E-06f)))) && ((((Math.Abs((float)(other.M31 - this.M31)) < 1E-06f) && (Math.Abs((float)(other.M32 - this.M32)) < 1E-06f)) && ((Math.Abs((float)(other.M33 - this.M33)) < 1E-06f) && (Math.Abs((float)(other.M34 - this.M34)) < 1E-06f))) && (((Math.Abs((float)(other.M41 - this.M41)) < 1E-06f) && (Math.Abs((float)(other.M42 - this.M42)) < 1E-06f)) && ((Math.Abs((float)(other.M43 - this.M43)) < 1E-06f) && (Math.Abs((float)(other.M44 - this.M44)) < 1E-06f))))) && (((Math.Abs((float)(other.M51 - this.M51)) < 1E-06f) && (Math.Abs((float)(other.M52 - this.M52)) < 1E-06f)) && (Math.Abs((float)(other.M53 - this.M53)) < 1E-06f))) && (Math.Abs((float)(other.M54 - this.M54)) < 1E-06f));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Matrix5x4)))
            {
                return false;
            }
            return this.Equals((Matrix5x4)value);
        }

        static Matrix5x4()
        {
            SizeInBytes = Marshal.SizeOf((Type)typeof(Matrix5x4));
            Zero = new Matrix5x4();
            Matrix5x4 matrixx = new Matrix5x4();
            matrixx.M11 = 1f;
            matrixx.M22 = 1f;
            matrixx.M33 = 1f;
            matrixx.M44 = 1f;
            matrixx.M54 = 0f;
            Identity = matrixx;
        }
    }
}