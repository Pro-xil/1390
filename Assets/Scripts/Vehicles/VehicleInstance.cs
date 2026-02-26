using System.Linq;
using OfflineVoxelMining.Telemetry;
using UnityEngine;

namespace OfflineVoxelMining.Vehicles
{
    public sealed class VehicleInstance
    {
        private readonly VehicleDefinition definition;

        public VehicleInstance(VehicleDefinition definition)
        {
            this.definition = definition;
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            Debug.Log($"Spawned vehicle {definition.Id} at {position}");
        }

        public Vector3 CalculateCenterOfMass()
        {
            if (definition.Blocks.Count == 0)
            {
                return Vector3.zero;
            }

            var totalMass = definition.Blocks.Sum(b => b.Mass);
            if (totalMass <= 0f)
            {
                return Vector3.zero;
            }

            var weighted = Vector3.zero;
            foreach (var block in definition.Blocks)
            {
                weighted += block.LocalPosition * block.Mass;
            }

            return weighted / totalMass;
        }

        public VehicleTelemetrySnapshot ReadTelemetry()
        {
            var mass = Mathf.Max(1f, definition.Blocks.Sum(b => b.Mass));
            return new VehicleTelemetrySnapshot(
                wheelForce: definition.BaseTorque / mass,
                engineTemperature: 75f,
                energyRate: 15f,
                mechanicalStress: mass / Mathf.Max(1f, definition.SuspensionStrength));
        }
    }
}
