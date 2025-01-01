using Photon.Deterministic;
using UnityEngine;

namespace Quantum
{
    public class AsteroidsProjectileConfig : AssetObject
    {
        [Tooltip("Speed applied to the projectile when spawned")]
        public FP ProjectileInitialSpeed = 15;

        [Tooltip("Time until destroy the projectile")]
        public FP ProjectileTTL = 1;
    }
}