using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace FoldingAtomata
{
    public static class Utils
    {
        public static OpenTK.Matrix4 XNA_OTK_Matrix(XNA.Matrix mat)
        {
            return new OpenTK.Matrix4(
                mat.M11, mat.M12, mat.M13, mat.M14,
                mat.M21, mat.M22, mat.M23, mat.M24,
                mat.M31, mat.M32, mat.M33, mat.M34,
                mat.M41, mat.M42, mat.M43, mat.M44);
        }

        public static float[] XNA_Float_Matrix(XNA.Matrix mat)
        {
            return new float[]{
                mat.M11, mat.M12, mat.M13, mat.M14,
                mat.M21, mat.M22, mat.M23, mat.M24,
                mat.M31, mat.M32, mat.M33, mat.M34,
                mat.M41, mat.M42, mat.M43, mat.M44
            };
        }

        public static float[] XNA_Float_Vector(XNA.Vector3 vec)
        {
            return new float[] { vec.X, vec.Y, vec.Z };
        }

        public static int FindAll<T>(this List<T> list, T search)
        {
            int i = 0;
            for (int j = 0; j < list.Count; j++)
            {
                if ((object)list[i] == (object)search) i++;
            }
            return i;
        }

        public static int Find_First_Not_Of(this string str, string whitespace, int k)
        {
            for (int x = k; x < str.Length; x++)
            {
                bool good = true;
                for (int y = 0; y < whitespace.Length; y++)
                    if (str[x] == whitespace[y])
                        good = false;

                if (good)
                    return x;
            }
            return -1;
        }
        public static int Find_Last_Not_Of(this string str, string whitespace, int k)
        {
            for (int x = k; x > -1; x--)
            {
                bool bad = true;
                for (int y = 0; y < whitespace.Length; y++)
                    if (str[x] == whitespace[y])
                        bad = false;

                if (!bad)
                    return x;
            }
            return -1;
        }

        public static byte[] ToByteArray(this char[] list)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < list.Length; i++)
                bytes.Add((byte)list[i]);
            return bytes.ToArray();
        }
        public static char[] ToCharArray(this byte[] list)
        {
            List<char> chars = new List<char>();
            for (int i = 0; i < list.Length; i++)
            {
                chars.Add((char)list[i]);
            }
            return chars.ToArray();
        }

        public static StringBuilder[] ToStringBuilderArray(this string[] list)
        {
            List<StringBuilder> builders = new List<StringBuilder>();
            for (int i = 0; i < list.Length; i++)
            {
                builders.Add(new StringBuilder(list[i]));
            }
            return builders.ToArray();
        }
    }

    public struct Pair<T1, T2>
    {
        public T1 First;
        public T2 Second;

        public Pair(T1 a, T2 b)
        {
            First = a;
            Second = b;
        }

        public static bool operator ==(Pair<T1, T2> a, Pair<T1, T2> b)
        {
            return a.First.Equals(b.First) && a.Second.Equals(b.Second);
        }

        public static bool operator !=(Pair<T1, T2> a, Pair<T1, T2> b)
        {
            return !(a.First.Equals(b.First) && a.Second.Equals(b.Second));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
