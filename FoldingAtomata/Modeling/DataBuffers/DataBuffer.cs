using FoldingAtomata.Modeling.Shading;
using System;

namespace FoldingAtomata.Modeling.DataBuffers
{
    public interface DataBuffer
    {
        void Store(int programHandle);
        void Enable();
        void Disable();

        ShaderSnippet GetVertexShaderGLSL();
        ShaderSnippet GetFragmentShaderGLSL();
    }
}
