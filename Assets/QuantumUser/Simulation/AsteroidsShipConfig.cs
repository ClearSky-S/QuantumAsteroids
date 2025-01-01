using Photon.Deterministic;
using UnityEngine;

namespace Quantum
{
    public class AsteroidsShipConfig : AssetObject
    {
        [Tooltip("The speed that the ship turns with added as torque")]
        public FP ShipTurnSpeed = 8;

        [Tooltip("The speed that the ship accelerates using add force")]
        public FP ShipAceleration = 6;

        [Tooltip("Time interval between ship shots")]
        public FP FireInterval = FP._0_10;

        [Tooltip("Displacement of the projectile spawn position related to the ship position")]
        public FP ShotOffset = 1;

        [Tooltip("Prototype reference to spawn ship projectiles")]
        public AssetRef<EntityPrototype> ProjectilePrototype;
    }
}