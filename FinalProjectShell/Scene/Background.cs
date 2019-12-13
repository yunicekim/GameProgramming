using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
/*
* Program name : Background.cs
* Purpose : to show the background on the play scene
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    enum BackgroundAncher
    {
        Left,
        Right
    }

    class Background : DrawableGameComponent
    {
        #region variables
        private static int backgroundCount = 0;

        Texture2D texture;
        Vector2 velocity;
        Vector2 position = Vector2.Zero;
        BackgroundAncher ancher;

        List<Rectangle> textureRects;

        #endregion

        #region ctors
        public Background(Game game) : base(game)
        {
            DrawOrder = backgroundCount;
            backgroundCount++;
        }
        #endregion

        #region overrides
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            //Iterates through all needed rectangles and draw them
            //to the screen
            foreach (Rectangle rect in textureRects)
            {
                sb.Draw(texture, rect, Color.White);
            }

            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// initialize the basic variable
        /// </summary>
        public override void Initialize()
        {
            this.velocity = new Vector2(0, 1);
            this.ancher = BackgroundAncher.Left;

            base.Initialize();
        }

        /// <summary>
        /// update the background's status
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            //Let's respond to left right arrows to move
            //Iterates through all background rectangles and subtract velocity
            //to reposition them - we are moving the texture to the left, so 
            //we subract
            for (int i = 0; i < textureRects.Count; i++)
            {
                Rectangle rect = textureRects[i];
                rect.Location -= velocity.ToPoint();
                textureRects[i] = rect;
            }

            //makes sure that the right edge of the first one
            //is not past the edge of the screen
            Rectangle firstRect = textureRects[0];
            if (firstRect.Bottom < 0)
            {
                // 1. remove the first one, 
                // 2. recalculate what the last one should be
                // 3. add it to the back of the list
                textureRects.RemoveAt(0);
                Rectangle lastRect = textureRects[textureRects.Count - 1];
                firstRect.Y = lastRect.Bottom;

                //adds it to the back of the list 
                textureRects.Add(firstRect);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// loads all needed to background
        /// </summary>
        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>("Images/solar-twin-hd186302");
            textureRects = CalculateBackgroundRectangleList();

            base.LoadContent();
        }
        #endregion

        #region helper methods
        /// <summary>
        /// Creates a list of rectangels for the backgrounds.
        /// Each rectangle is where the background tile must
        /// be drawn to cover the entire viewport
        /// </summary>
        /// <returns></returns>
        private List<Rectangle> CalculateBackgroundRectangleList()
        {
            List<Rectangle> neededRectangles = new List<Rectangle>();

            int rectangleCount = Game.GraphicsDevice.Viewport.Height / texture.Height + 2;
            int xPosition = CalculateXPosition();

            for (int i = 0; i < rectangleCount; i++)
            {
                neededRectangles.Add(new Rectangle(xPosition, texture.Height * i, texture.Width, texture.Height));
            }

            return neededRectangles;
        }

        /// <summary>
        /// calculate the x position
        /// </summary>
        /// <returns></returns>
        private int CalculateXPosition()
        {
            switch (ancher)
            {
                default:
                case BackgroundAncher.Left:
                    return 0;
                case BackgroundAncher.Right:
                    return Game.GraphicsDevice.Viewport.Width - texture.Width;
            }
        }
        #endregion

    }
}
