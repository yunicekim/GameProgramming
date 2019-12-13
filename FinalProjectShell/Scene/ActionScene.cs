using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
/*
* Program name : ActionScene.cs
* Purpose : to control all componet of the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public class ActionScene : GameScene
    {
        #region variables

        int lifeScore;
        GameOverOverlay gameOver;
        Song bgMusic;

        #endregion

        #region ctor
        public ActionScene(Game game) : base(game)
        {
        }
        #endregion

        #region overrides
        /// <summary>
        /// Creates and adds any components that belong to this scene
        /// </summary>
        public override void Initialize()
        {
            this.AddSceneComponent(new Player(Game, this));
            this.AddSceneComponent(new Background(Game));
            this.AddSceneComponent(new EnemyManager(Game, this));
            this.AddSceneComponent(new Score(Game, "fonts\\hudFont", HudLocation.TopRight));
            this.AddSceneComponent(new LifeScore(Game, "fonts\\hudFont", HudLocation.BottomRight));

            //background music setting
            bgMusic = Game.Content.Load<Song>("Sounds/Battle in the winter");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 3f;

            base.Initialize();
        }

        /// <summary>
        /// update the action scene's status
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    this.RemoveSceneComponent(gameOver);
                    Game.Services.GetService<StartScene>().Show();

                    this.lifeScore = Game.Services.GetService<LifeScore>().GetLifeScore();

                    //if the life is gone
                    if (this.lifeScore == 0)
                    {
                        //reset the game
                        ResetGame();
                    }
                }
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// shows this scene
        /// </summary>
        public override void Show()
        {
            MediaPlayer.Play(bgMusic);
            base.Show();
        }

        /// <summary>
        /// hides this scene
        /// </summary>
        public override void Hide()
        {
            MediaPlayer.Stop();
            base.Hide();
        }

        #endregion

        #region helper methods
        /// <summary>
        /// checks weapon has a collision with enemy
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public bool CheckWeaponForCollisionWithEnemy(Weapon weapon)
        {
            //If they collide with the weapon that was passed in
            foreach (GameComponent comp in SceneComponents)
            {
                if (comp is Enemy enemy)
                {
                    if (Game.Components.Contains(enemy))
                    {
                        if (weapon.GetWeaponRectangle().Intersects(enemy.GetEnemyRectangle()))
                        {
                            enemy.CollisionDetected();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// checks player has collision with enemy
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool CheckPlayerCollisionWithEnemy(Player player)
        {
            //If they collide with the weapon that was passed in
            foreach (GameComponent comp in SceneComponents)
            {
                if (comp is Enemy enemy)
                {
                    if (Game.Components.Contains(enemy))
                    {
                        if (player.GetPlayerRectangle().Intersects(enemy.GetEnemyRectangle()))
                        {
                            enemy.CollisionDetected();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// when the life is zero, show the game over string on the game scene
        /// </summary>
        public void ShowGameOver()
        {
            gameOver = new GameOverOverlay(Game, "fonts\\hudFont", HudLocation.CenterScreen);

            this.AddSceneComponent(gameOver);
            DisableComponents();

            //Gets the HighScrore scene from services
            //Calls method in HighScrore to add new score
            HighScoreScene hsScene = Game.Services.GetService<HighScoreScene>();
            hsScene.RecordScore(Game.Services.GetService<Score>().GetScore());

        }

        /// <summary>
        /// Freezes all component when the game over
        /// </summary>
        private void DisableComponents()
        {
            foreach (GameComponent component in this.SceneComponents)
            {
                component.Enabled = false;
            }
        }

        /// <summary>
        /// resets all variable for new game 
        /// </summary>
        private void ResetGame()
        {
            //Resets the score back to zero
            Game.Services.GetService<Score>().ResetScore();
            //Resets the life score back to three
            Game.Services.GetService<LifeScore>().ResetLifeScore();
            //Resets player's initial location
            Game.Services.GetService<Player>().ResetPosition();
        } 
        #endregion

    }
}
