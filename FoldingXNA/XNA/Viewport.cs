using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Viewport : IEquatable<Viewport>
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public float MinDepth;
        public float MaxDepth;
        public Viewport(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.MinDepth = 0f;
            this.MaxDepth = 1f;
        }

        public Viewport(int x, int y, int width, int height, float minDepth, float maxDepth)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.MinDepth = minDepth;
            this.MaxDepth = maxDepth;
        }

        public Viewport(DrawingRectangle bounds)
        {
            this.X = bounds.X;
            this.Y = bounds.Y;
            this.Width = bounds.Width;
            this.Height = bounds.Height;
            this.MinDepth = 0f;
            this.MaxDepth = 1f;
        }

        public DrawingRectangle Bounds
        {
            get
            {
                return new DrawingRectangle(this.X, this.Y, this.Width, this.Height);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }
        public bool Equals(Viewport other)
        {
            return (((((this.X == other.X) && (this.Y == other.Y)) && ((this.Width == other.Width) && (this.Height == other.Height))) && MathUtil.WithinEpsilon(this.MinDepth, other.MinDepth)) && MathUtil.WithinEpsilon(this.MaxDepth, other.MaxDepth));
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            return ((obj is Viewport) && this.Equals((Viewport)obj));
        }

        public override int GetHashCode()
        {
            int num = (this.X * 0x18d) ^ this.Y;
            num = (num * 0x18d) ^ this.Width;
            num = (num * 0x18d) ^ this.Height;
            num = (num * 0x18d) ^ ((float)this.MinDepth).GetHashCode();
            return ((num * 0x18d) ^ ((float)this.MaxDepth).GetHashCode());
        }

        public static bool operator ==(Viewport left, Viewport right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Viewport left, Viewport right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "{{X:{0} Y:{1} Width:{2} Height:{3} MinDepth:{4} MaxDepth:{5}}}", new object[] { (int)this.X, (int)this.Y, (int)this.Width, (int)this.Height, (float)this.MinDepth, (float)this.MaxDepth });
        }

        public Vector3 Project(Vector3 source, Matrix projection, Matrix view, Matrix world)
        {
            Matrix transform = Matrix.Multiply(Matrix.Multiply(world, view), projection);
            var v4 = Vector3.Transform(source, transform);
            Vector3 vector = new Vector3(v4.X, v4.Y, v4.Z);
            float a = (((source.X * transform.M14) + (source.Y * transform.M24)) + (source.Z * transform.M34)) + transform.M44;
            if (!MathUtil.WithinEpsilon(a, 1f))
            {
                vector = (Vector3)(vector / a);
            }
            vector.X = (((vector.X + 1f) * 0.5f) * this.Width) + this.X;
            vector.Y = (((-vector.Y + 1f) * 0.5f) * this.Height) + this.Y;
            vector.Z = (vector.Z * (this.MaxDepth - this.MinDepth)) + this.MinDepth;
            return vector;
        }

        public Vector3 Unproject(Vector3 source, Matrix projection, Matrix view, Matrix world)
        {
            Matrix transform = Matrix.Invert(Matrix.Multiply(Matrix.Multiply(world, view), projection));
            source.X = (((source.X - this.X) / ((float)this.Width)) * 2f) - 1f;
            source.Y = -((((source.Y - this.Y) / ((float)this.Height)) * 2f) - 1f);
            source.Z = (source.Z - this.MinDepth) / (this.MaxDepth - this.MinDepth);
            var v4 = Vector3.Transform(source, transform);
            Vector3 vector = new Vector3(v4.X, v4.Y, v4.Z);
            float a = (((source.X * transform.M14) + (source.Y * transform.M24)) + (source.Z * transform.M34)) + transform.M44;
            if (!MathUtil.WithinEpsilon(a, 1f))
            {
                vector = (Vector3)(vector / a);
            }
            return vector;
        }

        public float AspectRatio
        {
            get
            {
                if (this.Height != 0)
                {
                    return (((float)this.Width) / ((float)this.Height));
                }
                return 0f;
            }
        }
    }
}
