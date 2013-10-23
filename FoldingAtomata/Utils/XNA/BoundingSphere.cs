using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct BoundingSphere : IEquatable<BoundingSphere>, IFormattable
    {
        public Vector3 Center;
        public float Radius;
        public BoundingSphere(Vector3 center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public bool Intersects(ref Ray ray)
        {
            float num;
            return Collision.RayIntersectsSphere(ref ray, ref this, out num);
        }

        public bool Intersects(ref Ray ray, out float distance)
        {
            return Collision.RayIntersectsSphere(ref ray, ref this, out distance);
        }

        public bool Intersects(ref Ray ray, out Vector3 point)
        {
            return Collision.RayIntersectsSphere(ref ray, ref this, out point);
        }

        public PlaneIntersectionType Intersects(ref Plane plane)
        {
            return Collision.PlaneIntersectsSphere(ref plane, ref this);
        }

        public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            return Collision.SphereIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3);
        }

        public bool Intersects(ref BoundingBox box)
        {
            return Collision.BoxIntersectsSphere(ref box, ref this);
        }

        public bool Intersects(ref BoundingSphere sphere)
        {
            return Collision.SphereIntersectsSphere(ref this, ref sphere);
        }

        public ContainmentType Contains(ref Vector3 point)
        {
            return Collision.SphereContainsPoint(ref this, ref point);
        }

        public ContainmentType Contains(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            return Collision.SphereContainsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3);
        }

        public ContainmentType Contains(ref BoundingBox box)
        {
            return Collision.SphereContainsBox(ref this, ref box);
        }

        public ContainmentType Contains(ref BoundingSphere sphere)
        {
            return Collision.SphereContainsSphere(ref this, ref sphere);
        }

        public static void FromPoints(Vector3[] points, out BoundingSphere result)
        {
            Vector3 zero = Vector3.Zero;
            for (int i = 0; i < points.Length; i++)
            {
                Vector3.Add(ref points[i], ref zero, out zero);
            }
            zero = (Vector3)(zero / ((float)points.Length));
            float num2 = 0f;
            for (int j = 0; j < points.Length; j++)
            {
                float num4;
                Vector3.DistanceSquared(ref zero, ref points[j], out num4);
                if (num4 > num2)
                {
                    num2 = num4;
                }
            }
            num2 = (float)Math.Sqrt((double)num2);
            result.Center = zero;
            result.Radius = num2;
        }

        public static BoundingSphere FromPoints(Vector3[] points)
        {
            BoundingSphere sphere;
            FromPoints(points, out sphere);
            return sphere;
        }

        public static void FromBox(ref BoundingBox box, out BoundingSphere result)
        {
            Vector3.Lerp(ref box.Minimum, ref box.Maximum, 0.5f, out result.Center);
            float num = box.Minimum.X - box.Maximum.X;
            float num2 = box.Minimum.Y - box.Maximum.Y;
            float num3 = box.Minimum.Z - box.Maximum.Z;
            float num4 = (float)Math.Sqrt((double)(((num * num) + (num2 * num2)) + (num3 * num3)));
            result.Radius = num4 * 0.5f;
        }

        public static BoundingSphere FromBox(BoundingBox box)
        {
            BoundingSphere sphere;
            FromBox(ref box, out sphere);
            return sphere;
        }

        public static void Merge(ref BoundingSphere value1, ref BoundingSphere value2, out BoundingSphere result)
        {
            Vector3 vector = value2.Center - value1.Center;
            float num = vector.Length();
            float radius = value1.Radius;
            float num3 = value2.Radius;
            if ((radius + num3) >= num)
            {
                if ((radius - num3) >= num)
                {
                    result = value1;
                    return;
                }
                if ((num3 - radius) >= num)
                {
                    result = value2;
                    return;
                }
            }
            Vector3 vector2 = (Vector3)(vector * (1f / num));
            float num4 = Math.Min(-radius, num - num3);
            float num5 = (Math.Max(radius, num + num3) - num4) * 0.5f;
            result.Center = value1.Center + ((Vector3)(vector2 * (num5 + num4)));
            result.Radius = num5;
        }

        public static BoundingSphere Merge(BoundingSphere value1, BoundingSphere value2)
        {
            BoundingSphere sphere;
            Merge(ref value1, ref value2, out sphere);
            return sphere;
        }

        public static bool operator ==(BoundingSphere left, BoundingSphere right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoundingSphere left, BoundingSphere right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Center:{0} Radius:{1}", new object[] { this.Center.ToString(), ((float)this.Radius).ToString() });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Center:{0} Radius:{1}", new object[] { this.Center.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), ((float)this.Radius).ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "Center:{0} Radius:{1}", new object[] { this.Center.ToString(), ((float)this.Radius).ToString() });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "Center:{0} Radius:{1}", new object[] { this.Center.ToString(format, formatProvider), ((float)this.Radius).ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (this.Center.GetHashCode() + ((float)this.Radius).GetHashCode());
        }

        public bool Equals(BoundingSphere value)
        {
            return ((this.Center == value.Center) && (this.Radius == value.Radius));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(BoundingSphere)))
            {
                return false;
            }
            return this.Equals((BoundingSphere)value);
        }
    }
}