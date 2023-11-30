namespace _Dev.Game.Scripts.Entities.Units.AttackUnits
{
    public class Soldier2 : Soldier
    {
        public override ProductData GetProductData()
        {
            return new ProductData
            {
                Icon = ImageContainer.Instance.Soldier2Icon,
                Name = "Soldier 2",
                Producer = null,
            };
        }
    }
}