namespace Destiny.CoreModules
{
    public abstract class QuadtreeItem
    {
        public abstract bool Compare(QuadtreeItem other);
        public abstract QuadtreeItem Clone();
    }
}