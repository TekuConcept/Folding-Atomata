using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Ray : IEquatable<Ray>, IFormattable
    {
        public Vector3 Position;
        public Vector3 Direction;
        public Ray(Vector3 position, Vector3 direction)
        {
            this.Position = position;
            this.Direction = direction;
        }

        public bool Intersects(ref Vector3 point)
        {
            return Collision.RayIntersectsPoint(ref this, ref point);
        }

        public bool Intersects(ref Ray ray)
        {
            Vector3 vector;
            return Collision.RayIntersectsRay(ref this, ref ray, out vector);
        }

        public bool Intersects(ref Ray ray, out Vector3 point)
        {
            return Collision.RayIntersectsRay(ref this, ref ray, out point);
        }

        public bool Intersects(ref Plane plane)
        {
            float num;
            return Collision.RayIntersectsPlane(ref this, ref plane, out num);
        }

        public bool Intersects(ref Plane plane, out float distance)
        {
            return Collision.RayIntersectsPlane(ref this, ref plane, out distance);
        }

        public bool Intersects(ref Plane plane, out Vector3 point)
        {
            return Collision.RayIntersectsPlane(ref this, ref plane, out point);
        }

        public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            float num;
            return Collision.RayIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3, out num);
        }

        public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out float distance)
        {
            return Collision.RayIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3, out distance);
        }

        public bool Intersects(ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out Vector3 point)
        {
            return Collision.RayIntersectsTriangle(ref this, ref vertex1, ref vertex2, ref vertex3, out point);
        }

        public bool Intersects(ref BoundingBox box)
        {
            float num;
            return Collision.RayIntersectsBox(ref this, ref box, out num);
        }

        public bool Intersects(ref BoundingBox box, out float distance)
        {
            return Collision.RayIntersectsBox(ref this, ref box, out distance);
        }

        public bool Intersects(ref BoundingBox box, out Vector3 point)
        {
            return Collision.RayIntersectsBox(ref this, ref box, out point);
        }

        public bool Intersects(ref BoundingSphere sphere)
        {
            float num;
            return Collision.RayIntersectsSphere(ref this, ref sphere, out num);
        }

        public bool Intersects(ref BoundingSphere sphere, out float distance)
        {
            return Collision.RayIntersectsSphere(ref this, ref sphere, out distance);
        }

        public bool Intersects(ref BoundingSphere sphere, out Vector3 point)
        {
            return Collision.RayIntersectsSphere(ref this, ref sphere, out point);
        }

        public static Ray GetPickRay(int x, int y, ViewportF viewport, Matrix worldViewProjection)
        {
            Vector3 vector = new Vector3((float)x, (float)y, 0f);
            Vector3 vector2 = new Vector3((float)x, (float)y, 1f);
            vector = Vector3.Unproject(vector, viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth, worldViewProjection);
            Vector3 direction = Vector3.Unproject(vector2, viewport.X, viewport.Y, viewport.Width, viewport.Height, viewport.MinDepth, viewport.MaxDepth, worldViewProjection) - vector;
            direction.Normalize();
            return new Ray(vector, direction);
        }

        public static bool operator ==(Ray left, Ray right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Ray left, Ray right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Position:{0} Direction:{1}", new object[] { this.Position.ToString(), this.Direction.ToString() });
        }

        public string ToString(string format)
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "Position:{0} Direction:{1}", new object[] { this.Position.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture), this.Direction.ToString(format, (IFormatProvider)CultureInfo.CurrentCulture) });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "Position:{0} Direction:{1}", new object[] { this.Position.ToString(), this.Direction.ToString() });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "Position:{0} Direction:{1}", new object[] { this.Position.ToString(format, formatProvider), this.Direction.ToString(format, formatProvider) });
        }

        public override int GetHashCode()
        {
            return (this.Position.GetHashCode() + this.Direction.GetHashCode());
        }

        public bool Equals(Ray value)
        {
            return ((this.Position == value.Position) && (this.Direction == value.Direction));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Ray)))
            {
                return false;
            }
            return this.Equals((Ray)value);
        }
    }
}