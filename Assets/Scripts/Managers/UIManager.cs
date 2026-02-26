using OfflineVoxelMining.Core;
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
        }

        public override void Shutdown() { }
    }
}
