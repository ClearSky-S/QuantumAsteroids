using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class AsteroidsProjectileSystem : SystemMainThreadFilter<AsteroidsProjectileSystem.Filter>, ISignalAsteroidsShipShoot, ISignalOnCollisionProjectileHitAsteroid
    {
        public struct Filter
        {
            public EntityRef Entity;
            public AsteroidsProjectile* Projectile;
        }
        public override void Update(Frame f, ref Filter filter)
        {
            filter.Projectile->TTL -= f.DeltaTime;
            if (filter.Projectile->TTL <= 0)
            {
                f.Destroy(filter.Entity);
            }
        }
        public void AsteroidsShipShoot(Frame f, EntityRef owner, FPVector2 spawnPosition, AssetRef<EntityPrototype> projectilePrototype)
        {
            EntityRef projectileEntity = f.Create(projectilePrototype);
            Transform2D* projectileTransform = f.Unsafe.GetPointer<Transform2D>(projectileEntity);
            Transform2D* ownerTransform = f.Unsafe.GetPointer<Transform2D>(owner);


            projectileTransform->Rotation = ownerTransform->Rotation;
            projectileTransform->Position = spawnPosition;
            
            AsteroidsProjectile* projectile = f.Unsafe.GetPointer<AsteroidsProjectile>(projectileEntity);
            var config = f.FindAsset(projectile->ProjectileConfig);
            projectile->TTL = config.ProjectileTTL;
            projectile->Owner = owner;

            PhysicsBody2D* body = f.Unsafe.GetPointer<PhysicsBody2D>(projectileEntity);
            body->Velocity = ownerTransform->Up * config.ProjectileInitialSpeed;
        }
        
        public void OnCollisionProjectileHitAsteroid(Frame f, CollisionInfo2D info, AsteroidsProjectile* projectile, AsteroidsAsteroid* asteroid)
        {
            if (asteroid->ChildAsteroid != null)
            {
                f.Signals.SpawnAsteroid(asteroid->ChildAsteroid, info.Other);
                f.Signals.SpawnAsteroid(asteroid->ChildAsteroid, info.Other);
            }

            f.Destroy(info.Entity);
            f.Destroy(info.Other);
        }
    }
}