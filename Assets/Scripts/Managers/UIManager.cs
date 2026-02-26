using OfflineVoxelMining.Core;
using OfflineVoxelMining.Events;
using OfflineVoxelMining.UI;
using UnityEngine;

namespace OfflineVoxelMining.Managers
{
    public sealed class UIManager : ManagerBase
    {
        [SerializeField] private PersianLocalizationService localizationService;
        [SerializeField] private EngineeringHudController engineeringHudController;

        public override void Initialize(GameBootstrap bootstrap)
        {
            localizationService?.Load("fa-IR");
            engineeringHudController?.ShowSandboxBadge(bootstrap.CurrentMode == GameMode.InfiniteSandbox);
            GameEventBus.Subscribe<GameModeChangedEvent>(OnGameModeChanged);
        }

        private void OnGameModeChanged(GameModeChangedEvent evt)
        {
            engineeringHudController?.ShowSandboxBadge(evt.Mode == GameMode.InfiniteSandbox);
        }

        public override void Shutdown()
        {
            GameEventBus.Unsubscribe<GameModeChangedEvent>(OnGameModeChanged);
        }
    }
}
