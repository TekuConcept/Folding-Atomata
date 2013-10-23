using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Quaternion : IEquatable<Quaternion>, IFormattable
    {
        public static readonly int SizeInBytes;
        public static readonly Quaternion Zero;
        public static readonly Quaternion One;
        public static readonly Quaternion Identity;
        public float X;
        public float Y;
        public float Z;
        public float W;
        public Quaternion(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        public Quaternion(Vector4 value)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = value.W;
        }

        public Quaternion(Vector3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        public Quaternion(Vector2 value, float z, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Quaternion(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Quaternion.");
            }
            this.X = values[0];
            this.Y = values[1];
            this.Z = values[2];
            this.W = values[3];
        }

        public bool IsIdentity
        {
            get
            {
                return this.Equals(Identity);
            }
        }
        public bool IsNormalized
        {
            get
            {
                return (Math.Abs((float)(((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W)) - 1f)) < 1E-06f);
            }
        }
        public float Angle
        {
            get
            {
                float num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
                if (num < 1E-06f)
                {
                    return 0f;
                }
                return (float)(2.0 * Math.Acos((double)this.W));
            }
        }
        public Vector3 Axis
        {
            get
            {
                float num = ((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z);
                if (num < 1E-06f)
                {
                    return Vector3.UnitX;
                }
                float num2 = 1f / num;
                return new Vector3(this.X * num2, this.Y * num2, this.Z * num2);
            }
        }
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;

                    case 1:
                        return this.Y;

                    case 2:
                        return this.Z;

                    case 3:
                        return this.W;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Quaternion run from 0 to 3, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        return;

                    case 1:
                        this.Y = value;
                        return;

                    case 2:
                        this.Z = value;
                        return;

                    case 3:
                        this.W = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Quaternion run from 0 to 3, inclusive.");
            }
        }
        public void Conjugate()
        {
            this.X = -this.X;
            this.Y = -this.Y;
            this.Z = -this.Z;
        }

        public void Invert()
        {
            float num = this.LengthSquared();
            if (num > 1E-06f)
            {
                num = 1f / num;
                this.X = -this.X * num;
                this.Y = -this.Y * num;
                this.Z = -this.Z * num;
                this.W *= num;
            }
        }

        public float Length()
        {
            return (float)Math.Sqrt((double)((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W)));
        }

        public float LengthSquared()
        {
            return ((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W));
        }

        public void Normalize()
        {
            float num = this.Length();
            if (num > 1E-06f)
            {
                float num2 = 1f / num;
                this.X *= num2;
                this.Y *= num2;
                this.Z *= num2;
                this.W *= num2;
            }
        }

        public float[] ToArray()
        {
            return new float[] { this.X, this.Y, this.Z, this.W };
        }

        public static void Add(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.X = left.X + right.X;
            result.Y = left.Y + right.Y;
            result.Z = left.Z + right.Z;
            result.W = left.W + right.W;
        }

        public static Quaternion Add(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Add(ref left, ref right, out quaternion);
            return quaternion;
        }

        public static void Subtract(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            result.X = left.X - right.X;
            result.Y = left.Y - right.Y;
            result.Z = left.Z - right.Z;
            result.W = left.W - right.W;
        }

        public static Quaternion Subtract(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Subtract(ref left, ref right, out quaternion);
            return quaternion;
        }

        public static void Multiply(ref Quaternion value, float scale, out Quaternion result)
        {
            result.X = value.X * scale;
            result.Y = value.Y * scale;
            result.Z = value.Z * scale;
            result.W = value.W * scale;
        }

        public static Quaternion Multiply(Quaternion value, float scale)
        {
            Quaternion quaternion;
            Multiply(ref value, scale, out quaternion);
            return quaternion;
        }

        public static void Multiply(ref Quaternion left, ref Quaternion right, out Quaternion result)
        {
            float x = left.X;
            float y = left.Y;
            float z = left.Z;
            float w = left.W;
            float num5 = right.X;
            float num6 = right.Y;
            float num7 = right.Z;
            float num8 = right.W;
            result.X = (((num5 * w) + (x * num8)) + (num6 * z)) - (num7 * y);
            result.Y = (((num6 * w) + (y * num8)) + (num7 * x)) - (num5 * z);
            result.Z = (((num7 * w) + (z * num8)) + (num5 * y)) - (num6 * x);
            result.W = (num8 * w) - (((num5 * x) + (num6 * y)) + (num7 * z));
        }

        public static Quaternion Multiply(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Multiply(ref left, ref right, out quaternion);
            return quaternion;
        }

        public static void Negate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        public static Quaternion Negate(Quaternion value)
        {
            Quaternion quaternion;
            Negate(ref value, out quaternion);
            return quaternion;
        }

        public static void Barycentric(ref Quaternion value1, ref Quaternion value2, ref Quaternion value3, float amount1, float amount2, out Quaternion result)
        {
            Quaternion quaternion;
            Quaternion quaternion2;
            Slerp(ref value1, ref value2, amount1 + amount2, out quaternion);
            Slerp(ref value1, ref value3, amount1 + amount2, out quaternion2);
            Slerp(ref quaternion, ref quaternion2, amount2 / (amount1 + amount2), out result);
        }

        public static Quaternion Barycentric(Quaternion value1, Quaternion value2, Quaternion value3, float amount1, float amount2)
        {
            Quaternion quaternion;
            Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out quaternion);
            return quaternion;
        }

        public static void Conjugate(ref Quaternion value, out Quaternion result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = value.W;
        }

        public static Quaternion Conjugate(Quaternion value)
        {
            Quaternion quaternion;
            Conjugate(ref value, out quaternion);
            return quaternion;
        }

        public static void Dot(ref Quaternion left, ref Quaternion right, out float result)
        {
            result = (((left.X * right.X) + (left.Y * right.Y)) + (left.Z * right.Z)) + (left.W * right.W);
        }

        public static float Dot(Quaternion left, Quaternion right)
        {
            return ((((left.X * right.X) + (left.Y * right.Y)) + (left.Z * right.Z)) + (left.W * right.W));
        }

        public static void Exponential(ref Quaternion value, out Quaternion result)
        {
            float num = (float)Math.Sqrt((double)(((value.X * value.X) + (value.Y * value.Y)) + (value.Z * value.Z)));
            float num2 = (float)Math.Sin((double)num);
            if (Math.Abs(num2) >= 1E-06f)
            {
                float num3 = num2 / num;
                result.X = num3 * value.X;
                result.Y = num3 * value.Y;
                result.Z = num3 * value.Z;
            }
            else
            {
                result = value;
            }
            result.W = (float)Math.Cos((double)num);
        }

        public static Quaternion Exponential(Quaternion value)
        {
            Quaternion quaternion;
            Exponential(ref value, out quaternion);
            return quaternion;
        }

        public static void Invert(ref Quaternion value, out Quaternion result)
        {
            result = value;
            result.Invert();
        }

        public static Quaternion Invert(Quaternion value)
        {
            Quaternion quaternion;
            Invert(ref value, out quaternion);
            return quaternion;
        }

        public static void Lerp(ref Quaternion start, ref Quaternion end, float amount, out Quaternion result)
        {
            float num = 1f - amount;
            if (Dot(start, end) >= 0f)
            {
                result.X = (num * start.X) + (amount * end.X);
                result.Y = (num * start.Y) + (amount * end.Y);
                result.Z = (num * start.Z) + (amount * end.Z);
                result.W = (num * start.W) + (amount * end.W);
            }
            else
            {
                result.X = (num * start.X) - (amount * end.X);
                result.Y = (num * start.Y) - (amount * end.Y);
                result.Z = (num * start.Z) - (amount * end.Z);
                result.W = (num * start.W) - (amount * end.W);
            }
            result.Normalize();
        }

        public static Quaternion Lerp(Quaternion start, Quaternion end, float amount)
        {
            Quaternion quaternion;
            Lerp(ref start, ref end, amount, out quaternion);
            return quaternion;
        }

        public static void Logarithm(ref Quaternion value, out Quaternion result)
        {
            if (Math.Abs(value.W) < 1.0)
            {
                float num = (float)Math.Acos((double)value.W);
                float num2 = (float)Math.Sin((double)num);
                if (Math.Abs(num2) >= 1E-06f)
                {
                    float num3 = num / num2;
                    result.X = value.X * num3;
                    result.Y = value.Y * num3;
                    result.Z = value.Z * num3;
                }
                else
                {
                    result = value;
                }
            }
            else
            {
                result = value;
            }
            result.W = 0f;
        }

        public static Quaternion Logarithm(Quaternion value)
        {
            Quaternion quaternion;
            Logarithm(ref value, out quaternion);
            return quaternion;
        }

        public static void Normalize(ref Quaternion value, out Quaternion result)
        {
            Quaternion quaternion = value;
            result = quaternion;
            result.Normalize();
        }

        public static Quaternion Normalize(Quaternion value)
        {
            value.Normalize();
            return value;
        }

        public static void RotationAxis(ref Vector3 axis, float angle, out Quaternion result)
        {
            Vector3 vector;
            Vector3.Normalize(ref axis, out vector);
            float num = angle * 0.5f;
            float num2 = (float)Math.Sin((double)num);
            float num3 = (float)Math.Cos((double)num);
            result.X = vector.X * num2;
            result.Y = vector.Y * num2;
            result.Z = vector.Z * num2;
            result.W = num3;
        }

        public static Quaternion RotationAxis(Vector3 axis, float angle)
        {
            Quaternion quaternion;
            RotationAxis(ref axis, angle, out quaternion);
            return quaternion;
        }

        public static void RotationMatrix(ref Matrix matrix, out Quaternion result)
        {
            float num;
            float num3 = (matrix.M11 + matrix.M22) + matrix.M33;
            if (num3 > 0f)
            {
                num = (float)Math.Sqrt((double)(num3 + 1f));
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix.M23 - matrix.M32) * num;
                result.Y = (matrix.M31 - matrix.M13) * num;
                result.Z = (matrix.M12 - matrix.M21) * num;
            }
            else
            {
                float num2;
                if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
                {
                    num = (float)Math.Sqrt((double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                    num2 = 0.5f / num;
                    result.X = 0.5f * num;
                    result.Y = (matrix.M12 + matrix.M21) * num2;
                    result.Z = (matrix.M13 + matrix.M31) * num2;
                    result.W = (matrix.M23 - matrix.M32) * num2;
                }
                else if (matrix.M22 > matrix.M33)
                {
                    num = (float)Math.Sqrt((double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                    num2 = 0.5f / num;
                    result.X = (matrix.M21 + matrix.M12) * num2;
                    result.Y = 0.5f * num;
                    result.Z = (matrix.M32 + matrix.M23) * num2;
                    result.W = (matrix.M31 - matrix.M13) * num2;
                }
                else
                {
                    num = (float)Math.Sqrt((double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
                    num2 = 0.5f / num;
                    result.X = (matrix.M31 + matrix.M13) * num2;
                    result.Y = (matrix.M32 + matrix.M23) * num2;
                    result.Z = 0.5f * num;
                    result.W = (matrix.M12 - matrix.M21) * num2;
                }
            }
        }

        public static Quaternion RotationMatrix(Matrix matrix)
        {
            Quaternion quaternion;
            RotationMatrix(ref matrix, out quaternion);
            return quaternion;
        }

        public static void RotationYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
        {
            float num = roll * 0.5f;
            float num2 = pitch * 0.5f;
            float num3 = yaw * 0.5f;
            float num4 = (float)Math.Sin((double)num);
            float num5 = (float)Math.Cos((double)num);
            float num6 = (float)Math.Sin((double)num2);
            float num7 = (float)Math.Cos((double)num2);
            float num8 = (float)Math.Sin((double)num3);
            float num9 = (float)Math.Cos((double)num3);
            result.X = ((num9 * num6) * num5) + ((num8 * num7) * num4);
            result.Y = ((num8 * num7) * num5) - ((num9 * num6) * num4);
            result.Z = ((num9 * num7) * num4) - ((num8 * num6) * num5);
            result.W = ((num9 * num7) * num5) + ((num8 * num6) * num4);
        }

        public static Quaternion RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Quaternion quaternion;
            RotationYawPitchRoll(yaw, pitch, roll, out quaternion);
            return quaternion;
        }

        public static void Slerp(ref Quaternion start, ref Quaternion end, float amount, out Quaternion result)
        {
            float num;
            float num2;
            float num3 = Dot(start, end);
            if (Math.Abs(num3) > 0.999999f)
            {
                num2 = 1f - amount;
                num = amount * Math.Sign(num3);
            }
            else
            {
                float num4 = (float)Math.Acos((double)Math.Abs(num3));
                float num5 = (float)(1.0 / Math.Sin((double)num4));
                num2 = ((float)Math.Sin((double)((1f - amount) * num4))) * num5;
                num = (((float)Math.Sin((double)(amount * num4))) * num5) * Math.Sign(num3);
            }
            result.X = (num2 * start.X) + (num * end.X);
            result.Y = (num2 * start.Y) + (num * end.Y);
            result.Z = (num2 * start.Z) + (num * end.Z);
            result.W = (num2 * start.W) + (num * end.W);
        }

        public static Quaternion Slerp(Quaternion start, Quaternion end, float amount)
        {
            Quaternion quaternion;
            Slerp(ref start, ref end, amount, out quaternion);
            return quaternion;
        }

        public static void Squad(ref Quaternion value1, ref Quaternion value2, ref Quaternion value3, ref Quaternion value4, float amount, out Quaternion result)
        {
            Quaternion quaternion;
            Quaternion quaternion2;
            Slerp(ref value1, ref value4, amount, out quaternion);
            Slerp(ref value2, ref value3, amount, out quaternion2);
            Slerp(ref quaternion, ref quaternion2, (2f * amount) * (1f - amount), out result);
        }

        public static Quaternion Squad(Quaternion value1, Quaternion value2, Quaternion value3, Quaternion value4, float amount)
        {
            Quaternion quaternion;
            Squad(ref value1, ref value2, ref value3, ref value4, amount, out quaternion);
            return quaternion;
        }

        public static Quaternion[] SquadSetup(Quaternion value1, Quaternion value2, Quaternion value3, Quaternion value4)
        {
            Quaternion quaternion5;
            Quaternion quaternion6;
            Quaternion quaternion7 = value1 + value2;
            Quaternion quaternion8 = value1 - value2;
            Quaternion quaternion = (quaternion7.LengthSquared() < quaternion8.LengthSquared()) ? -value1 : value1;
            Quaternion quaternion9 = value2 + value3;
            Quaternion quaternion10 = value2 - value3;
            Quaternion quaternion2 = (quaternion9.LengthSquared() < quaternion10.LengthSquared()) ? -value3 : value3;
            Quaternion quaternion11 = value3 + value4;
            Quaternion quaternion12 = value3 - value4;
            Quaternion quaternion3 = (quaternion11.LengthSquared() < quaternion12.LengthSquared()) ? -value4 : value4;
            Quaternion quaternion4 = value2;
            Exponential(ref quaternion4, out quaternion5);
            Exponential(ref quaternion2, out quaternion6);
            return new Quaternion[] { (quaternion4 * Exponential((Quaternion)(-0.25f * (Logarithm(quaternion5 * quaternion2) + Logarithm(quaternion5 * quaternion))))), (quaternion2 * Exponential((Quaternion)(-0.25f * (Logarithm(quaternion6 * quaternion3) + Logarithm(quaternion6 * quaternion4))))), quaternion2 };
        }

        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Add(ref left, ref right, out quaternion);
            return quaternion;
        }

        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Subtract(ref left, ref right, out quaternion);
            return quaternion;
        }

        public static Quaternion operator -(Quaternion value)
        {
            Quaternion quaternion;
            Negate(ref value, out quaternion);
            return quaternion;
        }

        public static Quaternion operator *(float scale, Quaternion value)
        {
            Quaternion quaternion;
            Multiply(ref value, scale, out quaternion);
            return quaternion;
        }

        public static Quaternion operator *(Quaternion value, float scale)
        {
            Quaternion quaternion;
            Multiply(ref value, scale, out quaternion);
            return quaternion;
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            Quaternion quaternion;
            Multiply(ref left, ref right, out quaternion);
            return quaternion;
        }

        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { (float)this.X, (float)this.Y, (float)this.Z, (float)this.W });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { ((float)this.X).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Y).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Z).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.W).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { (float)this.X, (float)this.Y, (float)this.Z, (float)this.W });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { ((float)this.X).ToString(format, formatProvider), ((float)this.Y).ToString(format, formatProvider), ((float)this.Z).ToString(format, formatProvider), ((float)this.W).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (((((float)this.X).GetHashCode() + ((float)this.Y).GetHashCode()) + ((float)this.Z).GetHashCode()) + ((float)this.W).GetHashCode());
        }

        public bool Equals(Quaternion other)
        {
            return ((((Math.Abs((float)(other.X - this.X)) < 1E-06f) && (Math.Abs((float)(other.Y - this.Y)) < 1E-06f)) && (Math.Abs((float)(other.Z - this.Z)) < 1E-06f)) && (Math.Abs((float)(other.W - this.W)) < 1E-06f));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Quaternion)))
            {
                return false;
            }
            return this.Equals((Quaternion)value);
        }

        static Quaternion()
        {
            SizeInBytes = Marshal.SizeOf((Type)typeof(Quaternion));
            Zero = new Quaternion();
            One = new Quaternion(1f, 1f, 1f, 1f);
            Identity = new Quaternion(0f, 0f, 0f, 1f);
        }
    }
}