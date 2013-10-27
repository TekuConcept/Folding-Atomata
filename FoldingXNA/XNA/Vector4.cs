using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4 : IEquatable<Vector4>, IFormattable
    {
        public static readonly int SizeInBytes;
        public static readonly Vector4 Zero;
        public static readonly Vector4 UnitX;
        public static readonly Vector4 UnitY;
        public static readonly Vector4 UnitZ;
        public static readonly Vector4 UnitW;
        public static readonly Vector4 One;
        public float X;
        public float Y;
        public float Z;
        public float W;
        public Vector4(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Vector4(Vector3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        public Vector4(Vector2 value, float z, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        public Vector4(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Vector4.");
            }
            this.X = values[0];
            this.Y = values[1];
            this.Z = values[2];
            this.W = values[3];
        }

        public bool IsNormalized
        {
            get
            {
                return (Math.Abs((float)(((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) + (this.W * this.W)) - 1f)) < 1E-06f);
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
                throw new ArgumentOutOfRangeException("index", "Indices for Vector4 run from 0 to 3, inclusive.");
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
                throw new ArgumentOutOfRangeException("index", "Indices for Vector4 run from 0 to 3, inclusive.");
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

        public static void Add(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 Add(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static void Subtract(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4 Subtract(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static void Multiply(ref Vector4 value, float scale, out Vector4 result)
        {
            result = new Vector4(value.X * scale, value.Y * scale, value.Z * scale, value.W * scale);
        }

        public static Vector4 Multiply(Vector4 value, float scale)
        {
            return new Vector4(value.X * scale, value.Y * scale, value.Z * scale, value.W * scale);
        }

        public static void Modulate(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result = new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }

        public static Vector4 Modulate(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }

        public static void Divide(ref Vector4 value, float scale, out Vector4 result)
        {
            result = new Vector4(value.X / scale, value.Y / scale, value.Z / scale, value.W / scale);
        }

        public static Vector4 Divide(Vector4 value, float scale)
        {
            return new Vector4(value.X / scale, value.Y / scale, value.Z / scale, value.W / scale);
        }

        public static void Negate(ref Vector4 value, out Vector4 result)
        {
            result = new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static Vector4 Negate(Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static void Barycentric(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, float amount1, float amount2, out Vector4 result)
        {
            result = new Vector4((value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X)), (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y)), (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z)), (value1.W + (amount1 * (value2.W - value1.W))) + (amount2 * (value3.W - value1.W)));
        }

        public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
        {
            Vector4 vector;
            Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out vector);
            return vector;
        }

        public static void Clamp(ref Vector4 value, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            float x = value.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            float y = value.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            float z = value.Z;
            z = (z > max.Z) ? max.Z : z;
            z = (z < min.Z) ? min.Z : z;
            float w = value.W;
            w = (w > max.W) ? max.W : w;
            w = (w < min.W) ? min.W : w;
            result = new Vector4(x, y, z, w);
        }

        public static Vector4 Clamp(Vector4 value, Vector4 min, Vector4 max)
        {
            Vector4 vector;
            Clamp(ref value, ref min, ref max, out vector);
            return vector;
        }

        public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            float num4 = value1.W - value2.W;
            result = (float)Math.Sqrt((double)((((num * num) + (num2 * num2)) + (num3 * num3)) + (num4 * num4)));
        }

        public static float Distance(Vector4 value1, Vector4 value2)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            float num4 = value1.W - value2.W;
            return (float)Math.Sqrt((double)((((num * num) + (num2 * num2)) + (num3 * num3)) + (num4 * num4)));
        }

        public static void DistanceSquared(ref Vector4 value1, ref Vector4 value2, out float result)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            float num4 = value1.W - value2.W;
            result = (((num * num) + (num2 * num2)) + (num3 * num3)) + (num4 * num4);
        }

        public static float DistanceSquared(Vector4 value1, Vector4 value2)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            float num4 = value1.W - value2.W;
            return ((((num * num) + (num2 * num2)) + (num3 * num3)) + (num4 * num4));
        }

        public static void Dot(ref Vector4 left, ref Vector4 right, out float result)
        {
            result = (((left.X * right.X) + (left.Y * right.Y)) + (left.Z * right.Z)) + (left.W * right.W);
        }

        public static float Dot(Vector4 left, Vector4 right)
        {
            return ((((left.X * right.X) + (left.Y * right.Y)) + (left.Z * right.Z)) + (left.W * right.W));
        }

        public static void Normalize(ref Vector4 value, out Vector4 result)
        {
            Vector4 vector = value;
            result = vector;
            result.Normalize();
        }

        public static Vector4 Normalize(Vector4 value)
        {
            value.Normalize();
            return value;
        }

        public static void Lerp(ref Vector4 start, ref Vector4 end, float amount, out Vector4 result)
        {
            result.X = start.X + ((end.X - start.X) * amount);
            result.Y = start.Y + ((end.Y - start.Y) * amount);
            result.Z = start.Z + ((end.Z - start.Z) * amount);
            result.W = start.W + ((end.W - start.W) * amount);
        }

        public static Vector4 Lerp(Vector4 start, Vector4 end, float amount)
        {
            Vector4 vector;
            Lerp(ref start, ref end, amount, out vector);
            return vector;
        }

        public static void SmoothStep(ref Vector4 start, ref Vector4 end, float amount, out Vector4 result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.X = start.X + ((end.X - start.X) * amount);
            result.Y = start.Y + ((end.Y - start.Y) * amount);
            result.Z = start.Z + ((end.Z - start.Z) * amount);
            result.W = start.W + ((end.W - start.W) * amount);
        }

        public static Vector4 SmoothStep(Vector4 start, Vector4 end, float amount)
        {
            Vector4 vector;
            SmoothStep(ref start, ref end, amount, out vector);
            return vector;
        }

        public static void Hermite(ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, float amount, out Vector4 result)
        {
            float num = amount * amount;
            float num2 = amount * num;
            float num3 = ((2f * num2) - (3f * num)) + 1f;
            float num4 = (-2f * num2) + (3f * num);
            float num5 = (num2 - (2f * num)) + amount;
            float num6 = num2 - num;
            result = new Vector4((((value1.X * num3) + (value2.X * num4)) + (tangent1.X * num5)) + (tangent2.X * num6), (((value1.Y * num3) + (value2.Y * num4)) + (tangent1.Y * num5)) + (tangent2.Y * num6), (((value1.Z * num3) + (value2.Z * num4)) + (tangent1.Z * num5)) + (tangent2.Z * num6), (((value1.W * num3) + (value2.W * num4)) + (tangent1.W * num5)) + (tangent2.W * num6));
        }

        public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
        {
            Vector4 vector;
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out vector);
            return vector;
        }

        public static void CatmullRom(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, float amount, out Vector4 result)
        {
            float num = amount * amount;
            float num2 = amount * num;
            result.X = 0.5f * ((((2f * value2.X) + ((-value1.X + value3.X) * amount)) + (((((2f * value1.X) - (5f * value2.X)) + (4f * value3.X)) - value4.X) * num)) + ((((-value1.X + (3f * value2.X)) - (3f * value3.X)) + value4.X) * num2));
            result.Y = 0.5f * ((((2f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((2f * value1.Y) - (5f * value2.Y)) + (4f * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (3f * value2.Y)) - (3f * value3.Y)) + value4.Y) * num2));
            result.Z = 0.5f * ((((2f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((2f * value1.Z) - (5f * value2.Z)) + (4f * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (3f * value2.Z)) - (3f * value3.Z)) + value4.Z) * num2));
            result.W = 0.5f * ((((2f * value2.W) + ((-value1.W + value3.W) * amount)) + (((((2f * value1.W) - (5f * value2.W)) + (4f * value3.W)) - value4.W) * num)) + ((((-value1.W + (3f * value2.W)) - (3f * value3.W)) + value4.W) * num2));
        }

        public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
        {
            Vector4 vector;
            CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out vector);
            return vector;
        }

        public static void Max(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result.X = (left.X > right.X) ? left.X : right.X;
            result.Y = (left.Y > right.Y) ? left.Y : right.Y;
            result.Z = (left.Z > right.Z) ? left.Z : right.Z;
            result.W = (left.W > right.W) ? left.W : right.W;
        }

        public static Vector4 Max(Vector4 left, Vector4 right)
        {
            Vector4 vector;
            Max(ref left, ref right, out vector);
            return vector;
        }

        public static void Min(ref Vector4 left, ref Vector4 right, out Vector4 result)
        {
            result.X = (left.X < right.X) ? left.X : right.X;
            result.Y = (left.Y < right.Y) ? left.Y : right.Y;
            result.Z = (left.Z < right.Z) ? left.Z : right.Z;
            result.W = (left.W < right.W) ? left.W : right.W;
        }

        public static Vector4 Min(Vector4 left, Vector4 right)
        {
            Vector4 vector;
            Min(ref left, ref right, out vector);
            return vector;
        }

        public static void Orthogonalize(Vector4[] destination, params Vector4[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            }
            for (int i = 0; i < source.Length; i++)
            {
                Vector4 right = source[i];
                for (int j = 0; j < i; j++)
                {
                    right -= (Vector4)((Dot(destination[j], right) / Dot(destination[j], destination[j])) * destination[j]);
                }
                destination[i] = right;
            }
        }

        public static void Orthonormalize(Vector4[] destination, params Vector4[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            }
            for (int i = 0; i < source.Length; i++)
            {
                Vector4 right = source[i];
                for (int j = 0; j < i; j++)
                {
                    right -= (Vector4)(Dot(destination[j], right) * destination[j]);
                }
                right.Normalize();
                destination[i] = right;
            }
        }

        public static void Transform(ref Vector4 vector, ref Quaternion rotation, out Vector4 result)
        {
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            result = new Vector4(((vector.X * ((1f - num10) - num12)) + (vector.Y * (num8 - num6))) + (vector.Z * (num9 + num5)), ((vector.X * (num8 + num6)) + (vector.Y * ((1f - num7) - num12))) + (vector.Z * (num11 - num4)), ((vector.X * (num9 - num5)) + (vector.Y * (num11 + num4))) + (vector.Z * ((1f - num7) - num10)), vector.W);
        }

        public static Vector4 Transform(Vector4 vector, Quaternion rotation)
        {
            Vector4 vector2;
            Transform(ref vector, ref rotation, out vector2);
            return vector2;
        }

        public static void Transform(Vector4[] source, ref Quaternion rotation, Vector4[] destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            }
            float num = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W * num;
            float num5 = rotation.W * num2;
            float num6 = rotation.W * num3;
            float num7 = rotation.X * num;
            float num8 = rotation.X * num2;
            float num9 = rotation.X * num3;
            float num10 = rotation.Y * num2;
            float num11 = rotation.Y * num3;
            float num12 = rotation.Z * num3;
            float num13 = (1f - num10) - num12;
            float num14 = num8 - num6;
            float num15 = num9 + num5;
            float num16 = num8 + num6;
            float num17 = (1f - num7) - num12;
            float num18 = num11 - num4;
            float num19 = num9 - num5;
            float num20 = num11 + num4;
            float num21 = (1f - num7) - num10;
            for (int i = 0; i < source.Length; i++)
            {
                destination[i] = new Vector4(((source[i].X * num13) + (source[i].Y * num14)) + (source[i].Z * num15), ((source[i].X * num16) + (source[i].Y * num17)) + (source[i].Z * num18), ((source[i].X * num19) + (source[i].Y * num20)) + (source[i].Z * num21), source[i].W);
            }
        }

        public static void Transform(ref Vector4 vector, ref Matrix transform, out Vector4 result)
        {
            result = new Vector4((((vector.X * transform.M11) + (vector.Y * transform.M21)) + (vector.Z * transform.M31)) + (vector.W * transform.M41), (((vector.X * transform.M12) + (vector.Y * transform.M22)) + (vector.Z * transform.M32)) + (vector.W * transform.M42), (((vector.X * transform.M13) + (vector.Y * transform.M23)) + (vector.Z * transform.M33)) + (vector.W * transform.M43), (((vector.X * transform.M14) + (vector.Y * transform.M24)) + (vector.Z * transform.M34)) + (vector.W * transform.M44));
        }

        public static Vector4 Transform(Vector4 vector, Matrix transform)
        {
            Vector4 vector2;
            Transform(ref vector, ref transform, out vector2);
            return vector2;
        }

        public static void Transform(ref Vector4 vector, ref Matrix5x4 transform, out Vector4 result)
        {
            result = new Vector4(((((vector.X * transform.M11) + (vector.Y * transform.M21)) + (vector.Z * transform.M31)) + (vector.W * transform.M41)) + transform.M51, ((((vector.X * transform.M12) + (vector.Y * transform.M22)) + (vector.Z * transform.M32)) + (vector.W * transform.M42)) + transform.M52, ((((vector.X * transform.M13) + (vector.Y * transform.M23)) + (vector.Z * transform.M33)) + (vector.W * transform.M43)) + transform.M53, ((((vector.X * transform.M14) + (vector.Y * transform.M24)) + (vector.Z * transform.M34)) + (vector.W * transform.M44)) + transform.M54);
        }

        public static Vector4 Transform(Vector4 vector, Matrix5x4 transform)
        {
            Vector4 vector2;
            Transform(ref vector, ref transform, out vector2);
            return vector2;
        }

        public static void Transform(Vector4[] source, ref Matrix transform, Vector4[] destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (destination.Length < source.Length)
            {
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            }
            for (int i = 0; i < source.Length; i++)
            {
                Transform(ref source[i], ref transform, out destination[i]);
            }
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }

        public static Vector4 operator +(Vector4 value)
        {
            return value;
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4 operator -(Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static Vector4 operator *(float scale, Vector4 value)
        {
            return new Vector4(value.X * scale, value.Y * scale, value.Z * scale, value.W * scale);
        }

        public static Vector4 operator *(Vector4 value, float scale)
        {
            return new Vector4(value.X * scale, value.Y * scale, value.Z * scale, value.W * scale);
        }

        public static Vector4 operator /(Vector4 value, float scale)
        {
            return new Vector4(value.X / scale, value.Y / scale, value.Z / scale, value.W / scale);
        }

        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Vector2(Vector4 value)
        {
            return new Vector2(value.X, value.Y);
        }

        public static explicit operator Vector3(Vector4 value)
        {
            return new Vector3(value.X, value.Y, value.Z);
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
                this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2} W:{3}", new object[] { ((float)this.X).ToString(format, formatProvider), ((float)this.Y).ToString(format, formatProvider), ((float)this.Z).ToString(format, formatProvider), ((float)this.W).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (((((float)this.X).GetHashCode() + ((float)this.Y).GetHashCode()) + ((float)this.Z).GetHashCode()) + ((float)this.W).GetHashCode());
        }

        public bool Equals(Vector4 other)
        {
            return ((((Math.Abs((float)(other.X - this.X)) < 1E-06f) && (Math.Abs((float)(other.Y - this.Y)) < 1E-06f)) && (Math.Abs((float)(other.Z - this.Z)) < 1E-06f)) && (Math.Abs((float)(other.W - this.W)) < 1E-06f));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Vector4)))
            {
                return false;
            }
            return this.Equals((Vector4)value);
        }

        static Vector4()
        {
            SizeInBytes = Marshal.SizeOf((Type)typeof(Vector4));
            Zero = new Vector4();
            UnitX = new Vector4(1f, 0f, 0f, 0f);
            UnitY = new Vector4(0f, 1f, 0f, 0f);
            UnitZ = new Vector4(0f, 0f, 1f, 0f);
            UnitW = new Vector4(0f, 0f, 0f, 1f);
            One = new Vector4(1f, 1f, 1f, 1f);
        }
    }
}