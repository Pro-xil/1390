using System;
using System.Collections.Generic;
using System.IO;
using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.Mods
{
    public sealed class ModSystem : MonoBehaviour
    {
        [SerializeField] private string modsFolderName = "Mods";
        [SerializeField] private string logFileName = "mod_errors.log";

        private readonly List<string> loadedMods = new();

        public void ReloadMods()
        {
            loadedMods.Clear();
            var root = Path.Combine(Application.dataPath, "..", modsFolderName);
            Directory.CreateDirectory(root);

            foreach (var file in Directory.GetFiles(root, "*.json", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    var name = Path.GetFileNameWithoutExtension(file);
                    loadedMods.Add(name);
                }
                catch (Exception ex)
                {
                    LogError($"Invalid mod file '{file}': {ex.Message}");
                }
            }

            GameEventBus.Publish(new ModsReloadedEvent(loadedMods));
        }

        private void LogError(string message)
        {
            var path = Path.Combine(Application.persistentDataPath, logFileName);
            File.AppendAllText(path, $"[{DateTime.UtcNow:O}] {message}\n");
        }
    }
}
