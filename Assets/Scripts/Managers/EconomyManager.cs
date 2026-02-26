using System.Collections.Generic;
using OfflineVoxelMining.Core;
using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class EconomyManager : ManagerBase
    {
        private readonly Dictionary<string, int> inventory = new();
        private bool sandboxUnlimited;

        public override void Initialize(GameBootstrap bootstrap)
        {
            sandboxUnlimited = bootstrap.ActiveRules.UnlimitedMoney;
            GameEventBus.Subscribe<ResourceCollectedEvent>(OnResourceCollected);
            GameEventBus.Subscribe<GameModeChangedEvent>(OnModeChanged);
        }

        public int GetAmount(string resourceId)
        {
            return inventory.TryGetValue(resourceId, out var amount) ? amount : 0;
        }

        public bool TrySpend(string resourceId, int amount)
        {
            if (sandboxUnlimited)
            {
                return true;
            }

            if (!inventory.TryGetValue(resourceId, out var current) || current < amount)
            {
                return false;
            }

            inventory[resourceId] = current - amount;
            return true;
        }

        private void OnModeChanged(GameModeChangedEvent evt)
        {
            sandboxUnlimited = GameModeRuleBook.Resolve(evt.Mode).UnlimitedMoney;
        }

        private void OnResourceCollected(ResourceCollectedEvent evt)
        {
            if (!inventory.ContainsKey(evt.ResourceId))
            {
                inventory[evt.ResourceId] = 0;
            }

            inventory[evt.ResourceId] += evt.Amount;
        }

        public override void Shutdown()
        {
            GameEventBus.Unsubscribe<ResourceCollectedEvent>(OnResourceCollected);
            GameEventBus.Unsubscribe<GameModeChangedEvent>(OnModeChanged);
        }
    }
}
