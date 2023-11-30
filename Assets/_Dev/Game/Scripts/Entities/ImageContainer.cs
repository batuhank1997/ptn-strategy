using _Dev.Utilities.Singleton;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Dev.Game.Scripts.Entities
{
    [CreateAssetMenu(fileName = "ImageContainer", menuName = "ScriptableObjects/ImageContainer", order = 0)]
    public class ImageContainer : ScriptableSingleton<ImageContainer>
    {
        public Sprite PowerPlantIcon;
        public Sprite BarracksIcon;
        public Sprite Soldier1Icon;
        public Sprite Soldier2Icon;
        public Sprite Soldier3Icon;
    }
}