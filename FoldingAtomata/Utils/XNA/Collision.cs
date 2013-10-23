using System;

namespace XNA
{
    public static class Collision
    {
        // Methods
        public static ContainmentType BoxContainsBox(ref BoundingBox box1, ref BoundingBox box2)
        {
            if ((box1.Maximum.X < box2.Minimum.X) || (box1.Minimum.X > box2.Maximum.X))
            {
                return ContainmentType.Disjoint;
            }
            if ((box1.Maximum.Y < box2.Minimum.Y) || (box1.Minimum.Y > box2.Maximum.Y))
            {
                return ContainmentType.Disjoint;
            }
            if ((box1.Maximum.Z < box2.Minimum.Z) || (box1.Minimum.Z > box2.Maximum.Z))
            {
                return ContainmentType.Disjoint;
            }
            if ((((box1.Minimum.X <= box2.Minimum.X) && (box2.Maximum.X <= box1.Maximum.X)) && ((box1.Minimum.Y <= box2.Minimum.Y) && (box2.Maximum.Y <= box1.Maximum.Y))) && ((box1.Minimum.Z <= box2.Minimum.Z) && (box2.Maximum.Z <= box1.Maximum.Z)))
            {
                return ContainmentType.Contains;
            }
            return ContainmentType.Intersects;
        }

        public static ContainmentType BoxContainsPoint(ref BoundingBox box, ref Vector3 point)
        {
            if ((((box.Minimum.X <= point.X) && (box.Maximum.X >= point.X)) && ((box.Minimum.Y <= point.Y) && (box.Maximum.Y >= point.Y))) && ((box.Minimum.Z <= point.Z) && (box.Maximum.Z >= point.Z)))
            {
                return ContainmentType.Contains;
            }
            return ContainmentType.Disjoint;
        }

        public static ContainmentType BoxContainsSphere(ref BoundingBox box, ref BoundingSphere sphere)
        {
            Vector3 vector;
            Vector3.Clamp(ref sphere.Center, ref box.Minimum, ref box.Maximum, out vector);
            if (Vector3.DistanceSquared(sphere.Center, vector) > (sphere.Radius * sphere.Radius))
            {
                return ContainmentType.Disjoint;
            }
            if ((((((box.Minimum.X + sphere.Radius) <= sphere.Center.X) && (sphere.Center.X <= (box.Maximum.X - sphere.Radius))) && (((box.Maximum.X - box.Minimum.X) > sphere.Radius) && ((box.Minimum.Y + sphere.Radius) <= sphere.Center.Y))) && (((sphere.Center.Y <= (box.Maximum.Y - sphere.Radius)) && ((box.Maximum.Y - box.Minimum.Y) > sphere.Radius)) && (((box.Minimum.Z + sphere.Radius) <= sphere.Center.Z) && (sphere.Center.Z <= (box.Maximum.Z - sphere.Radius))))) && ((box.Maximum.X - box.Minimum.X) > sphere.Radius))
            {
                return ContainmentType.Contains;
            }
            return ContainmentType.Intersects;
        }

        public static bool BoxIntersectsBox(ref BoundingBox box1, ref BoundingBox box2)
        {
            if ((box1.Minimum.X > box2.Maximum.X) || (box2.Minimum.X > box1.Maximum.X))
            {
                return false;
            }
            if ((box1.Minimum.Y > box2.Maximum.Y) || (box2.Minimum.Y > box1.Maximum.Y))
            {
                return false;
            }
            return ((box1.Minimum.Z <= box2.Maximum.Z) && (box2.Minimum.Z <= box1.Maximum.Z));
        }

        public static bool BoxIntersectsSphere(ref BoundingBox box, ref BoundingSphere sphere)
        {
            Vector3 vector;
            Vector3.Clamp(ref sphere.Center, ref box.Minimum, ref box.Maximum, out vector);
            return (Vector3.DistanceSquared(sphere.Center, vector) <= (sphere.Radius * sphere.Radius));
        }

        public static void ClosestPointBoxPoint(ref BoundingBox box, ref Vector3 point, out Vector3 result)
        {
            Vector3 vector;
            Vector3.Max(ref point, ref box.Minimum, out vector);
            Vector3.Min(ref vector, ref box.Maximum, out result);
        }

