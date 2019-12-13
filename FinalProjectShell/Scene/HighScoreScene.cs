using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/*
* Program name : HelpScoreScene.cs
* Purpose : to show the Help image on the Help score scene using component of the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public class HighScoreScene : GameScene
    {
        #region variables
        HighScoreComponent highScoreComponent;
        #endregion

        #region ctor
        public HighScoreScene(Game game) : base(game)
        {
        }
        #endregion

        #region overrides
        /// <summary>
        /// initialize the high score component
        /// </summary>
        public override void Initialize()
        {
            //Creates and adds any components that belong to 
            //this scene to the Scene components list
            highScoreComponent = new HighScoreComponent(Game);
            this.AddSceneComponent(highScoreComponent);
            this.Hide();
            base.Initialize();
        }

        /// <summary>
        /// updates the screen's condition
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }
            base.Update(gameTime);
        }

        #endregion

        #region helper methods
        /// <summary>
        /// Records high score
        /// </summary>
        /// <param name="score"></param>
        public void RecordScore(int score)
        {
            highScoreComponent.AddScoreToList(score);
        } 
        #endregion
    }
}
