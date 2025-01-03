using UnityEngine.Scripting;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class AsteroidsCollisionsSystem : SystemSignalsOnly, ISignalOnCollisionEnter2D
    {
        public void OnCollisionEnter2D(Frame f, CollisionInfo2D info)
        {
            // Projectile is colliding with something
            if (f.Unsafe.TryGetPointer<AsteroidsProjectile>(info.Entity, out var projectile))
            {
                if (f.Unsafe.TryGetPointer<AsteroidsShip>(info.Other, out var ship))
                {
                    // Projectile Hit Ship
                    f.Signals.OnCollisionProjectileHitShip(info, projectile, ship);
                }
                else if (f.Unsafe.TryGetPointer<AsteroidsAsteroid>(info.Other, out var asteroid))
                {
                    // projectile Hit Asteroid
                    f.Signals.OnCollisionProjectileHitAsteroid(info, projectile, asteroid);
                }
            }

            // Ship is colliding with something
            else if (f.Unsafe.TryGetPointer<AsteroidsShip>(info.Entity, out var ship))
            {
                if (f.Unsafe.TryGetPointer<AsteroidsAsteroid>(info.Other, out var asteroid))
                {
                    // Asteroid Hit Ship
                    f.Signals.OnCollisionAsteroidHitShip(info, ship, asteroid);
                }
            }
        }
    }
}