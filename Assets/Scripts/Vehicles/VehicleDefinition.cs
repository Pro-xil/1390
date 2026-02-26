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
        public float BaseTorque;
        public float SuspensionStrength;
        public float FuelCapacity;
    }

    [Serializable]
    public sealed class VehicleBlock
    {
        public string BlockType;
        public Vector3 LocalPosition;
        public float Mass;
        public int Durability;
    }
}
