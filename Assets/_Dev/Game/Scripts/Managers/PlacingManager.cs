﻿using System;
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
        
        private Building _buildingToPlace;
        private Unit _unitToPlace;
        private bool _canPlaceBuilding;

        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellSelected);
            EventSystemManager.AddListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);
        }

        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellSelected);
            EventSystemManager.RemoveListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);
        }

        private void OnCursorDirectionChanged(EventArgs obj)
        {
            _cellsToPlace.Clear();
        }
        
        public void SetBuildingForPlacing(Building building)
        {
            _buildingToPlace = building;
        }
        
        public void SetUnitForPlacing(Unit unit)
        {
            _unitToPlace = unit;
        }

        private void OnCellSelected(EventArgs obj)
        {
            if (_buildingToPlace != null && _canPlaceBuilding)
            {
                _cellsToPlace.ForEach(c => c.PlaceBuilding(_buildingToPlace));
                if (_buildingToPlace is IProducer producer)
                    producer.SpawnPosition = _cellsToPlace[0].GetCoordinates() + new Vector2(-1, 0); //todo: refactor
                _buildingToPlace = null;
                _cellsToPlace.Clear();
            }
            else if (_unitToPlace != null && _canPlaceBuilding)
            {
                var cell = _cellsToPlace[0];
                cell.PlaceUnits(new List<Unit>(){_unitToPlace});
                _unitToPlace = null;
            }
        }
        
        public void SetPlacableCellVisualsIfPlacing(Cell cellUnderCursor)
        {
            //todo: refactor
            
            if (_buildingToPlace != null)
            {
                var size = _buildingToPlace.Size;

                for (var i = 0; i < size.x; i++)
                {
                    for (var j = 0; j < size.y; j++)
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
            
            else if (_unitToPlace != null)
            {
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
        }
    }
}