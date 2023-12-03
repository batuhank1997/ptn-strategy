using _Dev.Game.Scripts.Components;

namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier1 : Soldier
    {
        public Soldier1()
        {
            DamageDealer = new DamageDealer(2);
        }

        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.Soldier1Icon,
                Name = "Soldier 1",
                Producer = null,
                Product = this
            };
        }
    }
}