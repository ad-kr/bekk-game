namespace ADKR.Game
{
    public abstract class CharacterState<T> : CharacterState where T : Character
    {
        public new T Char { get; set; }

        internal override void SetCharacter(Character character)
        {
            base.SetCharacter(character);
            Char = character as T;
        }
    }

    public abstract class CharacterState
    {
        public virtual Character Char { get; set; }

        public virtual void Start() { }
        public virtual void Update(double delta) { }
        public virtual void End() { }

        internal virtual void SetCharacter(Character character)
        {
            Char = character;
        }
    }
}