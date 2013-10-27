using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace FoldingXNA
{
    public struct Atom
    {
        public String Symbol;
        public float Radius;
        public float Charge;
        public float Mass;
        public int Number;
        public Color4 Color;
        public List<Vector3> KeyFrames;

        public Atom(String sym, float rad, float chrg, float mas, int num)
        {
            Symbol = sym;
            Radius = rad;
            Charge = chrg;
            Mass = mas;
            Number = num;
            Color = GetColor(GetScaleValue(num));
            KeyFrames = new List<Vector3>();
        }

        public void AddKeyFrame(Vector3 value)
        {
            KeyFrames.Add(value);
        }

        #region - Static -
        public static Color4 GetColor(int scale)
        {
            switch (scale)
            {
                case 1: return Color4.MediumOrchid;
                case 2: return Color4.Lavender;
                case 3: return Color4.Green;
                case 4: return Color4.White;
                default: return Color4.Black;
            }
        }
        public static int GetScaleValue(int electrons)
        {
            if (electrons <= 2) return 1;
            else if (electrons <= 10) return 2;
            else if (electrons <= 28) return 3;
            else if (electrons <= 60) return 4;
            else return 5;
        }
        public static Matrix4 GetScale(int scale)
        {
            switch (scale)
            {
                case 1: return Matrix4.Scale(0.2F);
                case 2: return Matrix4.Scale(0.4F);
                case 3: return Matrix4.Scale(0.8F);
                case 4: return Matrix4.Scale(1.2F);
                default: return Matrix4.Scale(0.1F);
            }
        }
        #endregion
    }

    public struct BondID
    {
        public int A, B;

        public BondID(int a, int b)
        {
            A = a;
            B = b;
        }
    }
}
