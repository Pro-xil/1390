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
        [SerializeField] private int biomeSeed = 1390;

        public void PrewarmBiomes()
        {
            Random.InitState(biomeSeed);
            Debug.Log("Biome generator ready for desert, snow, forest, caves, crystal and radiation zones.");
        }

        public BiomeType EvaluateBiome(Vector3 worldPosition)
        {
            if (worldPosition.y < -40f)
            {
                var caveNoise = Mathf.PerlinNoise(worldPosition.x * 0.03f, worldPosition.z * 0.03f);
                return caveNoise > 0.82f ? BiomeType.CrystalRareZone : BiomeType.UndergroundCaves;
            }

            var temp = Mathf.PerlinNoise(worldPosition.x * 0.002f, worldPosition.z * 0.002f);
            var humidity = Mathf.PerlinNoise((worldPosition.x + biomeSeed) * 0.002f, (worldPosition.z - biomeSeed) * 0.002f);
            if (temp > 0.75f && humidity < 0.35f) return BiomeType.Desert;
            if (temp < 0.25f) return BiomeType.Snow;
            if (humidity > 0.7f && temp > 0.45f) return BiomeType.Forest;

            var radiation = Mathf.PerlinNoise(worldPosition.x * 0.0007f, worldPosition.z * 0.0007f);
            return radiation > 0.93f ? BiomeType.RadiationHazardZone : BiomeType.Forest;
        }
    }
}
