using System;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
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
        
        private CursorDirection _cursorDirection;
        
        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnGridSelected);
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
                    
                    GridManager.Instance.GetCell(coordinates).SetCellVisual(CellState.ReadyForPlacement);
                }
            }
        }
        
        public void SetBuildingForPlacing(Building building)
        {
            _buildingToPlace = building;
        }

        private void OnGridSelected(EventArgs obj)
        {
            var args = (Vector2Arguments) obj;
            
            _selectedCell = GridManager.Instance.GetCell(args.Value);
            Debug.Log($"_selectedCell: {_selectedCell.name}");
        }
        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnGridSelected);
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
