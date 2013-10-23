using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DrawingPoint : IEquatable<DrawingPoint>
    {
        public int X;
        public int Y;
        public DrawingPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Equals(DrawingPoint other)
        {
            return ((other.X == this.X) && (other.Y == this.Y));
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(DrawingPoint))
            {
                return false;
            }
            return this.Equals((DrawingPoint)obj);
        }

        public override int GetHashCode()
        {
            return ((this.X * 0x18d) ^ this.Y);
        }

        public static bool operator ==(DrawingPoint left, DrawingPoint right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DrawingPoint left, DrawingPoint right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", (object[])new object[] { ((int)this.X), ((int)this.Y) });
        }
    }
}
