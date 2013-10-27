using OpenTK;
using System;
using System.Collections.Generic;

namespace FoldingXNA
{
    public static class XNAHelper
    {
        public static Vector3 GetForward(this Matrix4 mat)
        {
            return new Vector3(-mat.M31, -mat.M32, -mat.M33);
        }
        public static void SetForward(this Matrix4 mat, Vector3 value)
        {
            mat.M31 = -value.X;
            mat.M32 = -value.Y;
            mat.M33 = -value.Z;
        }
        public static Vector3 GetTranslation(this Matrix4 mat)
        {
            return new Vector3(mat.M41, mat.M42, mat.M43);
        }
        public static void SetTranslation(this Matrix4 mat, Vector3 value)
        {
            mat.M41 = value.X;
            mat.M42 = value.Y;
            mat.M43 = value.Z;
        }
        public static void AddToTranslation(this Matrix4 mat, Vector3 value)
        {
            mat.M41 += value.X;
            mat.M42 += value.Y;
            mat.M43 += value.Y;
        }


        public static bool DecomposeMatrix(ref Matrix4 mat, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            translation.X = mat.M41;
            translation.Y = mat.M42;
            translation.Z = mat.M43;
            scale.X = (float)Math.Sqrt((double)(((mat.M11 * mat.M11) + (mat.M12 * mat.M12)) + (mat.M13 * mat.M13)));
            scale.Y = (float)Math.Sqrt((double)(((mat.M21 * mat.M21) + (mat.M22 * mat.M22)) + (mat.M23 * mat.M23)));
            scale.Z = (float)Math.Sqrt((double)(((mat.M31 * mat.M31) + (mat.M32 * mat.M32)) + (mat.M33 * mat.M33)));
            if (((Math.Abs(scale.X) < 1E-06f) || (Math.Abs(scale.Y) < 1E-06f)) || (Math.Abs(scale.Z) < 1E-06f))
            {
                rotation = Quaternion.Identity;
                return false;
            }
            Matrix4 matrix = new Matrix4();
            matrix.M11 = mat.M11 / scale.X;
            matrix.M12 = mat.M12 / scale.X;
            matrix.M13 = mat.M13 / scale.X;
            matrix.M21 = mat.M21 / scale.Y;
            matrix.M22 = mat.M22 / scale.Y;
            matrix.M23 = mat.M23 / scale.Y;
            matrix.M31 = mat.M31 / scale.Z;
            matrix.M32 = mat.M32 / scale.Z;
            matrix.M33 = mat.M33 / scale.Z;
            matrix.M44 = 1f;
            RotationMatrix(ref matrix, out rotation);
            rotation = RotationMatrix(matrix);
            return true;
        }
        public static void RotationMatrix(ref Matrix4 matrix, out Quaternion result)
        {
            result = new Quaternion();
            float num;
            float num3 = (matrix.M11 + matrix.M22) + matrix.M33;
            if (num3 > 0f)
            {
                num = (float)Math.Sqrt((double)(num3 + 1f));
                result.W = num * 0.5f;
                num = 0.5f / num;
                result.X = (matrix.M23 - matrix.M32) * num;
                result.Y = (matrix.M31 - matrix.M13) * num;
                result.Z = (matrix.M12 - matrix.M21) * num;
            }
            else
            {
                float num2;
                if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
                {
                    num = (float)Math.Sqrt((double)(((1f + matrix.M11) - matrix.M22) - matrix.M33));
                    num2 = 0.5f / num;
                    result.X = 0.5f * num;
                    result.Y = (matrix.M12 + matrix.M21) * num2;
                    result.Z = (matrix.M13 + matrix.M31) * num2;
                    result.W = (matrix.M23 - matrix.M32) * num2;
                }
                else if (matrix.M22 > matrix.M33)
                {
                    num = (float)Math.Sqrt((double)(((1f + matrix.M22) - matrix.M11) - matrix.M33));
                    num2 = 0.5f / num;
                    result.X = (matrix.M21 + matrix.M12) * num2;
                    result.Y = 0.5f * num;
                    result.Z = (matrix.M32 + matrix.M23) * num2;
                    result.W = (matrix.M31 - matrix.M13) * num2;
                }
                else
                {
                    num = (float)Math.Sqrt((double)(((1f + matrix.M33) - matrix.M11) - matrix.M22));
                    num2 = 0.5f / num;
                    result.X = (matrix.M31 + matrix.M13) * num2;
                    result.Y = (matrix.M32 + matrix.M23) * num2;
                    result.Z = 0.5f * num;
                    result.W = (matrix.M12 - matrix.M21) * num2;
                }
            }
        }
        public static Quaternion RotationMatrix(Matrix4 matrix)
        {
            Quaternion quaternion;
            RotationMatrix(ref matrix, out quaternion);
            return quaternion;
        }
    }
}
