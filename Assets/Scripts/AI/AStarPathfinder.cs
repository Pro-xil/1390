using System.Collections.Generic;
using UnityEngine;

namespace OfflineVoxelMining.AI
{
    public interface IAStarGrid
    {
        IEnumerable<Vector3> GetNeighbors(Vector3 node);
        float Cost(Vector3 from, Vector3 to);
        float Heuristic(Vector3 from, Vector3 to);
        bool IsWalkable(Vector3 node);
    }

    public static class AStarPathfinder
    {
        public static List<Vector3> FindPath(Vector3 start, Vector3 goal, IAStarGrid grid)
        {
            var open = new PriorityQueue<Vector3, float>();
            var cameFrom = new Dictionary<Vector3, Vector3>();
            var gScore = new Dictionary<Vector3, float> { [start] = 0f };
            open.Enqueue(start, 0f);

            while (open.Count > 0)
            {
                var current = open.Dequeue();
                if (Vector3.Distance(current, goal) < 0.1f)
                {
                    return Reconstruct(cameFrom, current);
                }

                foreach (var neighbor in grid.GetNeighbors(current))
                {
                    if (!grid.IsWalkable(neighbor)) continue;

                    var tentativeG = gScore[current] + grid.Cost(current, neighbor);
                    if (gScore.TryGetValue(neighbor, out var existingG) && tentativeG >= existingG) continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG;
                    var fScore = tentativeG + grid.Heuristic(neighbor, goal);
                    open.Enqueue(neighbor, fScore);
                }
            }

            return new List<Vector3>();
        }

        private static List<Vector3> Reconstruct(Dictionary<Vector3, Vector3> cameFrom, Vector3 current)
        {
            var result = new List<Vector3> { current };
            while (cameFrom.TryGetValue(current, out var prev))
            {
                result.Add(prev);
                current = prev;
            }

            result.Reverse();
            return result;
        }
    }
}
