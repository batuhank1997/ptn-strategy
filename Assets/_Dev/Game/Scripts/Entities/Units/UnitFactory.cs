namespace _Dev.Game.Scripts.Entities.Units
{
    public static class UnitFactory
    {
        public static Unit Create<T>() where T : Unit , new()
        {
            return new T();
        }
    }
}