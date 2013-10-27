using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FoldingXNA
{
    public static class Utils
    {
        public static List<Atom> ParseFile(string path, out List<BondID> bonds)
        {
            String data = File.ReadAllText(path);
            return ParseString(data, out bonds);
        }
        public static List<Atom> ParseString(string data, out List<BondID> bonds)
        {
            List<Atom> atoms = new List<Atom>();
            bonds = new List<BondID>();
            
            // partition file
            MatchCollection topTok = Regex.Matches(data, @"(?<=\[)((?>[^\[\]]+|\[(?<DEPTH>)|\](?<-DEPTH>))*(?(DEPTH)(?!)))(?=\])", RegexOptions.Multiline);
            
            // get atom data
            MatchCollection topA = Regex.Matches(topTok[0].Value, @"(?<=\[)((?>[^\[\]]+|\[(?<DEPTH>)|\](?<-DEPTH>))*(?(DEPTH)(?!)))(?=\])", RegexOptions.Multiline);
            
            // get bond data
            MatchCollection topB = Regex.Matches(topTok[1].Value, @"(?<=\[)((?>[^\[\]]+|\[(?<DEPTH>)|\](?<-DEPTH>))*(?(DEPTH)(?!)))(?=\])", RegexOptions.Multiline);

            foreach (Match token in topA)
            {
                String[] tokens = token.Value.Split(',');
                atoms.Add(new Atom(tokens[0].Replace("\"", ""), float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3]), int.Parse(tokens[4])));
            }
            foreach (Match token in topB)
            {
                String[] tokens = token.Value.Split(',');
                bonds.Add(new BondID(int.Parse(tokens[0]), int.Parse(tokens[1])));
            }

            for (int i = 2; i < topTok.Count; i++)
            {
                int idx = 0;
                MatchCollection topC = Regex.Matches(topTok[i].Value, @"(?<=\[)((?>[^\[\]]+|\[(?<DEPTH>)|\](?<-DEPTH>))*(?(DEPTH)(?!)))(?=\])", RegexOptions.Multiline);
                foreach (Match token in topC)
                {
                    String[] tokens = token.Value.Replace("\r", "").Replace("\n", "").Split(',');
                    Atom atom = atoms[idx];
                    atom.AddKeyFrame(new Vector3(float.Parse(tokens[0]), float.Parse(tokens[1]), float.Parse(tokens[2])));
                    atoms[idx] = atom;
                    idx++;
                }
            }

            return atoms;
        }
        
        // Generate Metrix from Atom Vectors
        public static Matrix4 GenerateBondMatrixOld(Vector3 a, Vector3 b)
        {
            float BOND_SCALE = 0.06F;
            float FLT_EPSILON = 1.19209290E-07F;

            float dist = Distance(b, a);


            // scale
            Matrix4 scale;
            if (dist <= FLT_EPSILON) 
                scale = Matrix4.Scale(BOND_SCALE, BOND_SCALE, FLT_EPSILON);
            else scale = Matrix4.Scale(BOND_SCALE, BOND_SCALE, dist);
            // translate
            var translation = Matrix4.CreateTranslation(a);


            // rotate
            var z = b - a;
            var cross = Cross(Vector3.UnitZ, z);
            var dot   =   Dot(Vector3.UnitZ, z);


            if (dist == 0) dist = 1;
            float angle = (float)(Math.Acos(dot)); // radians

            var rotation = Matrix4.CreateFromAxisAngle(cross, angle); // radians

            return scale * rotation * translation;
        }
        public static Matrix4 GenerateBondMatrix(Vector3 start, Vector3 end)
        {
            float FLT_EPSILON = 1.19209290E-07F;
            float distance = GetMagnitude(start - end);
            if (distance <= FLT_EPSILON)
                return Matrix4.Scale(new Vector3(0.06F, 0.06F, FLT_EPSILON));

            var matrix = AlignBetween(start, end);
            return Matrix4.Scale(new Vector3(0.06F, 0.06F, distance)) * matrix;
        }
        public static Matrix4 AlignBetween(Vector3 a, Vector3 b)
        {
            Vector3 z = new Vector3(0, 0, 1);
            Vector3 p = b - a;

            float radians = (float)Math.Acos(GetDotProduct(z, p) / GetMagnitude(p));

            var translated = Matrix4.CreateTranslation(a);
            return Matrix4.CreateFromAxisAngle(Vector3.Cross(z, p), radians) * translated;
        }
        public static float GetDotProduct(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z; ;
        }
        public static float GetMagnitude(Vector3 vector)
        {
            return (float)Math.Sqrt(GetDotProduct(vector, vector));
        }

        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            Vector3 vector = new Vector3();
            vector.X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y);
            vector.Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z);
            vector.Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X);
            return vector;
        }
        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return (((vector1.X * vector2.X) + (vector1.Y * vector2.Y)) + (vector1.Z * vector2.Z));
        }
        public static Matrix4 CreateFromAxisAngle(Vector3 axis, float angle)
        {
            Matrix4 matrix = new Matrix4();
            float x = axis.X;
            float y = axis.Y;
            float z = axis.Z;
            float num2 = (float) Math.Sin((double) angle);
            float num = (float) Math.Cos((double) angle);
            float num11 = x * x;
            float num10 = y * y;
            float num9 = z * z;
            float num8 = x * y;
            float num7 = x * z;
            float num6 = y * z;
            matrix.M11 = num11 + (num * (1f - num11));
            matrix.M12 = (num8 - (num * num8)) + (num2 * z);
            matrix.M13 = (num7 - (num * num7)) - (num2 * y);
            matrix.M14 = 0f;
            matrix.M21 = (num8 - (num * num8)) - (num2 * z);
            matrix.M22 = num10 + (num * (1f - num10));
            matrix.M23 = (num6 - (num * num6)) + (num2 * x);
            matrix.M24 = 0f;
            matrix.M31 = (num7 - (num * num7)) + (num2 * y);
            matrix.M32 = (num6 - (num * num6)) - (num2 * x);
            matrix.M33 = num9 + (num * (1f - num9));
            matrix.M34 = 0f;
            matrix.M41 = 0f;
            matrix.M42 = 0f;
            matrix.M43 = 0f;
            matrix.M44 = 1f;
            return matrix;
        }
        public static Matrix4 Multiply(Matrix4 matrix1, Matrix4 matrix2)
        {
            Matrix4 matrix = new Matrix4();
            matrix.M11 = (((matrix1.M11 * matrix2.M11) + (matrix1.M12 * matrix2.M21)) + (matrix1.M13 * matrix2.M31)) + (matrix1.M14 * matrix2.M41);
            matrix.M12 = (((matrix1.M11 * matrix2.M12) + (matrix1.M12 * matrix2.M22)) + (matrix1.M13 * matrix2.M32)) + (matrix1.M14 * matrix2.M42);
            matrix.M13 = (((matrix1.M11 * matrix2.M13) + (matrix1.M12 * matrix2.M23)) + (matrix1.M13 * matrix2.M33)) + (matrix1.M14 * matrix2.M43);
            matrix.M14 = (((matrix1.M11 * matrix2.M14) + (matrix1.M12 * matrix2.M24)) + (matrix1.M13 * matrix2.M34)) + (matrix1.M14 * matrix2.M44);
            matrix.M21 = (((matrix1.M21 * matrix2.M11) + (matrix1.M22 * matrix2.M21)) + (matrix1.M23 * matrix2.M31)) + (matrix1.M24 * matrix2.M41);
            matrix.M22 = (((matrix1.M21 * matrix2.M12) + (matrix1.M22 * matrix2.M22)) + (matrix1.M23 * matrix2.M32)) + (matrix1.M24 * matrix2.M42);
            matrix.M23 = (((matrix1.M21 * matrix2.M13) + (matrix1.M22 * matrix2.M23)) + (matrix1.M23 * matrix2.M33)) + (matrix1.M24 * matrix2.M43);
            matrix.M24 = (((matrix1.M21 * matrix2.M14) + (matrix1.M22 * matrix2.M24)) + (matrix1.M23 * matrix2.M34)) + (matrix1.M24 * matrix2.M44);
            matrix.M31 = (((matrix1.M31 * matrix2.M11) + (matrix1.M32 * matrix2.M21)) + (matrix1.M33 * matrix2.M31)) + (matrix1.M34 * matrix2.M41);
            matrix.M32 = (((matrix1.M31 * matrix2.M12) + (matrix1.M32 * matrix2.M22)) + (matrix1.M33 * matrix2.M32)) + (matrix1.M34 * matrix2.M42);
            matrix.M33 = (((matrix1.M31 * matrix2.M13) + (matrix1.M32 * matrix2.M23)) + (matrix1.M33 * matrix2.M33)) + (matrix1.M34 * matrix2.M43);
            matrix.M34 = (((matrix1.M31 * matrix2.M14) + (matrix1.M32 * matrix2.M24)) + (matrix1.M33 * matrix2.M34)) + (matrix1.M34 * matrix2.M44);
            matrix.M41 = (((matrix1.M41 * matrix2.M11) + (matrix1.M42 * matrix2.M21)) + (matrix1.M43 * matrix2.M31)) + (matrix1.M44 * matrix2.M41);
            matrix.M42 = (((matrix1.M41 * matrix2.M12) + (matrix1.M42 * matrix2.M22)) + (matrix1.M43 * matrix2.M32)) + (matrix1.M44 * matrix2.M42);
            matrix.M43 = (((matrix1.M41 * matrix2.M13) + (matrix1.M42 * matrix2.M23)) + (matrix1.M43 * matrix2.M33)) + (matrix1.M44 * matrix2.M43);
            matrix.M44 = (((matrix1.M41 * matrix2.M14) + (matrix1.M42 * matrix2.M24)) + (matrix1.M43 * matrix2.M34)) + (matrix1.M44 * matrix2.M44);
            return matrix;
        }

        public static float Distance(Vector3 value1, Vector3 value2)
        {
            return (float)Math.Sqrt(((Math.Pow(value1.X - value2.X, 2) + Math.Pow(value1.Y - value2.Y, 2)) + Math.Pow(value1.Z - value2.Z, 2)));
        }
        public static float Angle(Vector2 a, Vector2 b)
        {
            return Angle(a.X, a.Y, b.X, b.Y);
        }
        public static float Angle(float x1, float y1, float x2, float y2)
        {
            try
            {
                float
                    deltaX = Distance(x1, x2),
                    deltaY = Distance(y1, y2);
                double ang = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;

                // fix
                if (y1 < y2) if (x1 < x2) ang += 180; else ang = (90 - ang) + 270;
                else if (x1 < x2) ang = (90 - ang) + 90;

                return (float)ang;
            }
            catch
            {
                if (y1 == y2)
                {
                    if (x1 > x2) return 0;
                    else return 180;
                }
                else
                {
                    if (y1 > y2) return 90;
                    else return 270;
                }
            }
        }
        public static float Distance(float a, float b)
        {
            float c = b - a;
            if (c < 0) c *= -1;
            return c;
        }

        public static T[] Reverse<T>(this T[] n)
        {
            List<T> N = new List<T>(n);
            N.Reverse();
            return N.ToArray();
        }
        public static float[] FloatMatrix(Matrix4 mat4)
        {
            float[] mat = new float[] { 
                mat4.M11, mat4.M12, mat4.M13, mat4.M14,
                mat4.M21, mat4.M22, mat4.M23, mat4.M24,
                mat4.M31, mat4.M32, mat4.M33, mat4.M34,
                mat4.M41, mat4.M42, mat4.M43, mat4.M44,
            };

            return mat;
        }
    }
}
