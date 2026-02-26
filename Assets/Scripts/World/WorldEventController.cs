using OfflineVoxelMining.Events;
using UnityEngine;

namespace OfflineVoxelMining.World
{
    public sealed class WorldEventController : MonoBehaviour
    {
        public void ScheduleEvents()
        {
            InvokeRepeating(nameof(TriggerMeteorImpact), 20f, 120f);
        }

        private void TriggerMeteorImpact()
        {
            var location = new Vector3(Random.Range(-200f, 200f), 60f, Random.Range(-200f, 200f));
            GameEventBus.Publish(new HazardTriggeredEvent("MeteorImpact", location));
        }
    }
}
