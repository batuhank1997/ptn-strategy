namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier3 : Soldier
    {
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.Soldier3Icon,
                Name = "Soldier 3",
                Producer = null,
                Product = this
            };
        }
    }
}