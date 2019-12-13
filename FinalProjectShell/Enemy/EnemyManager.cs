using Microsoft.Xna.Framework;
using System;
/*
* Program name : EnemyManager.cs
* Purpose : to control all kinds of different enemies of the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class EnemyManager : GameComponent
    {
        #region variables

        const int STARTING_Y_POSITION = -250;
        const double CREATION_INTERVAL = 3;
        double creationTimer = 0;

        Random random = new Random();
        GameScene parent;

        #endregion

        #region ctor
        /// <summary>
        /// Links this object to the Game scene
        /// </summary>
        /// <param name="game"></param>
        /// <param name="parent"></param>
        public EnemyManager(Game game, GameScene parent) : base(game)
        {
            this.parent = parent;
            if (Game.Services.GetService<EnemyManager>() == null)
            {
                Game.Services.AddService<EnemyManager>(this);
            }
        }

        #endregion

        #region overrides
        /// <summary>
        /// adds 3 different type's enemies using manager
        /// </summary>
        public override void Initialize()
        {
            parent.AddSceneComponent(new Enemy(Game, CreateRandomLocation(), EnemyType.LaundryBasket));
            parent.AddSceneComponent(new Enemy(Game, CreateRandomLocation(), EnemyType.LunchBox));
            parent.AddSceneComponent(new Enemy(Game, CreateRandomLocation(), EnemyType.Vacum));

            base.Initialize();
        }

        /// <summary>
        /// in some intervals, add new enemy on the scene at the random location using random type
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            creationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (creationTimer >= CREATION_INTERVAL)
            {
                creationTimer = 0;
                parent.AddSceneComponent(new Enemy(Game, CreateRandomLocation(), CreateRandomType()));
            }
            base.Update(gameTime);
        }

        #endregion

        #region helper methods
        /// <summary>
        /// create random position to draw the enemy
        /// </summary>
        /// <returns></returns>
        private Vector2 CreateRandomLocation()
        {
            float xPos = random.Next(0, Game.GraphicsDevice.Viewport.Width);

            return new Vector2(xPos, STARTING_Y_POSITION);
        }

        /// <summary>
        /// choose random enemy type to draw
        /// </summary>
        /// <returns></returns>
        private EnemyType CreateRandomType()
        {
            int maxCount = Enum.GetNames(typeof(EnemyType)).Length;

            return (EnemyType)random.Next(0, maxCount);
        } 
        #endregion
    }
}
