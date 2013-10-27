using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FoldingXNA
{
    public class Renderer
    {
        public static int X { get; private set; }
        public static int Y { get; private set; }
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static void Flush()
        {
            GL.Flush();
        }
        public static void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        public static void ClearColor(float red, float green, float blue, float alpha = 0.0F)
        {
            GL.ClearColor(red, green, blue, alpha);
        }
        public static void ClearColor(Color4 color)
        {
            GL.ClearColor(color);
        }
        public static void Resize(int width, int height)
        {
            Resize(0, 0, width, height);
        }
        public static void Resize(int x, int y, int w, int h)
        {
            GL.Viewport(x, y, w, h);
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        #region - View -
        public static bool ApplyMatrix(Matrix4 mat, int handle = 0)
        {
            GL.UniformMatrix4(handle, false, ref mat);
            return true;
        }
        public static bool ResetTransform(int matrixhandle = 0)
        {
            try
            {
                var m = Matrix4.Identity;
                GL.UniformMatrix4(matrixhandle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Transform(Vector3 vec, int handle = 0)
        {
            try
            {
                var m = Matrix4.CreateTranslation(vec);
                GL.UniformMatrix4(handle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Translate(float x, float y, float z, int handle = 0)
        {
            try
            {
                var m = Matrix4.CreateTranslation(x, y, z);
                GL.UniformMatrix4(handle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Translate(Vector3 scale, int handle = 0)
        {
            return Translate(scale.X, scale.Y, scale.Z, handle);
        }
        public static bool TranslateX(float x, int handle = 0)
        {
            return Translate(x, 0, 0, handle);
        }
        public static bool TranslateY(float y, int handle = 0)
        {
            return Translate(0, y, 0, handle);
        }
        public static bool TranslateZ(float z, int handle = 0)
        {
            return Translate(0, 0, z, handle);
        }
        public static bool Rotate(float x, float y, float z, int handle = 0)
        {
            bool flag = true;
            flag &= RotateX(x, handle);
            flag &= RotateY(y, handle);
            flag &= RotateZ(z, handle);
            return flag;
        }
        public static bool Rotate(Vector3 scale, int handle = 0)
        {
            return Rotate(scale.X, scale.Y, scale.Z, handle);
        }
        public static bool RotateX(float x, int handle = 0)
        {
            try
            {
                var m = Matrix4.CreateRotationX(x);
                GL.UniformMatrix4(handle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool RotateY(float y, int handle = 0)
        {
            try
            {
                var m = Matrix4.CreateRotationY(y);
                GL.UniformMatrix4(handle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool RotateZ(float z, int handle = 0)
        {
            try
            {
                var m = Matrix4.CreateRotationZ(z);
                GL.UniformMatrix4(handle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Scale(float value, int handle = 0)
        {
            return Scale(value, value, value, handle);
        }
        public static bool Scale(float x, float y, float z, int handle = 0)
        {
            try
            {
                var m = Matrix4.Identity;
                m.M11 = x;
                m.M22 = y;
                m.M33 = z;
                GL.UniformMatrix4(handle, false, ref m);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool Scale(Vector3 scale, int handle = 0)
        {
            return Scale(scale.X, scale.Y, scale.Z, handle);
        }
        #endregion

        public static void Draw(BeginMode mode, DrawElementsType type, int indcCount, int vertID, int indcID)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertID); // 1(sphere), 4(cube)
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indcID); // 3(sphere), 6(cube)
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

            GL.DrawElements(mode, indcCount, type, IntPtr.Zero);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            
            Flush();
        }

        public static void ApplyColor(Color4 col, int handle)
        {
            GL.Uniform3(handle, col.R, col.G, col.B);
        }
        public static int GetShaderProperty(int sceneHandle, string name)
        {
            return GL.GetUniformLocation(sceneHandle, name);
        }
        public static int CreateVertexShader(string code)
        {
            return CreateShader(code, ShaderType.VertexShader);
        }
        public static int CreateFragmentShader(string code)
        {
            return CreateShader(code, ShaderType.FragmentShader);
        }
        private static int CreateShader(string code, ShaderType type)
        {
            int handle = GL.CreateShader(type);
            GL.ShaderSource(handle, code);
            GL.CompileShader(handle);
            return handle;
        }
        public static int CreateScene(int shaderhandle, params int[] othershaders)
        {
            int handle = GL.CreateProgram();

            GL.AttachShader(handle, shaderhandle);
            for (int i = 0; i < othershaders.Length; i++)
                GL.AttachShader(handle, othershaders[i]);
            GL.LinkProgram(handle);

            string programInfoLog;
            GL.GetProgramInfoLog(handle, out programInfoLog);
            Debug.WriteLine(programInfoLog);

            return handle;
        }
        public static void SetScene(int scenehandle)
        {
            GL.UseProgram(scenehandle);
        }
        public static void Enabled(EnableCap cap, bool enable)
        {
            if (enable)
                GL.Enable(cap);
            else
                GL.Disable(cap);
        }
        public static void CullMode(CullFaceMode mode)
        {
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(mode);
        }

        public static int LoadBuffer(Vector3[] vertices, int scene, string propertyName, int index)
        {
            int handle;

            GL.GenBuffers(1, out handle);
            GL.BindBuffer(BufferTarget.ArrayBuffer, handle);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer,
                new IntPtr(vertices.Length * Vector3.SizeInBytes),
                vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(index);
            GL.BindAttribLocation(scene, index, propertyName);
            GL.VertexAttribPointer(index, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

            return handle;
        }
        public static int LoadBuffer(uint[] indices)
        {
            int handle;

            GL.GenBuffers(1, out handle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, handle);
            GL.BufferData<uint>(BufferTarget.ElementArrayBuffer,
                new IntPtr(indices.Length * Vector3.SizeInBytes),
                indices, BufferUsageHint.StaticDraw);

            return handle;
        }
    }
}
