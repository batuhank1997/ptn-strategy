using System;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_selectionSpriteRenderer;
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
            SetCellVisual(CellState.UnderCursor);
        }

        private void OnMouseDown()
        {
            EventSystemManager.InvokeEvent(EventId.on_grid_left_click, new Vector2Arguments(_cellCoordinates));
        }

        private void OnMouseExit()
        {
            SetCellVisual(CellState.Empty);
        }

        private void SetCellVisual(CellState state)
        {
            switch (state)
            {
                case CellState.Empty:
                    SetCellSpriteAlpha(NOT_SELECTED_ALPHA);
                    break;
                case CellState.UnderCursor:
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
            var color = m_selectionSpriteRenderer.color;
            color = new Color(color.r, color.g, color.b, a);
            m_selectionSpriteRenderer.color = color;
        }
        
        private void SetSprite(Sprite sprite)
        {
            m_selectionSpriteRenderer.sprite = sprite;
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