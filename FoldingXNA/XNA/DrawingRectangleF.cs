using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DrawingRectangleF : IEquatable<DrawingRectangleF>
    {
        public Vector2 Position;
        public Vector2 Size;
        public DrawingRectangleF(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
        }

        public DrawingRectangleF(float x, float y, float width, float height)
        {
            this.Position = new Vector2(x, y);
            this.Size = new Vector2(width, height);
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

        public float X
        {
            get
            {
                return this.Position.X;
            }
            set
            {
                this.Position.X = value;
            }
        }
        public float Y
        {
            get
            {
                return this.Position.Y;
            }
            set
            {
                this.Position.Y = value;
            }
        }
        public float Width
        {
            get
            {
                return this.Size.X;
            }
            set
            {
                this.Size.X = value;
            }
        }
        public float Height
        {
            get
            {
                return this.Size.Y;
            }
            set
            {
                this.Size.Y = value;
            }
        }
        public bool Equals(DrawingRectangleF other)
        {
            return (((((float)other.X).Equals(this.X) && ((float)other.Y).Equals(this.Y)) && ((float)other.Width).Equals(this.Width)) && ((float)other.Height).Equals(this.Height));
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (obj.GetType() != typeof(DrawingRectangleF))
            {
                return false;
            }
            return this.Equals((DrawingRectangleF)obj);
        }

        public override int GetHashCode()
        {
            int num = (((float)this.X).GetHashCode() * 0x18d) ^ ((float)this.Y).GetHashCode();
            num = (num * 0x18d) ^ ((float)this.Width).GetHashCode();
            return ((num * 0x18d) ^ ((float)this.Height).GetHashCode());
        }

        public static bool operator ==(DrawingRectangleF left, DrawingRectangleF right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DrawingRectangleF left, DrawingRectangleF right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("(X: {0} Y: {1} W: {2} H: {3})", (object[])new object[] { ((float)this.X), ((float)this.Y), ((float)this.Width), ((float)this.Height) });
        }
    }
}
