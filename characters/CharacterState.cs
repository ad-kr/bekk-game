namespace ADKR.Game
{
    public abstract class CharacterState<T> : CharacterState where T : Character
    {
        public new T Character { get; set; }

        internal override void SetCharacter(Character character)
        {
            base.SetCharacter(character);
            Character = character as T;
        }
    }

    public abstract class CharacterState
    {
        public virtual Character Character { get; set; }

        public virtual void Start() { }
        public virtual void Update(double delta) { }
        public virtual void End() { }

        internal virtual void SetCharacter(Character character)
        {
            Character = character;
        }
    }
}