using OfflineVoxelMining.Core;
using OfflineVoxelMining.World;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class WorldManager : ManagerBase
    {
        [SerializeField] private ChunkStreamer chunkStreamer;
        [SerializeField] private BiomeGenerator biomeGenerator;
        [SerializeField] private WorldEventController worldEventController;

        public override void Initialize(GameBootstrap bootstrap)
        {
            chunkStreamer?.InitializeStreaming();
            biomeGenerator?.PrewarmBiomes();
            worldEventController?.ScheduleEvents();
        }

        public override void Shutdown()
        {
            chunkStreamer?.Shutdown();
        }
    }
}
