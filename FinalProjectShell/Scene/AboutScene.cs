using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
/*
* Program name : AboutScene.cs
* Purpose : to show the screen about a developer of this game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public class AboutScene : GameScene
    {
        #region ctor
        public AboutScene(Game game) : base(game)
        {
        } 
        #endregion

        #region overrides
        /// <summary>
        /// initialze all needed in this scene
        /// </summary>
        public override void Initialize()
        {
            //Creates and adds any components that belong to 
            //this scene to the Scene components list
            this.AddSceneComponent(new AboutTextComponent(Game));
            this.Hide();
            base.Initialize();
        }

        /// <summary>
        /// updated this scene's status
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
    }
}
