using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
/*
* Program name : Score.cs
* Purpose : to count and show the score on the play screen
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class Score : HudString
    {
        #region variables
        int score; 
        #endregion

        #region ctor
        public Score(Game game, string fontName, HudLocation screenLocation) : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<Score>() == null)
            {
                Game.Services.AddService<Score>(this);
            }
        } 
        #endregion

        #region overrides
        /// <summary>
        /// shows the updated score on the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            displayString = $"Score: {score}";

            base.Update(gameTime);
        } 
        #endregion

        #region helper methods
        /// <summary>
        /// add score
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(int value)
        {
            score += value;
        }

        /// <summary>
        /// get the score's value
        /// </summary>
        /// <returns></returns>
        internal int GetScore()
        {
            return this.score;
        }

        /// <summary>
        /// reset the score's value
        /// </summary>
        internal void ResetScore()
        {
            score = 0;
        } 
        #endregion
    }
}
