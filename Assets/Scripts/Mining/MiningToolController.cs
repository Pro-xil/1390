using UnityEngine;

namespace OfflineVoxelMining.Mining
{
    public enum MiningOperationMode
    {
        OnFoot,
        WhileDriving
    }

    public sealed class MiningToolController : MonoBehaviour
    {
        [SerializeField] private bool instantMining;
        [SerializeField] private float laserCooldown = 0.8f;
        [SerializeField] private float explosiveCooldown = 4f;
        [SerializeField] private float drillCooldown = 0.2f;

        private float nextActionTime;

        public void InitializeTools(bool sandboxMode)
        {
            instantMining = sandboxMode;
        }

        public bool FireLaser(MiningOperationMode mode)
        {
            return TryAct(laserCooldown, mode);
        }

        public bool DeployExplosiveCharge(MiningOperationMode mode)
        {
            return TryAct(explosiveCooldown, mode);
        }

        public bool StartRotaryDrill(MiningOperationMode mode)
        {
            return TryAct(drillCooldown, mode);
        }

        public float ScanSignalStrength(Vector3 worldPosition)
        {
            var baseSignal = Mathf.PerlinNoise(worldPosition.x * 0.06f, worldPosition.z * 0.06f);
            return worldPosition.y < -20f ? Mathf.Min(1f, baseSignal + 0.2f) : baseSignal;
        }

        private bool TryAct(float cooldown, MiningOperationMode mode)
        {
            if (instantMining)
            {
                return true;
            }

            var modePenalty = mode == MiningOperationMode.WhileDriving ? 0.4f : 0f;
            if (Time.time < nextActionTime + modePenalty)
            {
                return false;
            }

            nextActionTime = Time.time + cooldown;
            return true;
        }
    }
}
