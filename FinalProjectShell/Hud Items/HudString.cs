using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
* Program name : HudString.cs
* Purpose : to Show strings on the play screen
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class HudString : DrawableGameComponent
    {
        #region variables
        protected string displayString;
        protected HudLocation screenLocation;
        protected bool gameEnded;

        SpriteFont font;
        string fontName;
        #endregion

        #region ctor
        public HudString(Game game, string fontName, HudLocation screenLocation) : base(game)
        {
            this.fontName = fontName;
            this.screenLocation = screenLocation;
            this.DrawOrder = int.MaxValue;
            displayString = "HUD string not set";
        }
        #endregion

        #region overrides
        /// <summary>
        /// Draws a string to the proper position on the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            if (!gameEnded)
            {
                sb.DrawString(font, displayString, GetPosition(), Color.Blue);
            }
            else
            {
                sb.DrawString(font, displayString, GetPosition(), Color.Red);
            }
            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>(fontName);
            base.LoadContent();
        }

        #endregion

        #region helper methods
        /// <summary>
        /// get the position to draw on the scene
        /// </summary>
        /// <returns></returns>
        private Vector2 GetPosition()
        {
            Vector2 location = Vector2.Zero;

            float stringWidth = font.MeasureString(displayString).X;
            float stringHeight = font.MeasureString(displayString).Y;
            int displayWidth = Game.GraphicsDevice.Viewport.Width;
            int displayHeight = Game.GraphicsDevice.Viewport.Height;

            switch (screenLocation)
            {
                case HudLocation.TopLeft:
                    // display at 0,0
                    break;
                case HudLocation.TopCenter:
                    location.X = displayWidth / 2 - stringWidth / 2;
                    location.Y = 0;
                    break;
                case HudLocation.TopRight:
                    location.X = displayWidth - stringWidth;
                    location.Y = 0;
                    break;
                case HudLocation.CenterScreen:
                    location.X = displayWidth / 2 - stringWidth / 2;
                    location.Y = displayHeight / 2 - stringHeight / 2;
                    break;
                case HudLocation.BottomLeft:
                    location.Y = displayHeight - stringHeight;
                    break;
                case HudLocation.BottomCenter:
                    location.X = displayWidth / 2 - stringWidth / 2;
                    location.Y = displayHeight - stringHeight;
                    break;
                case HudLocation.BottomRight:
                    location.X = displayWidth - stringWidth;
                    location.Y = displayHeight - stringHeight;
                    break;
                default:
                    break;
            }

            return location;
        } 
        #endregion
    }
}
