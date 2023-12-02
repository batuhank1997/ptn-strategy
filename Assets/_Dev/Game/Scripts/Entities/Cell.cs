using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
using _Dev.Game.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private bool m_isDebug;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private TextMeshPro m_countText;
        [SerializeField] private TextMeshPro m_debugText;
        [SerializeField] private Sprite m_emptySprite;
        [SerializeField] private Sprite m_validPlacementSprite;
        [SerializeField] private Sprite m_invalidPlacementSprite;
        public bool IsOccupied => _units.Count != 0 || _building != null;

        private List<Unit> _units = new List<Unit>();
        private Building _building;

        private const float SELECTED_ALPHA = 0.8f;
        private const float NOT_SELECTED_ALPHA = 1f;

        private Vector2 _cellCoordinates;

        public void Init(Vector2 coordinates)
        {
            _cellCoordinates = coordinates;
            
            name = $"Grid ({_cellCoordinates.x} : {_cellCoordinates.y})";
            if (m_isDebug)
            {
                m_debugText.gameObject.SetActive(true);
                m_debugText.text = $"{_cellCoordinates.x}:{_cellCoordinates.y}";
            }
            
            SetCellVisual(CellState.Empty);
            EventSystemManager.AddListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);
        }

        private void OnDestroy()
        {
            EventSystemManager.RemoveListener(EventId.on_cursor_direction_changed, OnCursorDirectionChanged);
        }

        public void PlaceBuilding(Building buildingToPlace)
        {
            m_countText.gameObject.SetActive(false);
            _building = buildingToPlace;
            SetCellVisual(CellState.HasBuilding);
        }
        
        public void PlaceUnit(Unit unit)
        {
            m_countText.gameObject.SetActive(true);
            m_countText.text = (_units.Count + 1).ToString();
            _units.Add(unit);
            SetSprite(unit.GetProductData().Icon);
        }

        public Vector2 GetCoordinates()
        {
            return _cellCoordinates;
        }

        private void OnCursorDirectionChanged(EventArgs obj)
        {
            if (_building != null)
            {
                SetCellVisual(CellState.HasBuilding);
                return;
            }

            SetCellVisual(_units.Count != 0 ? CellState.HasUnit : CellState.Empty);
        }

        private void OnMouseEnter()
        {
            GridInputManager.Instance.SetCellUnderCursor(this);
            GridInputManager.Instance.SetCellStatesIfPlacing();
        }

        private void OnMouseDown()
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            if (IsOccupied)
            {
                //todo:select unit or building
            }

            EventSystemManager.InvokeEvent(
                Input.GetMouseButtonDown(0) ? EventId.on_grid_left_click : EventId.on_grid_right_click,
                new Vector2Arguments(_cellCoordinates));
        }

        //todo: refactor
        public void SetCellVisual(CellState state)
        {
            switch (state)
            {
                case CellState.Empty:
                    m_countText.text = "";
                    SetSprite(m_emptySprite);
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
                case CellState.HasBuilding:
                    SetSprite(_building.GetProductData().Icon);
                    SetCellSpriteAlpha(NOT_SELECTED_ALPHA);
                    break;
                case CellState.HasUnit:
                    SetSprite(_units[0].GetProductData().Icon);
                    SetCellSpriteAlpha(NOT_SELECTED_ALPHA);
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

        public (IProduct, int) GetProductAndAmount()
        {
            if (_building != null)
                return (_building, 1);

            if (_units.Count != 0)
                return (_units[0], _units.Count);

            return (null, 0);
        }

        public void ResetCell()
        {
            _building = null;
            _units.Clear();
            SetCellVisual(CellState.Empty);
        }
    }

    public enum CellState
    {
        Empty,
        UnderCursor,
        ReadyForPlacement,
        InvalidForPlacement,
        HasBuilding,
        HasUnit,
    }
}