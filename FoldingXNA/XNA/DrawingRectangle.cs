using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DrawingRectangle : IEquatable<DrawingRectangle>
    {
        public static readonly DrawingRectangle Empty;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public DrawingRectangle(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public bool Contains(int x, int y)
        {
            return (((x >= this.X) && (x <= (this.X + this.Width))) && ((y >= this.Y) && (y <= (this.Y + this.Width))));
        }

        public bool Contains(float x, float y)
        {
            return (((x >= this.X) && (x <= (this.X + this.Width))) && ((y >= this.Y) && (y <= (this.Y + this.Width))));
        }

        public bool Contains(Vector2 vector2D)
        {
            return (((vector2D.X >= this.X) && (vector2D.X <= (this.X + this.Width))) && ((vector2D.Y >= this.Y) && (vector2D.Y <= (this.Y + this.Width))));
        }

        public bool Contains(DrawingPoint point)
        {
            return (((point.X >= this.X) && (point.X <= (this.X + this.Width))) && ((point.Y >= this.Y) && (point.Y <= (this.Y + this.Width))));
        }

        public bool Contains(DrawingPointF point)
        {
            return (((point.X >= this.X) && (point.X <= (this.X + this.Width))) && ((point.Y >= this.Y) && (point.Y <= (this.Y + this.Width))));
        }

        public bool Equals(DrawingRectangle other)
        {
            return ((((other.X == this.X) && (other.Y == this.Y)) && (other.Width == this.Width)) && (other.Height == this.Height));
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(DrawingRectangle))
            {
                return false;
            }
            return this.Equals((DrawingRectangle)obj);
        }

        public override int GetHashCode()
        {
            int num = (this.X * 0x18d) ^ this.Y;
            num = (num * 0x18d) ^ this.Width;
            return ((num * 0x18d) ^ this.Height);
        }

        public static bool operator ==(DrawingRectangle left, DrawingRectangle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DrawingRectangle left, DrawingRectangle right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("(X: {0} Y: {1} W: {2} H: {3})", (object[])new object[] { ((int)this.X), ((int)this.Y), ((int)this.Width), ((int)this.Height) });
        }

        static DrawingRectangle()
        {
            Empty = new DrawingRectangle();
        }
    }
}
