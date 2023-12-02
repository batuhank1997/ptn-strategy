using System.Collections.Generic;
using System.Linq;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Components
{
    public static class PathFinder
    {
        private static readonly Dictionary<Vector2, Cell> _cells = GridManager.Instance.GetAllCells();

        public static List<Cell> FindPath(Vector2 startPosition, Vector2 targetPosition)
        {
            var openSet = new HashSet<Vector2>();
            var closedSet = new HashSet<Vector2>();
            var cameFrom = new Dictionary<Vector2, Vector2>();
            var gScore = new Dictionary<Vector2, float>();
            var fScore = new Dictionary<Vector2, float>();

            openSet.Add(startPosition);
            gScore[startPosition] = 0;
            fScore[startPosition] = Heuristic(startPosition, targetPosition);

            while (openSet.Count > 0)
            {
                var current = GetLowestFScore(openSet, fScore);
                
                if (current == targetPosition)
                    return ReconstructPath(cameFrom, targetPosition);

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (!_cells.ContainsKey(neighbor))
                        continue;
                    
                    if (closedSet.Contains(neighbor))
                        continue;

                    var tentativeGScore = gScore[current] + 1; // Assuming a uniform cost of 1 between tiles

                    if (openSet.Contains(neighbor) && !(tentativeGScore < gScore[neighbor])) continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetPosition);

                    openSet.Add(neighbor);
                }
            }

            return null;
        }

        private static float Heuristic(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        private static Vector2 GetLowestFScore(HashSet<Vector2> openSet, Dictionary<Vector2, float> fScore)
        {
            var lowestFScore = float.MaxValue;
            var lowestFScoreTile = Vector2.zero;

            foreach (var tile in openSet)
            {
                if (!fScore.ContainsKey(tile) || !(fScore[tile] < lowestFScore)) continue;

                lowestFScore = fScore[tile];
                lowestFScoreTile = tile;
            }

            return lowestFScoreTile;
        }

        private static List<Cell> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
        {
            var path = new List<Cell>();
            path.Add(_cells[current]);

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(_cells[current]);
            }

            path.Reverse();
            return path;
        }

        private static List<Vector2> GetNeighbors(Vector2 position)
        {
            var neighbors = new List<Vector2>
            {
                new(position.x, position.y + 1),
                new(position.x, position.y - 1),
                new(position.x + 1, position.y),
                new(position.x - 1, position.y),
                new(position.x + 1, position.y + 1),
                new(position.x - 1, position.y - 1),
                new(position.x + 1, position.y - 1),
                new(position.x - 1, position.y + 1)
            };

            neighbors = neighbors.Where(neighbor => _cells.ContainsKey(neighbor) && !_cells[neighbor].IsOccupied)
                .ToList();

            return neighbors;
        }
    }
}