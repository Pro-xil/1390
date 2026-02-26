using System;
using System.Collections.Generic;
using System.IO;
using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.Mods
{
    [Serializable]
    public sealed class ModManifest
    {
        public string id;
        public string version;
        public string entryScript;
    }

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
                    var manifest = JsonUtility.FromJson<ModManifest>(File.ReadAllText(file));
                    ValidateManifest(file, manifest);
                    loadedMods.Add(manifest.id);
                }
                catch (Exception ex)
                {
                    LogError($"Invalid mod file '{file}': {ex.Message}");
                }
            }

            GameEventBus.Publish(new ModsReloadedEvent(loadedMods));
        }

        private static void ValidateManifest(string source, ModManifest manifest)
        {
            if (manifest == null || string.IsNullOrWhiteSpace(manifest.id))
            {
                throw new InvalidDataException($"Missing required mod id in {source}");
            }

            if (!string.IsNullOrWhiteSpace(manifest.entryScript))
            {
                var ext = Path.GetExtension(manifest.entryScript).ToLowerInvariant();
                if (ext is not ".lua" and not ".cs")
                {
                    throw new InvalidDataException($"Unsupported script extension '{ext}' in {source}");
                }
            }
        }

        private void LogError(string message)
        {
            var path = Path.Combine(Application.persistentDataPath, logFileName);
            File.AppendAllText(path, $"[{DateTime.UtcNow:O}] {message}\n");
            Debug.LogWarning(message);
        }
    }
}
