using OfflineVoxelMining.Core;
using UnityEngine;

namespace OfflineVoxelMining.AI
{
    public sealed class DifficultyDirector : MonoBehaviour
    {
        [SerializeField] private AnimationCurve difficultyCurve;
        private GameMode mode;

        public void SetMode(GameMode gameMode) => mode = gameMode;

        public float EvaluateDifficulty(float sessionMinutes)
        {
            if (mode == GameMode.InfiniteSandbox)
            {
                return 0.2f;
            }

            return difficultyCurve == null ? 1f : difficultyCurve.Evaluate(sessionMinutes);
        }
    }

    public sealed class NpcAgent : MonoBehaviour { }
}
