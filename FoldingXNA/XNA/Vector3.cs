﻿using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        public static readonly int SizeInBytes;
        public static readonly Vector3 Zero;
        public static readonly Vector3 UnitX;
        public static readonly Vector3 UnitY;
        public static readonly Vector3 UnitZ;
        public static readonly Vector3 One;
        public float X;
        public float Y;
        public float Z;
        public Vector3(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(Vector2 value, float z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        public Vector3(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 3)
            {
                throw new ArgumentOutOfRangeException("values", "There must be three and only three input values for Vector3.");
            }
            this.X = values[0];
            this.Y = values[1];
            this.Z = values[2];
        }

        public bool IsNormalized
        {
            get
            {
                return (Math.Abs((float)((((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)) - 1f)) < 1E-06f);
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
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
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
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Vector3 run from 0 to 2, inclusive.");
            }
        }
        public float Length()
        {
            return (float)Math.Sqrt((double)(((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z)));
        }

        public float LengthSquared()
        {
            return (((this.X * this.X) + (this.Y * this.Y)) + (this.Z * this.Z));
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
            }
        }

        public float[] ToArray()
        {
            return new float[] { this.X, this.Y, this.Z };
        }

        public static void Add(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static void Subtract(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static void Multiply(ref Vector3 value, float scale, out Vector3 result)
        {
            result = new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static Vector3 Multiply(Vector3 value, float scale)
        {
            return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static void Modulate(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3 Modulate(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static void Divide(ref Vector3 value, float scale, out Vector3 result)
        {
            result = new Vector3(value.X / scale, value.Y / scale, value.Z / scale);
        }

        public static Vector3 Divide(Vector3 value, float scale)
        {
            return new Vector3(value.X / scale, value.Y / scale, value.Z / scale);
        }

        public static void Negate(ref Vector3 value, out Vector3 result)
        {
            result = new Vector3(-value.X, -value.Y, -value.Z);
        }

        public static Vector3 Negate(Vector3 value)
        {
            return new Vector3(-value.X, -value.Y, -value.Z);
        }

        public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1, float amount2, out Vector3 result)
        {
            result = new Vector3((value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X)), (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y)), (value1.Z + (amount1 * (value2.Z - value1.Z))) + (amount2 * (value3.Z - value1.Z)));
        }

        public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
        {
            Vector3 vector;
            Barycentric(ref value1, ref value2, ref value3, amount1, amount2, out vector);
            return vector;
        }

        public static void Clamp(ref Vector3 value, ref Vector3 min, ref Vector3 max, out Vector3 result)
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
            result = new Vector3(x, y, z);
        }

        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            Vector3 vector;
            Clamp(ref value, ref min, ref max, out vector);
            return vector;
        }

        public static void Cross(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result = new Vector3((left.Y * right.Z) - (left.Z * right.Y), (left.Z * right.X) - (left.X * right.Z), (left.X * right.Y) - (left.Y * right.X));
        }

        public static Vector3 Cross(Vector3 left, Vector3 right)
        {
            Vector3 vector;
            Cross(ref left, ref right, out vector);
            return vector;
        }

        public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            result = (float)Math.Sqrt((double)(((num * num) + (num2 * num2)) + (num3 * num3)));
        }

        public static float Distance(Vector3 value1, Vector3 value2)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            return (float)Math.Sqrt((double)(((num * num) + (num2 * num2)) + (num3 * num3)));
        }

        public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            result = ((num * num) + (num2 * num2)) + (num3 * num3);
        }

        public static float DistanceSquared(Vector3 value1, Vector3 value2)
        {
            float num = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            return (((num * num) + (num2 * num2)) + (num3 * num3));
        }

        public static void Dot(ref Vector3 left, ref Vector3 right, out float result)
        {
            result = ((left.X * right.X) + (left.Y * right.Y)) + (left.Z * right.Z);
        }

        public static float Dot(Vector3 left, Vector3 right)
        {
            return (((left.X * right.X) + (left.Y * right.Y)) + (left.Z * right.Z));
        }

        public static void Normalize(ref Vector3 value, out Vector3 result)
        {
            result = value;
            result.Normalize();
        }

        public static Vector3 Normalize(Vector3 value)
        {
            value.Normalize();
            return value;
        }

        public static void Lerp(ref Vector3 start, ref Vector3 end, float amount, out Vector3 result)
        {
            result.X = start.X + ((end.X - start.X) * amount);
            result.Y = start.Y + ((end.Y - start.Y) * amount);
            result.Z = start.Z + ((end.Z - start.Z) * amount);
        }

        public static Vector3 Lerp(Vector3 start, Vector3 end, float amount)
        {
            Vector3 vector;
            Lerp(ref start, ref end, amount, out vector);
            return vector;
        }

        public static void SmoothStep(ref Vector3 start, ref Vector3 end, float amount, out Vector3 result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.X = start.X + ((end.X - start.X) * amount);
            result.Y = start.Y + ((end.Y - start.Y) * amount);
            result.Z = start.Z + ((end.Z - start.Z) * amount);
        }

        public static Vector3 SmoothStep(Vector3 start, Vector3 end, float amount)
        {
            Vector3 vector;
            SmoothStep(ref start, ref end, amount, out vector);
            return vector;
        }

        public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, float amount, out Vector3 result)
        {
            float num = amount * amount;
            float num2 = amount * num;
            float num3 = ((2f * num2) - (3f * num)) + 1f;
            float num4 = (-2f * num2) + (3f * num);
            float num5 = (num2 - (2f * num)) + amount;
            float num6 = num2 - num;
            result.X = (((value1.X * num3) + (value2.X * num4)) + (tangent1.X * num5)) + (tangent2.X * num6);
            result.Y = (((value1.Y * num3) + (value2.Y * num4)) + (tangent1.Y * num5)) + (tangent2.Y * num6);
            result.Z = (((value1.Z * num3) + (value2.Z * num4)) + (tangent1.Z * num5)) + (tangent2.Z * num6);
        }

        public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
        {
            Vector3 vector;
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out vector);
            return vector;
        }

        public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, float amount, out Vector3 result)
        {
            float num = amount * amount;
            float num2 = amount * num;
            result.X = 0.5f * ((((2f * value2.X) + ((-value1.X + value3.X) * amount)) + (((((2f * value1.X) - (5f * value2.X)) + (4f * value3.X)) - value4.X) * num)) + ((((-value1.X + (3f * value2.X)) - (3f * value3.X)) + value4.X) * num2));
            result.Y = 0.5f * ((((2f * value2.Y) + ((-value1.Y + value3.Y) * amount)) + (((((2f * value1.Y) - (5f * value2.Y)) + (4f * value3.Y)) - value4.Y) * num)) + ((((-value1.Y + (3f * value2.Y)) - (3f * value3.Y)) + value4.Y) * num2));
            result.Z = 0.5f * ((((2f * value2.Z) + ((-value1.Z + value3.Z) * amount)) + (((((2f * value1.Z) - (5f * value2.Z)) + (4f * value3.Z)) - value4.Z) * num)) + ((((-value1.Z + (3f * value2.Z)) - (3f * value3.Z)) + value4.Z) * num2));
        }

        public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
        {
            Vector3 vector;
            CatmullRom(ref value1, ref value2, ref value3, ref value4, amount, out vector);
            return vector;
        }

        public static void Max(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result.X = (left.X > right.X) ? left.X : right.X;
            result.Y = (left.Y > right.Y) ? left.Y : right.Y;
            result.Z = (left.Z > right.Z) ? left.Z : right.Z;
        }

        public static Vector3 Max(Vector3 left, Vector3 right)
        {
            Vector3 vector;
            Max(ref left, ref right, out vector);
            return vector;
        }

        public static void Min(ref Vector3 left, ref Vector3 right, out Vector3 result)
        {
            result.X = (left.X < right.X) ? left.X : right.X;
            result.Y = (left.Y < right.Y) ? left.Y : right.Y;
            result.Z = (left.Z < right.Z) ? left.Z : right.Z;
        }

        public static Vector3 Min(Vector3 left, Vector3 right)
        {
            Vector3 vector;
            Min(ref left, ref right, out vector);
            return vector;
        }

        public static void Project(ref Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, ref Matrix worldViewProjection, out Vector3 result)
        {
            Vector3 vector2 = new Vector3();
            TransformCoordinate(ref vector, ref worldViewProjection, out vector2);
            result = new Vector3((((1f + vector2.X) * 0.5f) * width) + x, (((1f - vector2.Y) * 0.5f) * height) + y, (vector2.Z * (maxZ - minZ)) + minZ);
        }

        public static Vector3 Project(Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection)
        {
            Vector3 vector2;
            Project(ref vector, x, y, width, height, minZ, maxZ, ref worldViewProjection, out vector2);
            return vector2;
        }

        public static void Unproject(ref Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, ref Matrix worldViewProjection, out Vector3 result)
        {
            Vector3 coordinate = new Vector3();
            Matrix matrix = new Matrix();
            Matrix.Invert(ref worldViewProjection, out matrix);
            coordinate.X = (((vector.X - x) / width) * 2f) - 1f;
            coordinate.Y = -((((vector.Y - y) / height) * 2f) - 1f);
            coordinate.Z = (vector.Z - minZ) / (maxZ - minZ);
            TransformCoordinate(ref coordinate, ref matrix, out result);
        }

        public static Vector3 Unproject(Vector3 vector, float x, float y, float width, float height, float minZ, float maxZ, Matrix worldViewProjection)
        {
            Vector3 vector2;
            Unproject(ref vector, x, y, width, height, minZ, maxZ, ref worldViewProjection, out vector2);
            return vector2;
        }

        public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            float num = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
            result.X = vector.X - ((2f * num) * normal.X);
            result.Y = vector.Y - ((2f * num) * normal.Y);
            result.Z = vector.Z - ((2f * num) * normal.Z);
        }

        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            Vector3 vector2;
            Reflect(ref vector, ref normal, out vector2);
            return vector2;
        }

        public static void Orthogonalize(Vector3[] destination, params Vector3[] source)
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
                Vector3 right = source[i];
                for (int j = 0; j < i; j++)
                {
                    right -= (Vector3)((Dot(destination[j], right) / Dot(destination[j], destination[j])) * destination[j]);
                }
                destination[i] = right;
            }
        }

        public static void Orthonormalize(Vector3[] destination, params Vector3[] source)
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
                Vector3 right = source[i];
                for (int j = 0; j < i; j++)
                {
                    right -= (Vector3)(Dot(destination[j], right) * destination[j]);
                }
                right.Normalize();
                destination[i] = right;
            }
        }

        public static void Transform(ref Vector3 vector, ref Quaternion rotation, out Vector3 result)
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
            result = new Vector3(((vector.X * ((1f - num10) - num12)) + (vector.Y * (num8 - num6))) + (vector.Z * (num9 + num5)), ((vector.X * (num8 + num6)) + (vector.Y * ((1f - num7) - num12))) + (vector.Z * (num11 - num4)), ((vector.X * (num9 - num5)) + (vector.Y * (num11 + num4))) + (vector.Z * ((1f - num7) - num10)));
        }

        public static Vector3 Transform(Vector3 vector, Quaternion rotation)
        {
            Vector3 vector2;
            Transform(ref vector, ref rotation, out vector2);
            return vector2;
        }

        public static void Transform(Vector3[] source, ref Quaternion rotation, Vector3[] destination)
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
                destination[i] = new Vector3(((source[i].X * num13) + (source[i].Y * num14)) + (source[i].Z * num15), ((source[i].X * num16) + (source[i].Y * num17)) + (source[i].Z * num18), ((source[i].X * num19) + (source[i].Y * num20)) + (source[i].Z * num21));
            }
        }

        public static void Transform(ref Vector3 vector, ref Matrix transform, out Vector4 result)
        {
            result = new Vector4((((vector.X * transform.M11) + (vector.Y * transform.M21)) + (vector.Z * transform.M31)) + transform.M41, (((vector.X * transform.M12) + (vector.Y * transform.M22)) + (vector.Z * transform.M32)) + transform.M42, (((vector.X * transform.M13) + (vector.Y * transform.M23)) + (vector.Z * transform.M33)) + transform.M43, (((vector.X * transform.M14) + (vector.Y * transform.M24)) + (vector.Z * transform.M34)) + transform.M44);
        }

        public static Vector4 Transform(Vector3 vector, Matrix transform)
        {
            Vector4 vector2;
            Transform(ref vector, ref transform, out vector2);
            return vector2;
        }

        public static void Transform(Vector3[] source, ref Matrix transform, Vector4[] destination)
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

        public static void TransformCoordinate(ref Vector3 coordinate, ref Matrix transform, out Vector3 result)
        {
            Vector4 vector = new Vector4();
            vector.X = (((coordinate.X * transform.M11) + (coordinate.Y * transform.M21)) + (coordinate.Z * transform.M31)) + transform.M41;
            vector.Y = (((coordinate.X * transform.M12) + (coordinate.Y * transform.M22)) + (coordinate.Z * transform.M32)) + transform.M42;
            vector.Z = (((coordinate.X * transform.M13) + (coordinate.Y * transform.M23)) + (coordinate.Z * transform.M33)) + transform.M43;
            vector.W = 1f / ((((coordinate.X * transform.M14) + (coordinate.Y * transform.M24)) + (coordinate.Z * transform.M34)) + transform.M44);
            result = new Vector3(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W);
        }

        public static Vector3 TransformCoordinate(Vector3 coordinate, Matrix transform)
        {
            Vector3 vector;
            TransformCoordinate(ref coordinate, ref transform, out vector);
            return vector;
        }

        public static void TransformCoordinate(Vector3[] source, ref Matrix transform, Vector3[] destination)
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
                TransformCoordinate(ref source[i], ref transform, out destination[i]);
            }
        }

        public static void TransformNormal(ref Vector3 normal, ref Matrix transform, out Vector3 result)
        {
            result = new Vector3(((normal.X * transform.M11) + (normal.Y * transform.M21)) + (normal.Z * transform.M31), ((normal.X * transform.M12) + (normal.Y * transform.M22)) + (normal.Z * transform.M32), ((normal.X * transform.M13) + (normal.Y * transform.M23)) + (normal.Z * transform.M33));
        }

        public static Vector3 TransformNormal(Vector3 normal, Matrix transform)
        {
            Vector3 vector;
            TransformNormal(ref normal, ref transform, out vector);
            return vector;
        }

        public static void TransformNormal(Vector3[] source, ref Matrix transform, Vector3[] destination)
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
                TransformNormal(ref source[i], ref transform, out destination[i]);
            }
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static Vector3 operator +(Vector3 value)
        {
            return value;
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3 operator -(Vector3 value)
        {
            return new Vector3(-value.X, -value.Y, -value.Z);
        }

        public static Vector3 operator *(float scale, Vector3 value)
        {
            return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static Vector3 operator *(Vector3 value, float scale)
        {
            return new Vector3(value.X * scale, value.Y * scale, value.Z * scale);
        }

        public static Vector3 operator /(Vector3 value, float scale)
        {
            return new Vector3(value.X / scale, value.Y / scale, value.Z / scale);
        }

        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Vector2(Vector3 value)
        {
            return new Vector2(value.X, value.Y);
        }

        public static explicit operator Vector4(Vector3 value)
        {
            return new Vector4(value, 0f);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", new object[] { (float)this.X, (float)this.Y, (float)this.Z });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "X:{0} Y:{1} Z:{2}", new object[] { ((float)this.X).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Y).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Z).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", new object[] { (float)this.X, (float)this.Y, (float)this.Z });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "X:{0} Y:{1} Z:{2}", new object[] { ((float)this.X).ToString(format, formatProvider), ((float)this.Y).ToString(format, formatProvider), ((float)this.Z).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return ((((float)this.X).GetHashCode() + ((float)this.Y).GetHashCode()) + ((float)this.Z).GetHashCode());
        }

        public bool Equals(Vector3 other)
        {
            return (((Math.Abs((float)(other.X - this.X)) < 1E-06f) && (Math.Abs((float)(other.Y - this.Y)) < 1E-06f)) && (Math.Abs((float)(other.Z - this.Z)) < 1E-06f));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Vector3)))
            {
                return false;
            }
            return this.Equals((Vector3)value);
        }

        static Vector3()
        {
            SizeInBytes = Marshal.SizeOf((Type)typeof(Vector3));
            Zero = new Vector3();
            UnitX = new Vector3(1f, 0f, 0f);
            UnitY = new Vector3(0f, 1f, 0f);
            UnitZ = new Vector3(0f, 0f, 1f);
            One = new Vector3(1f, 1f, 1f);
        }
    }
}