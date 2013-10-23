using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
using XNA;

namespace FoldingAtomata.Modeling.Shading
{
    public class ShaderProgram
    {
        public ShaderProgram(Shader vertex, Shader fragment)
        {
            _handle = GL.CreateProgram();
            _vertex = vertex;
            _fragment = fragment;
        }
        ~ShaderProgram()
        {
            Console.WriteLine("Program {0} deallocating!", GetHandle());
            GL.DeleteProgram(_handle);
        }
        public int GetHandle()
        {
            return _handle;
        }

        int _handle;
        Shader _vertex;
        Shader _fragment;

        // <!-- Warning -->
        public static ShaderProgram MakeProgram(Shader vertex, Shader fragment)
        {
            var program = new ShaderProgram(vertex, fragment);
            var programHandle = program.GetHandle();

            GL.AttachShader(programHandle, vertex.GetHandle());
            GL.AttachShader(programHandle, fragment.GetHandle());
            GL.LinkProgram (programHandle);

            int link_ok = 0;
            GL.GetProgram(programHandle, ProgramParameter.LinkStatus, out link_ok);

            int bufLength = 0;
            StringBuilder buf = new StringBuilder(8192);
            GL.GetProgramInfoLog(programHandle, buf.Capacity, out bufLength, buf);
            CheckGLError();
            
            if (link_ok != 0)
            {
                Console.Write("Attached and linked shaders.");
                if (bufLength > 0)
                    Console.Write(" Program Info Log: \n\n{0}", buf.ToString());
                Console.WriteLine();
            }
            else
            {
                string stream = "";
                stream = String.Format("Could not link shader program \n{0}", buf.ToString());
                throw new Exception(stream);
            }

            return program;
        }
        public static void CheckGLError()
        {
            Console.Write("GL status: ");

            var error = GL.GetError();
            switch (error)
            {
                case ErrorCode.NoError:
                    Console.WriteLine("GL_NO_ERROR");
                    break;

                default:
                    Console.WriteLine(error);

                    String stream = "";
                    stream = String.Format("GL ERROR: 0x{1}", error);
                    throw new Exception(stream);
            }
        }
    }
}
