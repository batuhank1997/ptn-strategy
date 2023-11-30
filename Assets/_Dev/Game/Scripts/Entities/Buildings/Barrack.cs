using _Dev.Game.Scripts.Entities.Units;
using UnityEngine;

namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class Barrack : Building, IProducer
    {
        public Barrack()
        {
            _buildingSo = Resources.Load<BuildingSo>("Buildings/Barrack");
        }
        
        public void Produce()
        {
            
        }

        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.BarracksIcon,
                Name = _buildingSo.Name
            };
        }
    }
}
