using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class PowerPlant : Building
    {
        public PowerPlant()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/PowerPlant");
        }

        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.PowerPlantIcon,
                Name = _buildingSo.Name
            };
        }
    }
}