        public static void ClosestPointPlanePoint(ref Plane plane, ref Vector3 point, out Vector3 result)
        {
            float num;
            Vector3.Dot(ref plane.Normal, ref point, out num);
            float num2 = num - plane.D;
            result = (Vector3)(point - (num2 * plane.Normal));
        }

        public static void ClosestPointPointTriangle(ref Vector3 point, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out Vector3 result)
        {
            Vector3 left = vertex2 - vertex1;
            Vector3 vector2 = vertex3 - vertex1;
            Vector3 right = point - vertex1;
            float num = Vector3.Dot(left, right);
            float num2 = Vector3.Dot(vector2, right);
            if ((num <= 0f) && (num2 <= 0f))
            {
                result = vertex1;
            }
            Vector3 vector4 = point - vertex2;
            float num3 = Vector3.Dot(left, vector4);
            float num4 = Vector3.Dot(vector2, vector4);
            if ((num3 >= 0f) && (num4 <= num3))
            {
                result = vertex2;
            }
            float num5 = (num * num4) - (num3 * num2);
            if (((num5 <= 0f) && (num >= 0f)) && (num3 <= 0f))
            {
                float num6 = num / (num - num3);
                result = (Vector3)(vertex1 + (num6 * left));
            }
            Vector3 vector5 = point - vertex3;
            float num7 = Vector3.Dot(left, vector5);
            float num8 = Vector3.Dot(vector2, vector5);
            if ((num8 >= 0f) && (num7 <= num8))
            {
                result = vertex3;
            }
            float num9 = (num7 * num2) - (num * num8);
            if (((num9 <= 0f) && (num2 >= 0f)) && (num8 <= 0f))
            {
                float num10 = num2 / (num2 - num8);
                result = (Vector3)(vertex1 + (num10 * vector2));
            }
            float num11 = (num3 * num8) - (num7 * num4);
            if (((num11 <= 0f) && ((num4 - num3) >= 0f)) && ((num7 - num8) >= 0f))
            {
                float num12 = (num4 - num3) / ((num4 - num3) + (num7 - num8));
                result = (Vector3)(vertex2 + (num12 * (vertex3 - vertex2)));
            }
            float num13 = 1f / ((num11 + num9) + num5);
            float num14 = num9 * num13;
            float num15 = num5 * num13;
            result = (Vector3)((vertex1 + (left * num14)) + (vector2 * num15));
        }

        public static void ClosestPointSpherePoint(ref BoundingSphere sphere, ref Vector3 point, out Vector3 result)
        {
            Vector3.Subtract(ref point, ref sphere.Center, out result);
            result.Normalize();
            result = (Vector3)(result * sphere.Radius);
            result += sphere.Center;
        }

        public static void ClosestPointSphereSphere(ref BoundingSphere sphere1, ref BoundingSphere sphere2, out Vector3 result)
        {
            Vector3.Subtract(ref sphere2.Center, ref sphere1.Center, out result);
            result.Normalize();
            result = (Vector3)(result * sphere1.Radius);
            result += sphere1.Center;
        }

        public static float DistanceBoxBox(ref BoundingBox box1, ref BoundingBox box2)
        {
            float num = 0f;
            if (box1.Minimum.X > box2.Maximum.X)
            {
                float num2 = box2.Maximum.X - box1.Minimum.X;
                num += num2 * num2;
            }
            else if (box2.Minimum.X > box1.Maximum.X)
            {
                float num3 = box1.Maximum.X - box2.Minimum.X;
                num += num3 * num3;
            }
            if (box1.Minimum.Y > box2.Maximum.Y)
            {
                float num4 = box2.Maximum.Y - box1.Minimum.Y;
                num += num4 * num4;
            }
            else if (box2.Minimum.Y > box1.Maximum.Y)
            {
                float num5 = box1.Maximum.Y - box2.Minimum.Y;
                num += num5 * num5;
            }
            if (box1.Minimum.Z > box2.Maximum.Z)
            {
                float num6 = box2.Maximum.Z - box1.Minimum.Z;
                num += num6 * num6;
            }
            else if (box2.Minimum.Z > box1.Maximum.Z)
            {
                float num7 = box1.Maximum.Z - box2.Minimum.Z;
                num += num7 * num7;
            }
            return (float)Math.Sqrt((double)num);
        }

