using UnityEngine;

namespace OfflineVoxelMining.Core
{
    public abstract class ManagerBase : MonoBehaviour
    {
        public abstract void Initialize(GameBootstrap bootstrap);
        public abstract void Shutdown();
    }
}
