using UnityEngine;

namespace OfflineVoxelMining.Mining
{
    public sealed class MiningToolController : MonoBehaviour
    {
        [SerializeField] private bool instantMining;

        public void InitializeTools(bool sandboxMode)
        {
            instantMining = sandboxMode;
        }

        public void FireLaser() { }
        public void DeployExplosiveCharge() { }
        public void StartRotaryDrill() { }
        public float ScanSignalStrength(Vector3 worldPosition) => Mathf.PerlinNoise(worldPosition.x, worldPosition.z);
    }
}