        public static float DistanceBoxPoint(ref BoundingBox box, ref Vector3 point)
        {
            float num = 0f;
            if (point.X < box.Minimum.X)
            {
                num += (box.Minimum.X - point.X) * (box.Minimum.X - point.X);
            }
            if (point.X > box.Maximum.X)
            {
                num += (point.X - box.Maximum.X) * (point.X - box.Maximum.X);
            }
            if (point.Y < box.Minimum.Y)
            {
                num += (box.Minimum.Y - point.Y) * (box.Minimum.Y - point.Y);
            }
            if (point.Y > box.Maximum.Y)
            {
                num += (point.Y - box.Maximum.Y) * (point.Y - box.Maximum.Y);
            }
            if (point.Z < box.Minimum.Z)
            {
                num += (box.Minimum.Z - point.Z) * (box.Minimum.Z - point.Z);
            }
            if (point.Z > box.Maximum.Z)
            {
                num += (point.Z - box.Maximum.Z) * (point.Z - box.Maximum.Z);
            }
            return (float)Math.Sqrt((double)num);
        }

        public static float DistancePlanePoint(ref Plane plane, ref Vector3 point)
        {
            float num;
            Vector3.Dot(ref plane.Normal, ref point, out num);
            return (num - plane.D);
        }

        public static float DistanceSpherePoint(ref BoundingSphere sphere, ref Vector3 point)
        {
            float num;
            Vector3.Distance(ref sphere.Center, ref point, out num);
            num -= sphere.Radius;
            return Math.Max(num, 0f);
        }

        public static float DistanceSphereSphere(ref BoundingSphere sphere1, ref BoundingSphere sphere2)
        {
            float num;
            Vector3.Distance(ref sphere1.Center, ref sphere2.Center, out num);
            num -= sphere1.Radius + sphere2.Radius;
            return Math.Max(num, 0f);
        }

        public static PlaneIntersectionType PlaneIntersectsBox(ref Plane plane, ref BoundingBox box)
        {
            Vector3 vector;
            Vector3 vector2;
            float num;
            vector2.X = (plane.Normal.X >= 0f) ? box.Minimum.X : box.Maximum.X;
            vector2.Y = (plane.Normal.Y >= 0f) ? box.Minimum.Y : box.Maximum.Y;
            vector2.Z = (plane.Normal.Z >= 0f) ? box.Minimum.Z : box.Maximum.Z;
            vector.X = (plane.Normal.X >= 0f) ? box.Maximum.X : box.Minimum.X;
            vector.Y = (plane.Normal.Y >= 0f) ? box.Maximum.Y : box.Minimum.Y;
            vector.Z = (plane.Normal.Z >= 0f) ? box.Maximum.Z : box.Minimum.Z;
            Vector3.Dot(ref plane.Normal, ref vector2, out num);
            if ((num + plane.D) > 0f)
            {
                return PlaneIntersectionType.Front;
            }
            if ((Vector3.Dot(plane.Normal, vector) + plane.D) < 0f)
            {
                return PlaneIntersectionType.Back;
            }
            return PlaneIntersectionType.Intersecting;
        }

        public static bool PlaneIntersectsPlane(ref Plane plane1, ref Plane plane2)
        {
            Vector3 vector;
            float num;
            Vector3.Cross(ref plane1.Normal, ref plane2.Normal, out vector);
            Vector3.Dot(ref vector, ref vector, out num);
            if (Math.Abs(num) < 1E-06f)
            {
                return false;
            }
            return true;
        }

        public static bool PlaneIntersectsPlane(ref Plane plane1, ref Plane plane2, out Ray line)
        {
            Vector3 vector;
            float num;
            Vector3 vector2;
            Vector3.Cross(ref plane1.Normal, ref plane2.Normal, out vector);
            Vector3.Dot(ref vector, ref vector, out num);
            if (Math.Abs(num) < 1E-06f)
            {
                line = new Ray();
                return false;
            }
            Vector3 left = (Vector3)((plane1.D * plane2.Normal) - (plane2.D * plane1.Normal));
            Vector3.Cross(ref left, ref vector, out vector2);
            line.Position = vector2;
            line.Direction = vector;
            line.Direction.Normalize();
            return true;
        }

