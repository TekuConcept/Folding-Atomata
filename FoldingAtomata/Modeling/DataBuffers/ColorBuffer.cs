using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using XNA;

namespace FoldingAtomata.Modeling.DataBuffers
{
    public class ColorBuffer : OptionalDataBuffer
    {
        public ColorBuffer(Vector3 color, int count)
        {
            //_colors.reserve(count);
            for (int j = 0; j < count; j++)
                _colors.Add(color);
        }
        public ColorBuffer(List<Vector3> colors)
        {
            _colors = colors;
        }
        public List<Vector3> GetColors()
        {
            return _colors;
        }
        public virtual void Store(int programHandle)
        {
            GL.GenBuffers(1, out _colorBuffer);
            _colorAttrib = GL.GetAttribLocation(programHandle, "vertexColor");

            Vector3[] _ver = _colors.ToArray();
            IntPtr size = new IntPtr(_ver.Length * Vector3.SizeInBytes);
            GCHandle handle1 = GCHandle.Alloc(_ver);
            IntPtr ptr = (IntPtr)handle1;

            GL.BindBuffer(BufferTarget.ArrayBuffer, _colorBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, size,
                ptr, BufferUsageHint.StaticDraw);
        }
        public virtual void Enable()
        {
            GL.EnableVertexAttribArray(_colorAttrib);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _colorBuffer);
            GL.VertexAttribPointer(_colorAttrib, 3, VertexAttribPointerType.Float, false, 0, 0);
        }
        public virtual void Disable()
        {
            GL.DisableVertexAttribArray(_colorAttrib);
        }
        public virtual ShaderSnippet GetVertexShaderGLSL()
        {
            return new ShaderSnippet(
                @"
                    //ColorBuffer fields
                    attribute vec3 vertexColor;
                    varying vec3 vertexColorBlend;
                ",
                @"
                    //ColorBuffer methods
                ",
                @"
                    //ColorBuffer main method code
                    vertexColorBlend = vertexColor;
                "
            );
        }
        public virtual ShaderSnippet GetFragmentShaderGLSL()
        {
            return new ShaderSnippet(
                @"
                    //ColorBuffer fields
                    varying vec3 vertexColorBlend;
                ",
                @"
                    //ColorBuffer methods
                ",
                @"
                    //ColorBuffer main method code
                    colors.material = vertexColorBlend;
                "
            );
        }

        List<Vector3> _colors = new List<Vector3>();
        uint _colorBuffer;
        int _colorAttrib;
    }
}
