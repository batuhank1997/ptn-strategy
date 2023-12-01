using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductionProduct : MonoBehaviour
    {
        [SerializeField] private Button m_button;
        
        public Image Icon;
        public TextMeshProUGUI Name;

        public void SetProductData(Sprite icon, string name)
        {
            Icon.sprite = icon;
            Name.text = name;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Debug.Log("Button clicked");
        }
    }
}