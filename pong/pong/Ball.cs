using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Threading;
using SharpDX;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace pong
{
    internal class Ball:Sprite
    {
        private Random rand;
        public int player1Score;
        public int player2Score;
        public bool isSlow;
        public bool isSlowBegin;
        public long end;
        public bool isrestart;
        public Ball(Texture2D texture, Vector2 position, Vector2 direction, float speed, Rectangle screen):base(texture, position, direction, speed, screen) 
        {
            rand= new Random();
            player1Score= 0;
            player2Score= 0;
            isSlow = false;
            isSlowBegin = false;
            isrestart = false; 
        }
        
        public override void Update(GameTime gametime)
        {
            BoundsScreen();
            if (!isSlow)
            {
                SpeedUp();
                isSlowBegin = false;
            }
            else
            {
               if(!isSlowBegin)
                {
                    isSlowBegin = true;
                    end = this.SlowBall(gametime);
                }
                if (gametime.TotalGameTime.Ticks - end >= 0)  isSlow = false;
            }
            
            base.Update(gametime);
        }
        public void BoundsScreen() {
            if (position.Y < 0 || position.Y > screen.Height - texture.Height) direction.Y *= -1;
            if (position.X < 0) {
                player2Score++;
                Restart();
                isrestart = true;
            } 
            if(position.X > screen.Width - texture.Width) {
                player1Score++;
                Restart();
                isrestart = true;
            }
        }

        public void BoundsPaddle(Paddle_Player PaddlePlayer, PaddleAI PaddleAI)
        {
            if (spriteBox.Intersects(PaddlePlayer.spriteBox)||spriteBox.Intersects(PaddleAI.spriteBox))
            {
                direction.X *= -1;
                direction.Y += rand.NextFloat(-1,1);
            }
            
        }

        public void StartBallPosition()
        {

        }
        public void Restart()
        {

            Poisition = new Microsoft.Xna.Framework.Vector2(screen.Width / 2 - texture.Width / 2, screen.Height / 2 - texture.Height / 2);
            speed = 3f;
            int randNumber = rand.Next(0, 4);

            switch (randNumber)
            {
                case 0:
                    Direction = new Vector2(1, 1);
                    break;

                case 1:
                    Direction = new Vector2(-1, 1);
                    break;

                case 2:
                    Direction = new Vector2(1, -1);
                    break;

                case 3:
                    Direction = new Vector2(-1, -1);
                    break;

            }

        }
        private void SpeedUp()
        {
            if (speed < 20) speed += 0.05f;
        }
        
        public long SlowBall(GameTime gametime)
        {
                isSlow = true;
                speed = 3f;
                TimeSpan duration = new TimeSpan(50000000);
                long end1 = gametime.TotalGameTime.Ticks + duration.Ticks;
                return end1;
        }
    }
}
