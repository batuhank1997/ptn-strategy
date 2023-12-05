using _Dev.Game.Scripts.Entities;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        private Cell _cellUnderCursor;
        private Camera _mainCamera;
     
        public void Initilize()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            HandleHoover();
            HandleLeftClick();
            HandleRightClick();
        }

        private void HandleHoover()
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            var currentCell = GetCellUnderCursor();

            if (currentCell == null) return;
            
            if (currentCell == _cellUnderCursor)
                return;
            
            EventSystemManager.InvokeEvent(EventId.on_cursor_direction_changed);
            _cellUnderCursor = currentCell;
            
            if (!_cellUnderCursor.IsOccupied)
                _cellUnderCursor.SetCellVisual(CellState.UnderCursor);
            
            //todo: refactor
            PlacingManager.Instance.SetPlacableCellVisualsIfPlacing(_cellUnderCursor);
        }

        private void HandleLeftClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
                
            var cell = GetCellUnderCursor();
            EventSystemManager.InvokeEvent(EventId.on_grid_left_click, new Vector2Arguments(cell.GetCoordinates()));
        }
        
        private void HandleRightClick()
        {
            if (!Input.GetMouseButtonDown(1)) return;
            
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
            
            var cell = GetCellUnderCursor();
            EventSystemManager.InvokeEvent(EventId.on_grid_right_click, new Vector2Arguments(cell.GetCoordinates()));
        }
        
        private Cell GetCellUnderCursor()
        {
            var mousePos = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var gridSize = GridManager.Instance.GetGridSize();
            var zeroPos = new Vector2(-gridSize.x / 2, -gridSize.y / 2);
            
            var x = Mathf.Abs(zeroPos.x - mousePos.x);
            var y = Mathf.Abs(zeroPos.y - mousePos.y);
            
            var pos = new Vector2((int)x, (int)y);
            return GridManager.Instance.GetCell(pos);
        }
    }
}