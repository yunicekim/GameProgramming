using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
/*
* Program name : HighScoreComponent.cs
* Purpose : to show the hight score on the High score scene
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class HighScoreComponent : DrawableGameComponent
    {
        #region variables
        Texture2D background;
        List<int> scores;
        SpriteFont regularFont;

        //list count's limit is ten
        private const int MAX_COUNT = 10;
        //the file name that hold the top 10 score
        private string fileName = "highScore.txt";
        private string noFile;
        private string topTenScore;

        #endregion

        #region ctor
        public HighScoreComponent(Game game) : base(game)
        {
        } 
        #endregion

        #region overrides
        /// <summary>
        /// initialize the file to hold top ten scores
        /// </summary>
        public override void Initialize()
        {
            scores = new List<int>();

            topTenScore = "Top 10 Scores";

            if (File.Exists(fileName))
            {
                //Reads the file, and converts all read strings
                //to ints, and load them in your scores list
                string[] scoreStrings = File.ReadAllLines(fileName);
                //Converts the scoreStrings to a list of integers

                for (int i = 0; i < scoreStrings.Length; i++)
                {
                    scores.Add(int.Parse(scoreStrings[i]));
                }
            }
            else
            {
                noFile = "File of High score does not exist";
            }

            base.Initialize();
        }

        /// <summary>
        /// Shows the top 10 scores after reading the hight score file 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = Game.Services.GetService<SpriteBatch>();

            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            if (scores.Count != 0)
            {
                spriteBatch.DrawString(regularFont,
                                        topTenScore,
                                        new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 100,
                                                    Game.GraphicsDevice.Viewport.Height / 4 - 50),
                                        Color.Red);
                for (int i = 0; i < scores.Count; i++)
                {
                    spriteBatch.DrawString(regularFont,
                                        scores[i].ToString(),
                                        new Vector2((Game.GraphicsDevice.Viewport.Width / 2 - 30),
                                                    (Game.GraphicsDevice.Viewport.Height / 4) + (30 * i)),
                                        Color.Yellow);
                }
            }
            else
            {
                spriteBatch.DrawString(regularFont,
                                        noFile,
                                        new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - 230,
                                                    Game.GraphicsDevice.Viewport.Height / 2 - 50),
                                        Color.Yellow);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        } 
        #endregion

        #region helper methods
        /// <summary>
        /// loads all needed in the hight score scene
        /// </summary>
        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("Images/solar-twin-hd186302");
            regularFont = Game.Content.Load<SpriteFont>("fonts\\hudFont");

            base.LoadContent();
        }

        /// <summary>
        /// Adds score to the list
        /// </summary>
        /// <param name="score"></param>
        public void AddScoreToList(int score)
        {
            scores.Add(score);
            //sort the list
            scores.Sort();
            //arrange the order backward to show the highest score the first of the list
            scores.Reverse();

            //if the list is over 10
            if (scores.Count > MAX_COUNT)
            {
                //delete the smallest score
                scores.RemoveAt(MAX_COUNT);
            }
            //Writes the list of scores to file
            File.WriteAllLines(fileName, GetScoresAsStringArray());
        }

        /// <summary>
        /// Get the list from the file
        /// </summary>
        /// <returns></returns>
        private string[] GetScoresAsStringArray()
        {
            int itemCount = scores.Count;
            string[] scoreStrings = new string[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                scoreStrings[i] = scores[i].ToString();
            }
            return scoreStrings;
        } 
        #endregion
    }
}
