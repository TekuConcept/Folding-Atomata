using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct BoundingBox : IEquatable<BoundingBox>, IFormattable
    {
        public Vector3 Minimum;
        public Vector3 Maximum;
        public BoundingBox(Vector3 minimum, Vector3 maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        public Vector3[] GetCorners()
        {
            return new Vector3[] { new Vector3(this.Minimum.X, this.Maximum.Y, this.Maximum.Z), new Vector3(this.Maximum.X, this.Maximum.Y, this.Maximum.Z), new Vector3(this.Maximum.X, this.Minimum.Y, this.Maximum.Z), new Vector3(this.Minimum.X, this.Minimum.Y, this.Maximum.Z), new Vector3(this.Minimum.X, this.Maximum.Y, this.Minimum.Z), new Vector3(this.Maximum.X, this.Maximum.Y, this.Minimum.Z), new Vector3(this.Maximum.X, this.Minimum.Y, this.Minimum.Z), new Vector3(this.Minimum.X, this.Minimum.Y, this.Minimum.Z) };
        }

        public bool Intersects(ref Ray ray)
        {
            float num;
            return Collision.RayIntersectsBox(ref ray, ref this, out num);
        }

        public bool Intersects(ref Ray ray, out float distance)
        {
            return Collision.RayIntersectsBox(ref ray, ref this, out distance);
        }

        public bool Intersects(ref Ray ray, out Vector3 point)
        {
            return Collision.RayIntersectsBox(ref ray, ref this, out point);
        }

        public PlaneIntersectionType Intersects(ref Plane plane)
        {
            return Collision.PlaneIntersectsBox(ref plane, ref this);
        }

        public bool Intersects(ref BoundingBox box)
        {
            return Collision.BoxIntersectsBox(ref this, ref box);
        }

        public bool Intersects(ref BoundingSphere sphere)
        {
            return Collision.BoxIntersectsSphere(ref this, ref sphere);
        }

        public ContainmentType Contains(ref Vector3 point)
        {
            return Collision.BoxContainsPoint(ref this, ref point);
        }

        public ContainmentType Contains(ref BoundingBox box)
        {
            return Collision.BoxContainsBox(ref this, ref box);
        }

        public ContainmentType Contains(ref BoundingSphere sphere)
        {
            return Collision.BoxContainsSphere(ref this, ref sphere);
        }

        public static void FromPoints(Vector3[] points, out BoundingBox result)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            Vector3 left = new Vector3(float.MaxValue);
            Vector3 vector2 = new Vector3(float.MinValue);
            for (int i = 0; i < points.Length; i++)
            {
                Vector3.Min(ref left, ref points[i], out left);
                Vector3.Max(ref vector2, ref points[i], out vector2);
            }
            result = new BoundingBox(left, vector2);
        }

        public static BoundingBox FromPoints(Vector3[] points)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }
            Vector3 left = new Vector3(float.MaxValue);
            Vector3 vector2 = new Vector3(float.MinValue);
            for (int i = 0; i < points.Length; i++)
            {
                Vector3.Min(ref left, ref points[i], out left);
                Vector3.Max(ref vector2, ref points[i], out vector2);
            }
            return new BoundingBox(left, vector2);
        }

        public static void FromSphere(ref BoundingSphere sphere, out BoundingBox result)
        {
            result.Minimum = new Vector3(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, sphere.Center.Z - sphere.Radius);
            result.Maximum = new Vector3(sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius, sphere.Center.Z + sphere.Radius);
        }

        public static BoundingBox FromSphere(BoundingSphere sphere)
        {
            BoundingBox box;
            box.Minimum = new Vector3(sphere.Center.X - sphere.Radius, sphere.Center.Y - sphere.Radius, sphere.Center.Z - sphere.Radius);
            box.Maximum = new Vector3(sphere.Center.X + sphere.Radius, sphere.Center.Y + sphere.Radius, sphere.Center.Z + sphere.Radius);
            return box;
        }

        public static void Merge(ref BoundingBox value1, ref BoundingBox value2, out BoundingBox result)
        {
            Vector3.Min(ref value1.Minimum, ref value2.Minimum, out result.Minimum);
            Vector3.Max(ref value1.Maximum, ref value2.Maximum, out result.Maximum);
        }

        public static BoundingBox Merge(BoundingBox value1, BoundingBox value2)
        {
            BoundingBox box;
            Vector3.Min(ref value1.Minimum, ref value2.Minimum, out box.Minimum);
            Vector3.Max(ref value1.Maximum, ref value2.Maximum, out box.Maximum);
            return box;
        }

        public static bool operator ==(BoundingBox left, BoundingBox right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoundingBox left, BoundingBox right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Minimum:{0} Maximum:{1}", new object[] { this.Minimum.ToString(), this.Maximum.ToString() });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Minimum:{0} Maximum:{1}", new object[] { this.Minimum.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), this.Maximum.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "Minimum:{0} Maximum:{1}", new object[] { this.Minimum.ToString(), this.Maximum.ToString() });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(formatProvider, "Minimum:{0} Maximum:{1}", new object[] { this.Minimum.ToString(format, formatProvider), this.Maximum.ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (this.Minimum.GetHashCode() + this.Maximum.GetHashCode());
        }

        public bool Equals(BoundingBox value)
        {
            return ((this.Minimum == value.Minimum) && (this.Maximum == value.Maximum));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(BoundingBox)))
            {
                return false;
            }
            return this.Equals((BoundingBox)value);
        }
    }
}