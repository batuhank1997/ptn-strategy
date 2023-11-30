using _Dev.Utilities.Singleton;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities
{
    [CreateAssetMenu(fileName = "ImageContainer", menuName = "ScriptableObjects/ImageContainer", order = 0)]
    public class ImageContainer : ScriptableSingleton<ImageContainer>
    {
        public Sprite PowerPlantIcon;
        public Sprite BarracksIcon;
        public Sprite SoldierIcon;
    }
}