using SharpDX;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Tao.OpenGl;

namespace FoldingXNA
{
    public class RendererOld
    {
        static RendererOld()
        {
            // -left_side, right_side, -bottom, top, near, -far
            //Gl.glOrtho(-1, 1, -1, 1, -1, 1);
            //Gl.glFrustum(-1, 1, -1, 1, -1, 1);
            Gl.glViewport(0, 0, 600, 400);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
        }
        public static void Clear()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
        }
        public static void Clear(float R, float G, float B, float A = 0.0F)
        {
            Gl.glClearColor(R, G, B, A);
            Clear();
        }
        public static void SetViewport(int width, int height)
        {
            Gl.glViewport(0, 0, width, height);
            //Glu.gluPerspective(45.0f, (double)this.Width / (double)this.Height, 0.01f, 5000.0f);
        }
        public static void Resize(int width, int height)
        {
            Resize(0, 0, width, height);
        }
        public static void Resize(int x, int y, int w, int h)
        {
            Gl.glViewport(x, y, w, h);
        }

        #region - View -
        public static void ResetTransform()
        {
            Gl.glLoadIdentity();
        }
        public static void Transform(Matrix mat4)
        {
            Gl.glLoadMatrixf(mat4.ToArray());
        }
        public static void Translate(float x, float y, float z)
        {
            Gl.glTranslatef(x, y, z);
        }
        public static void Translate(Vector3 scale)
        {
            Translate(scale.X, scale.Y, scale.Z);
        }
        public static void TranslateX(float x)
        {
            Translate(x, 0, 0);
        }
        public static void TranslateY(float y)
        {
            Translate(0, y, 0);
        }
        public static void TranslateZ(float z)
        {
            Translate(0, 0, z);
        }
        public static void Rotate(float x, float y, float z, float w = 0)
        {
            Gl.glRotatef(x, y, z, w);
        }
        public static void Rotate(Vector3 scale)
        {
            Rotate(scale.X, scale.Y, scale.Z);
        }
        public static void Rotate(Vector4 scale)
        {
            Rotate(scale.X, scale.Y, scale.Z, scale.W);
        }
        public static void RotateX(float x)
        {
            Rotate(x, 0, 0);
        }
        public static void RotateY(float y)
        {
            Rotate(0, y, 0);
        }
        public static void RotateZ(float z)
        {
            Rotate(0, 0, z);
        }
        public static void RotateW(float w)
        {
            Rotate(0, 0, 0, w);
        }
        public static void Scale(float value)
        {
            Scale(value, value, value);
        }
        public static void Scale(float x, float y, float z)
        {
            Gl.glScalef(x, y, z);
        }
        public static void Scale(Vector3 scale)
        {
            Scale(scale.X, scale.Y, scale.Z);
        }
        public static void ScaleX(float x)
        {
            Scale(x, 0, 0);
        }
        public static void ScaleY(float y)
        {
            Scale(0, y, 0);
        }
        public static void ScaleZ(float z)
        {
            Scale(0, 0, z);
        }
        #endregion

        #region - Object -
        public static void DrawTriangleStrip(float[] verticies)
        {
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, verticies);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_STRIP, 0, (verticies.Length / 3) - 2);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
        }
        public static void DrawTriangleStrip(float[] verticies, int count)
        {
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            //Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, verticies);
            //Gl.glVertexAttrib3fv(0, verticies);

            //Gl.glBindVertexShaderEXT(vao);
            //glDrawArraysInstanced(Gl.GL_TRIANGLES, 0, (verticies.Length / 3) - 2, count);
            //glDrawArraysInstanced(Gl.GL_TRIANGLE_STRIP, 0, count, (verticies.Length / 3) - 2);
            //glDrawArrays(Gl.GL_TRIANGLE_STRIP, 0, (verticies.Length / 3) - 2);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
        }
        public static void DrawTriangleFan(float[] verticies)
        {
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glVertexPointer(3, Gl.GL_FLOAT, 0, verticies);
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 0, (verticies.Length / 3) - 3);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
        }


        [DllImport("opengl32")]
        public static extern void glDrawArraysInstanced(int mode, int first, int count, int primcount);

        [DllImport("opengl32")]
        public static extern void glDrawArrays(int mode, int first, int count);

        #endregion
    }
}
