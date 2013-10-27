using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix : IEquatable<Matrix>, IFormattable
    {
        public static readonly int SizeInBytes;
        public static readonly Matrix Zero;
        public static readonly Matrix Identity;
        public float M11;
        public float M12;
        public float M13;
        public float M14;
        public float M21;
        public float M22;
        public float M23;
        public float M24;
        public float M31;
        public float M32;
        public float M33;
        public float M34;
        public float M41;
        public float M42;
        public float M43;
        public float M44;
        public Vector3 Up
        {
            get
            {
                Vector3 vector;
                vector.X = this.M21;
                vector.Y = this.M22;
                vector.Z = this.M23;
                return vector;
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
            }
        }
        public Vector3 Down
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M21;
                vector.Y = -this.M22;
                vector.Z = -this.M23;
                return vector;
            }
            set
            {
                this.M21 = -value.X;
                this.M22 = -value.Y;
                this.M23 = -value.Z;
            }
        }
        public Vector3 Right
        {
            get
            {
                Vector3 vector;
                vector.X = this.M11;
                vector.Y = this.M12;
                vector.Z = this.M13;
                return vector;
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
            }
        }
        public Vector3 Left
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M11;
                vector.Y = -this.M12;
                vector.Z = -this.M13;
                return vector;
            }
            set
            {
                this.M11 = -value.X;
                this.M12 = -value.Y;
                this.M13 = -value.Z;
            }
        }
        public Vector3 Forward
        {
            get
            {
                Vector3 vector;
                vector.X = -this.M31;
                vector.Y = -this.M32;
                vector.Z = -this.M33;
                return vector;
            }
            set
            {
                this.M31 = -value.X;
                this.M32 = -value.Y;
                this.M33 = -value.Z;
            }
        }
        public Vector3 Backward
        {
            get
            {
                Vector3 vector;
                vector.X = this.M31;
                vector.Y = this.M32;
                vector.Z = this.M33;
                return vector;
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
            }
        }
        public Matrix(float value)
        {
            this.M11 = this.M12 = this.M13 = this.M14 = this.M21 = this.M22 = this.M23 = this.M24 = this.M31 = this.M32 = this.M33 = this.M34 = this.M41 = this.M42 = this.M43 = this.M44 = value;
        }

        public Matrix(float M11, float M12, float M13, float M14, float M21, float M22, float M23, float M24, float M31, float M32, float M33, float M34, float M41, float M42, float M43, float M44)
        {
            this.M11 = M11;
            this.M12 = M12;
            this.M13 = M13;
            this.M14 = M14;
            this.M21 = M21;
            this.M22 = M22;
            this.M23 = M23;
            this.M24 = M24;
            this.M31 = M31;
            this.M32 = M32;
            this.M33 = M33;
            this.M34 = M34;
            this.M41 = M41;
            this.M42 = M42;
            this.M43 = M43;
            this.M44 = M44;
        }

        public Matrix(float[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length != 0x10)
            {
                throw new ArgumentOutOfRangeException("values", "There must be sixteen and only sixteen input values for Matrix.");
            }
            this.M11 = values[0];
            this.M12 = values[1];
            this.M13 = values[2];
            this.M14 = values[3];
            this.M21 = values[4];
            this.M22 = values[5];
            this.M23 = values[6];
            this.M24 = values[7];
            this.M31 = values[8];
            this.M32 = values[9];
            this.M33 = values[10];
            this.M34 = values[11];
            this.M41 = values[12];
            this.M42 = values[13];
            this.M43 = values[14];
            this.M44 = values[15];
        }

        public Vector4 Row1
        {
            get
            {
                return new Vector4(this.M11, this.M12, this.M13, this.M14);
            }
            set
            {
                this.M11 = value.X;
                this.M12 = value.Y;
                this.M13 = value.Z;
                this.M14 = value.W;
            }
        }
        public Vector4 Row2
        {
            get
            {
                return new Vector4(this.M21, this.M22, this.M23, this.M24);
            }
            set
            {
                this.M21 = value.X;
                this.M22 = value.Y;
                this.M23 = value.Z;
                this.M24 = value.W;
            }
        }
        public Vector4 Row3
        {
            get
            {
                return new Vector4(this.M31, this.M32, this.M33, this.M34);
            }
            set
            {
                this.M31 = value.X;
                this.M32 = value.Y;
                this.M33 = value.Z;
                this.M34 = value.W;
            }
        }
        public Vector4 Row4
        {
            get
            {
                return new Vector4(this.M41, this.M42, this.M43, this.M44);
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
                this.M44 = value.W;
            }
        }
        public Vector4 Column1
        {
            get
            {
                return new Vector4(this.M11, this.M21, this.M31, this.M41);
            }
            set
            {
                this.M11 = value.X;
                this.M21 = value.Y;
                this.M31 = value.Z;
                this.M41 = value.W;
            }
        }
        public Vector4 Column2
        {
            get
            {
                return new Vector4(this.M12, this.M22, this.M32, this.M42);
            }
            set
            {
                this.M12 = value.X;
                this.M22 = value.Y;
                this.M32 = value.Z;
                this.M42 = value.W;
            }
        }
        public Vector4 Column3
        {
            get
            {
                return new Vector4(this.M13, this.M23, this.M33, this.M43);
            }
            set
            {
                this.M13 = value.X;
                this.M23 = value.Y;
                this.M33 = value.Z;
                this.M43 = value.W;
            }
        }
        public Vector4 Column4
        {
            get
            {
                return new Vector4(this.M14, this.M24, this.M34, this.M44);
            }
            set
            {
                this.M14 = value.X;
                this.M24 = value.Y;
                this.M34 = value.Z;
                this.M44 = value.W;
            }
        }
        public Vector3 TranslationVector
        {
            get
            {
                return new Vector3(this.M41, this.M42, this.M43);
            }
            set
            {
                this.M41 = value.X;
                this.M42 = value.Y;
                this.M43 = value.Z;
            }
        }
        public Vector3 ScaleVector
        {
            get
            {
                return new Vector3(this.M11, this.M22, this.M33);
            }
            set
            {
                this.M11 = value.X;
                this.M22 = value.Y;
                this.M33 = value.Z;
            }
        }
        public bool IsIdentity
        {
            get
            {
                return this.Equals(Identity);
            }
        }
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.M11;

                    case 1:
                        return this.M12;

                    case 2:
                        return this.M13;

                    case 3:
                        return this.M14;

                    case 4:
                        return this.M21;

                    case 5:
                        return this.M22;

                    case 6:
                        return this.M23;

                    case 7:
                        return this.M24;

                    case 8:
                        return this.M31;

                    case 9:
                        return this.M32;

                    case 10:
                        return this.M33;

                    case 11:
                        return this.M34;

                    case 12:
                        return this.M41;

                    case 13:
                        return this.M42;

                    case 14:
                        return this.M43;

                    case 15:
                        return this.M44;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.M11 = value;
                        return;

                    case 1:
                        this.M12 = value;
                        return;

                    case 2:
                        this.M13 = value;
                        return;

                    case 3:
                        this.M14 = value;
                        return;

                    case 4:
                        this.M21 = value;
                        return;

                    case 5:
                        this.M22 = value;
                        return;

                    case 6:
                        this.M23 = value;
                        return;

                    case 7:
                        this.M24 = value;
                        return;

                    case 8:
                        this.M31 = value;
                        return;

                    case 9:
                        this.M32 = value;
                        return;

                    case 10:
                        this.M33 = value;
                        return;

                    case 11:
                        this.M34 = value;
                        return;

                    case 12:
                        this.M41 = value;
                        return;

                    case 13:
                        this.M42 = value;
                        return;

                    case 14:
                        this.M43 = value;
                        return;

                    case 15:
                        this.M44 = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
            }
        }
        public float this[int row, int column]
        {
            get
            {
                if ((row < 0) || (row > 3))
                {
                    throw new ArgumentOutOfRangeException("row", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                if ((column < 0) || (column > 3))
                {
                    throw new ArgumentOutOfRangeException("column", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                return this[(row * 4) + column];
            }
            set
            {
                if ((row < 0) || (row > 3))
                {
                    throw new ArgumentOutOfRangeException("row", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                if ((column < 0) || (column > 3))
                {
                    throw new ArgumentOutOfRangeException("column", "Rows and columns for matrices run from 0 to 3, inclusive.");
                }
                this[(row * 4) + column] = value;
            }
        }
        public float Determinant()
        {
            float num = (this.M33 * this.M44) - (this.M34 * this.M43);
            float num2 = (this.M32 * this.M44) - (this.M34 * this.M42);
            float num3 = (this.M32 * this.M43) - (this.M33 * this.M42);
            float num4 = (this.M31 * this.M44) - (this.M34 * this.M41);
            float num5 = (this.M31 * this.M43) - (this.M33 * this.M41);
            float num6 = (this.M31 * this.M42) - (this.M32 * this.M41);
            return ((((this.M11 * (((this.M22 * num) - (this.M23 * num2)) + (this.M24 * num3))) - (this.M12 * (((this.M21 * num) - (this.M23 * num4)) + (this.M24 * num5)))) + (this.M13 * (((this.M21 * num2) - (this.M22 * num4)) + (this.M24 * num6)))) - (this.M14 * (((this.M21 * num3) - (this.M22 * num5)) + (this.M23 * num6))));
        }

        public void Invert()
        {
            Invert(ref this, out this);
        }

        public void Transpose()
        {
            Transpose(ref this, out this);
        }

        public void Orthogonalize()
        {
            Orthogonalize(ref this, out this);
        }

        public void Orthonormalize()
        {
            Orthonormalize(ref this, out this);
        }

        public void DecomposeQR(out Matrix Q, out Matrix R)
        {
            Matrix matrix = this;
            matrix.Transpose();
            Orthonormalize(ref matrix, out Q);
            Q.Transpose();
            R = new Matrix();
            R.M11 = Vector4.Dot(Q.Column1, this.Column1);
            R.M12 = Vector4.Dot(Q.Column1, this.Column2);
            R.M13 = Vector4.Dot(Q.Column1, this.Column3);
            R.M14 = Vector4.Dot(Q.Column1, this.Column4);
            R.M22 = Vector4.Dot(Q.Column2, this.Column2);
            R.M23 = Vector4.Dot(Q.Column2, this.Column3);
            R.M24 = Vector4.Dot(Q.Column2, this.Column4);
            R.M33 = Vector4.Dot(Q.Column3, this.Column3);
            R.M34 = Vector4.Dot(Q.Column3, this.Column4);
            R.M44 = Vector4.Dot(Q.Column4, this.Column4);
        }

        public void DecomposeLQ(out Matrix L, out Matrix Q)
        {
            Orthonormalize(ref this, out Q);
            L = new Matrix();
            L.M11 = Vector4.Dot(Q.Row1, this.Row1);
            L.M21 = Vector4.Dot(Q.Row1, this.Row2);
            L.M22 = Vector4.Dot(Q.Row2, this.Row2);
            L.M31 = Vector4.Dot(Q.Row1, this.Row3);
            L.M32 = Vector4.Dot(Q.Row2, this.Row3);
            L.M33 = Vector4.Dot(Q.Row3, this.Row3);
            L.M41 = Vector4.Dot(Q.Row1, this.Row4);
            L.M42 = Vector4.Dot(Q.Row2, this.Row4);
            L.M43 = Vector4.Dot(Q.Row3, this.Row4);
            L.M44 = Vector4.Dot(Q.Row4, this.Row4);
        }

        public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            translation.X = this.M41;
            translation.Y = this.M42;
            translation.Z = this.M43;
            scale.X = (float)Math.Sqrt((double)(((this.M11 * this.M11) + (this.M12 * this.M12)) + (this.M13 * this.M13)));
            scale.Y = (float)Math.Sqrt((double)(((this.M21 * this.M21) + (this.M22 * this.M22)) + (this.M23 * this.M23)));
            scale.Z = (float)Math.Sqrt((double)(((this.M31 * this.M31) + (this.M32 * this.M32)) + (this.M33 * this.M33)));
            if (((Math.Abs(scale.X) < 1E-06f) || (Math.Abs(scale.Y) < 1E-06f)) || (Math.Abs(scale.Z) < 1E-06f))
            {
                rotation = Quaternion.Identity;
                return false;
            }
            Matrix matrix = new Matrix();
            matrix.M11 = this.M11 / scale.X;
            matrix.M12 = this.M12 / scale.X;
            matrix.M13 = this.M13 / scale.X;
            matrix.M21 = this.M21 / scale.Y;
            matrix.M22 = this.M22 / scale.Y;
            matrix.M23 = this.M23 / scale.Y;
            matrix.M31 = this.M31 / scale.Z;
            matrix.M32 = this.M32 / scale.Z;
            matrix.M33 = this.M33 / scale.Z;
            matrix.M44 = 1f;
            Quaternion.RotationMatrix(ref matrix, out rotation);
            rotation = Quaternion.RotationMatrix(matrix);
            return true;
        }

        public void ExchangeRows(int firstRow, int secondRow)
        {
            if (firstRow < 0)
            {
                throw new ArgumentOutOfRangeException("firstRow", "The parameter firstRow must be greater than or equal to zero.");
            }
            if (firstRow > 3)
            {
                throw new ArgumentOutOfRangeException("firstRow", "The parameter firstRow must be less than or equal to three.");
            }
            if (secondRow < 0)
            {
                throw new ArgumentOutOfRangeException("secondRow", "The parameter secondRow must be greater than or equal to zero.");
            }
            if (secondRow > 3)
            {
                throw new ArgumentOutOfRangeException("secondRow", "The parameter secondRow must be less than or equal to three.");
            }
            if (firstRow != secondRow)
            {
                float num = this[secondRow, 0];
                float num2 = this[secondRow, 1];
                float num3 = this[secondRow, 2];
                float num4 = this[secondRow, 3];
                this[secondRow, 0] = this[firstRow, 0];
                this[secondRow, 1] = this[firstRow, 1];
                this[secondRow, 2] = this[firstRow, 2];
                this[secondRow, 3] = this[firstRow, 3];
                this[firstRow, 0] = num;
                this[firstRow, 1] = num2;
                this[firstRow, 2] = num3;
                this[firstRow, 3] = num4;
            }
        }

        public void ExchangeColumns(int firstColumn, int secondColumn)
        {
            if (firstColumn < 0)
            {
                throw new ArgumentOutOfRangeException("firstColumn", "The parameter firstColumn must be greater than or equal to zero.");
            }
            if (firstColumn > 3)
            {
                throw new ArgumentOutOfRangeException("firstColumn", "The parameter firstColumn must be less than or equal to three.");
            }
            if (secondColumn < 0)
            {
                throw new ArgumentOutOfRangeException("secondColumn", "The parameter secondColumn must be greater than or equal to zero.");
            }
            if (secondColumn > 3)
            {
                throw new ArgumentOutOfRangeException("secondColumn", "The parameter secondColumn must be less than or equal to three.");
            }
            if (firstColumn != secondColumn)
            {
                float num = this[0, secondColumn];
                float num2 = this[1, secondColumn];
                float num3 = this[2, secondColumn];
                float num4 = this[3, secondColumn];
                this[0, secondColumn] = this[0, firstColumn];
                this[1, secondColumn] = this[1, firstColumn];
                this[2, secondColumn] = this[2, firstColumn];
                this[3, secondColumn] = this[3, firstColumn];
                this[0, firstColumn] = num;
                this[1, firstColumn] = num2;
                this[2, firstColumn] = num3;
                this[3, firstColumn] = num4;
            }
        }

        public float[] ToArray()
        {
            return new float[] { this.M11, this.M12, this.M13, this.M14, this.M21, this.M22, this.M23, this.M24, this.M31, this.M32, this.M33, this.M34, this.M41, this.M42, this.M43, this.M44 };
        }

        public static void Add(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.M11 = left.M11 + right.M11;
            result.M12 = left.M12 + right.M12;
            result.M13 = left.M13 + right.M13;
            result.M14 = left.M14 + right.M14;
            result.M21 = left.M21 + right.M21;
            result.M22 = left.M22 + right.M22;
            result.M23 = left.M23 + right.M23;
            result.M24 = left.M24 + right.M24;
            result.M31 = left.M31 + right.M31;
            result.M32 = left.M32 + right.M32;
            result.M33 = left.M33 + right.M33;
            result.M34 = left.M34 + right.M34;
            result.M41 = left.M41 + right.M41;
            result.M42 = left.M42 + right.M42;
            result.M43 = left.M43 + right.M43;
            result.M44 = left.M44 + right.M44;
        }

        public static Matrix Add(Matrix left, Matrix right)
        {
            Matrix matrix;
            Add(ref left, ref right, out matrix);
            return matrix;
        }

        public static void Subtract(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.M11 = left.M11 - right.M11;
            result.M12 = left.M12 - right.M12;
            result.M13 = left.M13 - right.M13;
            result.M14 = left.M14 - right.M14;
            result.M21 = left.M21 - right.M21;
            result.M22 = left.M22 - right.M22;
            result.M23 = left.M23 - right.M23;
            result.M24 = left.M24 - right.M24;
            result.M31 = left.M31 - right.M31;
            result.M32 = left.M32 - right.M32;
            result.M33 = left.M33 - right.M33;
            result.M34 = left.M34 - right.M34;
            result.M41 = left.M41 - right.M41;
            result.M42 = left.M42 - right.M42;
            result.M43 = left.M43 - right.M43;
            result.M44 = left.M44 - right.M44;
        }

        public static Matrix Subtract(Matrix left, Matrix right)
        {
            Matrix matrix;
            Subtract(ref left, ref right, out matrix);
            return matrix;
        }

        public static void Multiply(ref Matrix left, float right, out Matrix result)
        {
            result.M11 = left.M11 * right;
            result.M12 = left.M12 * right;
            result.M13 = left.M13 * right;
            result.M14 = left.M14 * right;
            result.M21 = left.M21 * right;
            result.M22 = left.M22 * right;
            result.M23 = left.M23 * right;
            result.M24 = left.M24 * right;
            result.M31 = left.M31 * right;
            result.M32 = left.M32 * right;
            result.M33 = left.M33 * right;
            result.M34 = left.M34 * right;
            result.M41 = left.M41 * right;
            result.M42 = left.M42 * right;
            result.M43 = left.M43 * right;
            result.M44 = left.M44 * right;
        }

        public static Matrix Multiply(Matrix left, float right)
        {
            Matrix matrix;
            Multiply(ref left, right, out matrix);
            return matrix;
        }

        public static void Multiply(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result = new Matrix();
            result.M11 = (((left.M11 * right.M11) + (left.M12 * right.M21)) + (left.M13 * right.M31)) + (left.M14 * right.M41);
            result.M12 = (((left.M11 * right.M12) + (left.M12 * right.M22)) + (left.M13 * right.M32)) + (left.M14 * right.M42);
            result.M13 = (((left.M11 * right.M13) + (left.M12 * right.M23)) + (left.M13 * right.M33)) + (left.M14 * right.M43);
            result.M14 = (((left.M11 * right.M14) + (left.M12 * right.M24)) + (left.M13 * right.M34)) + (left.M14 * right.M44);
            result.M21 = (((left.M21 * right.M11) + (left.M22 * right.M21)) + (left.M23 * right.M31)) + (left.M24 * right.M41);
            result.M22 = (((left.M21 * right.M12) + (left.M22 * right.M22)) + (left.M23 * right.M32)) + (left.M24 * right.M42);
            result.M23 = (((left.M21 * right.M13) + (left.M22 * right.M23)) + (left.M23 * right.M33)) + (left.M24 * right.M43);
            result.M24 = (((left.M21 * right.M14) + (left.M22 * right.M24)) + (left.M23 * right.M34)) + (left.M24 * right.M44);
            result.M31 = (((left.M31 * right.M11) + (left.M32 * right.M21)) + (left.M33 * right.M31)) + (left.M34 * right.M41);
            result.M32 = (((left.M31 * right.M12) + (left.M32 * right.M22)) + (left.M33 * right.M32)) + (left.M34 * right.M42);
            result.M33 = (((left.M31 * right.M13) + (left.M32 * right.M23)) + (left.M33 * right.M33)) + (left.M34 * right.M43);
            result.M34 = (((left.M31 * right.M14) + (left.M32 * right.M24)) + (left.M33 * right.M34)) + (left.M34 * right.M44);
            result.M41 = (((left.M41 * right.M11) + (left.M42 * right.M21)) + (left.M43 * right.M31)) + (left.M44 * right.M41);
            result.M42 = (((left.M41 * right.M12) + (left.M42 * right.M22)) + (left.M43 * right.M32)) + (left.M44 * right.M42);
            result.M43 = (((left.M41 * right.M13) + (left.M42 * right.M23)) + (left.M43 * right.M33)) + (left.M44 * right.M43);
            result.M44 = (((left.M41 * right.M14) + (left.M42 * right.M24)) + (left.M43 * right.M34)) + (left.M44 * right.M44);
        }

        public static Matrix Multiply(Matrix left, Matrix right)
        {
            Matrix matrix;
            Multiply(ref left, ref right, out matrix);
            return matrix;
        }

        public static void Divide(ref Matrix left, float right, out Matrix result)
        {
            float num = 1f / right;
            result.M11 = left.M11 * num;
            result.M12 = left.M12 * num;
            result.M13 = left.M13 * num;
            result.M14 = left.M14 * num;
            result.M21 = left.M21 * num;
            result.M22 = left.M22 * num;
            result.M23 = left.M23 * num;
            result.M24 = left.M24 * num;
            result.M31 = left.M31 * num;
            result.M32 = left.M32 * num;
            result.M33 = left.M33 * num;
            result.M34 = left.M34 * num;
            result.M41 = left.M41 * num;
            result.M42 = left.M42 * num;
            result.M43 = left.M43 * num;
            result.M44 = left.M44 * num;
        }

        public static Matrix Divide(Matrix left, float right)
        {
            Matrix matrix;
            Divide(ref left, right, out matrix);
            return matrix;
        }

        public static void Divide(ref Matrix left, ref Matrix right, out Matrix result)
        {
            result.M11 = left.M11 / right.M11;
            result.M12 = left.M12 / right.M12;
            result.M13 = left.M13 / right.M13;
            result.M14 = left.M14 / right.M14;
            result.M21 = left.M21 / right.M21;
            result.M22 = left.M22 / right.M22;
            result.M23 = left.M23 / right.M23;
            result.M24 = left.M24 / right.M24;
            result.M31 = left.M31 / right.M31;
            result.M32 = left.M32 / right.M32;
            result.M33 = left.M33 / right.M33;
            result.M34 = left.M34 / right.M34;
            result.M41 = left.M41 / right.M41;
            result.M42 = left.M42 / right.M42;
            result.M43 = left.M43 / right.M43;
            result.M44 = left.M44 / right.M44;
        }

        public static Matrix Divide(Matrix left, Matrix right)
        {
            Matrix matrix;
            Divide(ref left, ref right, out matrix);
            return matrix;
        }

        public static void Exponent(ref Matrix value, int exponent, out Matrix result)
        {
            if (exponent < 0)
            {
                throw new ArgumentOutOfRangeException("exponent", "The exponent can not be negative.");
            }
            if (exponent == 0)
            {
                result = Identity;
                return;
            }
            if (exponent == 1)
            {
                result = value;
                return;
            }
            Matrix identity = Identity;
            Matrix matrix2 = value;
        Label_0041:
            if ((exponent & 1) != 0)
            {
                identity *= matrix2;
            }
            exponent /= 2;
            if (exponent > 0)
            {
                matrix2 *= matrix2;
                goto Label_0041;
            }
            result = identity;
        }

        public static Matrix Exponent(Matrix value, int exponent)
        {
            Matrix matrix;
            Exponent(ref value, exponent, out matrix);
            return matrix;
        }

        public static void Negate(ref Matrix value, out Matrix result)
        {
            result.M11 = -value.M11;
            result.M12 = -value.M12;
            result.M13 = -value.M13;
            result.M14 = -value.M14;
            result.M21 = -value.M21;
            result.M22 = -value.M22;
            result.M23 = -value.M23;
            result.M24 = -value.M24;
            result.M31 = -value.M31;
            result.M32 = -value.M32;
            result.M33 = -value.M33;
            result.M34 = -value.M34;
            result.M41 = -value.M41;
            result.M42 = -value.M42;
            result.M43 = -value.M43;
            result.M44 = -value.M44;
        }

        public static Matrix Negate(Matrix value)
        {
            Matrix matrix;
            Negate(ref value, out matrix);
            return matrix;
        }

        public static void Lerp(ref Matrix start, ref Matrix end, float amount, out Matrix result)
        {
            result.M11 = start.M11 + ((end.M11 - start.M11) * amount);
            result.M12 = start.M12 + ((end.M12 - start.M12) * amount);
            result.M13 = start.M13 + ((end.M13 - start.M13) * amount);
            result.M14 = start.M14 + ((end.M14 - start.M14) * amount);
            result.M21 = start.M21 + ((end.M21 - start.M21) * amount);
            result.M22 = start.M22 + ((end.M22 - start.M22) * amount);
            result.M23 = start.M23 + ((end.M23 - start.M23) * amount);
            result.M24 = start.M24 + ((end.M24 - start.M24) * amount);
            result.M31 = start.M31 + ((end.M31 - start.M31) * amount);
            result.M32 = start.M32 + ((end.M32 - start.M32) * amount);
            result.M33 = start.M33 + ((end.M33 - start.M33) * amount);
            result.M34 = start.M34 + ((end.M34 - start.M34) * amount);
            result.M41 = start.M41 + ((end.M41 - start.M41) * amount);
            result.M42 = start.M42 + ((end.M42 - start.M42) * amount);
            result.M43 = start.M43 + ((end.M43 - start.M43) * amount);
            result.M44 = start.M44 + ((end.M44 - start.M44) * amount);
        }

        public static Matrix Lerp(Matrix start, Matrix end, float amount)
        {
            Matrix matrix;
            Lerp(ref start, ref end, amount, out matrix);
            return matrix;
        }

        public static void SmoothStep(ref Matrix start, ref Matrix end, float amount, out Matrix result)
        {
            amount = (amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount);
            amount = (amount * amount) * (3f - (2f * amount));
            result.M11 = start.M11 + ((end.M11 - start.M11) * amount);
            result.M12 = start.M12 + ((end.M12 - start.M12) * amount);
            result.M13 = start.M13 + ((end.M13 - start.M13) * amount);
            result.M14 = start.M14 + ((end.M14 - start.M14) * amount);
            result.M21 = start.M21 + ((end.M21 - start.M21) * amount);
            result.M22 = start.M22 + ((end.M22 - start.M22) * amount);
            result.M23 = start.M23 + ((end.M23 - start.M23) * amount);
            result.M24 = start.M24 + ((end.M24 - start.M24) * amount);
            result.M31 = start.M31 + ((end.M31 - start.M31) * amount);
            result.M32 = start.M32 + ((end.M32 - start.M32) * amount);
            result.M33 = start.M33 + ((end.M33 - start.M33) * amount);
            result.M34 = start.M34 + ((end.M34 - start.M34) * amount);
            result.M41 = start.M41 + ((end.M41 - start.M41) * amount);
            result.M42 = start.M42 + ((end.M42 - start.M42) * amount);
            result.M43 = start.M43 + ((end.M43 - start.M43) * amount);
            result.M44 = start.M44 + ((end.M44 - start.M44) * amount);
        }

        public static Matrix SmoothStep(Matrix start, Matrix end, float amount)
        {
            Matrix matrix;
            SmoothStep(ref start, ref end, amount, out matrix);
            return matrix;
        }

        public static void Transpose(ref Matrix value, out Matrix result)
        {
            Matrix matrix = new Matrix();
            matrix.M11 = value.M11;
            matrix.M12 = value.M21;
            matrix.M13 = value.M31;
            matrix.M14 = value.M41;
            matrix.M21 = value.M12;
            matrix.M22 = value.M22;
            matrix.M23 = value.M32;
            matrix.M24 = value.M42;
            matrix.M31 = value.M13;
            matrix.M32 = value.M23;
            matrix.M33 = value.M33;
            matrix.M34 = value.M43;
            matrix.M41 = value.M14;
            matrix.M42 = value.M24;
            matrix.M43 = value.M34;
            matrix.M44 = value.M44;
            result = matrix;
        }

        public static void TransposeByRef(ref Matrix value, ref Matrix result)
        {
            result.M11 = value.M11;
            result.M12 = value.M21;
            result.M13 = value.M31;
            result.M14 = value.M41;
            result.M21 = value.M12;
            result.M22 = value.M22;
            result.M23 = value.M32;
            result.M24 = value.M42;
            result.M31 = value.M13;
            result.M32 = value.M23;
            result.M33 = value.M33;
            result.M34 = value.M43;
            result.M41 = value.M14;
            result.M42 = value.M24;
            result.M43 = value.M34;
            result.M44 = value.M44;
        }

        public static Matrix Transpose(Matrix value)
        {
            Matrix matrix;
            Transpose(ref value, out matrix);
            return matrix;
        }

        public static void Invert(ref Matrix value, out Matrix result)
        {
            float num = (value.M31 * value.M42) - (value.M32 * value.M41);
            float num2 = (value.M31 * value.M43) - (value.M33 * value.M41);
            float num3 = (value.M34 * value.M41) - (value.M31 * value.M44);
            float num4 = (value.M32 * value.M43) - (value.M33 * value.M42);
            float num5 = (value.M34 * value.M42) - (value.M32 * value.M44);
            float num6 = (value.M33 * value.M44) - (value.M34 * value.M43);
            float num7 = ((value.M22 * num6) + (value.M23 * num5)) + (value.M24 * num4);
            float num8 = ((value.M21 * num6) + (value.M23 * num3)) + (value.M24 * num2);
            float num9 = ((value.M21 * -num5) + (value.M22 * num3)) + (value.M24 * num);
            float num10 = ((value.M21 * num4) + (value.M22 * -num2)) + (value.M23 * num);
            float num11 = (((value.M11 * num7) - (value.M12 * num8)) + (value.M13 * num9)) - (value.M14 * num10);
            if (Math.Abs(num11) <= 1E-06f)
            {
                result = Zero;
            }
            else
            {
                num11 = 1f / num11;
                float num12 = (value.M11 * value.M22) - (value.M12 * value.M21);
                float num13 = (value.M11 * value.M23) - (value.M13 * value.M21);
                float num14 = (value.M14 * value.M21) - (value.M11 * value.M24);
                float num15 = (value.M12 * value.M23) - (value.M13 * value.M22);
                float num16 = (value.M14 * value.M22) - (value.M12 * value.M24);
                float num17 = (value.M13 * value.M24) - (value.M14 * value.M23);
                float num18 = ((value.M12 * num6) + (value.M13 * num5)) + (value.M14 * num4);
                float num19 = ((value.M11 * num6) + (value.M13 * num3)) + (value.M14 * num2);
                float num20 = ((value.M11 * -num5) + (value.M12 * num3)) + (value.M14 * num);
                float num21 = ((value.M11 * num4) + (value.M12 * -num2)) + (value.M13 * num);
                float num22 = ((value.M42 * num17) + (value.M43 * num16)) + (value.M44 * num15);
                float num23 = ((value.M41 * num17) + (value.M43 * num14)) + (value.M44 * num13);
                float num24 = ((value.M41 * -num16) + (value.M42 * num14)) + (value.M44 * num12);
                float num25 = ((value.M41 * num15) + (value.M42 * -num13)) + (value.M43 * num12);
                float num26 = ((value.M32 * num17) + (value.M33 * num16)) + (value.M34 * num15);
                float num27 = ((value.M31 * num17) + (value.M33 * num14)) + (value.M34 * num13);
                float num28 = ((value.M31 * -num16) + (value.M32 * num14)) + (value.M34 * num12);
                float num29 = ((value.M31 * num15) + (value.M32 * -num13)) + (value.M33 * num12);
                result.M11 = num7 * num11;
                result.M12 = -num18 * num11;
                result.M13 = num22 * num11;
                result.M14 = -num26 * num11;
                result.M21 = -num8 * num11;
                result.M22 = num19 * num11;
                result.M23 = -num23 * num11;
                result.M24 = num27 * num11;
                result.M31 = num9 * num11;
                result.M32 = -num20 * num11;
                result.M33 = num24 * num11;
                result.M34 = -num28 * num11;
                result.M41 = -num10 * num11;
                result.M42 = num21 * num11;
                result.M43 = -num25 * num11;
                result.M44 = num29 * num11;
            }
        }

        public static Matrix Invert(Matrix value)
        {
            value.Invert();
            return value;
        }

        public static void Orthogonalize(ref Matrix value, out Matrix result)
        {
            result = value;
            result.Row2 -= (Vector4)((Vector4.Dot(result.Row1, result.Row2) / Vector4.Dot(result.Row1, result.Row1)) * result.Row1);
            result.Row3 -= (Vector4)((Vector4.Dot(result.Row1, result.Row3) / Vector4.Dot(result.Row1, result.Row1)) * result.Row1);
            result.Row3 -= (Vector4)((Vector4.Dot(result.Row2, result.Row3) / Vector4.Dot(result.Row2, result.Row2)) * result.Row2);
            result.Row4 -= (Vector4)((Vector4.Dot(result.Row1, result.Row4) / Vector4.Dot(result.Row1, result.Row1)) * result.Row1);
            result.Row4 -= (Vector4)((Vector4.Dot(result.Row2, result.Row4) / Vector4.Dot(result.Row2, result.Row2)) * result.Row2);
            result.Row4 -= (Vector4)((Vector4.Dot(result.Row3, result.Row4) / Vector4.Dot(result.Row3, result.Row3)) * result.Row3);
        }

        public static Matrix Orthogonalize(Matrix value)
        {
            Matrix matrix;
            Orthogonalize(ref value, out matrix);
            return matrix;
        }

        public static void Orthonormalize(ref Matrix value, out Matrix result)
        {
            result = value;
            result.Row1 = Vector4.Normalize(result.Row1);
            result.Row2 -= (Vector4)(Vector4.Dot(result.Row1, result.Row2) * result.Row1);
            result.Row2 = Vector4.Normalize(result.Row2);
            result.Row3 -= (Vector4)(Vector4.Dot(result.Row1, result.Row3) * result.Row1);
            result.Row3 -= (Vector4)(Vector4.Dot(result.Row2, result.Row3) * result.Row2);
            result.Row3 = Vector4.Normalize(result.Row3);
            result.Row4 -= (Vector4)(Vector4.Dot(result.Row1, result.Row4) * result.Row1);
            result.Row4 -= (Vector4)(Vector4.Dot(result.Row2, result.Row4) * result.Row2);
            result.Row4 -= (Vector4)(Vector4.Dot(result.Row3, result.Row4) * result.Row3);
            result.Row4 = Vector4.Normalize(result.Row4);
        }

        public static Matrix Orthonormalize(Matrix value)
        {
            Matrix matrix;
            Orthonormalize(ref value, out matrix);
            return matrix;
        }

        public static void UpperTriangularForm(ref Matrix value, out Matrix result)
        {
            result = value;
            int num = 0;
            int num2 = 4;
            int num3 = 4;
            for (int i = 0; i < num2; i++)
            {
                if (num3 <= num)
                {
                    return;
                }
                int firstRow = i;
                while (Math.Abs(result[firstRow, num]) < 1E-06f)
                {
                    firstRow++;
                    if (firstRow == num2)
                    {
                        firstRow = i;
                        num++;
                        if (num == num3)
                        {
                            return;
                        }
                    }
                }
                if (firstRow != i)
                {
                    result.ExchangeRows(firstRow, i);
                }
                float num6 = 1f / result[i, num];
                while (firstRow < num2)
                {
                    if (firstRow != i)
                    {
                        Matrix matrixRef = Matrix.Zero, matrixRef2 = Matrix.Zero, matrixRef3 = Matrix.Zero, matrixRef4 = Matrix.Zero;
                        int num7, num8, num9, num10;
                        (matrixRef)[num7 = firstRow, 0] = matrixRef[num7, 0] - ((result[i, 0] * num6) * result[firstRow, num]);
                        (matrixRef2)[num8 = firstRow, 1] = matrixRef2[num8, 1] - ((result[i, 1] * num6) * result[firstRow, num]);
                        (matrixRef3)[num9 = firstRow, 2] = matrixRef3[num9, 2] - ((result[i, 2] * num6) * result[firstRow, num]);
                        (matrixRef4)[num10 = firstRow, 3] = matrixRef4[num10, 3] - ((result[i, 3] * num6) * result[firstRow, num]);
                    }
                    firstRow++;
                }
                num++;
            }
        }

        public static Matrix UpperTriangularForm(Matrix value)
        {
            Matrix matrix;
            UpperTriangularForm(ref value, out matrix);
            return matrix;
        }

        public static void LowerTriangularForm(ref Matrix value, out Matrix result)
        {
            Matrix matrix = value;
            Transpose(ref matrix, out result);
            int num = 0;
            int num2 = 4;
            int num3 = 4;
            for (int i = 0; i < num2; i++)
            {
                if (num3 <= num)
                {
                    return;
                }
                int firstRow = i;
                while (Math.Abs(result[firstRow, num]) < 1E-06f)
                {
                    firstRow++;
                    if (firstRow == num2)
                    {
                        firstRow = i;
                        num++;
                        if (num == num3)
                        {
                            return;
                        }
                    }
                }
                if (firstRow != i)
                {
                    result.ExchangeRows(firstRow, i);
                }
                float num6 = 1f / result[i, num];
                while (firstRow < num2)
                {
                    if (firstRow != i)
                    {
                        Matrix matrixRef = Matrix.Zero, matrixRef2 = Matrix.Zero, matrixRef3 = Matrix.Zero, matrixRef4 = Matrix.Zero;
                        int num7, num8, num9, num10;
                        (matrixRef)[num7 = firstRow, 0] = matrixRef[num7, 0] - ((result[i, 0] * num6) * result[firstRow, num]);
                        (matrixRef2)[num8 = firstRow, 1] = matrixRef2[num8, 1] - ((result[i, 1] * num6) * result[firstRow, num]);
                        (matrixRef3)[num9 = firstRow, 2] = matrixRef3[num9, 2] - ((result[i, 2] * num6) * result[firstRow, num]);
                        (matrixRef4)[num10 = firstRow, 3] = matrixRef4[num10, 3] - ((result[i, 3] * num6) * result[firstRow, num]);
                    }
                    firstRow++;
                }
                num++;
            }
            Transpose(ref result, out result);
        }

        public static Matrix LowerTriangularForm(Matrix value)
        {
            Matrix matrix;
            LowerTriangularForm(ref value, out matrix);
            return matrix;
        }

        public static void RowEchelonForm(ref Matrix value, out Matrix result)
        {
            result = value;
            int num = 0;
            int num2 = 4;
            int num3 = 4;
            for (int i = 0; i < num2; i++)
            {
                Matrix matrixRef = Matrix.Zero, matrixRef2 = Matrix.Zero, matrixRef3 = Matrix.Zero, matrixRef4 = Matrix.Zero;
                int num7, num8, num9, num10;
                if (num3 <= num)
                {
                    return;
                }
                int firstRow = i;
                while (Math.Abs(result[firstRow, num]) < 1E-06f)
                {
                    firstRow++;
                    if (firstRow == num2)
                    {
                        firstRow = i;
                        num++;
                        if (num == num3)
                        {
                            return;
                        }
                    }
                }
                if (firstRow != i)
                {
                    result.ExchangeRows(firstRow, i);
                }
                float num6 = 1f / result[i, num];
                (matrixRef)[num7 = i, 0] = matrixRef[num7, 0] * num6;
                (matrixRef2)[num8 = i, 1] = matrixRef2[num8, 1] * num6;
                (matrixRef3)[num9 = i, 2] = matrixRef3[num9, 2] * num6;
                (matrixRef4)[num10 = i, 3] = matrixRef4[num10, 3] * num6;
                while (firstRow < num2)
                {
                    if (firstRow != i)
                    {
                        Matrix matrixRef5 = Matrix.Zero, matrixRef6 = Matrix.Zero, matrixRef7 = Matrix.Zero, matrixRef8 = Matrix.Zero;
                        int num11, num12, num13, num14;
                        (matrixRef5)[num11 = firstRow, 0] = matrixRef5[num11, 0] - (result[i, 0] * result[firstRow, num]);
                        (matrixRef6)[num12 = firstRow, 1] = matrixRef6[num12, 1] - (result[i, 1] * result[firstRow, num]);
                        (matrixRef7)[num13 = firstRow, 2] = matrixRef7[num13, 2] - (result[i, 2] * result[firstRow, num]);
                        (matrixRef8)[num14 = firstRow, 3] = matrixRef8[num14, 3] - (result[i, 3] * result[firstRow, num]);
                    }
                    firstRow++;
                }
                num++;
            }
        }

        public static Matrix RowEchelonForm(Matrix value)
        {
            Matrix matrix;
            RowEchelonForm(ref value, out matrix);
            return matrix;
        }

        public static void ReducedRowEchelonForm(ref Matrix value, ref Vector4 augment, out Matrix result, out Vector4 augmentResult)
        {
            float[,] numArray = new float[,] { { value[0, 0], value[0, 1], value[0, 2], value[0, 3], augment[0] }, { value[1, 0], value[1, 1], value[1, 2], value[1, 3], augment[1] }, { value[2, 0], value[2, 1], value[2, 2], value[2, 3], augment[2] }, { value[3, 0], value[3, 1], value[3, 2], value[3, 3], augment[3] } };
            int num = 0;
            int num2 = 4;
            int num3 = 5;
            for (int i = 0; i < num2; i++)
            {
                if (num3 <= num)
                {
                    break;
                }
                int num5 = i;
                while (numArray[num5, num] == 0f)
                {
                    num5++;
                    if (num5 == num2)
                    {
                        num5 = i;
                        num++;
                        if (num3 == num)
                        {
                            break;
                        }
                    }
                }
                for (int j = 0; j < num3; j++)
                {
                    float num7 = numArray[i, j];
                    numArray[i, j] = numArray[num5, j];
                    numArray[num5, j] = num7;
                }
                float num8 = numArray[i, num];
                for (int k = 0; k < num3; k++)
                {
                    float single1 = numArray[i, k];
                    single1 /= num8;
                }
                for (int m = 0; m < num2; m++)
                {
                    if (m != i)
                    {
                        float num11 = numArray[m, num];
                        for (int n = 0; n < num3; n++)
                        {
                            float single2 = numArray[m, n];
                            single2 -= num11 * numArray[i, n];
                        }
                    }
                }
                num++;
            }
            result.M11 = numArray[0, 0];
            result.M12 = numArray[0, 1];
            result.M13 = numArray[0, 2];
            result.M14 = numArray[0, 3];
            result.M21 = numArray[1, 0];
            result.M22 = numArray[1, 1];
            result.M23 = numArray[1, 2];
            result.M24 = numArray[1, 3];
            result.M31 = numArray[2, 0];
            result.M32 = numArray[2, 1];
            result.M33 = numArray[2, 2];
            result.M34 = numArray[2, 3];
            result.M41 = numArray[3, 0];
            result.M42 = numArray[3, 1];
            result.M43 = numArray[3, 2];
            result.M44 = numArray[3, 3];
            augmentResult.X = numArray[0, 4];
            augmentResult.Y = numArray[1, 4];
            augmentResult.Z = numArray[2, 4];
            augmentResult.W = numArray[3, 4];
        }

        public static void Billboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, ref Vector3 cameraForwardVector, out Matrix result)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 right = objectPosition - cameraPosition;
            float num = right.LengthSquared();
            if (num < 1E-06f)
            {
                right = -cameraForwardVector;
            }
            else
            {
                right = (Vector3)(right * ((float)(1.0 / Math.Sqrt((double)num))));
            }
            Vector3.Cross(ref cameraUpVector, ref right, out vector);
            vector.Normalize();
            Vector3.Cross(ref right, ref vector, out vector2);
            result.M11 = vector.X;
            result.M12 = vector.Y;
            result.M13 = vector.Z;
            result.M14 = 0f;
            result.M21 = vector2.X;
            result.M22 = vector2.Y;
            result.M23 = vector2.Z;
            result.M24 = 0f;
            result.M31 = right.X;
            result.M32 = right.Y;
            result.M33 = right.Z;
            result.M34 = 0f;
            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1f;
        }

        public static Matrix Billboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
        {
            Matrix matrix;
            Billboard(ref objectPosition, ref cameraPosition, ref cameraUpVector, ref cameraForwardVector, out matrix);
            return matrix;
        }

        public static void LookAtLH(ref Vector3 eye, ref Vector3 target, ref Vector3 up, out Matrix result)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3.Subtract(ref target, ref eye, out vector3);
            vector3.Normalize();
            Vector3.Cross(ref up, ref vector3, out vector);
            vector.Normalize();
            Vector3.Cross(ref vector3, ref vector, out vector2);
            result = Identity;
            result.M11 = vector.X;
            result.M21 = vector.Y;
            result.M31 = vector.Z;
            result.M12 = vector2.X;
            result.M22 = vector2.Y;
            result.M32 = vector2.Z;
            result.M13 = vector3.X;
            result.M23 = vector3.Y;
            result.M33 = vector3.Z;
            Vector3.Dot(ref vector, ref eye, out result.M41);
            Vector3.Dot(ref vector2, ref eye, out result.M42);
            Vector3.Dot(ref vector3, ref eye, out result.M43);
            result.M41 = -result.M41;
            result.M42 = -result.M42;
            result.M43 = -result.M43;
        }

        public static Matrix LookAtLH(Vector3 eye, Vector3 target, Vector3 up)
        {
            Matrix matrix;
            LookAtLH(ref eye, ref target, ref up, out matrix);
            return matrix;
        }

        public static void LookAtRH(ref Vector3 eye, ref Vector3 target, ref Vector3 up, out Matrix result)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3.Subtract(ref eye, ref target, out vector3);
            vector3.Normalize();
            Vector3.Cross(ref up, ref vector3, out vector);
            vector.Normalize();
            Vector3.Cross(ref vector3, ref vector, out vector2);
            result = Identity;
            result.M11 = vector.X;
            result.M21 = vector.Y;
            result.M31 = vector.Z;
            result.M12 = vector2.X;
            result.M22 = vector2.Y;
            result.M32 = vector2.Z;
            result.M13 = vector3.X;
            result.M23 = vector3.Y;
            result.M33 = vector3.Z;
            Vector3.Dot(ref vector, ref eye, out result.M41);
            Vector3.Dot(ref vector2, ref eye, out result.M42);
            Vector3.Dot(ref vector3, ref eye, out result.M43);
            result.M41 = -result.M41;
            result.M42 = -result.M42;
            result.M43 = -result.M43;
        }

        public static Matrix LookAtRH(Vector3 eye, Vector3 target, Vector3 up)
        {
            Matrix matrix;
            LookAtRH(ref eye, ref target, ref up, out matrix);
            return matrix;
        }

        public static void OrthoLH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float right = width * 0.5f;
            float top = height * 0.5f;
            OrthoOffCenterLH(-right, right, -top, top, znear, zfar, out result);
        }

        public static Matrix OrthoLH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            OrthoLH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        public static void OrthoRH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float right = width * 0.5f;
            float top = height * 0.5f;
            OrthoOffCenterRH(-right, right, -top, top, znear, zfar, out result);
        }

        public static Matrix OrthoRH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            OrthoRH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        public static void OrthoOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            float num = 1f / (zfar - znear);
            result = Identity;
            result.M11 = 2f / (right - left);
            result.M22 = 2f / (top - bottom);
            result.M33 = num;
            result.M41 = (left + right) / (left - right);
            result.M42 = (top + bottom) / (bottom - top);
            result.M43 = -znear * num;
        }

        public static Matrix OrthoOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            OrthoOffCenterLH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        public static void OrthoOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            OrthoOffCenterLH(left, right, bottom, top, znear, zfar, out result);
            result.M33 *= -1f;
        }

        public static Matrix OrthoOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            OrthoOffCenterRH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        public static void PerspectiveLH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float right = width * 0.5f;
            float top = height * 0.5f;
            PerspectiveOffCenterLH(-right, right, -top, top, znear, zfar, out result);
        }

        public static Matrix PerspectiveLH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            PerspectiveLH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        public static void PerspectiveRH(float width, float height, float znear, float zfar, out Matrix result)
        {
            float right = width * 0.5f;
            float top = height * 0.5f;
            PerspectiveOffCenterRH(-right, right, -top, top, znear, zfar, out result);
        }

        public static Matrix PerspectiveRH(float width, float height, float znear, float zfar)
        {
            Matrix matrix;
            PerspectiveRH(width, height, znear, zfar, out matrix);
            return matrix;
        }

        public static void PerspectiveFovLH(float fov, float aspect, float znear, float zfar, out Matrix result)
        {
            float num = (float)(1.0 / Math.Tan((double)(fov * 0.5f)));
            float num2 = num / aspect;
            float right = znear / num2;
            float top = znear / num;
            PerspectiveOffCenterLH(-right, right, -top, top, znear, zfar, out result);
        }

        public static Matrix PerspectiveFovLH(float fov, float aspect, float znear, float zfar)
        {
            Matrix matrix;
            PerspectiveFovLH(fov, aspect, znear, zfar, out matrix);
            return matrix;
        }

        public static void PerspectiveFovRH(float fov, float aspect, float znear, float zfar, out Matrix result)
        {
            float num = (float)(1.0 / Math.Tan((double)(fov * 0.5f)));
            float num2 = num / aspect;
            float right = znear / num2;
            float top = znear / num;
            PerspectiveOffCenterRH(-right, right, -top, top, znear, zfar, out result);
        }

        public static Matrix PerspectiveFovRH(float fov, float aspect, float znear, float zfar)
        {
            Matrix matrix;
            PerspectiveFovRH(fov, aspect, znear, zfar, out matrix);
            return matrix;
        }

        public static void PerspectiveOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            float num = zfar / (zfar - znear);
            result = new Matrix();
            result.M11 = (2f * znear) / (right - left);
            result.M22 = (2f * znear) / (top - bottom);
            result.M31 = (left + right) / (left - right);
            result.M32 = (top + bottom) / (bottom - top);
            result.M33 = num;
            result.M34 = 1f;
            result.M43 = -znear * num;
        }

        public static Matrix PerspectiveOffCenterLH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            PerspectiveOffCenterLH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        public static void PerspectiveOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar, out Matrix result)
        {
            PerspectiveOffCenterLH(left, right, bottom, top, znear, zfar, out result);
            result.M31 *= -1f;
            result.M32 *= -1f;
            result.M33 *= -1f;
            result.M34 *= -1f;
        }

        public static Matrix PerspectiveOffCenterRH(float left, float right, float bottom, float top, float znear, float zfar)
        {
            Matrix matrix;
            PerspectiveOffCenterRH(left, right, bottom, top, znear, zfar, out matrix);
            return matrix;
        }

        public static void Reflection(ref Plane plane, out Matrix result)
        {
            float x = plane.Normal.X;
            float y = plane.Normal.Y;
            float z = plane.Normal.Z;
            float num4 = -2f * x;
            float num5 = -2f * y;
            float num6 = -2f * z;
            result.M11 = (num4 * x) + 1f;
            result.M12 = num5 * x;
            result.M13 = num6 * x;
            result.M14 = 0f;
            result.M21 = num4 * y;
            result.M22 = (num5 * y) + 1f;
            result.M23 = num6 * y;
            result.M24 = 0f;
            result.M31 = num4 * z;
            result.M32 = num5 * z;
            result.M33 = (num6 * z) + 1f;
            result.M34 = 0f;
            result.M41 = num4 * plane.D;
            result.M42 = num5 * plane.D;
            result.M43 = num6 * plane.D;
            result.M44 = 1f;
        }

        public static Matrix Reflection(Plane plane)
        {
            Matrix matrix;
            Reflection(ref plane, out matrix);
            return matrix;
        }

        public static void Shadow(ref Vector4 light, ref Plane plane, out Matrix result)
        {
            float num = (((plane.Normal.X * light.X) + (plane.Normal.Y * light.Y)) + (plane.Normal.Z * light.Z)) + (plane.D * light.W);
            float num2 = -plane.Normal.X;
            float num3 = -plane.Normal.Y;
            float num4 = -plane.Normal.Z;
            float num5 = -plane.D;
            result.M11 = (num2 * light.X) + num;
            result.M21 = num3 * light.X;
            result.M31 = num4 * light.X;
            result.M41 = num5 * light.X;
            result.M12 = num2 * light.Y;
            result.M22 = (num3 * light.Y) + num;
            result.M32 = num4 * light.Y;
            result.M42 = num5 * light.Y;
            result.M13 = num2 * light.Z;
            result.M23 = num3 * light.Z;
            result.M33 = (num4 * light.Z) + num;
            result.M43 = num5 * light.Z;
            result.M14 = num2 * light.W;
            result.M24 = num3 * light.W;
            result.M34 = num4 * light.W;
            result.M44 = (num5 * light.W) + num;
        }

        public static Matrix Shadow(Vector4 light, Plane plane)
        {
            Matrix matrix;
            Shadow(ref light, ref plane, out matrix);
            return matrix;
        }

        public static void Scaling(ref Vector3 scale, out Matrix result)
        {
            Scaling(scale.X, scale.Y, scale.Z, out result);
        }

        public static Matrix Scaling(Vector3 scale)
        {
            Matrix matrix;
            Scaling(ref scale, out matrix);
            return matrix;
        }

        public static void Scaling(float x, float y, float z, out Matrix result)
        {
            result = Identity;
            result.M11 = x;
            result.M22 = y;
            result.M33 = z;
        }

        public static Matrix Scaling(float x, float y, float z)
        {
            Matrix matrix;
            Scaling(x, y, z, out matrix);
            return matrix;
        }

        public static void Scaling(float scale, out Matrix result)
        {
            result = Identity;
            result.M11 = result.M22 = result.M33 = scale;
        }

        public static Matrix Scaling(float scale)
        {
            Matrix matrix;
            Scaling(scale, out matrix);
            return matrix;
        }

        public static void RotationX(float angle, out Matrix result)
        {
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Identity;
            result.M22 = num;
            result.M23 = num2;
            result.M32 = -num2;
            result.M33 = num;
        }

        public static Matrix RotationX(float angle)
        {
            Matrix matrix;
            RotationX(angle, out matrix);
            return matrix;
        }

        public static void RotationY(float angle, out Matrix result)
        {
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Identity;
            result.M11 = num;
            result.M13 = -num2;
            result.M31 = num2;
            result.M33 = num;
        }

        public static Matrix RotationY(float angle)
        {
            Matrix matrix;
            RotationY(angle, out matrix);
            return matrix;
        }

        public static void RotationZ(float angle, out Matrix result)
        {
            float num = (float)Math.Cos((double)angle);
            float num2 = (float)Math.Sin((double)angle);
            result = Identity;
            result.M11 = num;
            result.M12 = num2;
            result.M21 = -num2;
            result.M22 = num;
        }

        public static Matrix RotationZ(float angle)
        {
            Matrix matrix;
            RotationZ(angle, out matrix);
            return matrix;
        }

        public static void RotationAxis(ref Vector3 axis, float angle, out Matrix result)
        {
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            float num4 = (float)Math.Cos((double)angle);
            float num5 = (float)Math.Sin((double)angle);
            float num6 = x * x;
            float num7 = y * y;
            float num8 = z * z;
            float num9 = x * y;
            float num10 = x * z;
            float num11 = y * z;
            result = Identity;
            result.M11 = num6 + (num4 * (1f - num6));
            result.M12 = (num9 - (num4 * num9)) + (num5 * z);
            result.M13 = (num10 - (num4 * num10)) - (num5 * y);
            result.M21 = (num9 - (num4 * num9)) - (num5 * z);
            result.M22 = num7 + (num4 * (1f - num7));
            result.M23 = (num11 - (num4 * num11)) + (num5 * x);
            result.M31 = (num10 - (num4 * num10)) + (num5 * y);
            result.M32 = (num11 - (num4 * num11)) - (num5 * x);
            result.M33 = num8 + (num4 * (1f - num8));
        }

        public static Matrix RotationAxis(Vector3 axis, float angle)
        {
            Matrix matrix;
            RotationAxis(ref axis, angle, out matrix);
            return matrix;
        }

        public static void RotationQuaternion(ref Quaternion rotation, out Matrix result)
        {
            float num = rotation.X * rotation.X;
            float num2 = rotation.Y * rotation.Y;
            float num3 = rotation.Z * rotation.Z;
            float num4 = rotation.X * rotation.Y;
            float num5 = rotation.Z * rotation.W;
            float num6 = rotation.Z * rotation.X;
            float num7 = rotation.Y * rotation.W;
            float num8 = rotation.Y * rotation.Z;
            float num9 = rotation.X * rotation.W;
            result = Identity;
            result.M11 = 1f - (2f * (num2 + num3));
            result.M12 = 2f * (num4 + num5);
            result.M13 = 2f * (num6 - num7);
            result.M21 = 2f * (num4 - num5);
            result.M22 = 1f - (2f * (num3 + num));
            result.M23 = 2f * (num8 + num9);
            result.M31 = 2f * (num6 + num7);
            result.M32 = 2f * (num8 - num9);
            result.M33 = 1f - (2f * (num2 + num));
        }

        public static Matrix RotationQuaternion(Quaternion rotation)
        {
            Matrix matrix;
            RotationQuaternion(ref rotation, out matrix);
            return matrix;
        }

        public static void RotationYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
        {
            Quaternion quaternion = new Quaternion();
            Quaternion.RotationYawPitchRoll(yaw, pitch, roll, out quaternion);
            RotationQuaternion(ref quaternion, out result);
        }

        public static Matrix RotationYawPitchRoll(float yaw, float pitch, float roll)
        {
            Matrix matrix;
            RotationYawPitchRoll(yaw, pitch, roll, out matrix);
            return matrix;
        }

        public static void Translation(ref Vector3 value, out Matrix result)
        {
            Translation(value.X, value.Y, value.Z, out result);
        }

        public static Matrix Translation(Vector3 value)
        {
            Matrix matrix;
            Translation(ref value, out matrix);
            return matrix;
        }

        public static void Translation(float x, float y, float z, out Matrix result)
        {
            result = Identity;
            result.M41 = x;
            result.M42 = y;
            result.M43 = z;
        }

        public static Matrix Translation(float x, float y, float z)
        {
            Matrix matrix;
            Translation(x, y, z, out matrix);
            return matrix;
        }

        public static void AffineTransformation(float scaling, ref Quaternion rotation, ref Vector3 translation, out Matrix result)
        {
            result = (Scaling(scaling) * RotationQuaternion(rotation)) * Translation(translation);
        }

        public static Matrix AffineTransformation(float scaling, Quaternion rotation, Vector3 translation)
        {
            Matrix matrix;
            AffineTransformation(scaling, ref rotation, ref translation, out matrix);
            return matrix;
        }

        public static void AffineTransformation(float scaling, ref Vector3 rotationCenter, ref Quaternion rotation, ref Vector3 translation, out Matrix result)
        {
            result = (((Scaling(scaling) * Translation(-rotationCenter)) * RotationQuaternion(rotation)) * Translation(rotationCenter)) * Translation(translation);
        }

        public static Matrix AffineTransformation(float scaling, Vector3 rotationCenter, Quaternion rotation, Vector3 translation)
        {
            Matrix matrix;
            AffineTransformation(scaling, ref rotationCenter, ref rotation, ref translation, out matrix);
            return matrix;
        }

        public static void AffineTransformation2D(float scaling, float rotation, ref Vector2 translation, out Matrix result)
        {
            result = (Scaling(scaling, scaling, 1f) * RotationZ(rotation)) * Translation((Vector3)translation);
        }

        public static Matrix AffineTransformation2D(float scaling, float rotation, Vector2 translation)
        {
            Matrix matrix;
            AffineTransformation2D(scaling, rotation, ref translation, out matrix);
            return matrix;
        }

        public static void AffineTransformation2D(float scaling, ref Vector2 rotationCenter, float rotation, ref Vector2 translation, out Matrix result)
        {
            result = (((Scaling(scaling, scaling, 1f) * Translation((Vector3) (- rotationCenter))) * RotationZ(rotation)) * Translation((Vector3)rotationCenter)) * Translation((Vector3)translation);
        }

        public static Matrix AffineTransformation2D(float scaling, Vector2 rotationCenter, float rotation, Vector2 translation)
        {
            Matrix matrix;
            AffineTransformation2D(scaling, ref rotationCenter, rotation, ref translation, out matrix);
            return matrix;
        }

        public static void Transformation(ref Vector3 scalingCenter, ref Quaternion scalingRotation, ref Vector3 scaling, ref Vector3 rotationCenter, ref Quaternion rotation, ref Vector3 translation, out Matrix result)
        {
            Matrix matrix = RotationQuaternion(scalingRotation);
            result = (((((((Translation(-scalingCenter) * Transpose(matrix)) * Scaling(scaling)) * matrix) * Translation(scalingCenter)) * Translation(-rotationCenter)) * RotationQuaternion(rotation)) * Translation(rotationCenter)) * Translation(translation);
        }

        public static Matrix Transformation(Vector3 scalingCenter, Quaternion scalingRotation, Vector3 scaling, Vector3 rotationCenter, Quaternion rotation, Vector3 translation)
        {
            Matrix matrix;
            Transformation(ref scalingCenter, ref scalingRotation, ref scaling, ref rotationCenter, ref rotation, ref translation, out matrix);
            return matrix;
        }

        public static void Transformation2D(ref Vector2 scalingCenter, float scalingRotation, ref Vector2 scaling, ref Vector2 rotationCenter, float rotation, ref Vector2 translation, out Matrix result)
        {
            result = (((((((Translation((Vector3) (- scalingCenter)) * RotationZ(-scalingRotation)) * Scaling((Vector3)scaling)) * RotationZ(scalingRotation)) * Translation((Vector3)scalingCenter)) * Translation((Vector3) (- rotationCenter))) * RotationZ(rotation)) * Translation((Vector3)rotationCenter)) * Translation((Vector3)translation);
            result.M33 = 1f;
            result.M44 = 1f;
        }

        public static Matrix Transformation2D(Vector2 scalingCenter, float scalingRotation, Vector2 scaling, Vector2 rotationCenter, float rotation, Vector2 translation)
        {
            Matrix matrix;
            Transformation2D(ref scalingCenter, scalingRotation, ref scaling, ref rotationCenter, rotation, ref translation, out matrix);
            return matrix;
        }

        public static Matrix operator +(Matrix left, Matrix right)
        {
            Matrix matrix;
            Add(ref left, ref right, out matrix);
            return matrix;
        }

        public static Matrix operator +(Matrix value)
        {
            return value;
        }

        public static Matrix operator -(Matrix left, Matrix right)
        {
            Matrix matrix;
            Subtract(ref left, ref right, out matrix);
            return matrix;
        }

        public static Matrix operator -(Matrix value)
        {
            Matrix matrix;
            Negate(ref value, out matrix);
            return matrix;
        }

        public static Matrix operator *(float left, Matrix right)
        {
            Matrix matrix;
            Multiply(ref right, left, out matrix);
            return matrix;
        }

        public static Matrix operator *(Matrix left, float right)
        {
            Matrix matrix;
            Multiply(ref left, right, out matrix);
            return matrix;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            Matrix matrix;
            Multiply(ref left, ref right, out matrix);
            return matrix;
        }

        public static Matrix operator /(Matrix left, float right)
        {
            Matrix matrix;
            Divide(ref left, right, out matrix);
            return matrix;
        }

        public static Matrix operator /(Matrix left, Matrix right)
        {
            Matrix matrix;
            Divide(ref left, ref right, out matrix);
            return matrix;
        }

        public static bool operator ==(Matrix left, Matrix right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Matrix left, Matrix right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]", new object[] { (float)this.M11, (float)this.M12, (float)this.M13, (float)this.M14, (float)this.M21, (float)this.M22, (float)this.M23, (float)this.M24, (float)this.M31, (float)this.M32, (float)this.M33, (float)this.M34, (float)this.M41, (float)this.M42, (float)this.M43, (float)this.M44 });
        }

        public string ToString(string format)
        {
            if (format == null)
            {
                return this.ToString();
            }
            return string.Format(format, (object[])new object[] { 
            CultureInfo.CurrentCulture, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]", ((float) this.M11).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M12).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M13).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M14).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M21).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M22).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M23).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M24).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M31).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M32).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M33).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M34).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M41).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M42).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), 
            ((float) this.M43).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture), ((float) this.M44).ToString(format, (IFormatProvider) CultureInfo.CurrentCulture)
         });
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]", new object[] { ((float)this.M11).ToString(formatProvider), ((float)this.M12).ToString(formatProvider), ((float)this.M13).ToString(formatProvider), ((float)this.M14).ToString(formatProvider), ((float)this.M21).ToString(formatProvider), ((float)this.M22).ToString(formatProvider), ((float)this.M23).ToString(formatProvider), ((float)this.M24).ToString(formatProvider), ((float)this.M31).ToString(formatProvider), ((float)this.M32).ToString(formatProvider), ((float)this.M33).ToString(formatProvider), ((float)this.M34).ToString(formatProvider), ((float)this.M41).ToString(formatProvider), ((float)this.M42).ToString(formatProvider), ((float)this.M43).ToString(formatProvider), ((float)this.M44).ToString(formatProvider) });
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return this.ToString(formatProvider);
            }
            return string.Format(format, (object[])new object[] { 
            formatProvider, "[M11:{0} M12:{1} M13:{2} M14:{3}] [M21:{4} M22:{5} M23:{6} M24:{7}] [M31:{8} M32:{9} M33:{10} M34:{11}] [M41:{12} M42:{13} M43:{14} M44:{15}]", ((float) this.M11).ToString(format, formatProvider), ((float) this.M12).ToString(format, formatProvider), ((float) this.M13).ToString(format, formatProvider), ((float) this.M14).ToString(format, formatProvider), ((float) this.M21).ToString(format, formatProvider), ((float) this.M22).ToString(format, formatProvider), ((float) this.M23).ToString(format, formatProvider), ((float) this.M24).ToString(format, formatProvider), ((float) this.M31).ToString(format, formatProvider), ((float) this.M32).ToString(format, formatProvider), ((float) this.M33).ToString(format, formatProvider), ((float) this.M34).ToString(format, formatProvider), ((float) this.M41).ToString(format, formatProvider), ((float) this.M42).ToString(format, formatProvider), 
            ((float) this.M43).ToString(format, formatProvider), ((float) this.M44).ToString(format, formatProvider)
         });
        }

        public override int GetHashCode()
        {
            return (((((((((((((((((float)this.M11).GetHashCode() + ((float)this.M12).GetHashCode()) + ((float)this.M13).GetHashCode()) + ((float)this.M14).GetHashCode()) + ((float)this.M21).GetHashCode()) + ((float)this.M22).GetHashCode()) + ((float)this.M23).GetHashCode()) + ((float)this.M24).GetHashCode()) + ((float)this.M31).GetHashCode()) + ((float)this.M32).GetHashCode()) + ((float)this.M33).GetHashCode()) + ((float)this.M34).GetHashCode()) + ((float)this.M41).GetHashCode()) + ((float)this.M42).GetHashCode()) + ((float)this.M43).GetHashCode()) + ((float)this.M44).GetHashCode());
        }

        public bool Equals(Matrix other)
        {
            return ((((((Math.Abs((float)(other.M11 - this.M11)) < 1E-06f) && (Math.Abs((float)(other.M12 - this.M12)) < 1E-06f)) && ((Math.Abs((float)(other.M13 - this.M13)) < 1E-06f) && (Math.Abs((float)(other.M14 - this.M14)) < 1E-06f))) && (((Math.Abs((float)(other.M21 - this.M21)) < 1E-06f) && (Math.Abs((float)(other.M22 - this.M22)) < 1E-06f)) && ((Math.Abs((float)(other.M23 - this.M23)) < 1E-06f) && (Math.Abs((float)(other.M24 - this.M24)) < 1E-06f)))) && ((((Math.Abs((float)(other.M31 - this.M31)) < 1E-06f) && (Math.Abs((float)(other.M32 - this.M32)) < 1E-06f)) && ((Math.Abs((float)(other.M33 - this.M33)) < 1E-06f) && (Math.Abs((float)(other.M34 - this.M34)) < 1E-06f))) && (((Math.Abs((float)(other.M41 - this.M41)) < 1E-06f) && (Math.Abs((float)(other.M42 - this.M42)) < 1E-06f)) && (Math.Abs((float)(other.M43 - this.M43)) < 1E-06f)))) && (Math.Abs((float)(other.M44 - this.M44)) < 1E-06f));
        }

        public override bool Equals(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(value.GetType(), typeof(Matrix)))
            {
                return false;
            }
            return this.Equals((Matrix)value);
        }

        static Matrix()
        {
            SizeInBytes = Marshal.SizeOf((Type)typeof(Matrix));
            Zero = new Matrix();
            Matrix matrix = new Matrix();
            matrix.M11 = 1f;
            matrix.M22 = 1f;
            matrix.M33 = 1f;
            matrix.M44 = 1f;
            Identity = matrix;
        }
    }
}