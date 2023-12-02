using _Dev.Game.Scripts.Entities;
using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
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
            HandleLeftClick();
            HandleRightClick();
        }
        
        private void HandleLeftClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
                
            var cell = GetCellUnderCursor();
            Debug.Log($"cell: {cell.name}");
        }
        
        private void HandleRightClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                
            }
        }
        
        private Cell GetCellUnderCursor()
        {
            var mousePos = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var gridSize = GridManager.Instance.GetGridSize();
            var zeroPos = new Vector2(-gridSize.x / 2, -gridSize.y / 2);
            
            var x = Mathf.Abs(zeroPos.x - mousePos.x);
            var y = Mathf.Abs(zeroPos.y - mousePos.y);
            
            var pos = new Vector2((int)x, (int)y);
            Debug.Log($"x: {pos.x} y: {pos.y}");
            return GridManager.Instance.GetCell(pos);
        }
    }
}