using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Dynamic;

namespace pong
{
    internal class PaddleAI:Sprite
    {
        public int score;
        public bool isSlowBall;
        public int slowTimes;
        public PaddleAI(Texture2D texture, Vector2 position, Vector2 direction, float speed, Rectangle screen):base(texture, position, direction, speed,screen)
        {
            score = 0;
            slowTimes = 1;
        }

        public override void Update(GameTime gametime)
        {
            direction = Vector2.Zero;
            InputKeyboard();
            BoundsRestrictions();
            base.Update(gametime);
        }
        private void InputKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                direction.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                direction.Y = 1;
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)&&slowTimes>0)
            {
                slowTimes--;
                isSlowBall = true;
            }
        }
        public void BoundsRestrictions()
        {
            if (position.Y < 0) position.Y = 0;
            if (position.Y > screen.Height - texture.Height) position.Y = screen.Height - texture.Height;

        }
    }
    
}