        public static PlaneIntersectionType PlaneIntersectsPoint(ref Plane plane, ref Vector3 point)
        {
            float num;
            Vector3.Dot(ref plane.Normal, ref point, out num);
            num += plane.D;
            if (num > 0f)
            {
                return PlaneIntersectionType.Front;
            }
            if (num < 0f)
            {
                return PlaneIntersectionType.Back;
            }
            return PlaneIntersectionType.Intersecting;
        }

        public static PlaneIntersectionType PlaneIntersectsSphere(ref Plane plane, ref BoundingSphere sphere)
        {
            float num;
            Vector3.Dot(ref plane.Normal, ref sphere.Center, out num);
            num += plane.D;
            if (num > sphere.Radius)
            {
                return PlaneIntersectionType.Front;
            }
            if (num < -sphere.Radius)
            {
                return PlaneIntersectionType.Back;
            }
            return PlaneIntersectionType.Intersecting;
        }

        public static PlaneIntersectionType PlaneIntersectsTriangle(ref Plane plane, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            PlaneIntersectionType type = PlaneIntersectsPoint(ref plane, ref vertex1);
            PlaneIntersectionType type2 = PlaneIntersectsPoint(ref plane, ref vertex2);
            PlaneIntersectionType type3 = PlaneIntersectsPoint(ref plane, ref vertex3);
            if (((type == PlaneIntersectionType.Front) && (type2 == PlaneIntersectionType.Front)) && (type3 == PlaneIntersectionType.Front))
            {
                return PlaneIntersectionType.Front;
            }
            if (((type == PlaneIntersectionType.Back) && (type2 == PlaneIntersectionType.Back)) && (type3 == PlaneIntersectionType.Back))
            {
                return PlaneIntersectionType.Back;
            }
            return PlaneIntersectionType.Intersecting;
        }

        public static bool RayIntersectsBox(ref Ray ray, ref BoundingBox box, out Vector3 point)
        {
            float num;
            if (!RayIntersectsBox(ref ray, ref box, out num))
            {
                point = Vector3.Zero;
                return false;
            }
            point = ray.Position + ((Vector3)(ray.Direction * num));
            return true;
        }

        public static bool RayIntersectsBox(ref Ray ray, ref BoundingBox box, out float distance)
        {
            distance = 0f;
            float maxValue = float.MaxValue;
            if (Math.Abs(ray.Direction.X) < 1E-06f)
            {
                if ((ray.Position.X < box.Minimum.X) || (ray.Position.X > box.Maximum.X))
                {
                    distance = 0f;
                    return false;
                }
            }
            else
            {
                float num2 = 1f / ray.Direction.X;
                float num3 = (box.Minimum.X - ray.Position.X) * num2;
                float num4 = (box.Maximum.X - ray.Position.X) * num2;
                if (num3 > num4)
                {
                    float num5 = num3;
                    num3 = num4;
                    num4 = num5;
                }
                distance = Math.Max(num3, distance);
                maxValue = Math.Min(num4, maxValue);
                if (distance > maxValue)
                {
                    distance = 0f;
                    return false;
                }
            }
            if (Math.Abs(ray.Direction.Y) < 1E-06f)
            {
                if ((ray.Position.Y < box.Minimum.Y) || (ray.Position.Y > box.Maximum.Y))
                {
                    distance = 0f;
                    return false;
                }
            }
            else
            {
                float num6 = 1f / ray.Direction.Y;
                float num7 = (box.Minimum.Y - ray.Position.Y) * num6;
                float num8 = (box.Maximum.Y - ray.Position.Y) * num6;
                if (num7 > num8)
                {
                    float num9 = num7;
                    num7 = num8;
                    num8 = num9;
                }
                distance = Math.Max(num7, distance);
                maxValue = Math.Min(num8, maxValue);
                if (distance > maxValue)
                {
                    distance = 0f;
                    return false;
                }
            }
            if (Math.Abs(ray.Direction.Z) < 1E-06f)
            {
                if ((ray.Position.Z < box.Minimum.Z) || (ray.Position.Z > box.Maximum.Z))
                {
                    distance = 0f;
                    return false;
                }
            }
            else
            {
                float num10 = 1f / ray.Direction.Z;
                float num11 = (box.Minimum.Z - ray.Position.Z) * num10;
                float num12 = (box.Maximum.Z - ray.Position.Z) * num10;
                if (num11 > num12)
                {
                    float num13 = num11;
                    num11 = num12;
                    num12 = num13;
                }
                distance = Math.Max(num11, distance);
                maxValue = Math.Min(num12, maxValue);
                if (distance > maxValue)
                {
                    distance = 0f;
                    return false;
                }
            }
            return true;
        }

