namespace _Dev.Game.Scripts.Entities.Buildings
{
    public static class BuildingFactory
    {
        public static Building Create<T>() where T : Building , new()
        {
            return new T();
        }
    }
}