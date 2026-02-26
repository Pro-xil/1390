using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OfflineVoxelMining.UI
{
    public sealed class PersianLocalizationService : MonoBehaviour
    {
        [SerializeField] private bool rightToLeftEnabled = true;

        private readonly Dictionary<string, string> dictionary = new();

        public void Load(string locale)
        {
            dictionary["mode_sandbox"] = "حالت سندباکس / مود شده";
            dictionary["mode_normal"] = "حالت پیشرفت عادی";
            dictionary["mission_mine_iron"] = "ماموریت: استخراج ۵۰ واحد سنگ آهن";
            dictionary["tooltip_scanner"] = "اسکنر زیرزمینی: قدرت سیگنال منبع";

            Debug.Log($"Localization loaded: {locale}, RTL={rightToLeftEnabled}");
        }

        public void Apply(Text target, string key)
        {
            if (target == null || !dictionary.TryGetValue(key, out var value))
            {
                return;
            }

            target.supportRichText = true;
            target.alignment = TextAnchor.MiddleRight;
            target.text = value;
        }
    }
}
