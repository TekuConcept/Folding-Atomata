using System;
using System.Collections.Generic;

namespace XNA
{
    public static class MathUtil
    {
        // Fields
        public const float Pi = 3.141593f;
        public const float PiOverFour = 0.7853982f;
        public const float PiOverTwo = 1.570796f;
        public const float TwoPi = 6.283185f;
        public const float ZeroTolerance = 1E-06f;

        // Methods
        public static T[] Array<T>(T value, int count)
        {
            T[] localArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                localArray[i] = value;
            }
            return localArray;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            if (value <= max)
            {
                return value;
            }
            return max;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            if (value <= max)
            {
                return value;
            }
            return max;
        }

        public static float DegreesToRadians(float degree)
        {
            return (degree * 0.01745329f);
        }

        public static float DegreesToRevolutions(float degree)
        {
            return (degree / 360f);
        }

        public static double Gauss(double amplitude, double x, double y, double radX, double radY, double sigmaX, double sigmaY)
        {
            double num = amplitude * 2.718281828;
            return (num - ((Math.Pow(x - (radX / 2.0), 2.0) / (2.0 * Math.Pow(sigmaX, 2.0))) + (Math.Pow(y - (radY / 2.0), 2.0) / (2.0 * Math.Pow(sigmaY, 2.0)))));
        }

        public static float Gauss(float amplitude, float x, float y, float radX, float radY, float sigmaX, float sigmaY)
        {
            float num = amplitude * 2.718282f;
            return (num - ((float)((Math.Pow((double)(x - (radX / 2f)), 2.0) / ((double)(2f * ((float)Math.Pow((double)sigmaX, 2.0))))) + (Math.Pow((double)(y - (radY / 2f)), 2.0) / ((double)(2f * ((float)Math.Pow((double)sigmaY, 2.0))))))));
        }

        public static float GradiansToDegrees(float gradian)
        {
            return (gradian * 0.9f);
        }

        public static float GradiansToRadians(float gradian)
        {
            return (gradian * 0.01570796f);
        }

        public static float GradiansToRevolutions(float gradian)
        {
            return (gradian / 400f);
        }

        public static float Mod(float value, float modulo)
        {
            if (modulo == 0f)
            {
                return value;
            }
            return (value - ((float)(modulo * Math.Floor((double)(value / modulo)))));
        }

        public static float Mod2PI(float value)
        {
            return Mod(value, 6.283185f);
        }

        public static Color NextColor(this Random random)
        {
            return new Color(random.NextFloat(0f, 1f), random.NextFloat(0f, 1f), random.NextFloat(0f, 1f), 1f);
        }

        public static Color NextColor(this Random random, float minBrightness, float maxBrightness)
        {
            return new Color(random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minBrightness, maxBrightness), 1f);
        }

        public static Color NextColor(this Random random, float minBrightness, float maxBrightness, float alpha)
        {
            return new Color(random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minBrightness, maxBrightness), alpha);
        }

        public static Color NextColor(this Random random, float minBrightness, float maxBrightness, float minAlpha, float maxAlpha)
        {
            return new Color(random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minBrightness, maxBrightness), random.NextFloat(minAlpha, maxAlpha));
        }

        public static double NextDouble(this Random random, double min, double max)
        {
            return (min + (random.NextDouble() * (max - min)));
        }

        public static DrawingPoint NextDPoint(this Random random, DrawingPoint min, DrawingPoint max)
        {
            return new DrawingPoint(random.Next(min.X, max.X), random.Next(min.Y, max.Y));
        }

        public static DrawingPointF NextDPointF(this Random random, DrawingPointF min, DrawingPointF max)
        {
            return new DrawingPointF(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y));
        }

        public static float NextFloat(this Random random, float min, float max)
        {
            return (min + ((float)(random.NextDouble() * (max - min))));
        }

        public static long NextLong(this Random random)
        {
            byte[] buffer = new byte[8];
            random.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static long NextLong(this Random random, long min, long max)
        {
            byte[] buffer = new byte[8];
            random.NextBytes(buffer);
            return (Math.Abs((long)(BitConverter.ToInt64(buffer, 0) % (max - min))) + min);
        }

        public static TimeSpan NextTimespan(this Random random, TimeSpan min, TimeSpan max)
        {
            return TimeSpan.FromTicks(random.NextLong(min.Ticks, max.Ticks));
        }

        public static Vector2 NextVector2(this Random random, Vector2 min, Vector2 max)
        {
            return new Vector2(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y));
        }

        public static Vector3 NextVector3(this Random random, Vector3 min, Vector3 max)
        {
            return new Vector3(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y), random.NextFloat(min.Z, max.Z));
        }

        public static Vector4 NextVector4(this Random random, Vector4 min, Vector4 max)
        {
            return new Vector4(random.NextFloat(min.X, max.X), random.NextFloat(min.Y, max.Y), random.NextFloat(min.Z, max.Z), random.NextFloat(min.W, max.W));
        }

        public static float RadiansToDegrees(float radian)
        {
            return (radian * 57.29578f);
        }

        public static float RadiansToGradians(float radian)
        {
            return (radian * 63.66198f);
        }

        public static float RadiansToRevolutions(float radian)
        {
            return (radian / 6.283185f);
        }

        public static float RevolutionsToDegrees(float revolution)
        {
            return (revolution * 360f);
        }

        public static float RevolutionsToGradians(float revolution)
        {
            return (revolution * 400f);
        }

        public static float RevolutionsToRadians(float revolution)
        {
            return (revolution * 6.283185f);
        }

        public static bool WithinEpsilon(float a, float b)
        {
            float num = a - b;
            return ((-1.401298E-45f <= num) && (num <= float.Epsilon));
        }

        public static int Wrap(int value, int min, int max)
        {
            if (min > max)
            {
                int num = min;
                min = max;
                max = num;
            }
            value -= min;
            int num2 = max - min;
            if (num2 == 0)
            {
                return max;
            }
            return ((value - (num2 * (value / num2))) + min);
        }

        public static float Wrap(float value, float min, float max)
        {
            if (min > max)
            {
                float num = min;
                min = max;
                max = num;
            }
            value -= min;
            float num2 = max - min;
            if (num2 == 0f)
            {
                return max;
            }
            return ((value - ((float)(num2 * Math.Floor((double)(value / num2))))) + min);
        }
    }
}
