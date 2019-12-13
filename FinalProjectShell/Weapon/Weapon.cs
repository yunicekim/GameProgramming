using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
/*
* Program name : Weapon.cs
* Purpose : to Make a weapon in the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*  Dec 6, 2019 added explosion animation by Yunice Kim
*/
namespace FinalProjectShell
{
    public class Weapon : DrawableGameComponent
    {
        #region variables
        private Texture2D texture;
        private Vector2 position;
        private float SPEED = 5f;
        SoundEffect soundFx;

        bool isAlive = true;
        double dieingTimer = 0.0;
        const double DIEING_DURATION = 1.5;

        //to shows explosion animation
        private Texture2D explodedTexture;
        Rectangle sourceRect;
        double frameTimer = 0.0;
        int currentFrame = 0;
        const int TILE_SIZE = 64;
        const int TILE_COUNT_EXPLOSION = 6;
        const double FRAME_DURATION = 0.1;

        #endregion

        #region ctor
        /// <summary>
        /// Sets the poisition of weapon in the contructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        public Weapon(Game game, Vector2 position) : base(game)
        {
            this.position = position;
        }
        #endregion

        #region overrides
        /// <summary>
        /// Draws the weapon's image on the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            if (isAlive)
            {
                sb.Draw(texture,
                    position,
                    Color.White);
            }
            else
            {
                sb.Draw(explodedTexture,
                    position,
                    sourceRect,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);
            }

            sb.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the weapon's position and contition
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (!isAlive)
            {
                dieingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (dieingTimer >= DIEING_DURATION)
                {
                    Game.Components.Remove(this);
                }

            }
            else
            {
                //makes the weapon go up continuously
                position.Y -= SPEED;

                //if the position is out of the screen
                if (position.Y < -texture.Height)
                {
                    //delete the component
                    Game.Components.Remove(this);
                }

                //Checks for collision with any enemy
                CheckWeaponForCollisionWithEnemy();
            }

            //shows the explosion images continuously
            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                frameTimer = 0;
                if (TILE_COUNT_EXPLOSION <= ++currentFrame)
                {
                    currentFrame = 0;
                }

                sourceRect.X = TILE_SIZE * currentFrame;
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// Loads all images needed in the weapon class
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/cookingKnife");
            explodedTexture = Game.Content.Load<Texture2D>("Images/Explosion Medium");

            //shows the explosion animation
            soundFx = Game.Content.Load<SoundEffect>("Sounds/explosion");
            sourceRect = new Rectangle(TILE_SIZE * currentFrame, 0, TILE_SIZE, TILE_SIZE);
            base.LoadContent();
        }

        #endregion

        #region helper methods
        /// <summary>
        /// Get the weapon's size
        /// </summary>
        /// <returns></returns>
        public Rectangle GetWeaponRectangle()
        {
            Rectangle bounds = texture.Bounds;
            bounds.Location = position.ToPoint();

            return bounds;
        }

        /// <summary>
        /// Check weapon collide with enemy
        /// </summary>
        private void CheckWeaponForCollisionWithEnemy()
        {
            ActionScene actionScene = Game.Services.GetService<ActionScene>();
            if (actionScene.CheckWeaponForCollisionWithEnemy(this))
            {
                soundFx.Play();
                isAlive = false;

                //Adds the score 
                AddScore();
            }
        }

        /// <summary>
        /// Adds the score to the score class
        /// </summary>
        private void AddScore()
        {
            Game.Services.GetService<Score>().AddScore(GetEnemyValue());
        }

        /// <summary>
        /// Get the enemy's value
        /// </summary>
        /// <returns></returns>
        private int GetEnemyValue()
        {
            return 1;
        } 
        #endregion
    }
}
