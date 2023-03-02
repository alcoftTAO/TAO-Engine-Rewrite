using System;

namespace TAO.Engine
{
    public class TVector2
    {
        public static TVector2 Zero = new TVector2(0, 0);
        public static TVector2 One = new TVector2(1, 1);
        public float X;
        public float Y;

        public TVector2(float X = 0, float Y = 0)
        {
            this.X = X;
            this.Y = Y;
        }

        public static TVector2 operator +(TVector2 a, TVector2 b)
        {
            return new TVector2(a.X + b.X, a.Y + b.Y);
        }

        public static TVector2 operator +(TVector2 a, float b)
        {
            return new TVector2(a.X + b, a.Y + b);
        }

        public static TVector2 operator -(TVector2 a, TVector2 b)
        {
            return new TVector2(a.X - b.X, a.Y - b.Y);
        }

        public static TVector2 operator -(TVector2 a, float b)
        {
            return new TVector2(a.X - b, a.Y - b);
        }

        public static TVector2 operator -(TVector2 a)
        {
            return new TVector2(-a.X, -a.Y);
        }

        public static TVector2 operator *(TVector2 a, TVector2 b)
        {
            return new TVector2(a.X * b.X, a.Y * b.Y);
        }

        public static TVector2 operator *(TVector2 a, float b)
        {
            return new TVector2(a.X * b, a.Y * b);
        }

        public static TVector2 operator /(TVector2 a, TVector2 b)
        {
            return new TVector2(a.X / b.X, a.Y / b.Y);
        }

        public static TVector2 operator /(TVector2 a, float b)
        {
            return new TVector2(a.X / b, a.Y / b);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }
    }

    public class TVector3
    {
        public static TVector3 Zero = new TVector3(0, 0, 0);
        public static TVector3 One = new TVector3(1, 1, 1);
        public float X;
        public float Y;
        public float Z;

        public TVector3(float X = 0, float Y = 0, float Z = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public static TVector3 operator +(TVector3 a, TVector3 b)
        {
            return new TVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static TVector3 operator +(TVector3 a, float b)
        {
            return new TVector3(a.X + b, a.Y + b, a.Z + b);
        }

        public static TVector3 operator -(TVector3 a, TVector3 b)
        {
            return new TVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static TVector3 operator -(TVector3 a, float b)
        {
            return new TVector3(a.X - b, a.Y - b, a.Z - b);
        }

        public static TVector3 operator -(TVector3 a)
        {
            return new TVector3(-a.X, -a.Y, -a.Z);
        }

        public static TVector3 operator *(TVector3 a, TVector3 b)
        {
            return new TVector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static TVector3 operator *(TVector3 a, float b)
        {
            return new TVector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static TVector3 operator /(TVector3 a, TVector3 b)
        {
            return new TVector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static TVector3 operator /(TVector3 a, float b)
        {
            return new TVector3(a.X / b, a.Y / b, a.Z / b);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }
    }

    public class TVector4
    {
        public static TVector4 Zero = new TVector4(0, 0, 0, 0);
        public static TVector4 One = new TVector4(1, 1, 1, 1);
        public float X;
        public float Y;
        public float Z;
        public float W;

        public TVector4(float X = 0, float Y = 0, float Z = 0, float W = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }

        public static TVector4 operator +(TVector4 a, TVector4 b)
        {
            return new TVector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static TVector4 operator +(TVector4 a, float b)
        {
            return new TVector4(a.X + b, a.Y + b, a.Z + b, a.W + b);
        }

        public static TVector4 operator -(TVector4 a, TVector4 b)
        {
            return new TVector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static TVector4 operator -(TVector4 a, float b)
        {
            return new TVector4(a.X - b, a.Y - b, a.Z - b, a.W - b);
        }

        public static TVector4 operator -(TVector4 a)
        {
            return new TVector4(-a.X, -a.Y, -a.Z, -a.W);
        }

        public static TVector4 operator *(TVector4 a, TVector4 b)
        {
            return new TVector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        public static TVector4 operator *(TVector4 a, float b)
        {
            return new TVector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        public static TVector4 operator /(TVector4 a, TVector4 b)
        {
            return new TVector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        public static TVector4 operator /(TVector4 a, float b)
        {
            return new TVector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ", " + W + ")";
        }
    }
}