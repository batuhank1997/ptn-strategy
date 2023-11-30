using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class PowerPlant : Building
    {
        public PowerPlant()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/PowerPlant");
        }
    }
}
