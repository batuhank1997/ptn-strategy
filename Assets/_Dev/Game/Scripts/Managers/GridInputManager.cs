using System;
using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class GridInputManager : Singleton<GridInputManager>
    {
        private Cell _cellUnderCursor;
        private Cell _selectedCell;
        
        public void Initilize()
        {
            EventSystemManager.AddListener(EventId.on_grid_left_click, OnGridSelected);
        }

        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_grid_left_click, OnGridSelected);
        }

        private void OnGridSelected(EventArgs obj)
        {
            var args = (Vector2Arguments) obj;
            
            _selectedCell = GridManager.Instance.GetCell(args.Value);
            Debug.Log($"_selectedCell: {_selectedCell.name}");
        }

        public void SetCellUnderCursor(Cell cell)
        {
            _cellUnderCursor = cell;
        }
    }
}
