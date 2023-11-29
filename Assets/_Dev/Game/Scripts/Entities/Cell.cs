using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_selectionSpriteRenderer;

        private const float SELECTED_ALPHA = 0.2f;
        private const float NOT_SELECTED_ALPHA = 0.1f;
        
        private Vector2 _cellCoordinates;

        public void Init(Vector2 coordinates)
        {
            _cellCoordinates = coordinates;
            name = $"Grid ({_cellCoordinates.x} : {_cellCoordinates.y})";
        }
        
        private void OnMouseEnter()
        {
            GridInputManager.Instance.SetCellUnderCursor(this);
            SetSelectionSpriteColorAlpha(SELECTED_ALPHA);
        }

        private void OnMouseDown()
        {
            EventSystemManager.InvokeEvent(EventId.on_grid_left_click, new Vector2Arguments(_cellCoordinates));
        }

        private void OnMouseExit()
        {
            SetSelectionSpriteColorAlpha(NOT_SELECTED_ALPHA);
        }

        private void SetSelectionSpriteColorAlpha(float a)
        {
            var color = m_selectionSpriteRenderer.color;
            color = new Color(color.r, color.g, color.b, a);
            m_selectionSpriteRenderer.color = color;
        }
    }
}