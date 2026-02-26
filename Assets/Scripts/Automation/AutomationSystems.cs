using UnityEngine;

namespace OfflineVoxelMining.Automation
{
    public sealed class MiningRobotController : MonoBehaviour
    {
        public void TickOfflineAi() { }
    }

    public sealed class ConveyorNetwork : MonoBehaviour
    {
        public void RouteOre(string oreId, int amount) { }
    }

    public sealed class OreFactoryProcessor : MonoBehaviour
    {
        public int Process(string oreId, int amount) => Mathf.FloorToInt(amount * 0.8f);
    }
}
