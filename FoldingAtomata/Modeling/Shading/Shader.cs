using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XNA;

namespace FoldingAtomata.Modeling.Shading
{
    struct VertexShaderTag { }
    struct FragmentShaderTag { }

    struct NShaderType
    {
        public ShaderType value;
        NShaderType(Object T){
            if (T is VertexShaderTag)
                value = ShaderType.VertexShader;
            else if (T is FragmentShaderTag)
                value = ShaderType.FragmentShader;
            else throw new ArgumentException("Invalid parameter: " + T.GetType().Name);
        }
    }

    public class Shader
    {
        public Shader(ShaderType type)
        {
            handle = GL.CreateShader(type);
        }
        ~Shader()
        {
            GL.DeleteShader(handle);
        }
        public int GetHandle()
        {
            return handle;
        }

        int handle;

        // <!-- Warning -->
        public static Shader MakeVertexShader(string path)
        {
            return Make_Shader(path, ShaderType.VertexShader);
        }
        public static Shader MakeVertexShaderStr(string shaderCode)
        {
            return Make_ShaderStr(shaderCode, ShaderType.VertexShader);
        }

        public static Shader MakeFragmentShader(string path)
        {
            return Make_Shader(path, ShaderType.FragmentShader);
        }
        public static Shader MakeFragmentShaderStr(string shaderCode)
        {
            return Make_ShaderStr(shaderCode, ShaderType.FragmentShader);
        }

        static string GetCode(string path)
        {
            return File.ReadAllText(path);
        }
        static void SetCode(int handle, string code)
        {
            int GL_FALSE = 0;
            int NULL = 0;
            Console.Write("Compiling GLSL shader... ");

            GL.ShaderSource(handle, code);
            GL.CompileShader(handle);

            int compile_ok = 0;
            GL.GetShader(handle, ShaderParameter.CompileStatus, out compile_ok);

            if (compile_ok == GL_FALSE)
            {
                StringBuilder buf = new StringBuilder(8192);
                GL.GetShaderInfoLog(handle, buf.Capacity, out NULL, buf);
                Console.WriteLine();

                Console.WriteLine(code);
                String stream = "";
                stream = "Compilation error in GLSL shader code \n" + buf.ToString();
                throw new Exception(stream);
            }
            else
                Console.WriteLine("done.");
        }

        public static Shader Make_ShaderStr(string code, ShaderType type)
        {
            var shader = new Shader(type);
            SetCode(shader.GetHandle(), code);
            return shader;
        }
        public static Shader Make_Shader(string path, ShaderType type)
        {
            var code = GetCode(path);
            var shader = new Shader(type);
            SetCode(shader.GetHandle(), code);
            return shader;
        }
    }
}
