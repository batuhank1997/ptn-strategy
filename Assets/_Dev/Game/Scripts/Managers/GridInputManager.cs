using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class GridInputManager : Singleton<GridInputManager>
    {
        private Cell _cellUnderCursor;
        private Cell _selectedCell;
        
        private Building _buildingToPlace;
        private readonly List<Cell> _cellsToPlace = new List<Cell>();
        
        private CursorDirection _cursorDirection;
        
        private bool _canPlaceBuilding = true;
        
        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellSelected);
        }
        
        public Cell GetSelectedCell()
        {
            return _selectedCell;
        }

        public void SetCellUnderCursor(Cell cell)
        {
            if (_cellUnderCursor == null)
            {
                _cellUnderCursor = cell;
                return;
            }
            
            var oldX = _cellUnderCursor.GetCoordinates().x;
            var oldY = _cellUnderCursor.GetCoordinates().y;
            
            _cellUnderCursor = cell;
            
            SetCursorDirection((int)oldX, (int)oldY);
        }
        
        private void SetCursorDirection(int x, int y)
        {
            var newX = (int)_cellUnderCursor.GetCoordinates().x;
            var newY = (int)_cellUnderCursor.GetCoordinates().y;
            
            if (x != newX)
                _cursorDirection = x < newX ? CursorDirection.Right : CursorDirection.Left;
            else if (y != newY)
                _cursorDirection = y < newY ? CursorDirection.Up : CursorDirection.Down;
            
            EventSystemManager.InvokeEvent(EventId.on_cursor_direction_changed, new EnumArguments(_cursorDirection));
            _cellsToPlace.Clear();
        }

        
        //todo: refactor
        public void SetCellStatesIfPlacing()
        {
            if (_buildingToPlace == null)
            {
                _cellUnderCursor.SetCellVisual(CellState.UnderCursor);
                return;
            }

            var size = _buildingToPlace.Size;
            
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var offset = new Vector2(i, j);
                    var coordinates = _cellUnderCursor.GetCoordinates() + offset;
                    
                    if (GridManager.Instance.IsOutsideOfGameBoard(coordinates)) 
                        continue;
                    
                    var cell = GridManager.Instance.GetCell(coordinates);
                    _cellsToPlace.Add(cell);
                    cell.SetCellVisual(CellState.ReadyForPlacement);
                }
            }
        }
        
        public void SetBuildingForPlacing(Building building)
        {
            _buildingToPlace = building;
        }

        private void OnCellSelected(EventArgs obj)
        {
            if (!_canPlaceBuilding) return;

            var args = (Vector2Arguments) obj;
            
            _selectedCell = GridManager.Instance.GetCell(args.Value);

            if (_buildingToPlace != null)
            {
                _cellsToPlace.ForEach(cell => cell.OccupyCell(_buildingToPlace));
                _buildingToPlace = null;
                _cellsToPlace.Clear();
                return;
            }
            
            Debug.Log($"_selectedCell: {_selectedCell.name}");
        }
        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellSelected);
        }
    }
    
    public enum CursorDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
