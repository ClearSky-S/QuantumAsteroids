using UnityEngine.Scripting;
using Photon.Deterministic;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class BoundarySystem : SystemMainThreadFilter<BoundarySystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform2D* Transform;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (IsOutOfBounds(filter.Transform->Position, new FPVector2(10, 10), out FPVector2 newPosition))
            {
                filter.Transform->Position = newPosition;
                filter.Transform->Teleport(f, newPosition);
            }
        }

        /// <summary>
        /// Test if a position is out of bounds and provide a warped position.
        /// When the entity leaves the bounds it will emerge on the other side.
        /// </summary>
        public bool IsOutOfBounds(FPVector2 position, FPVector2 mapExtends, out FPVector2 newPosition)
        {
            newPosition = position;

            if (position.X >= -mapExtends.X && position.X <= mapExtends.X &&
                position.Y >= -mapExtends.Y && position.Y <= mapExtends.Y)
            {
                // position is inside map bounds
                return false;
            }

            // warp x position
            if (position.X < -mapExtends.X)
            {
                newPosition.X = mapExtends.X;
            }
            else if (position.X > mapExtends.X)
            {
                newPosition.X = -mapExtends.X;
            }

            // warp y position
            if (position.Y < -mapExtends.Y)
            {
                newPosition.Y = mapExtends.Y;
            }
            else if (position.Y > mapExtends.Y)
            {
                newPosition.Y = -mapExtends.Y;
            }

            return true;
        }
    }
}