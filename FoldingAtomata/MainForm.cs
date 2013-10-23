using FoldingAtomata.NViewer;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoldingAtomata
{
    public partial class MainForm : Form
    {
        bool
            loaded = false,
            mouseFlag = false,
            _readyToUpdate = true;
        Timer animation = new Timer();
        Timer updater = new Timer();
        Timer mainLoop = new Timer();
        //MouseHookListener m_mouseListener;

        public MainForm()
        {
            InitializeComponent();

            animation.Interval = 15;
            updater.Interval = 20;
            mainLoop.Interval = 25;

            mainLoop.Tick += mainLoop_Tick;

            //AppHooker hooker = new AppHooker();
            //m_mouseListener = new MouseHookListener(hooker);
            //m_mouseListener.Enabled = true;
        }
        void mainLoop_Tick(object sender, EventArgs e)
        {
            glControl.Invalidate();
        }

        private void glControl_Load(object sender, EventArgs e)
        {
            CreateGLContext();
            AssignCallbacks();

            if (!Options.GetInstance().HighVerbosity) Console.SetOut(System.IO.TextWriter.Null);

            animation.Interval = Options.GetInstance().AnimationDelay;

            animation.Tick += AnimateThread;
            updater.Tick += UpdateThread;
            animation.Start();
            updater.Start();

            Console.WriteLine("Threads launched. FPS cap set at {0}", 60);
            mainLoop.Start();
        }
        private void glControl_Resize(object sender, EventArgs e)
        {
            if (!loaded) return;

            WindowReshapeCallback();
        }
        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            if (!loaded) return;

            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //glControl.SwapBuffers();

            RenderCallback();
        }

        void AssignCallbacks()
        {
            glControl.KeyDown += KeyPressCallback;
            glControl.KeyUp += KeyReleaseCallback;

            glControl.KeyDown += SpecialKeyPressCallback;
            glControl.KeyUp += SpecialKeyReleaseCallback;

            glControl.MouseClick += MouseClickCallback;
            glControl.MouseMove += MouseDragCallback;
            glControl.MouseMove += MouseMotionCallback;
            //m_mouseListener.MouseClick += MouseClickCallback;
            //m_mouseListener.MouseMove += MouseMotionCallback;
            //m_mouseListener.MouseMove += MouseDragCallback;

            // hack
            glControl.MouseDown += glControl_MouseDown;
            glControl.MouseUp += glControl_MouseUp;
            //m_mouseListener.MouseDown += glControl_MouseDown;
            //m_mouseListener.MouseUp += glControl_MouseUp;
        }

        void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseFlag = true;
        }
        void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            mouseFlag = false;
        }
        void MouseClickCallback(object sender, MouseEventArgs e)
        {
            try
            {
                OpenTK.Input.MouseButton mbtn;
                OpenTK.Input.MouseState msta;
                // hack
                GetMouse(ref e, out mbtn, out msta);

                var p = PointToScreen(e.Location);
                Viewer.Instance.User.OnMouseClick(mbtn, msta, e.X, e.Y, p.X-e.X, p.Y-e.Y);
                //Console.WriteLine("X: {0} Y: {0}", e.X, e.Y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during mouse click: {1}", ex.Message, ex.InnerException);
            }
        }
        void MouseMotionCallback(object sender, MouseEventArgs e)
        {
            if (mouseFlag) return;
            try
            {
                var p = PointToScreen(e.Location);
                Viewer.Instance.User.OnMouseMotion(e.X, e.Y, p.X-e.X, p.Y-e.Y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during mouse motion: {1}", ex.Message, ex.InnerException);
            }
        }
        void MouseDragCallback(object sender, MouseEventArgs e)
        {
            if (!mouseFlag) return;
            try
            {
                Viewer.Instance.User.OnMouseDrag(e.X, e.Y);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during mouse drag: {1}", ex.Message, ex.InnerException);
            }
        }
        void SpecialKeyPressCallback(object sender, KeyEventArgs e)
        {
            try
            {
                Viewer.Instance.User.OnSpecialKeyPress(e.KeyCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during special key press: {1}", ex.Message, ex.InnerException);
            }
        }
        void SpecialKeyReleaseCallback(object sender, KeyEventArgs e)
        {
            try
            {
                Viewer.Instance.User.OnSpecialKeyRelease(e.KeyCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during special key release: {1}", ex.Message, ex.InnerException);
            }
        }
        void KeyPressCallback(object sender, KeyEventArgs e)
        {
            try
            {
                Viewer.Instance.User.OnKeyPress(e.KeyCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during key press: {1}", ex.Message, ex.InnerException);
            }
        }
        void KeyReleaseCallback(object sender, KeyEventArgs e)
        {
            try
            {
                Viewer.Instance.User.OnKeyRelease(e.KeyCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught {0} during key release: {1}", ex.Message, ex.InnerException);
            }
        }

        void RenderCallback()
        {
            try
            {
                Viewer.Instance.Render();
                _readyToUpdate = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception during render: {1}; RenderCallback()", e.InnerException);
            }
        }
        void WindowReshapeCallback()
        {
            try
            {
                Viewer.Instance.HandleWindowReshape(glControl.Width, glControl.Height);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught expception in window callback: {1}; WindowReshapeCallback()", ex.InnerException);
            }
        }

        void CreateGLContext()
        {
            Console.WriteLine("<GL context>");
            Console.WriteLine("Vendor:   {0}", GL.GetString(StringName.Vendor));
            Console.WriteLine("Renderer: {0}", GL.GetString(StringName.Renderer));
            Console.WriteLine("Version:  {0}", GL.GetString(StringName.Version));
            Console.WriteLine("GLSL:     {0}", GL.GetString(StringName.ShadingLanguageVersion));
            Console.WriteLine("</GL context>");

            GlutManager.AddWindow(glControl.Bounds, glControl.SwapBuffers);
            loaded = true;
            GL.ClearColor(Color.AliceBlue);
        }
        void AssertSystemRequirements()
        {
            string stream = "";
            stream += GL.GetString(StringName.ShadingLanguageVersion);

            float version;
            version = float.Parse(stream);

            const float MIN_GLSL = 1.20f;

            if (version < MIN_GLSL)
                throw new Exception("Your driver/GPU does not support OpenGL 2.1");
            else
                Console.WriteLine("GLSL v{0} required, have {1}, so passed system requirements.", MIN_GLSL, version);
        }

        void GetMouse(ref MouseEventArgs e, out OpenTK.Input.MouseButton button, out OpenTK.Input.MouseState state)
        {
            switch (e.Button)
            {
                case MouseButtons.Left: button = OpenTK.Input.MouseButton.Left; break;
                case MouseButtons.Middle: button = OpenTK.Input.MouseButton.Middle; break;
                case MouseButtons.Right: button = OpenTK.Input.MouseButton.Right; break;
                case MouseButtons.Left | MouseButtons.Right:
                    button = OpenTK.Input.MouseButton.Left | OpenTK.Input.MouseButton.Right; break;
                case MouseButtons.Left | MouseButtons.Middle: 
                    button = OpenTK.Input.MouseButton.Left | OpenTK.Input.MouseButton.Middle; break;
                case MouseButtons.Right | MouseButtons.Middle: 
                    button = OpenTK.Input.MouseButton.Right | OpenTK.Input.MouseButton.Middle; break;
                case MouseButtons.Left | MouseButtons.Middle | MouseButtons.Right:
                    button = OpenTK.Input.MouseButton.Left | OpenTK.Input.MouseButton.Middle | OpenTK.Input.MouseButton.Right; break;
                default: button = OpenTK.Input.MouseButton.LastButton; break;
            }

            state = OpenTK.Input.Mouse.GetState(0);
        }

        void AnimateThread(object sender, EventArgs ex)
        {
            try
            {
                if (!_readyToUpdate) return;

                Viewer.Instance.Animate(animation.Interval);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during render: {1}", e.Message, e.InnerException);
            }
        }
        void UpdateThread(object sender, EventArgs ex)
        {
            int UPDATE_DELAY = 20; //17 is ~60, 20 is 50 FPS, 25 is 40 FPS
            try
            {
                if (!_readyToUpdate) return;

                Viewer.Instance.Update(UPDATE_DELAY);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during update: {1}", e.Message, e.InnerException);
            }
        }
    }
}
