namespace ADKR.Game
{
    public abstract class CharacterEffect<T> : CharacterEffect where T : Character
    {
        public new T Char { get; set; }

        internal override void SetCharacter(Character character)
        {
            base.SetCharacter(character);
            Char = character as T;
        }
    }

    public abstract class CharacterEffect
    {
        public virtual Character Char { get; set; }

        public bool IsUnique { get; set; } = false;

        public virtual void Start() { }
        public virtual void Update(double delta) { }
        public virtual void PhysicsUpdate(double delta) { }
        public virtual void End() { }

        internal virtual void SetCharacter(Character character)
        {
            Char = character;
        }
    }
}