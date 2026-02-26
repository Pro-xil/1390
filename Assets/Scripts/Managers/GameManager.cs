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
            ApplyMode(owner.CurrentMode);
            GameEventBus.Subscribe<GameModeChangedEvent>(OnModeChanged);
            Debug.Log($"GameManager initialized in mode: {bootstrap.CurrentMode} (Offline Only)");
        }

        public void SetMode(GameMode mode)
        {
            bootstrap.SetMode(mode);
        }

        public void ToggleGodMode(bool enabled)
        {
            if (!bootstrap.ActiveRules.AllowGodMode)
            {
                godMode = false;
                return;
            }

            godMode = enabled;
            Debug.Log($"Sandbox God Mode: {enabled}");
        }

        private void OnModeChanged(GameModeChangedEvent evt)
        {
            ApplyMode(evt.Mode);
        }

        private void ApplyMode(GameMode mode)
        {
            if (mode != GameMode.InfiniteSandbox)
            {
                godMode = false;
            }
        }

        public override void Shutdown()
        {
            GameEventBus.Unsubscribe<GameModeChangedEvent>(OnModeChanged);
            Debug.Log("GameManager shutdown complete.");
        }
    }
}
