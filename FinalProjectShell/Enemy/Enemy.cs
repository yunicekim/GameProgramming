using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
/*
* Program name : Enemy.cs
* Purpose : to Make a Enemy in the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    enum EnemyType
    {
        LaundryBasket,
        LunchBox,
        Vacum
    }

    class Enemy : DrawableGameComponent
    {
        #region variables

        Texture2D texture;
        Vector2 position;
        EnemyType type;
        Texture2D explodeTexture;
        SoundEffect soundFx;

        int xDirection = 1;
        int yDirection = 1;
        const float DISTANCE = 3f;

        bool isAlive = true;
        double dieingTimer = 0.0;
        const double DIEING_DURATION = 0.1;

        #endregion

        #region ctor

        public Enemy(Game game, Vector2 position, EnemyType type) : base(game)
        {
            this.position = position;
            this.type = type;
        }

        #endregion

        #region overrides

        /// <summary>
        /// draws the enemy on the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();

            if (isAlive)
            {
                sb.Draw(texture, position, Color.White);
            }
            
            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// update the enemy's status
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (isAlive == false)
            {
                dieingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (dieingTimer >= DIEING_DURATION)
                {
                    Game.Components.Remove(this);
                }
            }

            position.X += DISTANCE * xDirection;
            position.Y += DISTANCE * yDirection;

            if (position.X < 0 || position.X > GraphicsDevice.Viewport.Width - texture.Width)
            {
                xDirection *= -1;
            }

            if (position.Y < 0 || position.Y > GraphicsDevice.Viewport.Height - texture.Height * 3)
            {
                xDirection *= -1;
            }

            if (position.Y > GraphicsDevice.Viewport.Height)
            {
                Game.Components.Remove(this);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// load all needed to the enemy
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>($"Images/{type.ToString()}");
            explodeTexture = Game.Content.Load<Texture2D>("Images/boom");
            soundFx = Game.Content.Load<SoundEffect>("Sounds/explosion");

            base.LoadContent();
        }

        #endregion

        #region helper methods

        /// <summary>
        /// when collision detected, make collision sound
        /// </summary>
        internal void CollisionDetected()
        {
            // TODO:  what happens with this enemy
            // when a collision occured? Explode? disappear
            // and add to scrore etc.
            soundFx.Play();
            isAlive = false;
        }

        /// <summary>
        /// get the size of enemy
        /// </summary>
        /// <returns></returns>
        public Rectangle GetEnemyRectangle()
        {
            Rectangle bounds = texture.Bounds;
            bounds.Location = position.ToPoint();

            return bounds;
        } 

        #endregion
    }
}
