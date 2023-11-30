using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Dev.Game.Scripts.UI.Views
{
    public class ProductionProduct : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI Name;

        public void SetProductData(Sprite icon, string name)
        {
            Icon.sprite = icon;
            Name.text = name;
        }
    }
}