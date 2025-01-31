using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Paddle_Player PaddlePlayer;
        private PaddleAI PlayerAI;
        private Texture2D PaddleTexture, BallTexture, PaddleAITexture;
        private Ball _ball;
        private Microsoft.Xna.Framework.Rectangle screen;
        private Random rand;
        private SpriteFont ScorePlayer, ScoreAI,BallSpeed,winLose;
        private int winScore;
        private bool isOver;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            screen = new Microsoft.Xna.Framework.Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _graphics.ApplyChanges();
            rand = new Random();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            winScore = 5;
            isOver=false;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            PaddleTexture = Content.Load<Texture2D>("Paddle");
            PaddlePlayer = new Paddle_Player(PaddleTexture, new Vector2(0, screen.Height / 2 - PaddleTexture.Height / 2), Vector2.Zero, 5f, screen);
            BallTexture = Content.Load<Texture2D>("Ball");
            PaddleAITexture = Content.Load<Texture2D>("Paddle");
            PlayerAI = new PaddleAI(PaddleTexture, new Vector2(screen.Width - PaddleAITexture.Width, screen.Height / 2 - PaddleTexture.Height / 2), Vector2.Zero, 5f, screen);
            ScorePlayer = Content.Load<SpriteFont>("File");
            ScoreAI = Content.Load<SpriteFont>("File");
            BallSpeed= Content.Load<SpriteFont>("File");
            winLose = Content.Load<SpriteFont>("File");
            _ball = new Ball(BallTexture, new Vector2(screen.Width / 2 - BallTexture.Width / 2, screen.Height / 2 - BallTexture.Height / 2), new Vector2(0, -1), 1f, screen);
            _ball.Restart();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            if (_ball.player1Score<winScore && _ball.player2Score < winScore)
            {
                
                PaddlePlayer.Update(gameTime);
                PlayerAI.Update(gameTime);
                _ball.BoundsPaddle(PaddlePlayer, PlayerAI);
                _ball.Update(gameTime);
                if (PaddlePlayer.isSlowBall)
                {
                    PaddlePlayer.isSlowBall = false;
                    _ball.isSlow = true;
                }
                if (PlayerAI.isSlowBall)
                {
                    PlayerAI.isSlowBall = false;
                    _ball.isSlow = true;
                }
                if (_ball.isrestart) 
                { 
                    _ball.isSlow= false;
                    PlayerAI.slowTimes = 1;
                    PaddlePlayer.slowTimes = 1;
                    _ball.isrestart = false;
                }
                
            }
            else isOver = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(ScorePlayer,_ball.player1Score.ToString(),new Vector2(screen.Width/2-50,50),Microsoft.Xna.Framework.Color.White);
            _spriteBatch.DrawString(ScoreAI, _ball.player2Score.ToString(), new Vector2(screen.Width / 2 + 50, 50), Microsoft.Xna.Framework.Color.White);
            if (!isOver)
            {
                if (_ball.player1Score < winScore && _ball.player2Score < winScore)
                {
                    _spriteBatch.DrawString(winLose, "Slow Times:"+PaddlePlayer.slowTimes.ToString(), new Vector2(0, 70), Microsoft.Xna.Framework.Color.White);
                    _spriteBatch.DrawString(winLose, "Slow Times:" + PlayerAI.slowTimes.ToString(), new Vector2(screen.Width - 250, 70), Microsoft.Xna.Framework.Color.White);
                }

                //_spriteBatch.DrawString(BallSpeed, gameTime.TotalGameTime.Ticks.ToString(), new Vector2(screen.Width / 2, 70), Microsoft.Xna.Framework.Color.White);
                //_spriteBatch.DrawString(BallSpeed, _ball.end.ToString(), new Vector2(screen.Width / 2, 70), Microsoft.Xna.Framework.Color.White);
                PaddlePlayer.Draw(_spriteBatch);
                PlayerAI.Draw(_spriteBatch);
                _ball.Draw(_spriteBatch);
                if (_ball.isSlow) _spriteBatch.DrawString(winLose, "Slow>_<", new Vector2(screen.Width / 2 - 50, 90), Microsoft.Xna.Framework.Color.White);

            }
            else
            {
                if (_ball.player1Score >= winScore)
                {
                    _spriteBatch.DrawString(winLose, "Win!!!", new Vector2(10, 70), Microsoft.Xna.Framework.Color.White);
                    _spriteBatch.DrawString(winLose, "Lose...", new Vector2(screen.Width - 100, 70), Microsoft.Xna.Framework.Color.White);
                }
                else if (_ball.player2Score >= winScore)
                {
                    _spriteBatch.DrawString(winLose, "Lose...", new Vector2(50, 70), Microsoft.Xna.Framework.Color.White);
                    _spriteBatch.DrawString(winLose, "WIN...", new Vector2(screen.Width - 150, 70), Microsoft.Xna.Framework.Color.White);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
}
