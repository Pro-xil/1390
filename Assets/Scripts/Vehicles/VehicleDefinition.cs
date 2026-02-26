using System;
using System.Collections.Generic;
using UnityEngine;

namespace OfflineVoxelMining.Vehicles
{
    [Serializable]
    public sealed class VehicleDefinition
    {
        public string Id;
        public List<VehicleBlock> Blocks = new();
        public List<WheelSpec> Wheels = new();
        public float BaseTorque;
        public float SuspensionStrength;
        public float FuelCapacity;
        public float EngineEfficiency = 1f;
    }

    [Serializable]
    public sealed class VehicleBlock
    {
        public string BlockType;
        public Vector3 LocalPosition;
        public float Mass;
        public int Durability;
    }

    [Serializable]
    public sealed class WheelSpec
    {
        public Vector3 LocalPosition;
        public float Radius;
        public bool IsDriveWheel;
    }
}
