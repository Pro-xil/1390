using System.Collections.Generic;
using OfflineVoxelMining.Core;
using UnityEngine;

namespace OfflineVoxelMining.Events
{
    public readonly struct GameModeChangedEvent : IGameEvent
    {
        public GameModeChangedEvent(GameMode mode) => Mode = mode;
        public GameMode Mode { get; }
    }

    public readonly struct ChunkLoadedEvent : IGameEvent
    {
        public ChunkLoadedEvent(Vector3Int chunkCoord) => ChunkCoord = chunkCoord;
        public Vector3Int ChunkCoord { get; }
    }

    public readonly struct ResourceCollectedEvent : IGameEvent
    {
        public ResourceCollectedEvent(string resourceId, int amount)
        {
            ResourceId = resourceId;
            Amount = amount;
        }

        public string ResourceId { get; }
        public int Amount { get; }
    }

    public readonly struct VehicleTelemetryEvent : IGameEvent
    {
        public VehicleTelemetryEvent(float wheelForce, float engineTemp, float energyRate, float stress)
        {
            WheelForce = wheelForce;
            EngineTemperature = engineTemp;
            EnergyRate = energyRate;
            MechanicalStress = stress;
        }

        public float WheelForce { get; }
        public float EngineTemperature { get; }
        public float EnergyRate { get; }
        public float MechanicalStress { get; }
    }

    public readonly struct HazardTriggeredEvent : IGameEvent
    {
        public HazardTriggeredEvent(string hazardId, Vector3 position)
        {
            HazardId = hazardId;
            Position = position;
        }

        public string HazardId { get; }
        public Vector3 Position { get; }
    }

    public readonly struct ModsReloadedEvent : IGameEvent
    {
        public ModsReloadedEvent(IReadOnlyList<string> loadedMods) => LoadedMods = loadedMods;
        public IReadOnlyList<string> LoadedMods { get; }
    }
}
