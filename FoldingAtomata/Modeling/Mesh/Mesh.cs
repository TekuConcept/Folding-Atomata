using FoldingAtomata.Modeling.DataBuffers;
using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.Modeling.NMesh
{
    public class Mesh
    {
        public Mesh(VertexBuffer vertexBuffer, 
            BeginMode mode = BeginMode.Triangles)
        {
            _vertexBuffer = vertexBuffer;
            _indexBuffer = null;
            _renderingMode = mode;
        }
        public Mesh(VertexBuffer vertexBuffer, IndexBuffer indexBuffer, 
            BeginMode mode = BeginMode.Triangles)
        {
            _vertexBuffer = vertexBuffer;
            _indexBuffer = indexBuffer;
            _renderingMode = mode;
        }
        public virtual void Store(int programHandle)
        {
            _vertexBuffer.Store(programHandle);
            if (_indexBuffer != null)
                _indexBuffer.Store(programHandle);
        }
        public virtual void Enable()
        {
            _vertexBuffer.Enable();
            if (_indexBuffer != null)
                _indexBuffer.Enable();
        }
        public virtual void Disable()
        {
            _vertexBuffer.Disable();
            if (_indexBuffer != null)
                _indexBuffer.Disable();
        }
        public virtual void Draw()
        {
            //indexBuffer is drawn with priority if it is available
            if (_indexBuffer != null)
                _indexBuffer.Draw(_renderingMode);
            else
                _vertexBuffer.Draw(_renderingMode);
        }
        public virtual ShaderSnippet GetVertexShaderGLSL()
        {
            return new ShaderSnippet();
        }
        public virtual ShaderSnippet GetFragmentShaderGLSL()
        {
            return new ShaderSnippet();
        }
        public List<Vector3> GetVertices()
        {
            return _vertexBuffer.GetVertices();
        }
        
        public VertexBuffer GetVertexBuffer()
        {
            return _vertexBuffer;
        }
        public IndexBuffer GetIndexBuffer()
        {
            return _indexBuffer;
        }

        VertexBuffer _vertexBuffer;
        IndexBuffer _indexBuffer;
        BeginMode _renderingMode;
    }
}
