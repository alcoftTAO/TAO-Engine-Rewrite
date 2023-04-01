using System;

namespace TAO.Engine
{
    public class TCollider
    {
        public TVector3 Offset = TVector3.Zero;
        public TVector3 Size = TVector3.One;
        public bool Enabled = true;

        public TCollider()
        {

        }

        public TCollider(TVector3 Offset, TVector3 Size)
        {
            this.Offset = Offset;
            this.Size = Size;
        }

        public TCollider(TVector3 Offset, TVector3 Size, bool Enabled)
        {
            this.Offset = Offset;
            this.Size = Size;
            this.Enabled = Enabled;
        }

        public static bool Colliding(TCollider A, TCollider B)
        {
            bool x = (B.Offset.X + B.Size.X / 2 >= A.Offset.X - A.Size.X / 2) &&
                (B.Offset.X - B.Size.X / 2 <= A.Offset.X + A.Size.X / 2);
            bool y = (B.Offset.Y + B.Size.Y / 2 >= A.Offset.Y - A.Size.Y / 2) &&
                (B.Offset.Y - B.Size.Y / 2 <= A.Offset.Y + A.Size.Y / 2);
            bool z = (B.Offset.Z + B.Size.Z / 2 >= A.Offset.Z - A.Size.Z / 2) &&
                (B.Offset.Z - B.Size.Z / 2 <= A.Offset.Z + A.Size.Z / 2);

            return x && y && z && (A.Enabled && B.Enabled) && (A != B);
        }

        public bool Colliding(TCollider B)
        {
            return Colliding(this, B);
        }
    }
}