namespace GameEngine.GameData
{
    public abstract class QuadtreeItem
    {
        public abstract bool Compare(QuadtreeItem other);
        public abstract QuadtreeItem Clone();
    }
}