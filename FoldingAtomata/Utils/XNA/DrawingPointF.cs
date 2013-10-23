using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DrawingPointF : IEquatable<DrawingPointF>
    {
        public float X;
        public float Y;
        public DrawingPointF(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(DrawingPointF other)
        {
            return ((other.X == this.X) && (other.Y == this.Y));
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(DrawingPointF))
            {
                return false;
            }
            return this.Equals((DrawingPointF)obj);
        }

        public override int GetHashCode()
        {
            return ((((float)this.X).GetHashCode() * 0x18d) ^ ((float)this.Y).GetHashCode());
        }

        public static bool operator ==(DrawingPointF left, DrawingPointF right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DrawingPointF left, DrawingPointF right)
        {
            return !left.Equals(right);
        }

        public static implicit operator DrawingPointF(Vector2 input)
        {
            return new DrawingPointF(input.X, input.Y);
        }

        public static implicit operator Vector2(DrawingPointF input)
        {
            return new Vector2(input.X, input.Y);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", (object[])new object[] { ((float)this.X), ((float)this.Y) });
        }
    }
}
