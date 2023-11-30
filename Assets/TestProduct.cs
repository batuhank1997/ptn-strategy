using System;
using _Dev.Game.Scripts.Entities.Buildings;
using _Dev.Game.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

public class TestProduct : MonoBehaviour
{
    [SerializeField] private Button m_button;
    [SerializeField] private ProductType m_productType;

    private void Start()
    {
        m_button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        switch (m_productType)
        {
            case ProductType.Barrack:
                GridInputManager.Instance.SetBuildingForPlacing(new Barrack());
                break;
            case ProductType.PowerPlant:
                GridInputManager.Instance.SetBuildingForPlacing(new PowerPlant());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private enum ProductType
    {
        Barrack,
        PowerPlant,
    }
}