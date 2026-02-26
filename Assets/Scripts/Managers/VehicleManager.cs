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

        [SerializeField] private VehicleTelemetryPublisher telemetryPublisher;

        public override void Initialize(GameBootstrap bootstrap)
        {
            Debug.Log("VehicleManager ready: LEGO-style modular assembly enabled.");
        }

        public VehicleInstance BuildVehicle(VehicleDefinition definition, Transform spawnPoint)
        {
            garage.Add(definition);
            var instance = new VehicleInstance(definition);
            instance.Spawn(spawnPoint.position, spawnPoint.rotation);
            telemetryPublisher?.Publish(instance.ReadTelemetry());
            return instance;
        }

        public override void Shutdown()
        {
            garage.Clear();
        }
    }
}
