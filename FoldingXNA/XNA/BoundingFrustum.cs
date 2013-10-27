using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace XNA
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct BoundingFrustum : IEquatable<BoundingFrustum>
    {
        private Matrix pMatrix;
        private Plane pNear;
        private Plane pFar;
        private Plane pLeft;
        private Plane pRight;
        private Plane pTop;
        private Plane pBottom;
        public Matrix Matrix
        {
            get
            {
                return this.pMatrix;
            }
            set
            {
                this.pMatrix = value;
                GetPlanesFromMatrix(ref this.pMatrix, out this.pNear, out this.pFar, out this.pLeft, out this.pRight, out this.pTop, out this.pBottom);
            }
        }
        public Plane Near
        {
            get
            {
                return this.pNear;
            }
        }
        public Plane Far
        {
            get
            {
                return this.pFar;
            }
        }
        public Plane Left
        {
            get
            {
                return this.pLeft;
            }
        }
        public Plane Right
        {
            get
            {
                return this.pRight;
            }
        }
        public Plane Top
        {
            get
            {
                return this.pTop;
            }
        }
        public Plane Bottom
        {
            get
            {
                return this.pBottom;
            }
        }
        public BoundingFrustum(Matrix matrix)
        {
            this.pMatrix = matrix;
            GetPlanesFromMatrix(ref this.pMatrix, out this.pNear, out this.pFar, out this.pLeft, out this.pRight, out this.pTop, out this.pBottom);
        }

        public override int GetHashCode()
        {
            return this.pMatrix.GetHashCode();
        }

        public bool Equals(BoundingFrustum other)
        {
            return (this.pMatrix == other.pMatrix);
        }

        public override bool Equals(object obj)
        {
            return (((obj != null) && (obj is BoundingFrustum)) && this.Equals((BoundingFrustum)obj));
        }

        public static bool operator ==(BoundingFrustum left, BoundingFrustum right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BoundingFrustum left, BoundingFrustum right)
        {
            return !left.Equals(right);
        }

        public Plane GetPlane(int index)
        {
            switch (index)
            {
                case 0:
                    return this.pLeft;

                case 1:
                    return this.pRight;

                case 2:
                    return this.pTop;

                case 3:
                    return this.pBottom;

                case 4:
                    return this.pNear;

                case 5:
                    return this.pFar;
            }
            return new Plane();
        }

        private static void GetPlanesFromMatrix(ref Matrix matrix, out Plane near, out Plane far, out Plane left, out Plane right, out Plane top, out Plane bottom)
        {
            left.Normal.X = matrix.M14 + matrix.M11;
            left.Normal.Y = matrix.M24 + matrix.M21;
            left.Normal.Z = matrix.M34 + matrix.M31;
            left.D = matrix.M44 + matrix.M41;
            left.Normalize();
            right.Normal.X = matrix.M14 - matrix.M11;
            right.Normal.Y = matrix.M24 - matrix.M21;
            right.Normal.Z = matrix.M34 - matrix.M31;
            right.D = matrix.M44 - matrix.M41;
            right.Normalize();
            top.Normal.X = matrix.M14 - matrix.M12;
            top.Normal.Y = matrix.M24 - matrix.M22;
            top.Normal.Z = matrix.M34 - matrix.M32;
            top.D = matrix.M44 - matrix.M42;
            top.Normalize();
            bottom.Normal.X = matrix.M14 + matrix.M12;
            bottom.Normal.Y = matrix.M24 + matrix.M22;
            bottom.Normal.Z = matrix.M34 + matrix.M32;
            bottom.D = matrix.M44 + matrix.M42;
            bottom.Normalize();
            near.Normal.X = matrix.M13;
            near.Normal.Y = matrix.M23;
            near.Normal.Z = matrix.M33;
            near.D = matrix.M43;
            near.Normalize();
            far.Normal.X = matrix.M14 - matrix.M13;
            far.Normal.Y = matrix.M24 - matrix.M23;
            far.Normal.Z = matrix.M34 - matrix.M33;
            far.D = matrix.M44 - matrix.M43;
            far.Normalize();
        }

        private static Vector3 Get3PlanesInterPoint(ref Plane p1, ref Plane p2, ref Plane p3)
        {
            return (Vector3)((((-p1.D * Vector3.Cross(p2.Normal, p3.Normal)) / Vector3.Dot(p1.Normal, Vector3.Cross(p2.Normal, p3.Normal))) - ((p2.D * Vector3.Cross(p3.Normal, p1.Normal)) / Vector3.Dot(p2.Normal, Vector3.Cross(p3.Normal, p1.Normal)))) - ((p3.D * Vector3.Cross(p1.Normal, p2.Normal)) / Vector3.Dot(p3.Normal, Vector3.Cross(p1.Normal, p2.Normal))));
        }

        public static BoundingFrustum FromCamera(Vector3 cameraPos, Vector3 lookDir, Vector3 upDir, float fov, float znear, float zfar, float aspect)
        {
            lookDir = Vector3.Normalize(lookDir);
            upDir = Vector3.Normalize(upDir);
            Vector3 vector = cameraPos + ((Vector3)(lookDir * znear));
            Vector3 vector2 = cameraPos + ((Vector3)(lookDir * zfar));
            float num = (float)(znear * Math.Tan((double)(fov / 2f)));
            float num2 = (float)(zfar * Math.Tan((double)(fov / 2f)));
            float num3 = num * aspect;
            float num4 = num2 * aspect;
            Vector3 vector3 = Vector3.Normalize(Vector3.Cross(upDir, lookDir));
            Vector3 vector4 = (Vector3)((vector - (num * upDir)) + (num3 * vector3));
            Vector3 vector5 = (Vector3)((vector + (num * upDir)) + (num3 * vector3));
            Vector3 vector6 = (Vector3)((vector + (num * upDir)) - (num3 * vector3));
            Vector3 vector7 = (Vector3)((vector - (num * upDir)) - (num3 * vector3));
            Vector3 vector8 = (Vector3)((vector2 - (num2 * upDir)) + (num4 * vector3));
            Vector3 vector9 = (Vector3)((vector2 + (num2 * upDir)) + (num4 * vector3));
            Vector3 vector10 = (Vector3)((vector2 + (num2 * upDir)) - (num4 * vector3));
            Vector3 vector11 = (Vector3)((vector2 - (num2 * upDir)) - (num4 * vector3));
            BoundingFrustum frustum = new BoundingFrustum();
            frustum.pNear = new Plane(vector4, vector5, vector6);
            frustum.pFar = new Plane(vector10, vector9, vector8);
            frustum.pLeft = new Plane(vector7, vector6, vector10);
            frustum.pRight = new Plane(vector8, vector9, vector5);
            frustum.pTop = new Plane(vector5, vector9, vector10);
            frustum.pBottom = new Plane(vector11, vector8, vector4);
            frustum.pNear.Normalize();
            frustum.pFar.Normalize();
            frustum.pLeft.Normalize();
            frustum.pRight.Normalize();
            frustum.pTop.Normalize();
            frustum.pBottom.Normalize();
            frustum.pMatrix = Matrix.LookAtLH(cameraPos, cameraPos + ((Vector3)(lookDir * 10f)), upDir) * Matrix.PerspectiveFovLH(fov, aspect, znear, zfar);
            return frustum;
        }

        public static BoundingFrustum FromCamera(FrustumCameraParams cameraParams)
        {
            return FromCamera(cameraParams.Position, cameraParams.LookAtDir, cameraParams.UpDir, cameraParams.FOV, cameraParams.ZNear, cameraParams.ZFar, cameraParams.AspectRatio);
        }

        public Vector3[] GetCorners()
        {
            return new Vector3[] { Get3PlanesInterPoint(ref this.pNear, ref this.pBottom, ref this.pRight), Get3PlanesInterPoint(ref this.pNear, ref this.pTop, ref this.pRight), Get3PlanesInterPoint(ref this.pNear, ref this.pTop, ref this.pLeft), Get3PlanesInterPoint(ref this.pNear, ref this.pBottom, ref this.pLeft), Get3PlanesInterPoint(ref this.pFar, ref this.pBottom, ref this.pRight), Get3PlanesInterPoint(ref this.pFar, ref this.pTop, ref this.pRight), Get3PlanesInterPoint(ref this.pFar, ref this.pTop, ref this.pLeft), Get3PlanesInterPoint(ref this.pFar, ref this.pBottom, ref this.pLeft) };
        }

        public FrustumCameraParams GetCameraParams()
    {
        Vector3[] corners = this.GetCorners();
        FrustumCameraParams params_ = new FrustumCameraParams();
        params_.Position = Get3PlanesInterPoint(ref this.pRight, ref this.pTop, ref this.pLeft);
        params_.LookAtDir = this.pNear.Normal;
        params_.UpDir = Vector3.Normalize(Vector3.Cross(this.pRight.Normal, this.pNear.Normal));
        params_.FOV = (float) ((1.5707963267948966 - Math.Acos((double) Vector3.Dot(this.pNear.Normal, this.pTop.Normal))) * 2.0);
        Vector3 vector = corners[6] - corners[5];
        Vector3 vector2 = corners[4] - corners[5];
        params_.AspectRatio = vector.Length() / vector2.Length();
        params_.ZNear = (params_.Position + ((Vector3) (this.pNear.Normal * this.pNear.D))).Length();
        params_.ZFar = (params_.Position + ((Vector3) (this.pFar.Normal * this.pFar.D))).Length();
        return params_;
    }

        public ContainmentType Contains(ref Vector3 point)
        {
            PlaneIntersectionType front = PlaneIntersectionType.Front;
            PlaneIntersectionType type2 = PlaneIntersectionType.Front;
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        type2 = this.pNear.Intersects(ref point);
                        break;

                    case 1:
                        type2 = this.pFar.Intersects(ref point);
                        break;

                    case 2:
                        type2 = this.pLeft.Intersects(ref point);
                        break;

                    case 3:
                        type2 = this.pRight.Intersects(ref point);
                        break;

                    case 4:
                        type2 = this.pTop.Intersects(ref point);
                        break;

                    case 5:
                        type2 = this.pBottom.Intersects(ref point);
                        break;
                }
                switch (type2)
                {
                    case PlaneIntersectionType.Back:
                        return ContainmentType.Disjoint;

                    case PlaneIntersectionType.Intersecting:
                        front = PlaneIntersectionType.Intersecting;
                        break;
                }
            }
            switch (front)
            {
                case PlaneIntersectionType.Intersecting:
                    return ContainmentType.Intersects;
            }
            return ContainmentType.Contains;
        }

        public ContainmentType Contains(Vector3 point)
        {
            return this.Contains(ref point);
        }

        public ContainmentType Contains(Vector3[] points)
        {
            bool flag = false;
            bool flag2 = true;
            for (int i = 0; i < points.Length; i++)
            {
                switch (this.Contains(ref points[i]))
                {
                    case ContainmentType.Disjoint:
                        flag2 = false;
                        break;

                    case ContainmentType.Contains:
                    case ContainmentType.Intersects:
                        flag = true;
                        break;
                }
            }
            if (!flag)
            {
                return ContainmentType.Disjoint;
            }
            if (flag2)
            {
                return ContainmentType.Contains;
            }
            return ContainmentType.Intersects;
        }

        public void Contains(Vector3[] points, out ContainmentType result)
        {
            result = this.Contains(points);
        }

        private void GetBoxToPlanePVertexNVertex(ref BoundingBox box, ref Vector3 planeNormal, out Vector3 p, out Vector3 n)
        {
            p = box.Minimum;
            if (planeNormal.X >= 0f)
            {
                p.X = box.Maximum.X;
            }
            if (planeNormal.Y >= 0f)
            {
                p.Y = box.Maximum.Y;
            }
            if (planeNormal.Z >= 0f)
            {
                p.Z = box.Maximum.Z;
            }
            n = box.Maximum;
            if (planeNormal.X >= 0f)
            {
                n.X = box.Minimum.X;
            }
            if (planeNormal.Y >= 0f)
            {
                n.Y = box.Minimum.Y;
            }
            if (planeNormal.Z >= 0f)
            {
                n.Z = box.Minimum.Z;
            }
        }

        public ContainmentType Contains(ref BoundingBox box)
        {
            ContainmentType contains = ContainmentType.Contains;
            for (int i = 0; i < 6; i++)
            {
                Vector3 vector;
                Vector3 vector2;
                Plane plane = this.GetPlane(i);
                this.GetBoxToPlanePVertexNVertex(ref box, ref plane.Normal, out vector, out vector2);
                if (Collision.PlaneIntersectsPoint(ref plane, ref vector) == PlaneIntersectionType.Back)
                {
                    return ContainmentType.Disjoint;
                }
                if (Collision.PlaneIntersectsPoint(ref plane, ref vector2) == PlaneIntersectionType.Back)
                {
                    contains = ContainmentType.Intersects;
                }
            }
            return contains;
        }

        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            result = this.Contains(box.GetCorners());
        }

        public ContainmentType Contains(ref BoundingSphere sphere)
        {
            PlaneIntersectionType front = PlaneIntersectionType.Front;
            PlaneIntersectionType type2 = PlaneIntersectionType.Front;
            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        type2 = this.pNear.Intersects(ref sphere);
                        break;

                    case 1:
                        type2 = this.pFar.Intersects(ref sphere);
                        break;

                    case 2:
                        type2 = this.pLeft.Intersects(ref sphere);
                        break;

                    case 3:
                        type2 = this.pRight.Intersects(ref sphere);
                        break;

                    case 4:
                        type2 = this.pTop.Intersects(ref sphere);
                        break;

                    case 5:
                        type2 = this.pBottom.Intersects(ref sphere);
                        break;
                }
                switch (type2)
                {
                    case PlaneIntersectionType.Back:
                        return ContainmentType.Disjoint;

                    case PlaneIntersectionType.Intersecting:
                        front = PlaneIntersectionType.Intersecting;
                        break;
                }
            }
            switch (front)
            {
                case PlaneIntersectionType.Intersecting:
                    return ContainmentType.Intersects;
            }
            return ContainmentType.Contains;
        }

        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            result = this.Contains(ref sphere);
        }

        public bool Contains(ref BoundingFrustum frustum)
        {
            return (this.Contains(frustum.GetCorners()) != ContainmentType.Disjoint);
        }

        public void Contains(ref BoundingFrustum frustum, out bool result)
        {
            result = this.Contains(frustum.GetCorners()) != ContainmentType.Disjoint;
        }

        public bool Intersects(ref BoundingSphere sphere)
        {
            return (this.Contains(ref sphere) != ContainmentType.Disjoint);
        }

        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            result = this.Contains(ref sphere) != ContainmentType.Disjoint;
        }

        public bool Intersects(ref BoundingBox box)
        {
            return (this.Contains(ref box) != ContainmentType.Disjoint);
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            result = this.Contains(ref box) != ContainmentType.Disjoint;
        }

        private PlaneIntersectionType PlaneIntersectsPoints(ref Plane plane, Vector3[] points)
        {
            PlaneIntersectionType type = Collision.PlaneIntersectsPoint(ref plane, ref points[0]);
            for (int i = 1; i < points.Length; i++)
            {
                if (Collision.PlaneIntersectsPoint(ref plane, ref points[i]) != type)
                {
                    return PlaneIntersectionType.Intersecting;
                }
            }
            return type;
        }

        public PlaneIntersectionType Intersects(ref Plane plane)
        {
            return this.PlaneIntersectsPoints(ref plane, this.GetCorners());
        }

        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            result = this.PlaneIntersectsPoints(ref plane, this.GetCorners());
        }

        public float GetWidthAtDepth(float depth)
        {
            float num = (float)(1.5707963267948966 - Math.Acos((double)Vector3.Dot(this.pNear.Normal, this.pLeft.Normal)));
            return (float)((Math.Tan((double)num) * depth) * 2.0);
        }

        public float GetHeightAtDepth(float depth)
        {
            float num = (float)(1.5707963267948966 - Math.Acos((double)Vector3.Dot(this.pNear.Normal, this.pTop.Normal)));
            return (float)((Math.Tan((double)num) * depth) * 2.0);
        }

        private BoundingFrustum GetInsideOutClone()
        {
            BoundingFrustum frustum = this;
            frustum.pNear.Normal = -frustum.pNear.Normal;
            frustum.pFar.Normal = -frustum.pFar.Normal;
            frustum.pLeft.Normal = -frustum.pLeft.Normal;
            frustum.pRight.Normal = -frustum.pRight.Normal;
            frustum.pTop.Normal = -frustum.pTop.Normal;
            frustum.pBottom.Normal = -frustum.pBottom.Normal;
            return frustum;
        }

        public bool Intersects(ref Ray ray)
        {
            float? nullable;
            float? nullable2;
            return this.Intersects(ref ray, out nullable, out nullable2);
        }

        public bool Intersects(ref Ray ray, out float? inDistance, out float? outDistance)
        {
            if (this.Contains(ray.Position) != ContainmentType.Disjoint)
            {
                float num = float.MaxValue;
                for (int j = 0; j < 6; j++)
                {
                    float num3;
                    Plane plane = this.GetPlane(j);
                    if (Collision.RayIntersectsPlane(ref ray, ref plane, out num3) && (num3 < num))
                    {
                        num = num3;
                    }
                }
                inDistance = new float?(num);
                outDistance = null;
                return true;
            }
            float maxValue = float.MaxValue;
            float minValue = float.MinValue;
            for (int i = 0; i < 6; i++)
            {
                float num7;
                Plane plane2 = this.GetPlane(i);
                if (Collision.RayIntersectsPlane(ref ray, ref plane2, out num7))
                {
                    maxValue = Math.Min(maxValue, num7);
                    minValue = Math.Max(minValue, num7);
                }
            }
            Vector3 vector = ray.Position + ((Vector3)(ray.Direction * maxValue));
            Vector3 vector2 = ray.Position + ((Vector3)(ray.Direction * minValue));
            Vector3 point = (Vector3)((vector + vector2) / 2f);
            if (this.Contains(ref point) != ContainmentType.Disjoint)
            {
                inDistance = new float?(maxValue);
                outDistance = new float?(minValue);
                return true;
            }
            inDistance = null;
            outDistance = null;
            return false;
        }

        public float GetZoomToExtentsShiftDistance(Vector3[] points)
        {
            float num = (float)(1.5707963267948966 - Math.Acos((double)Vector3.Dot(this.pNear.Normal, this.pTop.Normal)));
            float num2 = (float)Math.Sin((double)num);
            float num3 = (float)(1.5707963267948966 - Math.Acos((double)Vector3.Dot(this.pNear.Normal, this.pLeft.Normal)));
            float num4 = (float)Math.Sin((double)num3);
            float num5 = num2 / num4;
            BoundingFrustum insideOutClone = this.GetInsideOutClone();
            float minValue = float.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                float num8 = Math.Max(Math.Max(Math.Max(Collision.DistancePlanePoint(ref insideOutClone.pTop, ref points[i]), Collision.DistancePlanePoint(ref insideOutClone.pBottom, ref points[i])), Collision.DistancePlanePoint(ref insideOutClone.pLeft, ref points[i]) * num5), Collision.DistancePlanePoint(ref insideOutClone.pRight, ref points[i]) * num5);
                minValue = Math.Max(minValue, num8);
            }
            return (-minValue / num2);
        }

        public float GetZoomToExtentsShiftDistance(ref BoundingBox boundingBox)
        {
            return this.GetZoomToExtentsShiftDistance(boundingBox.GetCorners());
        }

        public Vector3 GetZoomToExtentsShiftVector(Vector3[] points)
        {
            return (Vector3)(this.GetZoomToExtentsShiftDistance(points) * this.pNear.Normal);
        }

        public Vector3 GetZoomToExtentsShiftVector(ref BoundingBox boundingBox)
        {
            return (Vector3)(this.GetZoomToExtentsShiftDistance(boundingBox.GetCorners()) * this.pNear.Normal);
        }

        public bool IsOrthographic
        {
            get
            {
                return ((this.pLeft.Normal == -this.pRight.Normal) && (this.pTop.Normal == -this.pBottom.Normal));
            }
        }
    }
}
