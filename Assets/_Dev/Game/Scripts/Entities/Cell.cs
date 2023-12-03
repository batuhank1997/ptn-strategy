using System;
using System.Collections.Generic;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Entities.Units;
using _Dev.Game.Scripts.EventSystem;
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

        private readonly List<Unit> _units = new List<Unit>();
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
            _building = buildingToPlace;
            m_countText.gameObject.SetActive(false);
            SetCellVisual(CellState.HasBuilding);
        }
        
        public void PlaceUnits(List<Unit> units)
        {
            _units.AddRange(units);
            m_countText.gameObject.SetActive(true);
            m_countText.text = (_units.Count).ToString();
            SetCellVisual(CellState.HasUnit);
        }

        public Vector2 GetCoordinates()
        {
            return _cellCoordinates;
        }
        
        public Building GetBuilding()
        {
            return _building;
        }
        
        public List<Unit> GetUnits()
        {
            return _units;
        }

        public void ResetCell()
        {
            _building = null;
            _units.Clear();
            SetCellVisual(CellState.Empty);
        }

        private void OnCursorDirectionChanged(EventArgs obj)
        {
            if (_building != null)
                SetCellVisual(CellState.HasBuilding);
            else if (_units != null && _units.Count != 0)
                SetCellVisual(CellState.HasUnit);
            else
                SetCellVisual(CellState.Empty);
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