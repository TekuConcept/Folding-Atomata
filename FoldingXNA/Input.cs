using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FoldingXNA
{
    public static class KeyboardState
    {
        public static bool Shift { get; set; }
        public static Keys Modifiers { get; set; }
        public static int KeyValue { get; set; }
        public static Keys KeyData { get; set; }
        public static Keys KeyCode { get; set; }
        public static bool Ctrl { get; set; }
        public static bool Alt { get; set; }
        public static bool IsButtonDown { get; set; }
        public static char KeyChar { get; set; }

        internal static bool IsKeyDown(Keys keys)
        {
            return IsButtonDown && KeyData == keys;
        }
    }

    public static class MouseState
    {
        public static int X { get; set; }
        public static int Y { get; set; }
        public static MouseButtons Button { get; set; }
        public static int Delta { get; set; }
        public static int Clicks { get; set; }
        public static bool IsButtonDown { get; set; }

        public static void SetPosition(Point p)
        {
            Cursor.Position = p;
        }
    }
}
