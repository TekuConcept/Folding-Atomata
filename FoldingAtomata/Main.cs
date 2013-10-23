using FoldingAtomata.NViewer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XNA;
using GL0 = OpenTK.Graphics.ES10.GL;
using GL1 = OpenTK.Graphics.ES11.GL;
using GL2 = OpenTK.Graphics.ES20.GL;

namespace FoldingAtomata
{
    public class Main
    {
        static bool _readyToUpdate;
        const int MAX_FPS = 60;

        static void main(string[] args)
        {
            // initialize Glut
            int len = args.Length;
            //Glut.glutInit(ref len,  args.ToStringBuilderArray());

            // pass arguments to the Options object to be parsed
            if (!Options.HandleFlags(args.Length, args))
                return;

            // create a new window with title and default Glut bounds
            //string str = "Folding Atomata - third-party Folding@home simulation viewer";
            //InitializeGlutWindow(
                //Glut.glutGet(Glut.GLUT_SCREEN_WIDTH),
                //Glut.glutGet(Glut.GLUT_SCREEN_HEIGHT),
            //    ref str
            //);

            try
            {
                CreateGlContext();          // create an OpenGL context
                AssertSystemRequirements(); // check system requirements
                AssignCallbacks();          // set up key and mouse listeners
                //Glut.glutIgnoreKeyRepeat(1);

                Console.WriteLine("Finished Glut and window initialization.");

                // if HighVerbosity is true, do not write anything to the console
                if (!Options.GetInstance().HighVerbosity)
                {
                    //std.ofstream nullOut = ("/dev/null");
                    //std.cout.rdbuf(nullOut.rdbuf());
                    Console.SetOut(System.IO.TextWriter.Null);
                }

                // create to looping threads
                Thread updater = new Thread(UpdateThread);
                Thread animater = new Thread(AnimateThread);
                updater.Start();
                animater.Start();

                Console.WriteLine("Threads launched. FPS cap set at {0}", MAX_FPS);
                //Glut.glutMainLoop();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nCaught {0} during glut setup: {1}", e.Message, e.InnerException);
                //Glut.glutDestroyWindow(Glut.glutGetWindow());
                return;
            }

            return;
        }

