using System;
using OpenTK;
using OpenTK.Graphics;

namespace TAO.Engine.OpenGLCore
{
    public static class OpenGLMath
    {
        //TVector To Vector
        public static Vector2 TVectorToVector(TVector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector3 TVectorToVector(TVector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public static Vector4 TVectorToVector(TVector4 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }

        //Vector To TVector
        public static TVector2 TVectorToVector(Vector2 vector)
        {
            return new TVector2(vector.X, vector.Y);
        }

        public static TVector3 TVectorToVector(Vector3 vector)
        {
            return new TVector3(vector.X, vector.Y, vector.Z);
        }

        public static TVector4 TVectorToVector(Vector4 vector)
        {
            return new TVector4(vector.X, vector.Y, vector.Z, vector.W);
        }

        //TVector To Color4
        public static Color4 VectorToColor(TVector3 vector)
        {
            return new Color4(vector.X, vector.Y, vector.Z, 1);
        }

        public static Color4 VectorToColor(TVector4 vector)
        {
            return new Color4(vector.X, vector.Y, vector.Z, vector.W);
        }

        //Vector To Color4
        public static Color4 VectorToColor(Vector3 vector)
        {
            return new Color4(vector.X, vector.Y, vector.Z, 1);
        }

        public static Color4 VectorToColor(Vector4 vector)
        {
            return new Color4(vector.X, vector.Y, vector.Z, vector.W);
        }
    }
}