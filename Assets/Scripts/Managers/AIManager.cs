using System.Collections.Generic;
using OfflineVoxelMining.AI;
using OfflineVoxelMining.Core;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class AIManager : ManagerBase
    {
        [SerializeField] private DifficultyDirector difficultyDirector;
        private readonly List<NpcAgent> activeAgents = new();

        public override void Initialize(GameBootstrap bootstrap)
        {
            difficultyDirector?.SetMode(bootstrap.CurrentMode);
        }

        public List<Vector3> CalculatePath(Vector3 start, Vector3 target, IAStarGrid grid)
        {
            return AStarPathfinder.FindPath(start, target, grid);
        }

        public override void Shutdown()
        {
            activeAgents.Clear();
        }
    }
}