        public static bool RayIntersectsPlane(ref Ray ray, ref Plane plane, out Vector3 point)
        {
            float num;
            if (!RayIntersectsPlane(ref ray, ref plane, out num))
            {
                point = Vector3.Zero;
                return false;
            }
            point = ray.Position + ((Vector3)(ray.Direction * num));
            return true;
        }

        public static bool RayIntersectsPlane(ref Ray ray, ref Plane plane, out float distance)
        {
            float num;
            float num2;
            Vector3.Dot(ref plane.Normal, ref ray.Direction, out num);
            if (Math.Abs(num) < 1E-06f)
            {
                distance = 0f;
                return false;
            }
            Vector3.Dot(ref plane.Normal, ref ray.Position, out num2);
            distance = (-plane.D - num2) / num;
            if (distance < 0f)
            {
                if (distance < -1E-06f)
                {
                    distance = 0f;
                    return false;
                }
                distance = 0f;
            }
            return true;
        }

        public static bool RayIntersectsPoint(ref Ray ray, ref Vector3 point)
        {
            Vector3 vector;
            Vector3.Subtract(ref ray.Position, ref point, out vector);
            float num = Vector3.Dot(vector, ray.Direction);
            float num2 = Vector3.Dot(vector, vector) - 1E-06f;
            if ((num2 > 0f) && (num > 0f))
            {
                return false;
            }
            float num3 = (num * num) - num2;
            if (num3 < 0f)
            {
                return false;
            }
            return true;
        }

        public static bool RayIntersectsRay(ref Ray ray1, ref Ray ray2, out Vector3 point)
        {
            Vector3 vector;
            Vector3.Cross(ref ray1.Direction, ref ray2.Direction, out vector);
            float num = vector.Length();
            if (((Math.Abs(num) < 1E-06f) && (Math.Abs((float)(ray2.Position.X - ray1.Position.X)) < 1E-06f)) && ((Math.Abs((float)(ray2.Position.Y - ray1.Position.Y)) < 1E-06f) && (Math.Abs((float)(ray2.Position.Z - ray1.Position.Z)) < 1E-06f)))
            {
                point = Vector3.Zero;
                return true;
            }
            num *= num;
            float num2 = ray2.Position.X - ray1.Position.X;
            float num3 = ray2.Position.Y - ray1.Position.Y;
            float num4 = ray2.Position.Z - ray1.Position.Z;
            float x = ray2.Direction.X;
            float y = ray2.Direction.Y;
            float z = ray2.Direction.Z;
            float num8 = vector.X;
            float num9 = vector.Y;
            float num10 = vector.Z;
            float num11 = ((((((num2 * y) * num10) + ((num3 * z) * num8)) + ((num4 * x) * num9)) - ((num2 * z) * num9)) - ((num3 * x) * num10)) - ((num4 * y) * num8);
            x = ray1.Direction.X;
            y = ray1.Direction.Y;
            z = ray1.Direction.Z;
            float num12 = ((((((num2 * y) * num10) + ((num3 * z) * num8)) + ((num4 * x) * num9)) - ((num2 * z) * num9)) - ((num3 * x) * num10)) - ((num4 * y) * num8);
            float num13 = num11 / num;
            float num14 = num12 / num;
            Vector3 vector2 = ray1.Position + ((Vector3)(num13 * ray1.Direction));
            Vector3 vector3 = ray2.Position + ((Vector3)(num14 * ray2.Direction));
            if (((Math.Abs((float)(vector3.X - vector2.X)) > 1E-06f) || (Math.Abs((float)(vector3.Y - vector2.Y)) > 1E-06f)) || (Math.Abs((float)(vector3.Z - vector2.Z)) > 1E-06f))
            {
                point = Vector3.Zero;
                return false;
            }
            point = vector2;
            return true;
        }

