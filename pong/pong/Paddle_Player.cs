

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Dynamic;
using System.Windows.Forms;

namespace pong
{
    internal class Paddle_Player : Sprite
    {
        public int score;
        public bool isSlowBall;
        public int slowTimes;
        public Paddle_Player(Texture2D texture, Vector2 position, Vector2 direction, float speed, Rectangle screen) : base(texture, position, direction, speed,screen)
        {
            score = 0;
            slowTimes = 1;
            isSlowBall = false;
        }
        public override void Update(GameTime gametime)
        {
            direction = Vector2.Zero;
            InputKeyboard(gametime);
            BoundsRestrictions();
            base.Update(gametime);
        }
        public void BoundsRestrictions()
        {
            if (position.Y < 0) position.Y = 0;
            if (position.Y>screen.Height-texture.Height) position.Y=screen.Height-texture.Height;

        }

        private void InputKeyboard(GameTime gametime)
        {
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
                direction.Y = -1;
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) 
                direction.Y = 1;
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A) && slowTimes > 0)
            {
                slowTimes--;
                isSlowBall = true;
            }
        }

      

        
    }
}
