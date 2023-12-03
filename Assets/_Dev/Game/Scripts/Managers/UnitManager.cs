using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private readonly WaitForSeconds _delay = new WaitForSeconds(0.1f);
        
        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnLeftClick);
            EventSystemManager.AddListener(EventId.on_grid_right_click, OnRightClick);
        }


        private void OnDestroy()
        {
            _units?.Clear();
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnLeftClick);
            EventSystemManager.RemoveListener(EventId.on_grid_right_click, OnRightClick);
        }

        private void OnLeftClick(EventArgs obj)
        {
            var args = (Vector2Arguments) obj;
            
            var cell = GridManager.Instance.GetCell(args.Value);
            _unitsCell = cell;
            _units = _unitsCell.GetUnits();
        }
        
        private void OnRightClick(EventArgs obj)
        {
            var args = (Vector2Arguments) obj;
            
            var targetCell = GridManager.Instance.GetCell(args.Value);

            if (targetCell.GetBuilding() != null)
            {
                Attack();
                // return;
            }

            if (_units == null || _unitsCell.GetUnits() == null)
                return;

            StartCoroutine(StartUnitMovementRoutine(_unitsCell, targetCell));
        }

        private void Attack()
        {
            Debug.Log($"attack");
        }

        private IEnumerator StartUnitMovementRoutine(Cell currentCell, Cell targetCell)
        {
            var path = PathFinder.FindPath(currentCell.GetCoordinates(), targetCell.GetCoordinates());
            var units = new List<Unit>(_units);
            var sprite = units.First().GetProductData().Icon;
            
            _unitsCell.ResetCell();

            foreach (var cell in path)
            {
                if (cell == path.Last() || cell == path.First())
                    continue;

                yield return _delay;
                cell.PlayMovingAnimation(sprite);
            }
            
            units.ForEach(unit =>
            {
                unit.Mover.MoveToAlongPath(path, unit);
            });
            
        }
    }
}