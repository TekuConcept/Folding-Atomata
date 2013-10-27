using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace FoldingXNA
{
    public class BasicEffect
    {
        internal int vertID, fragID, sceneID,
            projID, viewID, colorID;
        internal Dictionary<String,EffectInfo> _info = new Dictionary<String,EffectInfo>();
        internal Matrix4 _proj, _view;
        internal Color4 _color;

        public struct Instance
        {
            public List<Vector3> KeyFrames;

            public Instance(Vector3 instance)
            {
                KeyFrames = new List<Vector3>();
                KeyFrames.Add(instance);
            }
        }
        internal struct EffectInfo
        {
            public int VertID;
            public int NormID;
            public int IndcID;
            public int Count;
            public BeginMode Mode;
            public Matrix4 World;
            public List<Instance> Instances;
            public String Name;

            public EffectInfo(int v, int n, int i, int c, BeginMode m, String s) 
                : this(v, n, i, c, m, s, Matrix4.Identity)
            {
            }
            public EffectInfo(int v, int n, int i, int c, BeginMode m, String s, Matrix4 w)
            {
                VertID = v;
                NormID = n;
                IndcID = i;
                Count = c;
                Mode = m;
                World = w;
                Name = s;
                Instances = new List<Instance>();
            }
        }

        public BasicEffect()
        {
            vertID = Renderer.CreateVertexShader(VertexShader);
            fragID = Renderer.CreateFragmentShader(FragmentShader);
            sceneID = Renderer.CreateScene(vertID, fragID);

            SetScene();

            projID = Renderer.GetShaderProperty(sceneID, VertexProjection);
            viewID = Renderer.GetShaderProperty(sceneID, VertexModelView);
            colorID = Renderer.GetShaderProperty(sceneID, VertexColor);
        }

        #region - Scene Mesh -
        public void AddMeshToScene(Vector3[] vertices, uint[] indices, BeginMode mode, String name = "mesh")
        {
            AddMeshToScene(vertices, vertices, indices, mode, Matrix4.Identity, name);
        }
        public void AddMeshToScene(Vector3[] vertices, uint[] indices, BeginMode mode, Matrix4 world, String name = "mesh")
        {
            AddMeshToScene(vertices, vertices, indices, mode, world, name);
        }
        public void AddMeshToScene(Vector3[] vertices, Vector3[] normals, uint[] indices, BeginMode mode, String name = "mesh")
        {
            AddMeshToScene(vertices, normals, indices, mode, Matrix4.Identity, name);
        }
        public void AddMeshToScene(Vector3[] vertices, Vector3[] normals, uint[] indices, BeginMode mode, Matrix4 world, String name = "mesh")
        {
            AddMeshToScene(vertices, normals, indices, mode, world, new Instance(Vector3.Zero), name);
        }
        public void AddMeshToScene(Vector3[] vertices, Vector3[] normals, uint[] indices, BeginMode mode, Matrix4 world, Instance inst, String name = "mesh")
        {
            EffectInfo info = new EffectInfo();
            info.VertID = Renderer.LoadBuffer(vertices, sceneID, VertexPosition, 0);
            info.NormID = Renderer.LoadBuffer(normals, sceneID, VertexNormal, 1);
            info.IndcID = Renderer.LoadBuffer(indices);
            info.Count = indices.Length;
            info.Mode = mode;
            info.World = world;
            info.Name = GetNewName(name);
            info.Instances = new List<Instance>();
            info.Instances.Add(inst);
            _info.Add(info.Name, info);
        }
        public void AddMeshToScene(Vector3[] vertices, Vector3[] normals, uint[] indices, BeginMode mode, Matrix4 world, Instance[] inst, String name = "mesh")
        {
            EffectInfo info = new EffectInfo();
            info.VertID = Renderer.LoadBuffer(vertices, sceneID, VertexPosition, 0);
            info.NormID = Renderer.LoadBuffer(normals, sceneID, VertexNormal, 1);
            info.IndcID = Renderer.LoadBuffer(indices);
            info.Count = indices.Length;
            info.Mode = mode;
            info.World = world;
            info.Name = GetNewName(name);
            info.Instances = new List<Instance>(inst);
            _info.Add(info.Name, info);
        }
        private string GetNewName(string name)
        {
            String n = name;
            int count = 0;
            while (_info.ContainsKey(n))
            {
                count++;
                n = String.Format("{0}_{1}", name, count);
            }
            return n;
        }

        public void ApplyMeshMatrix(Matrix4 world, string meshID, int instance = 0)
        {
            SafeMesh(meshID);

            var inf = _info[meshID];
            inf.World *= world;
            _info[meshID] = inf;
        }
        public void TranslateMesh(Vector3 translation, string meshID, int instance = 0)
        {
            TranslateMesh(translation.X, translation.Y, translation.Z, meshID, instance);
        }
        public void TranslateMesh(float x, float y, float z, string meshID, int instance = 0)
        {
            SafeMesh(meshID);

            var inf = _info[meshID];
            var world = Matrix4.CreateTranslation(x, y, z);
            inf.World *= world;
            _info[meshID] = inf;
        }
        public void RotateMesh(Vector3 rotation, string meshID, int instance)
        {
            RotateMesh(rotation.X, rotation.Y, rotation.Z, meshID, instance);
        }
        public void RotateMesh(float x, float y, float z, string meshID, int instance)
        {
            SafeMesh(meshID);

            var inf = _info[meshID];
            var world = 
                Matrix4.CreateRotationX(x) * 
                Matrix4.CreateRotationY(y) * 
                Matrix4.CreateRotationZ(z);
            inf.World *= world;
            _info[meshID] = inf;
        }
        public void ScaleMesh(Vector3 scale, string meshID, int instance)
        {
            ScaleMesh(scale.X, scale.Y, scale.Z, meshID, instance);
        }
        public void ScaleMesh(float x, float y, float z, string meshID, int instance)
        {
            SafeMesh(meshID);

            var inf = _info[meshID];
            var world = Matrix4.Scale(x, y, z);
            inf.World *= world;
            _info[meshID] = inf;
        }
        private void SafeMesh(string name)
        {
            if (!_info.ContainsKey(name))
                throw new ArgumentException("Mesh doesn't exist by name: " + name);
        }
        #endregion

        public void SetScene()
        {
            Renderer.SetScene(sceneID);
        }
        public void Draw()
        {
            SetScene();

            foreach(var v in _info)
            {
                for (int i = 0; i < v.Value.Instances.Count; i++)
                {
                    var m = v.Value.World * Matrix4.CreateTranslation(v.Value.Instances[i].KeyFrames[0]) * _view;
                    Renderer.ApplyMatrix(m, viewID);
                    Renderer.Draw(v.Value.Mode, DrawElementsType.UnsignedInt, v.Value.Count, v.Value.VertID, v.Value.IndcID);
                }
            }
        }
        public void Draw(string name)
        {
            var v = _info[name];
            for (int i = 0; i < v.Instances.Count; i++)
            {
                var m = v.World * Matrix4.CreateTranslation(v.Instances[i].KeyFrames[0]) * _view;
                Renderer.ApplyMatrix(m, viewID);
                Renderer.Draw(v.Mode, DrawElementsType.UnsignedInt, v.Count, v.VertID, v.IndcID);
            }
        }

        public Matrix4 Projection
        {
            get
            {
                return _proj;
            }
            set
            {
                _proj = value;
                Renderer.ApplyMatrix(_proj, projID);
            }
        }
        public Matrix4 View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;
                Renderer.ApplyMatrix(_view, viewID);
            }
        }
        public Color4 Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                Renderer.ApplyColor(_color, colorID);
            }
        }

        static string FragmentShader
        {
            get
            {
                return @"
                    #version 140
 
                    precision highp float;
 
                    // ambient lighting
                    const vec3 ambient = vec3( 0.1, 0.1, 0.1 );
                    // light location
                    const vec3 lightVecNormalized = normalize( vec3( 0.5, 0.5, 2 ) );
                    // color
                    //const vec3 lightColor = vec3( 1.0, 0.8, 1 );
 
                    in vec3 lightColor;
                    in vec3 normal;
 
                    out vec4 out_frag_color;
 
                    void main(void)
                    {
                      float diffuse = clamp( dot( lightVecNormalized, normalize( normal ) ), 0.0, 1.0 );
                      out_frag_color = vec4( ambient + diffuse * lightColor, 1.0 );
                    }";
            }
        }
        static string VertexShader
        {
            get
            {
                return @"
                    #version 140
 
                    // object space to camera space transformation
                    uniform mat4 modelview_matrix;            
 
                    // camera space to clip coordinates
                    uniform mat4 projection_matrix;
 
                    uniform vec3 light_Color;
 
                    // incoming vertex position
                    in vec3 vertex_position;
 
                    // incoming vertex normal
                    in vec3 vertex_normal;
 
                    // transformed vertex normal
                    out vec3 normal;
                    out vec3 lightColor;
 
                    void main(void)
                    {
                      //not a proper transformation if modelview_matrix involves non-uniform scaling
                      normal = ( modelview_matrix * vec4( vertex_normal, 0 ) ).xyz;

                      lightColor = light_Color;
 
                      // transforming the incoming vertex position
                      gl_Position = projection_matrix * modelview_matrix * vec4( vertex_position, 1 );
                    }";
            }
        }
        static string VertexModelView
        {
            get
            {
                return "modelview_matrix";
            }
        }
        static string VertexProjection
        {
            get
            {
                return "projection_matrix";
            }
        }
        static string VertexPosition
        {
            get
            {
                return "vertex_position";
            }
        }
        static string VertexNormal
        {
            get
            {
                return "vertex_normal";
            }
        }
        static string VertexColor
        {
            get
            {
                return "light_Color";
            }
        }
    }
}
