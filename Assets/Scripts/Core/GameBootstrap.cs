using System.Collections.Generic;
using OfflineVoxelMining.Managers;
using UnityEngine;

namespace OfflineVoxelMining.Core
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GameMode initialMode = GameMode.NormalProgression;

        private readonly List<ManagerBase> managers = new();

        public GameMode CurrentMode { get; private set; }

        private void Awake()
        {
            CurrentMode = initialMode;
            RegisterManagers();

            foreach (var manager in managers)
            {
                manager.Initialize(this);
            }
        }

        private void OnDestroy()
        {
            for (var i = managers.Count - 1; i >= 0; i--)
            {
                managers[i].Shutdown();
            }
        }

        private void RegisterManagers()
        {
            managers.Add(GetComponent<GameManager>());
            managers.Add(GetComponent<EconomyManager>());
            managers.Add(GetComponent<VehicleManager>());
            managers.Add(GetComponent<WorldManager>());
            managers.Add(GetComponent<AIManager>());
            managers.Add(GetComponent<MiningManager>());
            managers.Add(GetComponent<UIManager>());

            managers.RemoveAll(m => m == null);
        }
    }
}
