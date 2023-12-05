using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private Cell m_cell;
        [SerializeField] private Vector2 m_gridSize;
        public bool IsDebugging { get; set; }
        
        private readonly Dictionary<Vector2, Cell> _cells = new Dictionary<Vector2, Cell>();
        private GameObject _cellParent;
        
        private const float CELL_SIZE = 1f;

        public void Initilize()
        {
            CreateGameBoard();
        }

        public Cell GetCell(Vector2 pos)
        {
            return _cells.GetValueOrDefault(pos);
        }
        
        public Dictionary<Vector2, Cell> GetAllCells()
        {
            return _cells;
        }
        
        public Vector2 GetGridSize()
        {
            return m_gridSize;
        }
        
        public bool IsOutsideOfGameBoard(Vector2 coordinates)
        {
            return coordinates.x >= m_gridSize.x || coordinates.y >= m_gridSize.y || coordinates.x < 0 || coordinates.y < 0;
        }
        
        public List<Cell> GetNeighboringCells(Vector2 startingCell, Vector2 size)
        {
            var neighbors = new List<Cell>();

            var startingX = (int)startingCell.x;
            var startingY = (int)startingCell.y;

            for (var i = startingX - 1; i < startingX + size.x + 1; i++)
            {
                for (var j = startingY - 1; j < startingY + size.y + 1; j++)
                {
                    var pos = new Vector2(i, j);
                    
                    if (!IsOutsideOfGameBoard(pos))
                        neighbors.Add(GetCell(pos));
                }
            }

            return neighbors;
        }
        
        private void CreateGameBoard()
        {
            CreateCellParent();
            CreateCells();
        }

        private void CreateCells()
        {
            for (var x = 0; x < m_gridSize.x; x++)
            {
                for (var y = 0; y < m_gridSize.y; y++)
                {
                    var pos = new Vector2(x, y);
                    var cell = Instantiate(m_cell,  GetGridStartPos() + pos * CELL_SIZE, Quaternion.identity);
                    _cells.Add(pos, cell);
                    
                    cell.Init(pos, IsDebugging);
                    cell.transform.SetParent(_cellParent.transform);
                }
            }
        }

        private void CreateCellParent()
        {
            _cellParent = new GameObject("CellParent")
            {
                transform =
                {
                    position = -Vector2.up * 5
                }
            };
        }

        private Vector2 GetGridStartPos()
        {
            var position = _cellParent.transform.position;
            var pos = new Vector2(position.x - ((CELL_SIZE * m_gridSize.x) / 2), position.y);
            
            return pos;
        }

    }
}
