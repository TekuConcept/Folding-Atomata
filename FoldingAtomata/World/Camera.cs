using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using XNA;

namespace FoldingAtomata.World
{
    public class Camera
    {
        #region - Public -
        public Camera()
        {
            Reset();
        }
        public void Reset()
        {
            Position = new Vector3(0.0F, 0.0F, 0.5F);
            LookAt(new Vector3(0.0F, 0.0F, -1.0F), new Vector3(0.0F, 1.0F, 0.0F));

            FieldOfView   = 45.0f;         // frustrum viewing aperture
            AspectRatio   = 4.0f / 3.0f;   // frustrum view angling
            NearFieldClip = 0.005f;        // clip anything closer than this
            FarFieldClip  = 65536.0f;      // clip anything farther than this
            UpdateProjectionMatrix();
        }
        public void StartSync()
        {
            if (_viewUpdated)
                _tempViewMat = CalculateViewMatrix();

            if (_projectionUpdated)
                _tempProjMat = Projection;
        }
        public void Sync(int viewMatrixUniform, int projMatrixUniform)
        {
            if (viewMatrixUniform < 0 || projMatrixUniform < 0)
                throw new Exception("Unable to find Camera uniform variables!");

            //assemble view matrix and sync if it has updated
            if (_viewUpdated)
                GL.UniformMatrix4(viewMatrixUniform, 1, false, Utils.XNA_Float_Matrix(_tempViewMat));

            //sync projection matrix if it has updated
            if (_projectionUpdated)
                GL.UniformMatrix4(projMatrixUniform, 1, false, Utils.XNA_Float_Matrix(_tempProjMat));
        }
        public void EndSync()
        {
            _projectionUpdated = false;
            _viewUpdated = false;
        }

        // position and orient the camera
        public void LookAt(Vector3 gaze, Vector3 up)
        {
            if (gaze == Position)
                throw new Exception("Cannot look at same point as position!");

            Look = gaze;
            Up = up;
            _viewUpdated = true;
        }

        // translate irregardless of orientation
        public void TranslateX(float value)
        {
            Translate(new Vector3(value, 0, 0));
        }
        public void TranslateY(float value)
        {
            Translate(new Vector3(0, value, 0));
        }
        public void TranslateZ(float value)
        {
            Translate(new Vector3(0, 0, value));
        }
        public void Translate(XNA.Vector3 delta)
        {
            Position += delta;
            Look += delta;
            _viewUpdated = true;
        }

        // translation relative to orientation
        public void MoveForward(float value)
        {
            Translate(GetLookAt() * value);
        }
        public void MoveRight(float value)
        {
            Vector3 orientation = GetLookAt();
            Vector3 tangental = Vector3.Normalize(XNA.Vector3.Cross(orientation, Up));
            Translate(tangental * value);
        }
        public void MoveUp(float value)
        {
            Translate(Vector3.Normalize(Up) * value);
        }

