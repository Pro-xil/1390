using UnityEngine;

namespace OfflineVoxelMining.Core
{
    [System.Serializable]
    public struct GameModeRules
    {
        public bool UnlimitedMoney;
        public bool UnlimitedStorage;
        public bool FreeUpgrades;
        public bool InstantMining;
        public bool InfiniteFuel;
        public bool UnlockAllItems;
        public bool AllowGodMode;

        public static GameModeRules Sandbox => new()
        {
            UnlimitedMoney = true,
            UnlimitedStorage = true,
            FreeUpgrades = true,
            InstantMining = true,
            InfiniteFuel = true,
            UnlockAllItems = true,
            AllowGodMode = true
        };

        public static GameModeRules Progression => new()
        {
            UnlimitedMoney = false,
            UnlimitedStorage = false,
            FreeUpgrades = false,
            InstantMining = false,
            InfiniteFuel = false,
            UnlockAllItems = false,
            AllowGodMode = false
        };
    }

    public static class GameModeRuleBook
    {
        public static GameModeRules Resolve(GameMode mode)
        {
            return mode == GameMode.InfiniteSandbox ? GameModeRules.Sandbox : GameModeRules.Progression;
        }
    }
}
