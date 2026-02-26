using UnityEngine;

namespace OfflineVoxelMining.World
{
    public enum BiomeType
    {
        Desert,
        Snow,
        Forest,
        UndergroundCaves,
        CrystalRareZone,
        RadiationHazardZone
    }

    public sealed class BiomeGenerator : MonoBehaviour
    {
        public void PrewarmBiomes()
        {
            Debug.Log("Biome generator ready for desert, snow, forest, caves, crystal and radiation zones.");
        }
    }
}
