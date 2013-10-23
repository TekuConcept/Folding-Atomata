using FoldingAtomata.Modeling;
using FoldingAtomata.Modeling.DataBuffers;
using FoldingAtomata.Modeling.DataBuffers.SampledBuffers;
using FoldingAtomata.Modeling.NMesh;
using FoldingAtomata.NTrajectory;
using FoldingAtomata.PyON;
using FoldingAtomata.Sockets;
using FoldingAtomata.World;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using XNA;

namespace FoldingAtomata.NViewer
{
    public class Viewer
    {
        public Viewer()
        {
            _scene = new Scene(CreateCamera());
            User = new User(_scene);
            _timeSpentRendering = 0;
            _frameCount = 0;
            _needsRerendering = true;

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            AddModels();
            User.GrabPointer(0, 0);
            ReportFPS();
        }
        public void Update(int deltaTime)
        {
            User.Update(deltaTime);
        }
        public void Animate(int deltaTime)
        {
            bool animationHappened = false;
            foreach (var viewer in _slotViewers)
                if (viewer.Animate(deltaTime)) //test if animation happened
                    animationHappened = true;

            if (animationHappened)
                _needsRerendering = true; //the atoms moved, so redraw the scene
        }
        public void Render()
        {
            if (!_needsRerendering && !User.IsMoving)
                return;
            _needsRerendering = false; //it was true, so reset it and then render

            GL.ClearColor(OpenTK.Graphics.Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            _timeSpentRendering += _scene.Render();
            _frameCount++;

            GlutManager.SwapBuffers(0);
        }
        public void HandleWindowReshape(int width, int height)
        {
            _scene.Camera.AspectRatio = (width / (float)height);
            User.SetWindowOffset(GlutManager.GetX(0), GlutManager.GetY(0));
            _needsRerendering = true; //need to redraw after window update

            Console.Write("Windows updated to {0} by {1}, a ratio of {2}\n", width, height, (width / (float)height));
        }


        private void ReportFPS()
        {
            System.Threading.Thread fpsReporter = new System.Threading.Thread(
                () => 
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);

                        float msPerFrame = _timeSpentRendering / _frameCount;
                        Console.Write("{0} FPS, spent {1} ms rendering, avg {2} ms/frame\n", 
                            _frameCount, _timeSpentRendering, msPerFrame);
                        _frameCount = 0;
                        _timeSpentRendering = 0;
                    }
                });

            fpsReporter.Start();
        }
        private void AddModels()
        {
            AddSlotViewers();

            //if (!Options.GetInstance().SkyboxDisabled) AddSkybox();
        }
        private void AddSkybox()
        {
            Console.Write("Creating skybox...\n");

            var positiveX = Image.FromFile(Options.GetInstance().PathToImageA);//, false, true);
            var negativeX = Image.FromFile(Options.GetInstance().PathToImageA);//, true, false);
            var positiveY = Image.FromFile(Options.GetInstance().PathToImageB);//, false, true);
            var negativeY = Image.FromFile(Options.GetInstance().PathToImageB);//, true, false);
            var positiveZ = Image.FromFile(Options.GetInstance().PathToImageC);//, false, true);
            var negativeZ = Image.FromFile(Options.GetInstance().PathToImageC);//, true, false);

            List<OptionalDataBuffer> list = new List<OptionalDataBuffer>(){new TexturedCube(positiveX, negativeX, positiveY, negativeY, positiveZ, negativeZ)};
            var matrix = Matrix.Scaling(new Vector3(4096));
            var model = new InstancedModel(SkyboxMesh, matrix, list);
            _scene.AddModel(model); //add to Scene and save

            Console.Write("... done creating skybox.\n");
        }
        private void AddSlotViewers()
        {
            List<Trajectory> trajectories = GetTrajectories();

            var slot0Viewer = new SlotViewer(trajectories[0], _scene);
            _slotViewers.Add(slot0Viewer);
        }
        private List<Trajectory> GetTrajectories()
        {
            List<Trajectory> trajectories = new List<Trajectory>();

            try
            {
                var socket = new ClientSocket(
                    Options.GetInstance().Host,
                    Options.GetInstance().Port
                );

                FAHClientIO io = new FAHClientIO(socket);
                trajectories = io.GetTrajectories();

                if (trajectories.Count == 0)
                    Console.Write("Not enough slots to work with. Using demo protein.\n");
            }
            catch
            {
                Console.Write("Error connection to FAHClient (SocketException). Using demo protein.\n");
            }

            if (trajectories.Count == 0)
            {
                //const string FILENAME = "/usr/share/FoldingAtomata/demoProtein";
                //String proteinStr = File.ReadAllText(FILENAME);
                String proteinStr = global::FoldingAtomata.Resources.demoProtein;

                TrajectoryParser parser = new TrajectoryParser(proteinStr, false);
                trajectories.Add(parser.Parse());
            }

            return trajectories;
        }
        private Camera CreateCamera()
        {
            var camera = new Camera();
            camera.Position = new Vector3(0, -50, 0);
            
            camera.LookAt(
                new Vector3(0, 0, 0),
                new Vector3(0, 0, 1)
            );

            return camera;
        }

        Scene _scene;
        float _timeSpentRendering;
        int _frameCount;
        bool _needsRerendering;
        static Mesh _mesh = null;
        static Viewer _singleton = null;
        List<SlotViewer> _slotViewers = new List<SlotViewer>();
        private Mesh SkyboxMesh
        {
            get
            {
                if (_mesh != null) return _mesh;

                List<Vector3> VERTICES = new List<Vector3>(){
                    new Vector3(-1, -1, -1),
                    new Vector3(-1, -1,  1),
                    new Vector3(-1,  1, -1),
                    new Vector3(-1,  1,  1),
                    new Vector3( 1, -1, -1),
                    new Vector3( 1, -1,  1),
                    new Vector3( 1,  1, -1),
                    new Vector3( 1,  1,  1)
                };

                //visible from the inside only, so faces in
                List<int> INDICES = new List<int>(){
                    0, 1, 5, 4, //front
                    6, 7, 3, 2, //back
                    2, 0, 4, 6,  //top
                    7, 5, 1, 3, //bottom
                    2, 3, 1, 0, //left
                    4, 5, 7, 6  //right
                };

                var vBuffer = new VertexBuffer(VERTICES);
                var iBuffer = new IndexBuffer(INDICES, BeginMode.TriangleStrip); // quads
                _mesh = new Mesh(vBuffer, iBuffer, BeginMode.TriangleStrip); // quads
                return _mesh;
            }
        }
        public static Viewer Instance
        {
            get
            {
                try
                {
                    if (_singleton != null)
                        return _singleton;

                    Console.Write("Creating Viewer...\n");
                    _singleton = new Viewer();
                    Console.Write("... done creating Viewer.\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.Write("Caught exception during Viewer initiation: {1}; Viewer.Instance\n", e);
                }

                return _singleton;
            }
        }
        public User User
        {
            get;
            private set;
        }
    }
}
