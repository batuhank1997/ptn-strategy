using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    [CreateAssetMenu(fileName = "Building", menuName = "ScriptableObjects/Building")]
    public class BuildingSo : ScriptableObject
    {
        public Vector2 Size;
        public string Name;
    }
}