        public static bool RayIntersectsSphere(ref Ray ray, ref BoundingSphere sphere, out Vector3 point)
        {
            float num;
            if (!RayIntersectsSphere(ref ray, ref sphere, out num))
            {
                point = Vector3.Zero;
                return false;
            }
            point = ray.Position + ((Vector3)(ray.Direction * num));
            return true;
        }

        public static bool RayIntersectsSphere(ref Ray ray, ref BoundingSphere sphere, out float distance)
        {
            Vector3 vector;
            Vector3.Subtract(ref ray.Position, ref sphere.Center, out vector);
            float num = Vector3.Dot(vector, ray.Direction);
            float num2 = Vector3.Dot(vector, vector) - (sphere.Radius * sphere.Radius);
            if ((num2 > 0f) && (num > 0f))
            {
                distance = 0f;
                return false;
            }
            float num3 = (num * num) - num2;
            if (num3 < 0f)
            {
                distance = 0f;
                return false;
            }
            distance = -num - ((float)Math.Sqrt((double)num3));
            if (distance < 0f)
            {
                distance = 0f;
            }
            return true;
        }

        public static bool RayIntersectsTriangle(ref Ray ray, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out Vector3 point)
        {
            float num;
            if (!RayIntersectsTriangle(ref ray, ref vertex1, ref vertex2, ref vertex3, out num))
            {
                point = Vector3.Zero;
                return false;
            }
            point = ray.Position + ((Vector3)(ray.Direction * num));
            return true;
        }

        public static bool RayIntersectsTriangle(ref Ray ray, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3, out float distance)
        {
            Vector3 vector;
            Vector3 vector2;
            Vector3 vector3;
            Vector3 vector4;
            Vector3 vector5;
            vector.X = vertex2.X - vertex1.X;
            vector.Y = vertex2.Y - vertex1.Y;
            vector.Z = vertex2.Z - vertex1.Z;
            vector2.X = vertex3.X - vertex1.X;
            vector2.Y = vertex3.Y - vertex1.Y;
            vector2.Z = vertex3.Z - vertex1.Z;
            vector3.X = (ray.Direction.Y * vector2.Z) - (ray.Direction.Z * vector2.Y);
            vector3.Y = (ray.Direction.Z * vector2.X) - (ray.Direction.X * vector2.Z);
            vector3.Z = (ray.Direction.X * vector2.Y) - (ray.Direction.Y * vector2.X);
            float num = ((vector.X * vector3.X) + (vector.Y * vector3.Y)) + (vector.Z * vector3.Z);
            if ((num > -1E-06f) && (num < 1E-06f))
            {
                distance = 0f;
                return false;
            }
            float num2 = 1f / num;
            vector4.X = ray.Position.X - vertex1.X;
            vector4.Y = ray.Position.Y - vertex1.Y;
            vector4.Z = ray.Position.Z - vertex1.Z;
            float num3 = ((vector4.X * vector3.X) + (vector4.Y * vector3.Y)) + (vector4.Z * vector3.Z);
            num3 *= num2;
            if ((num3 < 0f) || (num3 > 1f))
            {
                distance = 0f;
                return false;
            }
            vector5.X = (vector4.Y * vector.Z) - (vector4.Z * vector.Y);
            vector5.Y = (vector4.Z * vector.X) - (vector4.X * vector.Z);
            vector5.Z = (vector4.X * vector.Y) - (vector4.Y * vector.X);
            float num4 = ((ray.Direction.X * vector5.X) + (ray.Direction.Y * vector5.Y)) + (ray.Direction.Z * vector5.Z);
            num4 *= num2;
            if ((num4 < 0f) || ((num3 + num4) > 1f))
            {
                distance = 0f;
                return false;
            }
            float num5 = ((vector2.X * vector5.X) + (vector2.Y * vector5.Y)) + (vector2.Z * vector5.Z);
            num5 *= num2;
            if (num5 < 0f)
            {
                distance = 0f;
                return false;
            }
            distance = num5;
            return true;
        }

