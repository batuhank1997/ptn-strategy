using _Dev.Game.Scripts.Components;
using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class PowerPlant : Building
    {
        public PowerPlant() : base()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/PowerPlant");
        }

        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.PowerPlantIcon,
                Name = _buildingSo.Name,
                Producer = null
            };
        }
    }
}
