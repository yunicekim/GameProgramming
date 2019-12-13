using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/*
* Program name : MenuComponent.cs
* Purpose : to show the list of main
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public class MenuComponent : DrawableGameComponent
    {
        #region variables
        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int SelectedIndex { get; set; }
        public Vector2 position;

        private Color regularColor = Color.Yellow;
        private Color hilightColor = Color.Red;
        private KeyboardState oldState;

        ActionScene actionScene; 
        #endregion

        #region ctor
        /// <summary>
        /// Set action scene and menuItems using the contructor
        /// </summary>
        /// <param name="game"></param>
        /// <param name="menuNames"></param>
        public MenuComponent(Game game, List<string> menuNames) : base(game)
        {
            menuItems = menuNames;
            actionScene = new ActionScene(game);
        } 
        #endregion

        #region overrides
        /// <summary>
        /// Updates the menu condition
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                KeyboardState ks = Keyboard.GetState();

                if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    SelectedIndex++;
                    if (SelectedIndex == menuItems.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
                if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = menuItems.Count - 1;
                    }
                }

                oldState = ks;

                //when pressed enter key, show the selected scene
                if (ks.IsKeyDown(Keys.Enter))
                {
                    SwitchScenesBasedOnSelection();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the changed images according the font
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 tempPos = position;

            sb.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                //If the selection is the item we are drawing
                //made it a the special font and colour
                if (SelectedIndex == i)
                {
                    activeFont = highlightFont;
                    activeColor = hilightColor;
                }

                sb.DrawString(activeFont, menuItems[i], tempPos, activeColor);

                //Updates the position of next string
                tempPos.Y += regularFont.LineSpacing;
            }

            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialize the menu's position
        /// </summary>
        public override void Initialize()
        {
            //starting position of the menu items
            position = new Vector2(GraphicsDevice.Viewport.Width / 2,
                                      GraphicsDevice.Viewport.Height / 2);
            base.Initialize();
        }

        /// <summary>
        /// Loads all things related to menu
        /// </summary>
        protected override void LoadContent()
        {
            //Loads the fonts we will be using for this menu
            regularFont = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            highlightFont = Game.Content.Load<SpriteFont>("Fonts/hilightFont");
            base.LoadContent();
        } 
        #endregion

        #region helper methods
        /// <summary>
        /// when seleted one thing, show scene related to the choice
        /// </summary>
        public virtual void SwitchScenesBasedOnSelection()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)SelectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<ActionScene>().Show();
                    break;
                case MenuSelection.Help:
                    Game.Services.GetService<HelpScene>().Show();
                    break;
                case MenuSelection.HighScore:
                    Game.Services.GetService<HighScoreScene>().Show();
                    break;
                case MenuSelection.About:
                    Game.Services.GetService<AboutScene>().Show();
                    break;
                case MenuSelection.Quit:
                    Game.Exit();
                    break;
                default:
                    //For now there is nothing handling the other options
                    //Simply shows this screen again
                    Game.Services.GetService<StartScene>().Show();
                    break;
            }
        }

        #endregion
    }
}
