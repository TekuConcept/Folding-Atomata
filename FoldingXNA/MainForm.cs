using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace FoldingXNA
{
    public partial class MainForm : Form
    {
        //List<Atom> map;
        //List<BondID> bonds;
        Camera cam;
        //bool capture = false, init = false;
        AtomShader effect;
        Matrix4 projectionMatrix, modelviewMatrix;
        float locX = 0F;
        float locY = 0.07F;
        float locZ = 0.02F;
        int fX=-1, fY=-1, fZ=-1;

        public MainForm()
        {
            InitializeComponent();

            GameTime.OnGameTick += gameTick;
        }


        #region - Camera -
        new float Move()
        {
                 if (KeyboardState.IsKeyDown(Keys.Up   ) || KeyboardState.IsKeyDown(Keys.W)) 
                     return 1.0F;  // Up or W - move ahead
            else if (KeyboardState.IsKeyDown(Keys.Down ) || KeyboardState.IsKeyDown(Keys.S)) 
                     return -1.0F; // Down or S - move back
            return 0.0F;
        }
        float Strafe()
        {
                 if (KeyboardState.IsKeyDown(Keys.Left ) || KeyboardState.IsKeyDown(Keys.A)) 
                     return -1.0F; // strafe left
            else if (KeyboardState.IsKeyDown(Keys.Right) || KeyboardState.IsKeyDown(Keys.D)) 
                     return 1.0F;  // strafe right
            return 0.0F;
        }
        Vector2 ChangeView(GameTime gameTime)
        {
            const float VERTICAL_INVERSION = -1.0f; // vertical view control
            // negate to reverse

            // handle change in view using right and left keys
            int widthMiddle = glControl.Width / 2;
            int heightMiddle = glControl.Height / 2;
            Vector2 change = Vector2.Zero;

            float scaleY = VERTICAL_INVERSION * (float)gameTime.ElapsedMilliseconds / 100.0f;
            float scaleX = (float)gameTime.ElapsedMilliseconds / 400.0f;

            // cursor not at center on X
            if (MouseState.X != widthMiddle)
            {
                change.X = MouseState.X - widthMiddle;
                change.X /= scaleX;
            }
            // cursor not at center on Y
            if (MouseState.Y != heightMiddle)
            {
                change.Y = MouseState.Y - heightMiddle;
                change.Y /= scaleY;
            }
            // reset cursor back to center
            MouseState.SetPosition(glControl.PointToScreen(new Point((int)widthMiddle, (int)heightMiddle)));
            return change;
        }
        #endregion


        #region - Working Code -
        private void Initialize()
        {
            GameTime.Start();

            effect = new AtomShader();

            // setup world matrices
            float widthToHeight = ClientSize.Width / (float)ClientSize.Height;
            projectionMatrix = (Matrix4.CreatePerspectiveFieldOfView(1.0f, widthToHeight, 0.001F, 1000));
            modelviewMatrix = (Matrix4.CreateRotationX(0.5f) * Matrix4.CreateTranslation(0, 0, -4));
            effect.View = modelviewMatrix;
            //effect.Projection = projectionMatrix;
            effect.Color = OpenTK.Graphics.Color4.LightBlue;

            cam = new Camera();
            cam.SetProjection(glControl.Width, glControl.Height);
            effect.Projection = cam.projectionMatrix;

            Renderer.Enabled(EnableCap.DepthTest, true);
            Renderer.CullMode(CullFaceMode.Back);
            Renderer.ClearColor(OpenTK.Graphics.Color4.CornflowerBlue);
        }
        private void LoadContent()
        {
            List<BondID> bonds;
            var atoms = Utils.ParseString(global::FoldingXNA.Properties.Resources.demoProtein, out bonds);

            List<Atom> ato = new List<Atom>();
            List<BondID> bnd = new List<BondID>();

            var a = new Atom("n", 1, 1, 1, 2);
            var b = new Atom("m", 1, 1, 1, 2);
            a.AddKeyFrame(new Vector3(0, 0, 0));
            b.AddKeyFrame(new Vector3(1, 1, 1));
            ato.Add(a);
            ato.Add(b);
            var c = new BondID(0, 1);
            bnd.Add(c);

            effect.Atoms = atoms;
            effect.Bonds = bonds;
        }

        protected void Update(object sender, GameTime gameTime)
        {
            if (KeyboardState.IsKeyDown(Keys.Escape)) this.Close();

            // (float)gameTime.ElapsedMilliseconds()/6000
            modelviewMatrix *= Matrix4.CreateRotationY(0.005F) * Matrix4.CreateTranslation(locX, locY, locZ);

            float inc = 0.0012F;
            locX += inc * fX;
            locY += inc * fY * 1.8F;
            locZ += inc * fZ * 1.15F;
            if (locX >  .1F && fX == 1) fX = -1; else if (locX < -.1F && fX == -1) fX = 1;
            if (locY >  .1F && fY == 1) fY = -1; else if (locY < -.1F && fY == -1) fY = 1;
            if (locZ >  .1F && fZ == 1) fZ = -1; else if (locZ < -.1F && fZ == -1) fZ = 1;

            // update camera
            //cam.SetFrameInterval(gameTime);
            //cam.Move(Move());
            //cam.Strafe(Strafe());
            //cam.SetView(ChangeView(gameTime));

            //modelviewMatrix *= Matrix4.CreateTranslation(Move() * Vector3.UnitZ);
            //modelviewMatrix = cam.viewMatrix;
            effect.View = modelviewMatrix;
        }
        protected void Draw(object sender, GameTime gameTime)
        {
            Renderer.Clear();

            effect.Draw();

            glControl.SwapBuffers();
        }
        #endregion


        #region - Events -
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameTime.Stop();
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            Initialize();
            LoadContent();
        }
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Update(sender, GameTime.Instance);
            Draw(sender, GameTime.Instance);
        }
        private void glControl1_Resize(object sender, EventArgs e)
        {
            Renderer.Resize(glControl.Width, glControl.Height);
        }
        private void gameTick(object sender, GameTime e)
        {
            glControl.Invalidate();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            MouseState.X = e.X;
            MouseState.Y = e.Y;
            MouseState.Delta = e.Delta;
            MouseState.Clicks = e.Clicks;
            MouseState.Button = e.Button;
            MouseState.IsButtonDown = true;
        }
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            MouseState.X = e.X;
            MouseState.Y = e.Y;
            MouseState.Delta = e.Delta;
            MouseState.Clicks = e.Clicks;
            MouseState.Button = e.Button;
        }
        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            MouseState.X = e.X;
            MouseState.Y = e.Y;
            MouseState.Delta = e.Delta;
            MouseState.Clicks = e.Clicks;
            MouseState.Button = e.Button;
            MouseState.IsButtonDown = false;
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (!KeyboardState.IsButtonDown)
            {
                KeyboardState.Alt = e.Alt;
                KeyboardState.Ctrl = e.Control;
                KeyboardState.KeyCode = e.KeyCode;
                KeyboardState.KeyData = e.KeyData;
                KeyboardState.KeyValue = e.KeyValue;
                KeyboardState.Modifiers = e.Modifiers;
                KeyboardState.Shift = e.Shift;
                KeyboardState.IsButtonDown = true;
            }
        }
        private void glControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            KeyboardState.KeyChar = e.KeyChar;
        }
        private void glControl_KeyUp(object sender, KeyEventArgs e)
        {
            KeyboardState.Alt = e.Alt;
            KeyboardState.Ctrl = e.Control;
            KeyboardState.KeyCode = e.KeyCode;
            KeyboardState.KeyData = e.KeyData;
            KeyboardState.KeyValue = e.KeyValue;
            KeyboardState.Modifiers = e.Modifiers;
            KeyboardState.Shift = e.Shift;
            KeyboardState.IsButtonDown = true;
        }
        #endregion

    }
}
