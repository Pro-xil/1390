using OfflineVoxelMining.Core;
using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class GameManager : ManagerBase
    {
        [SerializeField] private bool godMode;

        private GameBootstrap bootstrap;

        public bool IsOfflineOnly => true;
        public bool GodMode => godMode;

        public override void Initialize(GameBootstrap owner)
        {
            bootstrap = owner;
            GameEventBus.Publish(new GameModeChangedEvent(bootstrap.CurrentMode));
            Debug.Log($"GameManager initialized in mode: {bootstrap.CurrentMode} (Offline Only)");
        }

        public void SetMode(GameMode mode)
        {
            GameEventBus.Publish(new GameModeChangedEvent(mode));
        }

        public void ToggleGodMode(bool enabled)
        {
            godMode = enabled;
            Debug.Log($"Sandbox God Mode: {enabled}");
        }

        public override void Shutdown()
        {
            Debug.Log("GameManager shutdown complete.");
        }
    }
}
