using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.Telemetry
{
    public readonly struct VehicleTelemetrySnapshot
    {
        public VehicleTelemetrySnapshot(float wheelForce, float engineTemperature, float energyRate, float mechanicalStress, float fuelLevel)
        {
            WheelForce = wheelForce;
            EngineTemperature = engineTemperature;
            EnergyRate = energyRate;
            MechanicalStress = mechanicalStress;
            FuelLevel = fuelLevel;
        }

        public float WheelForce { get; }
        public float EngineTemperature { get; }
        public float EnergyRate { get; }
        public float MechanicalStress { get; }
        public float FuelLevel { get; }
    }

    public sealed class VehicleTelemetryPublisher : MonoBehaviour
    {
        public void Publish(VehicleTelemetrySnapshot snapshot)
        {
            GameEventBus.Publish(new VehicleTelemetryEvent(snapshot.WheelForce, snapshot.EngineTemperature, snapshot.EnergyRate, snapshot.MechanicalStress, snapshot.FuelLevel));
        }
    }
}
