using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Plane : IEquatable<Plane>, IFormattable
    {
        public Vector3 Normal;
        public float D;
        public Plane(float value)
        {
            this.Normal.X = this.Normal.Y = this.Normal.Z = this.D = value;
        }

        public Plane(float a, float b, float c, float d)
        {
            this.Normal.X = a;
            this.Normal.Y = b;
            this.Normal.Z = c;
            this.D = d;
        }

        public Plane(Vector3 point, Vector3 normal)
        {
            this.Normal = normal;
            this.D = -Vector3.Dot(normal, point);
        }

        public Plane(Vector3 value, float d)
        {
            this.Normal = value;
            this.D = d;
        }

        public Plane(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            float num = point2.X - point1.X;
            float num2 = point2.Y - point1.Y;
            float num3 = point2.Z - point1.Z;
            float num4 = point3.X - point1.X;
            float num5 = point3.Y - point1.Y;
            float num6 = point3.Z - point1.Z;
            float num7 = (num2 * num6) - (num3 * num5);
            float num8 = (num3 * num4) - (num * num6);
            float num9 = (num * num5) - (num2 * num4);
            float num10 = 1f / ((float)Math.Sqrt((double)(((num7 * num7) + (num8 * num8)) + (num9 * num9))));
            this.Normal.X = num7 * num10;
            this.Normal.Y = num8 * num10;
            this.Normal.Z = num9 * num10;
            this.D = -(((this.Normal.X * point1.X) + (this.Normal.Y * point1.Y)) + (this.Normal.Z * point1.Z));
        }

        public Plane(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 4)
            {
                throw new ArgumentOutOfRangeException("values", "There must be four and only four input values for Plane.");
            }
            this.Normal.X = values[0];
            this.Normal.Y = values[1];
            this.Normal.Z = values[2];
            this.D = values[3];
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.Normal.X;

                    case 1:
                        return this.Normal.Y;

                    case 2:
                        return this.Normal.Z;

                    case 3:
                        return this.D;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Plane run from 0 to 3, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.Normal.X = value;
                        return;

                    case 1:
                        this.Normal.Y = value;
                        return;

                    case 2:
                        this.Normal.Z = value;
                        return;

                    case 3:
                        this.D = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Plane run from 0 to 3, inclusive.");
            }
        }
        public void Normalize()
        {
            float num = 1f / ((float)Math.Sqrt((double)(((this.Normal.X * this.Normal.X) + (this.Normal.Y * this.Normal.Y)) + (this.Normal.Z * this.Normal.Z))));
            this.Normal.X *= num;
            this.Normal.Y *= num;
            this.Normal.Z *= num;
            this.D *= num;
        }

        public float[] ToArray()
        {
            return new float[] { this.Normal.X, this.Normal.Y, this.Normal.Z, this.D };
        }

        public PlaneIntersectionType Intersects(ref Vector3 point)
        {
            return Collision.PlaneIntersectsPoint(ref this, ref point);
        }

        public bool Intersects(ref Ray ray)
        {
            float num;
            return Collision.RayIntersectsPlane(ref ray, ref this, out num);
        }

        public bool Intersects(ref Ray ray, out float distance)
        {
            return Collision.RayIntersectsPlane(ref ray, ref this, out distance);
        }

        public bool Intersects(ref Ray ray, out Vector3 point)
        {
            return Collision.RayIntersectsPlane(ref ray, ref this, out point);
        }

        public bool Intersects(ref Plane plane)
        {
            return Collision.PlaneIntersectsPlane(ref this, ref plane);
        }

        public bool Intersects(ref Plane plane, out Ray line)
        {
            return Collision.PlaneIntersectsPlane(ref this, ref plane, out line);
        }

        public PlaneIntersectionType Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            return Collision.PlaneIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3);
        }

        public PlaneIntersectionType Intersects(ref BoundingBox box)
        {
            return Collision.PlaneIntersectsBox(ref this, ref box);
        }

        public PlaneIntersectionType Intersects(ref BoundingSphere sphere)
        {
            return Collision.PlaneIntersectsSphere(ref this, ref sphere);
        }

        public static void Multiply(ref Plane value, float scale, out Plane result)
        {
            result.Normal.X = value.Normal.X * scale;
            result.Normal.Y = value.Normal.Y * scale;
            result.Normal.Z = value.Normal.Z * scale;
            result.D = value.D * scale;
        }

        public static Plane Multiply(Plane value, float scale)
        {
            return new Plane(value.Normal.X * scale, value.Normal.Y * scale, value.Normal.Z * scale, value.D * scale);
        }

        public static void Dot(ref Plane left, ref Vector4 right, out float result)
        {
            result = (((left.Normal.X * right.X) + (left.Normal.Y * right.Y)) + (left.Normal.Z * right.Z)) + (left.D * right.W);
        }

        public static float Dot(Plane left, Vector4 right)
        {
            return ((((left.Normal.X * right.X) + (left.Normal.Y * right.Y)) + (left.Normal.Z * right.Z)) + (left.D * right.W));
        }

        public static void DotCoordinate(ref Plane left, ref Vector3 right, out float result)
        {
            result = (((left.Normal.X * right.X) + (left.Normal.Y * right.Y)) + (left.Normal.Z * right.Z)) + left.D;
        }

        public static float DotCoordinate(Plane left, Vector3 right)
        {
            return ((((left.Normal.X * right.X) + (left.Normal.Y * right.Y)) + (left.Normal.Z * right.Z)) + left.D);
        }

        public static void DotNormal(ref Plane left, ref Vector3 right, out float result)
        {
            result = ((left.Normal.X * right.X) + (left.Normal.Y * right.Y)) + (left.Normal.Z * right.Z);
        }

        public static float DotNormal(Plane left, Vector3 right)
        {
            return (((left.Normal.X * right.X) + (left.Normal.Y * right.Y)) + (left.Normal.Z * right.Z));
        }

        public static void Normalize(ref Plane plane, out Plane result)
        {
            float num = 1f / ((float)Math.Sqrt((double)(((plane.Normal.X * plane.Normal.X) + (plane.Normal.Y * plane.Normal.Y)) + (plane.Normal.Z * plane.Normal.Z))));
            result.Normal.X = plane.Normal.X * num;
            result.Normal.Y = plane.Normal.Y * num;
            result.Normal.Z = plane.Normal.Z * num;
            result.D = plane.D * num;
        }

        public static Plane Normalize(Plane plane)
        {
            float num = 1f / ((float)Math.Sqrt((double)(((plane.Normal.X * plane.Normal.X) + (plane.Normal.Y * plane.Normal.Y)) + (plane.Normal.Z * plane.Normal.Z))));
            return new Plane(plane.Normal.X * num, plane.Normal.Y * num, plane.Normal.Z * num, plane.D * num);
        }

        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
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
            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;
            result.Normal.X = ((x * ((1f - num10) - num12)) + (y * (num8 - num6))) + (z * (num9 + num5));
            result.Normal.Y = ((x * (num8 + num6)) + (y * ((1f - num7) - num12))) + (z * (num11 - num4));
            result.Normal.Z = ((x * (num9 - num5)) + (y * (num11 + num4))) + (z * ((1f - num7) - num10));
            result.D = plane.D;
        }

        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            Plane plane2;
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
            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;
            plane2.Normal.X = ((x * ((1f - num10) - num12)) + (y * (num8 - num6))) + (z * (num9 + num5));
            plane2.Normal.Y = ((x * (num8 + num6)) + (y * ((1f - num7) - num12))) + (z * (num11 - num4));
            plane2.Normal.Z = ((x * (num9 - num5)) + (y * (num11 + num4))) + (z * ((1f - num7) - num10));
            plane2.D = plane.D;
            return plane2;
        }

        public static void Transform(Plane[] planes, ref Quaternion rotation)
        {
            if (planes == null)
            {
                throw new ArgumentNullException("planes");
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
            for (int i = 0; i < planes.Length; i++)
            {
                float x = planes[i].Normal.X;
                float y = planes[i].Normal.Y;
                float z = planes[i].Normal.Z;
                planes[i].Normal.X = ((x * ((1f - num10) - num12)) + (y * (num8 - num6))) + (z * (num9 + num5));
                planes[i].Normal.Y = ((x * (num8 + num6)) + (y * ((1f - num7) - num12))) + (z * (num11 - num4));
                planes[i].Normal.Z = ((x * (num9 - num5)) + (y * (num11 + num4))) + (z * ((1f - num7) - num10));
            }
        }

        public static void Transform(ref Plane plane, ref Matrix transformation, out Plane result)
        {
            Matrix matrix;
            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;
            float d = plane.D;
            Matrix.Invert(ref transformation, out matrix);
            result.Normal.X = (((x * matrix.M11) + (y * matrix.M12)) + (z * matrix.M13)) + (d * matrix.M14);
            result.Normal.Y = (((x * matrix.M21) + (y * matrix.M22)) + (z * matrix.M23)) + (d * matrix.M24);
            result.Normal.Z = (((x * matrix.M31) + (y * matrix.M32)) + (z * matrix.M33)) + (d * matrix.M34);
            result.D = (((x * matrix.M41) + (y * matrix.M42)) + (z * matrix.M43)) + (d * matrix.M44);
        }

        public static Plane Transform(Plane plane, Matrix transformation)
        {
            Plane plane2;
            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;
            float d = plane.D;
            transformation.Invert();
            plane2.Normal.X = (((x * transformation.M11) + (y * transformation.M12)) + (z * transformation.M13)) + (d * transformation.M14);
            plane2.Normal.Y = (((x * transformation.M21) + (y * transformation.M22)) + (z * transformation.M23)) + (d * transformation.M24);
            plane2.Normal.Z = (((x * transformation.M31) + (y * transformation.M32)) + (z * transformation.M33)) + (d * transformation.M34);
            plane2.D = (((x * transformation.M41) + (y * transformation.M42)) + (z * transformation.M43)) + (d * transformation.M44);
            return plane2;
        }

        public static void Transform(Plane[] planes, ref Matrix transformation)
        {
            Matrix matrix;
            if (planes == null)
            {
                throw new ArgumentNullException("planes");
            }
            Matrix.Invert(ref transformation, out matrix);
            for (int i = 0; i < planes.Length; i++)
            {
                Transform(ref planes[i], ref transformation, out planes[i]);
            }
        }

        public static Plane operator *(float scale, Plane plane)
        {
            return new Plane(plane.Normal.X * scale, plane.Normal.Y * scale, plane.Normal.Z * scale, plane.D * scale);
        }

        public static Plane operator *(Plane plane, float scale)
        {
            return new Plane(plane.Normal.X * scale, plane.Normal.Y * scale, plane.Normal.Z * scale, plane.D * scale);
        }

        public static bool operator ==(Plane left, Plane right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Plane left, Plane right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "A:{0} B:{1} C:{2} D:{3}", new object[] { (float)this.Normal.X, (float)this.Normal.Y, (float)this.Normal.Z, (float)this.D });
        }

        public string ToString(string format)
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "A:{0} B:{1} C:{2} D:{3}", new object[] { ((float)this.Normal.X).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Normal.Y).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Normal.Z).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.D).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "A:{0} B:{1} C:{2} D:{3}", new object[] { (float)this.Normal.X, (float)this.Normal.Y, (float)this.Normal.Z, (float)this.D });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "A:{0} B:{1} C:{2} D:{3}", new object[] { ((float)this.Normal.X).ToString(format, formatProvider), ((float)this.Normal.Y).ToString(format, formatProvider), ((float)this.Normal.Z).ToString(format, formatProvider), ((float)this.D).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (this.Normal.GetHashCode() + ((float)this.D).GetHashCode());
        }

        public bool Equals(Plane value)
        {
            return ((this.Normal == value.Normal) && (this.D == value.D));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Plane)))
            {
                return false;
            }
            return this.Equals((Plane)value);
        }
    }
}