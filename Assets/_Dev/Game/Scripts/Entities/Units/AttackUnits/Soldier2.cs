using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier2 : Soldier
    {
        public Soldier2()
        {
            DamageDealer = new DamageDealer(5);
        }
        
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.Soldier2Icon,
                Name = "Soldier 2",
                Producer = null,
                Product = this
            };
        }
    }
}