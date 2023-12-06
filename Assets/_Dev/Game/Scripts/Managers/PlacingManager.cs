using System;
using System.Collections.Generic;
using System.Linq;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class PlacingManager : Singleton<PlacingManager>
    {
        private readonly List<Cell> _cellsToPlace = new List<Cell>();
        private readonly List<Barrack> _barracks = new List<Barrack>();

        private Building _buildingToPlace;
        private Unit _unitToPlace;
        private bool _canPlaceBuilding;

        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnLeftClick);
            EventSystemManager.AddListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);
        }

        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnLeftClick);
            EventSystemManager.RemoveListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);

            _barracks.ForEach(b => b.CleanUp());
        }

        private void OnCursorDirectionChanged(EventArgs obj)
        {
            _cellsToPlace.Clear();
        }

        public void SetBuildingForPlacing(Building building)
        {
            _buildingToPlace = building;

            if (_buildingToPlace is Barrack barrack)
                _barracks.Add(barrack);
        }

        public void SetUnitForPlacing(Unit unit)
        {
            ResetPlacing();
            _unitToPlace = unit;
        }

        public void SetPlacableCellVisualsIfPlacing(Cell cellUnderCursor)
        {
            PlacableCellsForBuilding(cellUnderCursor);
            PlacableCellsForUnit(cellUnderCursor);
        }

        private void PlacableCellsForBuilding(Cell cellUnderCursor)
        {
            if (_buildingToPlace == null) return;

            var x = _buildingToPlace.GetSize().x;
            var y = _buildingToPlace.GetSize().y;
            
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var offset = new Vector2(i, j);
                    var coordinates = cellUnderCursor.GetCoordinates() + offset;

                    if (GridManager.Instance.IsOutsideOfGameBoard(coordinates))
                        continue;

                    var cell = GridManager.Instance.GetCell(coordinates);
                    _cellsToPlace.Add(cell);
                    cell.SetCellVisual(CellState.ReadyForPlacement);
                }
            }

            SetVisuals(x, y);
        }

        private void SetVisuals(float x, float y)
        {
            bool isOccupied;
            
            if (_buildingToPlace is IProducer)
            {
                var spawnCell = GridManager.Instance.GetCell(_cellsToPlace[0].GetCoordinates() + Vector2.left);
                isOccupied = _cellsToPlace.Any(cell => cell.IsOccupied || cell.IsSpawnCell) || spawnCell.IsOccupied;
            }
            else
                isOccupied = _cellsToPlace.Any(cell => cell.IsOccupied || cell.IsSpawnCell);

            if (isOccupied || _cellsToPlace.Count < x * y)
            {
                _canPlaceBuilding = false;
                _cellsToPlace.ForEach(cell => cell.SetCellVisual(CellState.InvalidForPlacement));
            }
            else
                _canPlaceBuilding = true;
        }

        private void PlacableCellsForUnit(Cell cellUnderCursor)
        {
            if (_unitToPlace == null) return;

            var coordinates = cellUnderCursor.GetCoordinates();

            if (GridManager.Instance.IsOutsideOfGameBoard(coordinates) || cellUnderCursor.IsOccupied)
            {
                _canPlaceBuilding = false;
                return;
            }

            var cell = GridManager.Instance.GetCell(coordinates);
            _cellsToPlace.Add(cell);
            cell.SetCellVisual(CellState.ReadyForPlacement);

            _canPlaceBuilding = true;
        }

        private void OnLeftClick(EventArgs obj)
        {
            if (_buildingToPlace != null && _canPlaceBuilding)
            {
                _cellsToPlace.ForEach(c => c.PlaceBuilding(_buildingToPlace));
                _buildingToPlace.SetStartingPosition(_cellsToPlace[0].GetCoordinates());

                if (_buildingToPlace is IProducer producer)
                    producer.SetSpawnableArea();

                _buildingToPlace = null;
                _cellsToPlace.Clear();
            }
            else if (_unitToPlace != null && _canPlaceBuilding)
            {
                var cell = _cellsToPlace[0];
                _unitToPlace.SetStartingPosition(cell.GetCoordinates());
                cell.PlaceUnit(_unitToPlace);
                _unitToPlace = null;
            }
        }
        
        private void ResetPlacing()
        {
            _buildingToPlace = null;
            _unitToPlace = null;
            _cellsToPlace.Clear();
        }
    }
}