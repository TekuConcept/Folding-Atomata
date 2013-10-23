using FoldingAtomata.Modeling.DataBuffers;
using FoldingAtomata.World;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.Modeling.Shading
{
    public class ShaderManager
    {
        public static ShaderProgram CreateProgram(InstancedModel model,
            ShaderSnippet sceneVertexShader, ShaderSnippet sceneFragmentShader, List<Light> lights)
        {
            Console.Write("Creating vertex and fragment shaders for Model with {0} light(s)... ", lights.Count);

            var buffers = model.GetOptionalDataBuffers();
            var vertexShaderStr = AssembleVertexShaderStr(buffers, sceneVertexShader, lights);
            var fragmentShaderStr = AssembleFragmentShaderStr(buffers, sceneFragmentShader, lights);

            Console.WriteLine("done.");
            return ShaderProgram.MakeProgram(
                Shader.MakeVertexShaderStr(vertexShaderStr),
                Shader.MakeFragmentShaderStr(fragmentShaderStr)
            );
        }

        private static List<ShaderSnippet> AssembleVertexSnippets(ShaderSnippet sceneVertexShader,
            List<OptionalDataBuffer> buffers, List<Light> lights)
        {
            List<ShaderSnippet> vertexSnippets = new List<ShaderSnippet>();
            //vertexSnippets.reserve(1 + buffers.Count + lights.size());
            vertexSnippets.Add(sceneVertexShader);

            foreach (var buffer in buffers)
                vertexSnippets.Add(buffer.GetVertexShaderGLSL());

            if (lights.Count > 0) //only need one instance of light code
                vertexSnippets.Add(lights[0].GetVertexShaderGLSL());

            return vertexSnippets;
        }

        private static string AssembleVertexShaderStr(List<OptionalDataBuffer> buffers,
            ShaderSnippet sceneVertexShader, List<Light> lights)
        {
            var vertexSnippets = AssembleVertexSnippets(sceneVertexShader, buffers, lights);
            return BuildShader(
                AssembleFields(vertexSnippets),
                AssembleMethods(vertexSnippets),
                AssembleMainBodyCode(vertexSnippets)
            );
        }

        private static List<ShaderSnippet> AssembleFragmentSnippets(ShaderSnippet sceneFragmentShader,
            List<OptionalDataBuffer> buffers, List<Light> lights)
        {
            List<ShaderSnippet> fragmentSnippets = new List<ShaderSnippet>();
            //fragmentSnippets.reserve(1 + buffers.Count + lights.size());
            fragmentSnippets.Add(sceneFragmentShader);

            foreach (var buffer in buffers)
                fragmentSnippets.Add(buffer.GetFragmentShaderGLSL());

            if (lights.Count > 0) //only need one instance of light code
                fragmentSnippets.Add(lights[0].GetFragmentShaderGLSL());

            return fragmentSnippets;
        }

        private static string AssembleFragmentShaderStr(List<OptionalDataBuffer> buffers,
            ShaderSnippet sceneFragmentShader, List<Light> lights)
        {
            var fragmentSnippets = AssembleFragmentSnippets(sceneFragmentShader, buffers, lights);

            return BuildShader(
                AssembleFields(fragmentSnippets),
                AssembleMethods(fragmentSnippets),
                AssembleMainBodyCode(fragmentSnippets) + @"
                    //final fragment shader main body code from ShaderManager
                    if (colors.material == vec3(-1))
                        colors.material = vec3(1);

                    if (colors.lightBlend == vec3(-1))
                        colors.lightBlend = vec3(1);

                    vec3 lighting = ambientLight * colors.lightBlend;
                    vec3 color = colors.material * lighting;
                    gl_FragColor = vec4(color, 1);
                "
            );
        }

        private static string AssembleFields(List<ShaderSnippet> snippets)
        {
            string stream = "";
            foreach (var snippet in snippets) 
                stream += snippet.GetFields();

            return stream;
        }
        private static string AssembleMethods(List<ShaderSnippet> snippets)
        {
            string stream = "";
            foreach (var snippet in snippets)
                stream += snippet.GetMethods();
            return stream;
        }
        private static string AssembleMainBodyCode(List<ShaderSnippet> snippets)
        {
            string stream = "";
            foreach (var snippet in snippets)
                stream += snippet.GetMainBodyCode();
            return stream;
        }
        private static string BuildShader(string fields, string methods, string mainBodyCode)
        {
            return @"
                    #version 120
                "
                + fields
                + "\n"
                + methods
                + @"
                    void main()
                    {
                "
                + mainBodyCode
                + @"
                    }
                ";
        }
    }
}
