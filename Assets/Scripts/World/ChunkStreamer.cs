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
            LoadChunk(Vector3Int.zero);
        }

        public void LoadChunk(Vector3Int coord)
        {
            GameEventBus.Publish(new ChunkLoadedEvent(coord));
        }

        public void Shutdown() { }
    }
}
