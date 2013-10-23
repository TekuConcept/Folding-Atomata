using FoldingAtomata.World;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XNA;

namespace FoldingAtomata.NViewer
{
    public class User
    {
        public const float ACCELERATION = 0.0007F;
        public const float GEOMETRIC_SPEED_DECAY = 0.96F;
        public const float MAX_SPEED = 1.3F;
        public const float MIN_SPEED = 0.0000001F;
        public const float PITCH_COEFFICIENT = 0.05F;
        public const float YAW_COEFFICIENT = 0.05F;
        public const float ROLL_SPEED = 0.05F;

        public User(Scene scene)
        {
            _scene = scene;
            _mouseControlsCamera = true;
            _windowCenterX = (GlutManager.GetWidth(0) / 2);
            _windowCenterY = (GlutManager.GetHeight(0) / 2);
        }
        public void Update(int deltaTime)
        {
            ApplyAcceleration(deltaTime);

            _movementDelta = Vector3.Clamp(_movementDelta, new Vector3(-MAX_SPEED), new Vector3(MAX_SPEED));

            var camera = _scene.Camera;
            camera.MoveForward(_movementDelta.X);
            camera.MoveRight(_movementDelta.Y);
            camera.MoveUp(_movementDelta.Z);

            if (_downKeys.FindAll(KeyAction.POSITIVE_ROLL) > 0)
                camera.Roll(ROLL_SPEED * deltaTime);
            if (_downKeys.FindAll(KeyAction.NEGATIVE_ROLL) > 0)
                camera.Roll(-ROLL_SPEED * deltaTime);

            if (_downKeys.Count == 0)
                _movementDelta *= GEOMETRIC_SPEED_DECAY;
        }
        public void ApplyAcceleration(int deltaTime)
        {
            if (_downKeys.FindAll(KeyAction.FORWARD) > 0)
                _movementDelta.X += ACCELERATION * deltaTime;
            if (_downKeys.FindAll(KeyAction.BACKWARD) > 0)
                _movementDelta.X -= ACCELERATION * deltaTime;

            if (_downKeys.FindAll(KeyAction.RIGHT) > 0)
                _movementDelta.Y += ACCELERATION * deltaTime;
            if (_downKeys.FindAll(KeyAction.LEFT) > 0)
                _movementDelta.Y -= ACCELERATION * deltaTime;

            if (_downKeys.FindAll(KeyAction.UP) > 0)
                _movementDelta.Z += ACCELERATION * deltaTime;
            if (_downKeys.FindAll(KeyAction.DOWN) > 0)
                _movementDelta.Z -= ACCELERATION * deltaTime;
        }
        public void SetWindowOffset(int x, int y)
        {
            _windowCenterX = GlutManager.GetWidth(0) / 2;
            _windowCenterY = GlutManager.GetHeight(0) / 2;
        }
        public void GrabPointer(int xoff, int yoff)
        {
            GlutManager.SetCursor(GlutManager.Cursor.None);
            _mouseControlsCamera = true;
            RecenterCursor(xoff, yoff);
        }
        public void ReleasePointer()
        {
            GlutManager.SetCursor(GlutManager.Cursor.Default);
            _mouseControlsCamera = false;
        }
        public void RecenterCursor(int xoff, int yoff)
        {
            GlutManager.WarpPointer(_windowCenterX+xoff, _windowCenterY+yoff); //moves mouse cursor
            _mouseMoved = true;
        }
        public void OnKeyPress(Keys key)
        {
            switch(key)
            {
                case Keys.W:
                    _downKeys.Add(KeyAction.FORWARD);
                    break;

                case Keys.S:
                    _downKeys.Add(KeyAction.BACKWARD);
                    break;

                case Keys.D:
                    _downKeys.Add(KeyAction.RIGHT);
                    break;

                case Keys.A:
                    _downKeys.Add(KeyAction.LEFT);
                    break;

                case Keys.E:
                    _downKeys.Add(KeyAction.UP);
                    break;

                case Keys.Q:
                    _downKeys.Add(KeyAction.DOWN);
                    break;

                case Keys.Escape:
                    ReleasePointer();
                    break;
            }
        }
        public void OnKeyRelease(Keys key)
        {
            switch(key)
            {
                case Keys.W:
                    _downKeys.Remove(KeyAction.FORWARD);
                    break;

                case Keys.S:
                    _downKeys.Remove(KeyAction.BACKWARD);
                    break;

                case Keys.D:
                    _downKeys.Remove(KeyAction.RIGHT);
                    break;

                case Keys.A:
                    _downKeys.Remove(KeyAction.LEFT);
                    break;

                case Keys.E:
                    _downKeys.Remove(KeyAction.UP);
                    break;

                case Keys.Q:
                    _downKeys.Remove(KeyAction.DOWN);
                    break;
            }
        }
        public void OnSpecialKeyPress(Keys key)
        {
            switch(key)
            {
                case Keys.PageUp:
                    _downKeys.Add(KeyAction.NEGATIVE_ROLL);
                    break;

                case Keys.PageDown:
                    _downKeys.Add(KeyAction.POSITIVE_ROLL);
                    break;
            }
        }
        public void OnSpecialKeyRelease(Keys key)
        {
            switch(key)
            {
                case Keys.PageUp:
                    _downKeys.Remove(KeyAction.NEGATIVE_ROLL);
                    break;

                case Keys.PageDown:
                    _downKeys.Remove(KeyAction.POSITIVE_ROLL);
                    break;
            }
        }
        public void OnMouseClick(OpenTK.Input.MouseButton button, OpenTK.Input.MouseState state, int x, int y, int xoff, int yoff)
        {
            if (button == OpenTK.Input.MouseButton.Left)// && state.LeftButton == OpenTK.Input.ButtonState.Pressed)
            {
                if (_mouseControlsCamera) ReleasePointer();
                else GrabPointer(xoff, yoff);
            }
        }
        public void OnMouseMotion(int x, int y, int xoff, int yoff)
        {
            int lastX = _windowCenterX, lastY = _windowCenterY;

            if (!_mouseControlsCamera)
                return;

            if (!_determinedLastPosition)
            {
                lastX = x;
                lastY = y;
                _determinedLastPosition = true;
                return;
            }

            if (x != _windowCenterX || y != _windowCenterY)
            {
                _scene.Camera.Pitch((lastY - y) * PITCH_COEFFICIENT);
                _scene.Camera.Yaw((lastX - x) * YAW_COEFFICIENT);
                RecenterCursor(xoff, yoff);
            }

            lastX = x;
            lastY = y;
        }
        public void OnMouseDrag(int x, int y)
        {

        }


        private enum KeyAction
        {
            FORWARD, BACKWARD,
            RIGHT, LEFT,
            UP, DOWN,
            POSITIVE_ROLL, NEGATIVE_ROLL
        }

        private struct KeyHash
        {
            int Operator(KeyAction myEnum)
            {
                return (int)myEnum;
            }
        }

        private Scene _scene;
        private bool 
            _mouseControlsCamera, 
            _mouseMoved = false, 
            _determinedLastPosition = false;
        private int 
            _windowCenterX, 
            _windowCenterY;
        private Vector3 _movementDelta;
        private List<KeyAction> _downKeys = new List<KeyAction>();

        public bool IsMoving
        {
            get
            {
                var DELTA = SlotViewer.GetDotProduct(_movementDelta, _movementDelta);
                bool test = DELTA > MIN_SPEED || _mouseMoved;
                _mouseMoved = false; //minor hack; isMoving() must be called from render()
                return test;
            }
        }
    }
}
