namespace GameEngine.Components.Main

{
    public abstract partial class Component : IComponent
    {
        public virtual Component Copy()
        {
            return this.MemberwiseClone() as Component;
        }
    }
}
