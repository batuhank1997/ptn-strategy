using System.Collections.Generic;
using System.Linq;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Components
{
    public static class PathFinder
    {
        private static Dictionary<Vector2, Cell> _cells;
        private const int UNI_COST = 1;

        public static List<Cell> FindPath(Vector2 startPosition, Vector2 targetPosition)
        {
            _cells = GridManager.Instance.GetAllCells();

            var openSet = new HashSet<Vector2>();
            var closedSet = new HashSet<Vector2>();
            var cameFrom = new Dictionary<Vector2, Vector2>();
            var gScore = new Dictionary<Vector2, float>();
            var fScore = new Dictionary<Vector2, float>();

            openSet.Add(startPosition);
            gScore[startPosition] = 0;
            fScore[startPosition] = Heuristic(startPosition, targetPosition);

            if (_cells.ContainsKey(targetPosition) && _cells[targetPosition].IsOccupied)
            {
                var unitCell = _cells[startPosition];
                var targetCell = _cells[targetPosition];
                
                var closestPosition = FindClosestPosition(targetCell.GetBuilding().GetCellPositions(), unitCell.GetCoordinates());

                targetPosition = GetNeighbors(closestPosition).FirstOrDefault(v2 => !_cells[v2].IsOccupied);
            }

            while (openSet.Count > 0)
            {
                var current = GetLowestFScore(openSet, fScore);

                if (current == targetPosition)
                    return ReconstructPath(cameFrom, targetPosition);

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    var tentativeGScore = gScore[current] + CalculateCost(current, neighbor);

                    if (openSet.Contains(neighbor) && !(tentativeGScore < gScore[neighbor])) continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetPosition);

                    if (!openSet.Contains(neighbor))
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

        private static float CalculateCost(Vector2 current, Vector2 neighbor)
        {
            var dx = Mathf.Abs(current.x - neighbor.x);
            var dy = Mathf.Abs(current.y - neighbor.y);

            if (dx > 0 && dy > 0)
                return Mathf.Sqrt(2);

            return 1;
        }

        private static List<Vector2> GetNeighbors(Vector2 position, bool checkOccupied = true)
        {
            var neighbors = new List<Vector2>
            {
                new(position.x, position.y + 1),
                new(position.x, position.y - 1),
                new(position.x + 1, position.y),
                new(position.x - 1, position.y),
                new(position.x + 1, position.y + 1), // Diagonal neighbors
                new(position.x - 1, position.y - 1),
                new(position.x + 1, position.y - 1),
                new(position.x - 1, position.y + 1)
            };

            neighbors = checkOccupied
                ? neighbors.Where(neighbor => _cells.ContainsKey(neighbor) && !_cells[neighbor].IsOccupied).ToList()
                : neighbors.Where(neighbor => _cells.ContainsKey(neighbor)).ToList();

            return neighbors;
        }

        private static List<Cell> FindClosestUnoccupiedCell(Vector2 reversedStartPosition,
            Vector2 reversedTargetPosition)
        {
            _cells = GridManager.Instance.GetAllCells();

            var openSet = new HashSet<Vector2>();
            var closedSet = new HashSet<Vector2>();
            var cameFrom = new Dictionary<Vector2, Vector2>();
            var gScore = new Dictionary<Vector2, float>();
            var fScore = new Dictionary<Vector2, float>();

            openSet.Add(reversedStartPosition);
            gScore[reversedStartPosition] = 0;
            fScore[reversedStartPosition] = Heuristic(reversedStartPosition, reversedTargetPosition);

            while (openSet.Count > 0)
            {
                var current = GetLowestFScore(openSet, fScore);

                if (current == reversedTargetPosition)
                    return ReconstructPath(cameFrom, reversedTargetPosition);

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (var neighbor in GetNeighbors(current, false))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    var tentativeGScore = gScore[current] + UNI_COST;

                    if (openSet.Contains(neighbor) && !(tentativeGScore < gScore[neighbor])) continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, reversedTargetPosition);

                    openSet.Add(neighbor);
                }
            }

            return null;
        }

        private static Vector2 FindClosestPosition(List<Vector2> positions, Vector2 target)
        {
            if (positions == null || positions.Count == 0)
                return Vector2.zero;

            var minDistance = float.MaxValue;
            var closestPosition = positions[0];

            foreach (var position in positions)
            {
                var distance = Vector2.Distance(target, position);

                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closestPosition = position;
            }

            return closestPosition;
        }
    }
}