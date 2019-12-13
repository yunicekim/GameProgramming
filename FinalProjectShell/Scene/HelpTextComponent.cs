using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
* Program name : HelpTextComponent.cs
* Purpose : to show the Help image on the Help scene
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class HelpTextComponent : DrawableGameComponent
    {
        #region variables
        Texture2D texture;
        #endregion

        #region ctor
        public HelpTextComponent(Game game) : base(game)
        {
        }
        #endregion

        #region overrides
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/helpImage");
            base.LoadContent();
        } 
        #endregion
    }
}
