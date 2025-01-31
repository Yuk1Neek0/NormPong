using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    internal class Sprite
    {
        protected Texture2D texture;

        protected Vector2 direction;

        protected Vector2 position;
            
        public Vector2 Poisition
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public Vector2 Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        protected float speed;
        protected Rectangle screen;

        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        public Rectangle spriteBox { 
            get { return new Rectangle((int)position.X,(int)position.Y,(int)texture.Width,(int)texture.Height); }
        }
        public Sprite(Texture2D texture, Vector2 position, Vector2 direction, float speed, Rectangle screen)
        {
            this.texture = texture;
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            this.screen = screen;
        }

        public virtual void Update(GameTime gametime)
        {
            position += direction * speed;
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
