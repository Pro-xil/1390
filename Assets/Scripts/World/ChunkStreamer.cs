using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.World
{
    public sealed class ChunkStreamer : MonoBehaviour
    {
        [SerializeField] private int chunkSize = 32;
        [SerializeField] private int activeRadius = 6;

        public void InitializeStreaming()
        {
            LoadChunk(Vector3Int.zero, 0);
        }

        public void UpdateStreaming(Vector3 focusWorldPosition)
        {
            var focusChunk = WorldToChunk(focusWorldPosition);
            for (var x = -activeRadius; x <= activeRadius; x++)
            {
                for (var z = -activeRadius; z <= activeRadius; z++)
                {
                    var coord = new Vector3Int(focusChunk.x + x, 0, focusChunk.z + z);
                    var lod = Mathf.Clamp(Mathf.Max(Mathf.Abs(x), Mathf.Abs(z)) / 2, 0, 4);
                    LoadChunk(coord, lod);
                }
            }
        }

        public void LoadChunk(Vector3Int coord, int lod)
        {
            GameEventBus.Publish(new ChunkLoadedEvent(coord, lod));
        }

        private Vector3Int WorldToChunk(Vector3 world)
        {
            return new Vector3Int(
                Mathf.FloorToInt(world.x / chunkSize),
                Mathf.FloorToInt(world.y / chunkSize),
                Mathf.FloorToInt(world.z / chunkSize));
        }

        public void Shutdown() { }
    }
}
