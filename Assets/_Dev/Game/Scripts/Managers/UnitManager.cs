﻿using System;
using System.Collections;
using System.Collections.Generic;
using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class UnitManager : Singleton<UnitManager>
    {
        private List<Unit> _units;
        private Cell _unitsCell;
        
        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnCellSelected);
            EventSystemManager.AddListener(EventId.on_grid_right_click, OnCellTargeted);
        }


        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnCellSelected);
            EventSystemManager.RemoveListener(EventId.on_grid_right_click, OnCellTargeted);
        }

        private void OnCellSelected(EventArgs obj)
        {
            var args = (Vector2Arguments) obj;
            
            var cell = GridManager.Instance.GetCell(args.Value);
            _unitsCell = cell;
            _units = _unitsCell.GetUnits();
        }
        
        private void OnCellTargeted(EventArgs obj)
        {
            var args = (Vector2Arguments) obj;
            
            var targetCell = GridManager.Instance.GetCell(args.Value);

            if (_units == null || _unitsCell.GetUnits() == null)
                return;

            StartCoroutine(StartUnitMovementRoutine(_unitsCell, targetCell));
        }
        
        private IEnumerator StartUnitMovementRoutine(Cell currentCell, Cell targetCell)
        {
            var path = PathFinder.FindPath(currentCell.GetCoordinates(), targetCell.GetCoordinates());
            
            var delay = new WaitForSeconds(0.15f);

            if (path == null) yield break;
            
            var count = path.Count;
            
            for (var i = 1; i < count; i++)
            {
                var cell = path[i];

                cell.PlaceUnit(_units[0]);
                yield return delay;
                cell.ResetCell();
            }
        }
    }
}