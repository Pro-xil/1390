using System.Collections.Generic;
using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.Achievements
{
    public sealed class AchievementSystem : MonoBehaviour
    {
        private readonly Dictionary<string, int> stats = new();

        private void OnEnable()
        {
            GameEventBus.Subscribe<ResourceCollectedEvent>(OnResourceCollected);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<ResourceCollectedEvent>(OnResourceCollected);
        }

        public string GenerateWeeklyOfflineChallenge(int seed)
        {
            Random.InitState(seed);
            var target = Random.Range(100, 600);
            return $"Extract {target} units and reach 70 km/h in one run.";
        }

        private void OnResourceCollected(ResourceCollectedEvent evt)
        {
            stats.TryAdd(evt.ResourceId, 0);
            stats[evt.ResourceId] += evt.Amount;
        }
    }
}
