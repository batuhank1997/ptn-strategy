using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier3 : Soldier
    {
        public Soldier3()
        {
            DamageDealer = new DamageDealer(10);
        }
        
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.Soldier3Icon,
                Name = "Soldier 3",
                Producer = null,
                BoardProduct = this
            };
        }
    }
}