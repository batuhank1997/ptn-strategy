using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class GridManager : Singleton<GridManager>
    {
        [SerializeField] private Cell m_cell;
        [SerializeField] private Vector2 m_gridSize;
        
        private GameObject _cellParent;
        
        private readonly Dictionary<Vector2, Cell> _cells = new Dictionary<Vector2, Cell>();
        
        private const float CELL_SIZE = 1f;
        
        public void Initilize()
        {
            CreateGameBoard();
        }

        public Cell GetCell(Vector2 pos)
        {
            return _cells.GetValueOrDefault(pos);
        }
        
        public bool IsOutsideOfGameBoard(Vector2 coordinates)
        {
            return coordinates.x >= m_gridSize.x || coordinates.y >= m_gridSize.y || coordinates.x < 0 || coordinates.y < 0;
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
                    
                    cell.Init(pos);
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
            var pos = new Vector2(_cellParent.transform.position.x - ((CELL_SIZE * m_gridSize.x) / 2), _cellParent.transform.position.y);
            
            return pos;
        }
        
    }
}
