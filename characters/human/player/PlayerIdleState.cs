using Godot;

namespace ADKR.Game
{
    public class PlayerIdleState : CharacterState<Player>
    {

        public override void Update(double delta)
        {
            base.Update(delta);

            Vector2 dir = InputHandler.GetAxisInput();

            if (dir.LengthSquared() <= 0f) return;

            Character.State = new PlayerRunState(dir);
        }
    }
}
