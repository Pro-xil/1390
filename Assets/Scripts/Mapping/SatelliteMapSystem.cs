using UnityEngine;

namespace OfflineVoxelMining.Mapping
{
    public sealed class SatelliteMapSystem : MonoBehaviour
    {
        public Texture2D GenerateWorldOverview() => new(512, 512);
        public Vector3[] FindOptimalMiningPath(Vector3 start) => new[] { start, start + Vector3.forward * 25f };
    }
}
