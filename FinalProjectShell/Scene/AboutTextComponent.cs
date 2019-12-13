using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/*
* Program name : AboutTextComponent.cs
* Purpose : to show the About image on the about scene
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class AboutTextComponent : DrawableGameComponent
    {
        #region variables
        Texture2D texture;

        #endregion

        #region ctor
        public AboutTextComponent(Game game) : base(game)
        {
        }
        #endregion

        #region overrides
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Draws the about text on the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialze all needed to about scene
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/aboutImage");
            base.LoadContent();
        } 
        #endregion
    }
}
