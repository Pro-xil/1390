using System.Linq;
using OfflineVoxelMining.Telemetry;
using UnityEngine;

namespace OfflineVoxelMining.Vehicles
{
    public sealed class VehicleInstance
    {
        private readonly VehicleDefinition definition;
        private float fuel;

        public VehicleInstance(VehicleDefinition definition)
        {
            this.definition = definition;
            fuel = Mathf.Max(1f, definition.FuelCapacity);
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            Debug.Log($"Spawned vehicle {definition.Id} at {position}");
        }

        public Vector3 CalculateCenterOfMass()
        {
            if (definition.Blocks.Count == 0) return Vector3.zero;

            var totalMass = definition.Blocks.Sum(b => b.Mass);
            if (totalMass <= 0f) return Vector3.zero;

            var weighted = Vector3.zero;
            foreach (var block in definition.Blocks)
            {
                weighted += block.LocalPosition * block.Mass;
            }

            return weighted / totalMass;
        }

        public float SimulateDriveStep(float throttleInput, float deltaTime, bool infiniteFuel)
        {
            var driveWheels = Mathf.Max(1, definition.Wheels.Count(w => w.IsDriveWheel));
            var torquePerWheel = definition.BaseTorque * Mathf.Clamp01(throttleInput) / driveWheels;
            var consumedFuel = Mathf.Abs(throttleInput) * deltaTime * (1.2f / Mathf.Max(0.2f, definition.EngineEfficiency));

            if (!infiniteFuel)
            {
                fuel = Mathf.Max(0f, fuel - consumedFuel);
            }

            return torquePerWheel;
        }

        public VehicleTelemetrySnapshot ReadTelemetry()
        {
            var mass = Mathf.Max(1f, definition.Blocks.Sum(b => b.Mass));
            var avgSuspension = Mathf.Max(1f, definition.SuspensionStrength);
            return new VehicleTelemetrySnapshot(
                wheelForce: definition.BaseTorque / mass,
                engineTemperature: Mathf.Lerp(65f, 105f, 1f - Mathf.Clamp01(fuel / Mathf.Max(1f, definition.FuelCapacity))),
                energyRate: definition.BaseTorque / Mathf.Max(1f, definition.EngineEfficiency * 100f),
                mechanicalStress: mass / avgSuspension,
                fuelLevel: fuel);
        }
    }
}
