using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
/*
* Program name : HelpScene.cs
* Purpose : to show the help image on the Help scene using HelpTextComponent
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public class HelpScene : GameScene
    {
        #region ctor
        public HelpScene(Game game) : base(game)
        {
        }
        #endregion

        #region overrides
        public override void Initialize()
        {
            //Creates and adds any components that belong to 
            //this scene to the Scene components list
            this.AddSceneComponent(new HelpTextComponent(Game));
            this.Hide();
            base.Initialize();
        }

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
