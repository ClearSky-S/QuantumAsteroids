using Photon.Deterministic;
using UnityEngine.Scripting;

namespace Quantum.Asteroids
{
    [Preserve]
    public unsafe class AsteroidsShipSystem : SystemMainThreadFilter<AsteroidsShipSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform2D* Transform;
            public PhysicsBody2D* Body;
            public AsteroidsShip* AsteroidsShip;
        }
        public override void Update(Frame f, ref Filter filter)
        {
            Input* input = default;
            if(f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                input = f.GetPlayerInput(playerLink->PlayerRef);
            }

            UpdateShipMovement(f, ref filter, input);
            UpdateShipFire(f, ref filter, input);
        }
        
        private void UpdateShipMovement(Frame f, ref Filter filter, Input* input)
        {
            var config = f.FindAsset(filter.AsteroidsShip->ShipConfig);
            FP shipAcceleration = config.ShipAceleration;
            FP turnSpeed = config.ShipTurnSpeed;

            if (input->Up)
            {
                filter.Body->AddForce(filter.Transform->Up * shipAcceleration);
            }

            if (input->Left)
            {
                filter.Body->AddTorque(turnSpeed);
            }

            if (input->Right)
            {
                filter.Body->AddTorque(-turnSpeed);
            }

            filter.Body->AngularVelocity = FPMath.Clamp(filter.Body->AngularVelocity, -turnSpeed, turnSpeed);
        }
        private void UpdateShipFire(Frame f, ref Filter filter, Input* input)
        {
            var config = f.FindAsset(filter.AsteroidsShip->ShipConfig);

            if (input->Fire && filter.AsteroidsShip->FireInterval <= 0)
            {
                filter.AsteroidsShip->FireInterval = config.FireInterval;
                var relativeOffset = FPVector2.Up * config.ShotOffset;
                var spawnPosition = filter.Transform->TransformPoint(relativeOffset);
                f.Signals.AsteroidsShipShoot(filter.Entity, spawnPosition, config.ProjectilePrototype);
            }
            else
            {
                filter.AsteroidsShip->FireInterval -= f.DeltaTime;
            }
        }
    }
}