using OpenTK;
using System;
using System.Collections.Generic;

namespace FoldingXNA
{
    public static class Mesh
    {
        const int RES = 10;
        static Mesh()
        {
            InitializeCube();
            InitializeCircle();
            InitializeSphere();
            InitializePrism();
        }

        static void InitializeCube()
        {
            CubeVertices = new Vector3[]{
                    new Vector3(-1.0f, -1.0f,  1.0f),
                    new Vector3( 1.0f, -1.0f,  1.0f),
                    new Vector3( 1.0f,  1.0f,  1.0f),
                    new Vector3(-1.0f,  1.0f,  1.0f),
                    new Vector3(-1.0f, -1.0f, -1.0f),
                    new Vector3( 1.0f, -1.0f, -1.0f), 
                    new Vector3( 1.0f,  1.0f, -1.0f),
                    new Vector3(-1.0f,  1.0f, -1.0f) };
            CubeIndices = new uint[]{
                    // front face
                    0, 1, 2, 2, 3, 0,
                    // top face
                    3, 2, 6, 6, 7, 3,
                    // back face
                    7, 6, 5, 5, 4, 7,
                    // left face
                    4, 0, 3, 3, 7, 4,
                    // bottom face
                    0, 1, 5, 5, 4, 0,
                    // right face
                    1, 5, 6, 6, 2, 1, };
        }
        static void InitializeCircle()
        {
            GenCircle(RES, .0025F);
        }
        static void InitializeSphere()
        {
            GenerateSpiralSphere(1, RES, RES);
        }
        static void InitializePrism()
        {
            PrismVerticies = new Vector3[] { 
                new Vector3(0.0F, 0.86602540378f, 0), // b
                new Vector3( .5F, 0,              0), // a
                new Vector3(-.5F, 0,              0), // c

                new Vector3(0.0F, 0.86602540378f, 1),  // f
                new Vector3( .5F, 0,              1), // d
                new Vector3(-.5F, 0,              1), // e
            };

            PrismIndices = new uint[]{
                4, 1, 3, 0, 5, 2, 4, 1,
            };
        }
        static void GenCircle(int res, float scale)
        {
            double angle = MathHelper.TwoPi / res;
            CircleMeshType = 6; // Triangle Fan

            CircleVerticies = new float[(res + 1) * 3];
            CircleVerticies[0] = CircleVerticies[1] = CircleVerticies[2] = 0;

            for (int i = 3; i <= (res * 3); i += 3)
            {
                CircleVerticies[i + 0] = (float)Math.Round(Math.Sin(angle * i) * scale, 4);
                CircleVerticies[i + 1] = (float)Math.Round(Math.Cos(angle * i) * scale, 4);
                CircleVerticies[i + 2] = 0;
            }
        }
        static void GenerateSpiralSphere(float radius, int loops, int segmentsPerLoop)
        {
            List<Vector3> geometryData_ = new List<Vector3>();
            List<uint> indexData_ = new List<uint>();
            List<float> gfloat = new List<float>();
            //SphereMeshType = 5; // Triangle Strip

            for (uint loopSegmentNumber = 0; loopSegmentNumber < segmentsPerLoop; loopSegmentNumber++)
            {
                float theta = 0;
                float phi = (float)(loopSegmentNumber * 2 * MathHelper.Pi / segmentsPerLoop);
                float sinTheta = (float)Math.Sin(theta);
                float sinPhi = (float)Math.Sin(phi);
                float cosTheta = (float)Math.Cos(theta);
                float cosPhi = (float)Math.Cos(phi);

                float x = radius * cosPhi * sinTheta, y = radius * sinPhi * sinTheta, z = radius * cosTheta;
                geometryData_.Add(new Vector3(x, y, z));
            }
            for (int loopNumber = 0; loopNumber <= loops; ++loopNumber)
            {
                for (int loopSegmentNumber = 0; loopSegmentNumber < segmentsPerLoop; loopSegmentNumber++)
                {
                    float theta = (loopNumber * MathHelper.Pi / loops) + ((MathHelper.Pi * loopSegmentNumber) / (segmentsPerLoop * loops));
                    if (loopNumber == loops)
                    {
                        theta = MathHelper.Pi;
                    }
                    float phi = loopSegmentNumber * 2 * MathHelper.Pi / segmentsPerLoop;
                    float sinTheta = (float)Math.Sin(theta);
                    float sinPhi = (float)Math.Sin(phi);
                    float cosTheta = (float)Math.Cos(theta);
                    float cosPhi = (float)Math.Cos(phi);


                    float x = radius * cosPhi * sinTheta, y = radius * sinPhi * sinTheta, z = radius * cosTheta;
                    geometryData_.Add(new Vector3(x, y, z));
                }
            }
            for (int loopSegmentNumber = 0; loopSegmentNumber < segmentsPerLoop; loopSegmentNumber++)
            {
                indexData_.Add((uint)(loopSegmentNumber));
                indexData_.Add((uint)(segmentsPerLoop + loopSegmentNumber));
            }
            for (int loopNumber = 0; loopNumber < loops; loopNumber++)
            {
                for (int loopSegmentNumber = 0; loopSegmentNumber < segmentsPerLoop; loopSegmentNumber++)
                {
                    indexData_.Add((uint)(((loopNumber + 1) * segmentsPerLoop) + loopSegmentNumber));
                    indexData_.Add((uint)(((loopNumber + 2) * segmentsPerLoop) + loopSegmentNumber));
                }
            }

            /*
            foreach (int ui in indexData_)
            {
                gfloat.Add(geometryData_[ui].X);
                gfloat.Add(geometryData_[ui].Y);
                gfloat.Add(geometryData_[ui].Z);
            }
            SphereVerticies = gfloat.ToArray();
            */

            SphereVerticies = geometryData_.ToArray();
            SphereIndices = indexData_.ToArray();
        }

        public static float[] CircleVerticies { get; private set; }
        public static int CircleMeshType { get; private set; }
        public static Vector3[] SphereVerticies { get; private set; }
        public static uint[] SphereIndices { get; private set; }
        public static Vector3[] CubeVertices { get; private set; }
        public static uint[] CubeIndices { get; private set; }
        public static Vector3[] PrismVerticies { get; private set; }
        public static uint[] PrismIndices { get; private set; }
    }
}
