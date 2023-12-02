namespace _Dev.Game.Scripts.Entities.Buildings
{
    public class BuildingFactory
    {
        public Building Create<T>() where T : Building , new()
        {
            return new T();
        }
    }
}