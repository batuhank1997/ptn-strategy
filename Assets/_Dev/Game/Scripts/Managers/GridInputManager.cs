using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Dev.Game.Scripts.Components;
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
        private readonly List<Cell> _cellsToPlace = new List<Cell>();
        
        private CursorDirection _cursorDirection;
        
        private bool _canPlaceBuilding = true;
        
        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellSelected);
            EventSystemManager.AddListener(EventId.on_grid_right_click, OnCellRightClicked);
            EventSystemManager.AddListener(EventId.on_production_product_clicked, OnProductionProductClicked);
        }

        private void OnCellRightClicked(EventArgs obj)
        {
            if (_selectedCell == null) 
                return;

            var targetCell = GridManager.Instance.GetCell(((Vector2Arguments) obj).Value);
            var product = _selectedCell.GetProductAndAmount().Item1;
            
            if (product is not Unit unit) 
                return;
            
            StartCoroutine(StartUnitMovementRoutine(unit, targetCell));
        }
        
        private IEnumerator StartUnitMovementRoutine(Unit unit, Cell targetCell)
        {
            var path = PathFinder.FindPath(_selectedCell.GetCoordinates(), targetCell.GetCoordinates());
            
            if (path == null)
                yield return null;

            var delay = new WaitForSeconds(0.15f);
            
            while (path.Count > 0)
            {
                var nextCell = path.First();
                path.Remove(nextCell);
                _selectedCell.ResetCell();
                nextCell.PlaceUnit(unit);
                _selectedCell = nextCell;                
                yield return delay;
            }
        }

        private void OnProductionProductClicked(EventArgs obj)
        {
            var product = _selectedCell.GetProductAndAmount().Item1;
            var pos = product.GetProductData().Producer.SpawnPosition;
            var cell = GridManager.Instance.GetCell(pos);
            
            //todo: production
            var type = ((TypeArguments) obj).Type;
            
            product.GetProductData().Producer.Produce(cell, type);
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

            var isOccupied = _cellsToPlace.Any(cell => cell.IsOccupied);
            
            if (isOccupied || _cellsToPlace.Count < size.x * size.y)
            {
                _canPlaceBuilding = false;
                _cellsToPlace.ForEach(cell => cell.SetCellVisual(CellState.InvalidForPlacement));
            }
            else
            {
                _canPlaceBuilding = true;
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

            if (_buildingToPlace == null) return;
            
            _cellsToPlace.ForEach(cell => cell.PlaceBuilding(_buildingToPlace));
            
            if (_buildingToPlace is IProducer)
            {
                var producer = (IProducer) _buildingToPlace;
                var spawnCell = GridManager.Instance.GetCell(_cellsToPlace.First().GetCoordinates() + Vector2.left);
                producer.SpawnPosition = spawnCell.GetCoordinates();
            }
            
            _buildingToPlace = null;
            _cellsToPlace.Clear();
        }
        
        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellSelected);
            EventSystemManager.RemoveListener(EventId.on_grid_right_click, OnCellRightClicked);
            EventSystemManager.RemoveListener(EventId.on_production_product_clicked, OnProductionProductClicked);
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
