using System;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Sprite m_emptySprite;
        [SerializeField] private Sprite m_validPlacementSprite;
        [SerializeField] private Sprite m_invalidPlacementSprite;
        
        private IProduct _product;

        private const float SELECTED_ALPHA = 0.8f;
        private const float NOT_SELECTED_ALPHA = 1f;
        
        private Vector2 _cellCoordinates;

        public void Init(Vector2 coordinates)
        {
            _cellCoordinates = coordinates;
            name = $"Grid ({_cellCoordinates.x} : {_cellCoordinates.y})";
            EventSystemManager.AddListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);
        }

        private void OnCursorDirectionChanged(EventArgs obj)
        {
            SetCellVisual(CellState.Empty);
        }

        public IProduct GetProduct()
        {
            return _product;
        }
        
        public void SetProduct(IProduct product)
        {
            _product = product;
        }
        
        private void OnMouseEnter()
        {
            GridInputManager.Instance.SetCellUnderCursor(this);
            GridInputManager.Instance.SetCellStatesIfPlacing();
        }

        private void OnMouseDown()
        {
            //todo: if is occupied, select unit or building
            
            EventSystemManager.InvokeEvent(
                Input.GetMouseButtonDown(0) ? EventId.on_grid_left_click : EventId.on_grid_right_click,
                new Vector2Arguments(_cellCoordinates));
        }

        public void SetCellVisual(CellState state)
        {
            switch (state)
            {
                case CellState.Empty:
                    SetSprite(m_emptySprite);
                    SetCellSpriteAlpha(NOT_SELECTED_ALPHA);
                    break;
                case CellState.UnderCursor:
                    SetSprite(m_emptySprite);
                    SetCellSpriteAlpha(SELECTED_ALPHA);
                    break;
                case CellState.ReadyForPlacement:
                    SetSprite(m_validPlacementSprite);
                    break;
                case CellState.InvalidForPlacement:
                    SetSprite(m_invalidPlacementSprite);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        private void SetCellSpriteAlpha(float a)
        {
            var color = m_spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, a);
            m_spriteRenderer.color = color;
        }
        
        private void SetSprite(Sprite sprite)
        {
            m_spriteRenderer.sprite = sprite;
        }

        public Vector2 GetCoordinates()
        {
            return _cellCoordinates;
        }
    }

    public enum CellState
    {
        Empty,
        UnderCursor,
        ReadyForPlacement,
        InvalidForPlacement,
    }
}