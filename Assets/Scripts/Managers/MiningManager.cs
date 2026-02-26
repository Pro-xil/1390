using OfflineVoxelMining.Core;
using OfflineVoxelMining.Events;
using OfflineVoxelMining.Mining;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class MiningManager : ManagerBase
    {
        [SerializeField] private MiningToolController toolController;

        public override void Initialize(GameBootstrap bootstrap)
        {
            toolController?.InitializeTools(bootstrap.CurrentMode == GameMode.InfiniteSandbox);
        }

        public void Mine(string resourceId, int amount)
        {
            GameEventBus.Publish(new ResourceCollectedEvent(resourceId, amount));
        }

        public override void Shutdown() { }
    }
}