        // orientation controls
        public void Pitch(float theta)
        {
            Vector3 lookVector = Look - Position;
            Vector3 tangental = Vector3.Cross(lookVector, Up);
            Matrix rotationMatrix = Matrix.RotationAxis(tangental, theta);

            var q1 = new Quaternion(Up,  1);
            var m1 = Matrix.RotationQuaternion(q1);
            Up  = (rotationMatrix * m1).Up; //xyz

            var q2 = new Quaternion(lookVector, 1);
            var m2 = Matrix.RotationQuaternion(q2);
            lookVector = (rotationMatrix * m2).Forward; //xyz


            Look = Position + lookVector;
            _viewUpdated = true;
        }
        public void Yaw(float theta, bool aroundUpVector = true)
        {
            Vector3 vectorOfRotation = aroundUpVector ? Up : new Vector3(0, 0, 1);
            Matrix rotationMatrix = Matrix.RotationAxis(vectorOfRotation, theta);

            Vector3 lookVector = Look - Position;

            var q1 = new Quaternion(lookVector, 1);
            var m1 = Matrix.RotationQuaternion(q1);
            lookVector = (rotationMatrix * m1).Forward; //xyz
            var q2 = new Quaternion(Up, 1);
            var m2 = Matrix.RotationQuaternion(q2);
            Up  = (rotationMatrix * m2).Up; //xyz
            
            Look = Position + lookVector;
            _viewUpdated = true;
        }
        public void Roll(float theta)
        {
            Vector3 orientation = GetLookAt();
            Matrix rotationMatrix = Matrix.RotationAxis(orientation, theta);

            var q1 = new Quaternion(Up, 1);
            var m1 = Matrix.RotationQuaternion(q1);
            Up = (rotationMatrix * m1).Up; //xyz
            _viewUpdated = true;
        }
        public bool ConstrainedPitch(float theta)
        {
            var oldUpVector = Up;
            var oldLookingAt = Look;

            Pitch(theta);

            /*
                revert the pitch if any of the conditions are true:
                (orientation.z < 0 && upVector_.z < 0) ==> if looking down upside-down
                (orientation.z > 0 && upVector_.z < 0) ==> if looking up but tilted back
            */

            float temp = GetLookAt().Z;
            if (temp < 0) temp *= -1;
            if (Up.Z < 0 && Math.Abs(CalculateLookDirection().Z) > 0.00001f) // != 0
            {
                Up = oldUpVector;
                Look = oldLookingAt;
                _viewUpdated = true;
                return true;
            }

            return false;
        }
        public bool ConstrainedRoll(float theta)
        {
            var oldUpVector = Up;
            Roll(theta);

            if (Up.Z < 0)
            {
                Up = oldUpVector;
                _viewUpdated = true;
                Console.Write("Camera roll constrained. Reverted request.\n");
                return true;
            }

            return false;
        }

        // adjusts the camera's frustrum (it's fisheye perspective properties)
        public void SetPerspective(float fov, float aspect, float nearClip, float farClip)
        {
            AspectRatio = aspect;
            NearFieldClip = nearClip;
            FarFieldClip = farClip;
            FieldOfView = fov;
            UpdateProjectionMatrix();
        }
        private void UpdateProjectionMatrix()
        {
            Projection = Matrix.PerspectiveFovLH(FieldOfView, AspectRatio, NearFieldClip, FarFieldClip);
            _projectionUpdated = true;
        }

        // accessors for camera's position and orientation
        public Vector3 GetLookAt()
        {
            return Vector3.Normalize(Look - Position);
        }
        public Matrix CalculateViewMatrix()
        {
            return Matrix.LookAtLH(Position, Look, Up);
        }
        public Vector3 CalculateLookDirection()
        {
            return Vector3.Normalize(Look - Position);
        }

        public new String ToString()
        {
            return String.Format("Look: [{0}] Pos: [{1}] Up: [{2}]", Look.ToString(), Position.ToString(), Up.ToString());
        }
        #endregion

        Vector3 _pos;
        Matrix _tempViewMat, _tempProjMat;
        float _fov, _ar, _nfc, _ffc;
        bool _viewUpdated, _projectionUpdated;
        public Vector3 Look { get; set; }
        public Vector3 Position
        {
            get
            {
                return _pos;
            }
            set
            {
                if (value == Look)
                    throw new Exception("Cannot be where we're looking at!");

                _pos = value;
            }
        }
        public Vector3 Up { get; set; }
        public Matrix Projection { get; set; }
        public float FieldOfView
        {
            get
            {
                return _fov;
            }
            set
            {
                _fov = value;
                UpdateProjectionMatrix();
            }
        }
        public float AspectRatio
        {
            get
            {
                return _ar;
            }
            set
            {
                _ar = value;
                UpdateProjectionMatrix();
            }
        }
        public float NearFieldClip
        {
            get
            {
                return _nfc;
            }
            set
            {
                _nfc = value;
                UpdateProjectionMatrix();
            }
        }
        public float FarFieldClip
        {
            get
            {
                return _ffc;
            }
            set
            {
                _ffc = value;
                UpdateProjectionMatrix();
            }
        }
    }
}
