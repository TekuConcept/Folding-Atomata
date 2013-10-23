using FoldingAtomata.Modeling.NMesh;
using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using XNA;

namespace FoldingAtomata.Modeling.DataBuffers
{
    public class IndexBuffer : DataBuffer
    {
        public IndexBuffer(int length, BeginMode type = BeginMode.Triangles)
        {
            _acceptedAsType = type;

            //_indices.reserve(length);
            for (int j = 0; j < length; j++)
                _indices.Add((int)j);
        }
        public IndexBuffer(List<int> indices, BeginMode type = BeginMode.Triangles)
        {
            _indices = indices;
            _acceptedAsType = type;
        }
        public void Draw(BeginMode mode)
        {
            int i = 0;
            GL.DrawElements(mode, _indices.Count, DrawElementsType.UnsignedShort, ref i);
        }

        //<!-- Warning -->

        public virtual void Store(int programHandle)
        {
            GL.GenBuffers(1, out _indexBuffer);

            int[] _ind = _indices.ToArray();
            IntPtr size = new IntPtr(_ind.Length * sizeof(int));
            GCHandle handle1 = GCHandle.Alloc(_ind);
            IntPtr ptr = (IntPtr)handle1;

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, size,
                ptr, BufferUsageHint.StaticDraw);
        }
        public virtual void Enable()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
        }
        public virtual void Disable()
        {

        }
        public virtual ShaderSnippet GetVertexShaderGLSL()
        {
            return new ShaderSnippet();
        }
        public virtual ShaderSnippet GetFragmentShaderGLSL()
        {
            return new ShaderSnippet();
        }

        List<int> _indices = new List<int>();
        uint _indexBuffer;
        BeginMode _acceptedAsType;
    }
}
