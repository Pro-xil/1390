using System.Collections.Generic;
using OfflineVoxelMining.Core;
using OfflineVoxelMining.Telemetry;
using OfflineVoxelMining.Vehicles;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class VehicleManager : ManagerBase
    {
        private readonly List<VehicleDefinition> garage = new();
        private readonly List<VehicleInstance> activeVehicles = new();
        private GameModeRules rules;

        [SerializeField] private VehicleTelemetryPublisher telemetryPublisher;

        public override void Initialize(GameBootstrap bootstrap)
        {
            rules = bootstrap.ActiveRules;
            Debug.Log("VehicleManager ready: LEGO-style modular assembly enabled.");
        }

        public VehicleInstance BuildVehicle(VehicleDefinition definition, Transform spawnPoint)
        {
            garage.Add(definition);
            var instance = new VehicleInstance(definition);
            activeVehicles.Add(instance);
            instance.Spawn(spawnPoint.position, spawnPoint.rotation);
            telemetryPublisher?.Publish(instance.ReadTelemetry());
            return instance;
        }

        public void SimulateVehicle(VehicleInstance vehicle, float throttle, float deltaTime)
        {
            vehicle.SimulateDriveStep(throttle, deltaTime, rules.InfiniteFuel);
            telemetryPublisher?.Publish(vehicle.ReadTelemetry());
        }

        public override void Shutdown()
        {
            garage.Clear();
            activeVehicles.Clear();
        }
    }
}
