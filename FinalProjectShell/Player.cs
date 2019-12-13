using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
/*
* Program name : Player.cs
* Purpose : to Make a player in the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public class Player : DrawableGameComponent
    {
        #region variables
        private Texture2D player;
        private Texture2D explodePlayer;
        private Vector2 position = Vector2.Zero;
        private float speed = 3f;
        SoundEffect soundFx;
        SoundEffect soundFxForShooting;

        bool isAlive = true;
        double dieingTimer = 0.0;
        const double DIEING_DURATION = 0.1;

        //Connects this object to the game scene
        GameScene parent;

        KeyboardState oldKS;
        #endregion

        #region ctor
        /// <summary>
        /// Creates player's instance using GameScene's link
        /// </summary>
        /// <param name="game"></param>
        /// <param name="parent"></param>
        public Player(Game game, GameScene parent) : base(game)
        {
            if (Game.Services.GetService<Player>() == null)
            {
                Game.Services.AddService<Player>(this);
            }

            this.parent = parent;
            DrawOrder = 100;
        }
        #endregion

        #region overrides
        /// <summary>
        /// Loads contents need to use for player
        /// </summary>
        protected override void LoadContent()
        {
            player = Game.Content.Load<Texture2D>("Images/wonderWoman");
            explodePlayer = Game.Content.Load<Texture2D>("Images/wonderWoman-ouch");
            position = new Vector2((GraphicsDevice.Viewport.Width - player.Width) / 2,
                             (GraphicsDevice.Viewport.Height - player.Height));
            soundFx = Game.Content.Load<SoundEffect>("Sounds/ouch");
            soundFxForShooting = Game.Content.Load<SoundEffect>("Sounds/SHOOT011");

            base.LoadContent();
        }

        /// <summary>
        /// Updates the player's condition
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //if the player is dying
            if (!isAlive)
            {
                dieingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (dieingTimer >= DIEING_DURATION)
                {
                    isAlive = true;
                    dieingTimer = 0;
                }
            }
            else
            {
                KeyboardState ks = Keyboard.GetState();
                //moves the player when pressed the left or right key
                if (ks.IsKeyDown(Keys.Left))
                {
                    position.X -= speed;
                }
                else if (ks.IsKeyDown(Keys.Right))
                {
                    position.X += speed;
                }

                position.X = MathHelper.Clamp(position.X,
                                                0,
                                                GraphicsDevice.Viewport.Width - player.Width);
                position.Y = MathHelper.Clamp(position.Y,
                                                    0,
                                                    GraphicsDevice.Viewport.Height - player.Height);

                //When pressed the space bar, shoot the knife upward
                if (ks.IsKeyDown(Keys.Space) && oldKS.IsKeyUp(Keys.Space))
                {
                    parent.AddSceneComponent(new Weapon(Game, position));
                    soundFxForShooting.Play();
                }

                oldKS = ks;

                //Checks for collision with any enemy
                CollidedWithEnemy();

                //Checks for ending game
                FinishLevel();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the player on the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();

            if (isAlive)
            {
                sb.Draw(player,
                        position,
                        Color.White);
            }
            else
            {
                sb.Draw(explodePlayer, position, Color.White);
            }

            sb.End();

            base.Draw(gameTime);
        }

        #endregion

        #region helper methods
        /// <summary>
        /// Check the finish level
        /// </summary>
        private void FinishLevel()
        {
            if (Game.Services.GetService<LifeScore>().GetLifeScore() == 0)
            {
                //Shows notification of ending game
                ActionScene actionScene = Game.Services.GetService<ActionScene>();
                actionScene.ShowGameOver();
            }
        }

        /// <summary>
        /// Check collision with enemy
        /// </summary>
        internal void CollidedWithEnemy()
        {
            ActionScene actionScene = Game.Services.GetService<ActionScene>();
            if (actionScene != null)
            {
                if (actionScene.CheckPlayerCollisionWithEnemy(this))
                {
                    isAlive = false;
                    soundFx.Play();
                    RemoveLife();
                }
            }

        }

        /// <summary>
        /// Reset the player's position when the game starts new game
        /// </summary>
        internal void ResetPosition()
        {
            position = new Vector2((GraphicsDevice.Viewport.Width - player.Width) / 2,
                             (GraphicsDevice.Viewport.Height - player.Height));
        }

        /// <summary>
        /// Removes the player's life
        /// </summary>
        private void RemoveLife()
        {
            Game.Services.GetService<LifeScore>().RemoveLife();
        }

        /// <summary>
        /// Get the size of the player
        /// </summary>
        /// <returns></returns>
        public Rectangle GetPlayerRectangle()
        {
            Rectangle bounds = player.Bounds;
            bounds.Location = position.ToPoint();

            return bounds;
        } 
        #endregion
    }
}
