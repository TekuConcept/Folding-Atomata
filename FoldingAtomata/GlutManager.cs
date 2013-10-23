using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FoldingAtomata
{
    public static class GlutManager
    {
        public delegate void BufferSwap();

        static int windowCount = 0;
        static List<Rectangle> sizes = new List<Rectangle>();
        static List<BufferSwap> bufferswaps = new List<BufferSwap>();

        public static int AddWindow(Rectangle s, BufferSwap buffer)
        {
            sizes.Add(s);
            bufferswaps.Add(buffer);
            windowCount++;
            return sizes.Count-1;
        }
        public static void RemoveWindow(int window)
        {
            CheckError(window);
            sizes.RemoveAt(window);
            bufferswaps.RemoveAt(window);
            windowCount--;
        }
        public static Size GetSize(int window)
        {
            CheckError(window);

            return sizes[window].Size;
        }
        public static Point GetPosition(int window)
        {
            CheckError(window);

            return sizes[window].Location;
        }
        public static int GetX(int window)
        {
            CheckError(window);

            return sizes[window].X; 
        }
        public static int GetY(int window)
        {
            CheckError(window);

            return sizes[window].Y;
        }
        public static int GetWidth(int window)
        {
            CheckError(window);

            return sizes[window].Width;
        }
        public static int GetHeight(int window)
        {
            CheckError(window);

            return sizes[window].Height;
        }
        public static void SetCursor(Cursor cursor)
        {
            switch (cursor) 
            {
                case Cursor.None: System.Windows.Forms.Cursor.Hide(); break;
                case Cursor.Crosshair:
                    System.Windows.Forms.Cursor.Show();
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Cross;
                    break;
                default:
                    System.Windows.Forms.Cursor.Show();
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; break;
            }
        }
        public static void WarpPointer(int x, int y)
        {
            // move mouse cursor
            System.Windows.Forms.Cursor.Position = new Point(x, y);
        }
        public static void SwapBuffers(int window)
        {
            CheckError(window);

            bufferswaps[window]();
        }
        private static void CheckError(int window)
        {
            if (window < 0) throw new ArgumentException("Window must be greater than or equal to 0");
            if (window >= sizes.Count) throw new ArgumentException("No such window exist with id: " + window);
        }

        public enum Cursor
        {
            None,
            Crosshair,
            Default
        }
    }
}
