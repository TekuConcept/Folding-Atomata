using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using XNA;

namespace FoldingAtomata.Modeling.DataBuffers
{
    public class VertexBuffer : DataBuffer
    {
        public VertexBuffer(List<Vector3> vertices)
        {
            _vertices = vertices;
        }
        public virtual void Store(int programHandle)
        {
            GL.GenBuffers(1, out _vertexBuffer);
            _vertexAttrib = GL.GetAttribLocation(programHandle, "vertex");

            Vector3[] _ver = _vertices.ToArray();

            IntPtr size = new IntPtr(_ver.Length * Vector3.SizeInBytes);
            GCHandle handle1 = GCHandle.Alloc(_ver);
            IntPtr ptr = (IntPtr)handle1;

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, size, ptr, BufferUsageHint.StaticDraw);
            GL.EnableVertexAttribArray(_vertexAttrib);
        }
        public virtual void Enable()
        {
            GL.EnableVertexAttribArray(_vertexAttrib);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.VertexAttribPointer(_vertexAttrib, 3, VertexAttribPointerType.Float, false, 0, 0);
        }
        public virtual void Disable()
        {
            GL.DisableVertexAttribArray(_vertexAttrib);
        }
        public virtual ShaderSnippet GetVertexShaderGLSL()
        {
            return new ShaderSnippet();
        }
        public virtual ShaderSnippet GetFragmentShaderGLSL()
        {
            return new ShaderSnippet();
        }
        public void Draw(BeginMode mode)
        {
            int i = 0;
            GL.DrawElements(mode, _vertices.Count, DrawElementsType.UnsignedShort, ref i);
        }
        public List<Vector3> GetVertices()
        {
            return _vertices;
        }

        List<Vector3> _vertices = new List<Vector3>();
        uint _vertexBuffer;
        int _vertexAttrib;
    }
}
