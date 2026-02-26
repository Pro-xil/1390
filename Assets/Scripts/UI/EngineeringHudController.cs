using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.UI
{
    public sealed class EngineeringHudController : MonoBehaviour
    {
        [SerializeField] private GameObject sandboxBadge;

        private void OnEnable()
        {
            GameEventBus.Subscribe<VehicleTelemetryEvent>(OnTelemetry);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<VehicleTelemetryEvent>(OnTelemetry);
        }

        public void ShowSandboxBadge(bool visible)
        {
            if (sandboxBadge != null)
            {
                sandboxBadge.SetActive(visible);
            }
        }

        private void OnTelemetry(VehicleTelemetryEvent evt)
        {
            Debug.Log($"Telemetry | Wheel:{evt.WheelForce:F2} Temp:{evt.EngineTemperature:F1}C Energy:{evt.EnergyRate:F2} Stress:{evt.MechanicalStress:F2}");
        }
    }
}