        #region - main -
        static void AnimateThread()
        {
            int ANIMATE_DELAY = Options.GetInstance().AnimationDelay;
            try
            {
                while (!_readyToUpdate)
                    Thread.Sleep(15);

                while (true)
                {
                    Viewer.Instance.Animate(ANIMATE_DELAY);
                    Thread.Sleep(ANIMATE_DELAY);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during render: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void UpdateThread()
        {
            int UPDATE_DELAY = 20; //17 is ~60, 20 is 50 FPS, 25 is 40 FPS
            try
            {
                while (!_readyToUpdate)
                    Thread.Sleep(UPDATE_DELAY);

                while (true)
                {
                    Viewer.Instance.Update(UPDATE_DELAY);
                    Thread.Sleep(UPDATE_DELAY);
                }
            }
            catch (Exception  e)
            {
                Console.WriteLine("Caught {0} during update: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void RenderCallback()
        {
            try
            {
                int startTime = Glut.glutGet(Glut.GLUT_ELAPSED_TIME);
                Viewer.Instance.Render();
                _readyToUpdate = true;
                int endTime = Glut.glutGet(Glut.GLUT_ELAPSED_TIME);

                //reduce extremely fast polling
                int delay = (int)(1000.0f / MAX_FPS - (endTime - startTime)) - 1;
                if (delay > 0)
                    Thread.Sleep(delay);

                Glut.glutPostRedisplay();
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during render: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void WindowReshapeCallback(int width, int height)
        {
            try
            {
                Viewer.Instance.HandleWindowReshape(width, height);
                GL2.Viewport(0, 0, width, height); //this is a subtle but critical call!
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} in window callback: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void KeyPressCallback(byte key, int x, int y)
        {
            try
            {
                //Viewer.Instance.User.OnKeyPress((Keys)key);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during key press: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void KeyReleaseCallback(byte key, int x, int y)
        {
            try
            {
                //Viewer.Instance.User.OnKeyRelease((Keys)key);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during key release: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void SpecialKeyPressCallback(int key, int x, int y)
        {
            try
            {
                //Viewer.Instance.User.OnSpecialKeyPress((Keys)key);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during special key press: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void SpecialKeyReleaseCallback(int key, int x, int y)
        {
            try
            {
                //Viewer.Instance.User.OnSpecialKeyRelease((Keys)key);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during special key release: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void MouseClickCallback(int button, int state, int x, int y)
        {
            try
            {
                //Viewer.Instance.User.OnMouseClick(GetButton(button), GetState(state), x, y);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during mouse click: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void MouseMotionCallback(int x, int y)
        {
            try
            {
                Viewer.Instance.User.OnMouseMotion(x, y);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during mouse motion: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void MouseDragCallback(int x, int y)
        {
            try
            {
                Viewer.Instance.User.OnMouseDrag(x, y);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught {0} during mouse drag: {1}", e.Message, e.InnerException);
                Glut.glutDestroyWindow(Glut.glutGetWindow());
            }
        }
        static void InitializeGlutWindow(int width, int height, ref string windowTitle)
        {
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow(windowTitle);

            Console.WriteLine("window is {0} by {1}, a ratio of {2}",  width, height, (width / (float)height));
        }
        static void CreateGlContext()
        {
            /*
            int glew_status = glewInit();
            if (glew_status == GLEW_OK) Console.WriteLine("Successfully created GL context.");
            else
            {
                string errorStream = "";
                errorStream += "glewInit() error. Failed to create GL context.\n";
                errorStream += glewGetErrorString(glew_status);
                throw new Exception(errorStream);
            }
            */

            Console.WriteLine("<GL context>");
            Console.WriteLine("Vendor:   {0}", GL2.GetString(OpenTK.Graphics.ES20.StringName.Vendor));
            Console.WriteLine("Renderer: {0}", GL2.GetString(OpenTK.Graphics.ES20.StringName.Renderer));
            Console.WriteLine("Version:  {0}", GL2.GetString(OpenTK.Graphics.ES20.StringName.Version));
            Console.WriteLine("GLSL:     {0}", GL2.GetString(OpenTK.Graphics.ES20.StringName.ShadingLanguageVersion));
            Console.WriteLine("</GL context>");
        }
        static void AssertSystemRequirements()
        {
            string stream = "";
            stream += GL2.GetString(OpenTK.Graphics.ES20.StringName.ShadingLanguageVersion);

            float version;
            version = float.Parse(stream);

            const float MIN_GLSL = 1.20f;

            if (version < MIN_GLSL)
                throw new Exception("Your driver/GPU does not support OpenGL 2.1");
            else
                Console.WriteLine("GLSL v{0} required, have {1}, so passed system requirements.", MIN_GLSL, version);
        }
        static void AssignCallbacks()
        {
            Glut.glutDisplayFunc(RenderCallback);           // refresh
            Glut.glutReshapeFunc(WindowReshapeCallback);    // resize redraw

            Glut.KeyboardCallback pressCall = new Glut.KeyboardCallback(KeyPressCallback);
            Glut.KeyboardUpCallback releaseCall = new Glut.KeyboardUpCallback(KeyReleaseCallback);
            Glut.glutKeyboardFunc(pressCall);
            Glut.glutKeyboardUpFunc(releaseCall);
            Glut.glutSpecialFunc(SpecialKeyPressCallback);
            Glut.glutSpecialUpFunc(SpecialKeyReleaseCallback);

            Glut.glutMouseFunc(MouseClickCallback);
            Glut.glutMotionFunc(MouseDragCallback);
            Glut.glutPassiveMotionFunc(MouseMotionCallback);
        }

        // <!-- Warning -->
        static OpenTK.Input.MouseState GetState(int state)
        {
            return new OpenTK.Input.MouseState();
        }
        static OpenTK.Input.MouseButton GetButton(int button)
        {
            return OpenTK.Input.MouseButton.LastButton;
        }
        #endregion

    }
}
