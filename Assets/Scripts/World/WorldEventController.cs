using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.World
{
    public sealed class WorldEventController : MonoBehaviour
    {
        public void ScheduleEvents()
        {
            InvokeRepeating(nameof(TriggerMeteorImpact), 20f, 120f);
            InvokeRepeating(nameof(TriggerEarthquake), 45f, 180f);
            InvokeRepeating(nameof(SpawnRareVein), 30f, 150f);
        }

        private void TriggerMeteorImpact()
        {
            var location = new Vector3(Random.Range(-200f, 200f), 60f, Random.Range(-200f, 200f));
            GameEventBus.Publish(new HazardTriggeredEvent("MeteorImpact", location));
        }

        private void TriggerEarthquake()
        {
            var epicenter = new Vector3(Random.Range(-500f, 500f), 0f, Random.Range(-500f, 500f));
            GameEventBus.Publish(new HazardTriggeredEvent("Earthquake", epicenter));
        }

        private void SpawnRareVein()
        {
            var veinPoint = new Vector3(Random.Range(-300f, 300f), Random.Range(-120f, -20f), Random.Range(-300f, 300f));
            GameEventBus.Publish(new HazardTriggeredEvent("RareVeinSpawn", veinPoint));
        }
    }
}
