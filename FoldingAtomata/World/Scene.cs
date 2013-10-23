using FoldingAtomata.Modeling;
using FoldingAtomata.Modeling.Shading;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.World
{
    public class Scene
    {
        public Scene(Camera camera)
        {
            Camera = camera;
            Lights = new List<Light>();
        }
        public void AddModel(InstancedModel model)
        {
            AddModel(model, ShaderManager.CreateProgram(model,
                GetVertexShaderGLSL(), GetFragmentShaderGLSL(), Lights));
        }
        public void AddModel(InstancedModel model, ShaderProgram program)
        {
            var programHandle = program.GetHandle();
            GL.UseProgram(programHandle);

            model.SaveAs(programHandle);
            int ambLU = GL.GetUniformLocation(programHandle, "ambientLight");
            int viewU = GL.GetUniformLocation(programHandle, "viewMatrix");
            int projU = GL.GetUniformLocation(programHandle, "projMatrix");

            _renderables.Add(new Renderable(model, program, ambLU, viewU, projU));
        }
        public void AddLight(Light light)
        {
            Lights.Add(light);
            Console.Write("Successfully added a Light to the Scene.\n");
        }
        public float Render()
        {
            var start = DateTime.Now;

            Camera.StartSync();
            foreach (var renderable in _renderables)
            {
                int handle = renderable.program.GetHandle();
                GL.UseProgram(handle);
                Camera.Sync(renderable.viewUniform, renderable.projUniform);
                SyncLighting(handle, renderable.ambientLightUniform);

                renderable.model.Render(handle);
            }
            Camera.EndSync();
            DoneSyncingLighting();

            var diff = (DateTime.Now - start).Ticks;
            return diff / 1000.0f;
        }
        public void SyncLighting(int programHandle, int ambientLightUniform)
        {
            if (_ambientLightUpdated)
                GL.Uniform3(ambientLightUniform, 1, Utils.XNA_Float_Vector(AmbientLight));

            //todo: Light::sync needs to be optimized like Camera::sync
            for (int j = 0; j < Lights.Count; j++)
                Lights[j].Sync(programHandle, j);
        }
        public void DoneSyncingLighting()
        {
            _ambientLightUpdated = true;
        }
        public virtual ShaderSnippet GetVertexShaderGLSL() // string[] = SnippetPtr
        {
            return new ShaderSnippet(
                @"
                    // ********* VERTEX SHADER ********* \\

                    //Scene fields
                    attribute vec3 vertex; //position of the vertex
                    uniform mat4 viewMatrix, projMatrix; //Camera view and projection matrices
                    uniform mat4 modelMatrix; //matrix transforming model mesh into world space
                ",
                @"
                    //Scene methods
                    vec4 projectVertex()
                    {
                        mat4 MVP = projMatrix * viewMatrix * modelMatrix; //Calculate the Model-View-Projection matrix
                        return MVP * vec4(vertex, 1); // Convert from model space to clip space
                    }
                ",
                @"
                    //Scene main method code
                    gl_Position = projectVertex();
                "
            );
        }
        public virtual ShaderSnippet GetFragmentShaderGLSL() // string[] = SnippetPtr
        {
            return new ShaderSnippet(
                @"
                    // ********* FRAGMENT SHADER ********* \\

                    //Scene fields
                    uniform vec3 ambientLight;
                    uniform mat4 viewMatrix, projMatrix; //Camera view and projection matrices
                    uniform mat4 modelMatrix; //matrix transforming model mesh into world space

                    struct Colors
                    {
                        vec3 material, lightBlend;
                    };
                ",
                @"
                    //Scene methods
                ",
                @"
                    //Scene main method code
                    Colors colors;
                    colors.material = vec3(-1); //init to invalid if not needed
                    colors.lightBlend = vec3(-1);
                "
            );
        }

        public struct Renderable
        {
            public InstancedModel model;
            public ShaderProgram program;
            public int ambientLightUniform, viewUniform, projUniform;

            public Renderable(InstancedModel m, ShaderProgram prog, int a, int v, int p)
            {
                model = m;
                program = prog;
                ambientLightUniform = a;
                viewUniform = v;
                projUniform = p;
            }
        }

        List<Renderable> _renderables = new List<Renderable>();
        bool _ambientLightUpdated;
        public List<Program> Programs { get; set; }
        public Dictionary<int, InstancedModel> Models { get; set; }
        public List<Light> Lights { get; set; }
        public Camera Camera { get; set; }
        public Vector3 AmbientLight { get; set; }
    }
}
