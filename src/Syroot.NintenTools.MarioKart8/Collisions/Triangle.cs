namespace Syroot.NintenTools.MarioKart8.Collisions
{
    /// <summary>
    /// Represents a triangle as stored in a collision file.
    /// </summary>
    public struct Triangle
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        public float Length;
        public ushort PositionIndex;
        public ushort DirectionIndex;
        public ushort NormalAIndex;
        public ushort NormalBIndex;
        public ushort NormalCIndex;
        public ushort CollisionFlags;
        public uint TriangleIndex;

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        internal Triangle(float length, ushort positionIndex, ushort directionIndex, ushort normalAIndex,
            ushort normalBIndex, ushort normalCIndex, ushort collisionFlags, uint triangleIndex)
        {
            Length = length;
            PositionIndex = positionIndex;
            DirectionIndex = directionIndex;
            NormalAIndex = normalAIndex;
            NormalBIndex = normalBIndex;
            NormalCIndex = normalCIndex;
            CollisionFlags = collisionFlags;
            TriangleIndex = triangleIndex;
        }
    }
}