        public static ContainmentType SphereContainsBox(ref BoundingSphere sphere, ref BoundingBox box)
        {
            Vector3 vector;
            if (!BoxIntersectsSphere(ref box, ref sphere))
            {
                return ContainmentType.Disjoint;
            }
            float num = sphere.Radius * sphere.Radius;
            vector.X = sphere.Center.X - box.Minimum.X;
            vector.Y = sphere.Center.Y - box.Maximum.Y;
            vector.Z = sphere.Center.Z - box.Maximum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Maximum.X;
            vector.Y = sphere.Center.Y - box.Maximum.Y;
            vector.Z = sphere.Center.Z - box.Maximum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Maximum.X;
            vector.Y = sphere.Center.Y - box.Minimum.Y;
            vector.Z = sphere.Center.Z - box.Maximum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Minimum.X;
            vector.Y = sphere.Center.Y - box.Minimum.Y;
            vector.Z = sphere.Center.Z - box.Maximum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Minimum.X;
            vector.Y = sphere.Center.Y - box.Maximum.Y;
            vector.Z = sphere.Center.Z - box.Minimum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Maximum.X;
            vector.Y = sphere.Center.Y - box.Maximum.Y;
            vector.Z = sphere.Center.Z - box.Minimum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Maximum.X;
            vector.Y = sphere.Center.Y - box.Minimum.Y;
            vector.Z = sphere.Center.Z - box.Minimum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            vector.X = sphere.Center.X - box.Minimum.X;
            vector.Y = sphere.Center.Y - box.Minimum.Y;
            vector.Z = sphere.Center.Z - box.Minimum.Z;
            if (vector.LengthSquared() > num)
            {
                return ContainmentType.Intersects;
            }
            return ContainmentType.Contains;
        }

        public static ContainmentType SphereContainsPoint(ref BoundingSphere sphere, ref Vector3 point)
        {
            if (Vector3.DistanceSquared(point, sphere.Center) <= (sphere.Radius * sphere.Radius))
            {
                return ContainmentType.Contains;
            }
            return ContainmentType.Disjoint;
        }

        public static ContainmentType SphereContainsSphere(ref BoundingSphere sphere1, ref BoundingSphere sphere2)
        {
            float num = Vector3.Distance(sphere1.Center, sphere2.Center);
            if ((sphere1.Radius + sphere2.Radius) < num)
            {
                return ContainmentType.Disjoint;
            }
            if ((sphere1.Radius - sphere2.Radius) < num)
            {
                return ContainmentType.Intersects;
            }
            return ContainmentType.Contains;
        }

        public static ContainmentType SphereContainsTriangle(ref BoundingSphere sphere, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            ContainmentType type = SphereContainsPoint(ref sphere, ref vertex1);
            ContainmentType type2 = SphereContainsPoint(ref sphere, ref vertex2);
            ContainmentType type3 = SphereContainsPoint(ref sphere, ref vertex3);
            if (((type == ContainmentType.Contains) && (type2 == ContainmentType.Contains)) && (type3 == ContainmentType.Contains))
            {
                return ContainmentType.Contains;
            }
            if (SphereIntersectsTriangle(ref sphere, ref vertex1, ref vertex2, ref vertex3))
            {
                return ContainmentType.Intersects;
            }
            return ContainmentType.Disjoint;
        }

        public static bool SphereIntersectsSphere(ref BoundingSphere sphere1, ref BoundingSphere sphere2)
        {
            float num = sphere1.Radius + sphere2.Radius;
            return (Vector3.DistanceSquared(sphere1.Center, sphere2.Center) <= (num * num));
        }

        public static bool SphereIntersectsTriangle(ref BoundingSphere sphere, ref Vector3 vertex1, ref Vector3 vertex2, ref Vector3 vertex3)
        {
            Vector3 vector;
            float num;
            ClosestPointPointTriangle(ref sphere.Center, ref vertex1, ref vertex2, ref vertex3, out vector);
            Vector3 left = vector - sphere.Center;
            Vector3.Dot(ref left, ref left, out num);
            return (num <= (sphere.Radius * sphere.Radius));
        }
    }

    public enum ContainmentType
    {
        Disjoint,
        Contains,
        Intersects
    }

    public enum PlaneIntersectionType
    {
        Back,
        Front,
        Intersecting
    }
}