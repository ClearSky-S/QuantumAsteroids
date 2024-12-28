using Photon.Deterministic;
using UnityEngine;

namespace Quantum.Asteroids
{
    public class AsteroidsGameConfig : AssetObject
    {
        [Header("Asteroids configuration")]
        [Tooltip("Prototype reference to spawn asteroids")]
        public AssetRef<EntityPrototype> AsteroidPrototype;

        [Tooltip("Speed applied to the asteroid when spawned")]
        public FP AsteroidInitialSpeed = 8;

        [Tooltip("Minimum torque applied to the asteroid when spawned")]
        public FP AsteroidInitialTorqueMin = 7;

        [Tooltip("Maximum torque applied to the asteroid when spawned")]
        public FP AsteroidInitialTorqueMax = 20;

        [Tooltip(
            "Distance to the center of the map. This value is the radius in a random circular location where the asteroid is spawned")]
        public FP AsteroidSpawnDistanceToCenter = 20;

        [Tooltip(
            "Amount of asteroids spawned in level 1. In each level, the number os asteroids spawned is increased by one")]
        public int InitialAsteroidsCount = 5;
    }
}