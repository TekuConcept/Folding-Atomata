using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.World
{
    public class Light
    {
        static int nLights_;

        public Light()
        {
            Position = Vector3.Zero;
            Color = Vector3.One;
            Power = 0.5F;
            nLights_++;
        }
        public Light(ref Vector3 position, ref Vector3 color, float power)
        {
            Position = position;
            Color = color;
            Power = power;
            nLights_++;
        }
        public void Sync(int handle, int lightID)
        {
            var lightRef = String.Format("lights[{0}]", lightID);
            
            int posLoc = GL.GetUniformLocation(handle, (lightRef + ".position").ToString());
            GL.Uniform3(posLoc, 1, Utils.XNA_Float_Vector(Position));

            int colorLoc = GL.GetUniformLocation(handle, (lightRef + ".color").ToString());
            GL.Uniform3(colorLoc, 1, Utils.XNA_Float_Vector(Color));

            int powerLoc = GL.GetUniformLocation(handle, (lightRef + ".power").ToString());
            float power = IsEmitting ? Power : 0;
            GL.Uniform1(powerLoc, power);

            if (posLoc < 0 || colorLoc < 0 || powerLoc < 0)
                throw new Exception("Unable to find Light uniform variables!");
        }
        public virtual ShaderSnippet GetVertexShaderGLSL() // string[] = shadersnipet
        {
            return new ShaderSnippet(
                @"
                    //Light fields
                    varying vec3 fragmentPosition;
                ",
                @"
                    //Light methods
                ",
                @"
                    //Light main method code
                    fragmentPosition = (modelMatrix * vec4(vertex, 1)).xyz;
                "
            );
        }
        public virtual ShaderSnippet GetFragmentShaderGLSL() // string[] = shadersnipet
        {
            //http://www.opengl.org/discussion_boards/showthread.php/164100-GLSL-multiple-lights
            //http://en.wikibooks.org/wiki/GLSL_Programming/GLUT/Multiple_Lights
            //http://www.geeks3d.com/20091013/shader-library-phong-shader-with-multiple-lights-glsl/
            //http://Viewerdev.stackexchange.com/questions/53822/variable-number-of-lights-in-a-glsl-shader
            //http://stackoverflow.com/questions/8202173/setting-the-values-of-a-struct-array-from-js-to-glsl

            string fieldStrStream = String.Format(@"
                    //Light fields
                    // http://stackoverflow.com/questions/8202173/setting-the-values-of-a-struct-array-from-js-to-glsl

                    struct Light
                    {
                        vec3 position, color;
                        float power; //its maximum distance of influence
                    };

                    uniform Light lights[{0}];
                    varying vec3 fragmentPosition;", nLights_);

            return new ShaderSnippet(
                fieldStrStream,
                @"
                    //Light methods
                ",
                @"
                    //Light main method code
                    colors.lightBlend = vec3(0); //see Scene::getFragmentShaderGLSL()

                    for (int j = 0; j < lights.length(); j++)
                    {
                        float distance = length(fragmentPosition - lights[j].position);
                        float scaledDistance = distance / lights[j].power;
                        vec3 luminosity = lights[j].color * (1 - scaledDistance);
                        colors.lightBlend += clamp(luminosity, 0, 1);
                    }

                    //colors.lightBlend = normalize(colors.lightBlend);
                 "
            );
        }

        public Vector3 Position { get; set; }
        public Vector3 Color {get; set; }
        public float Power { get; set; }
        public bool IsEmitting { get; set; }
    }
}
