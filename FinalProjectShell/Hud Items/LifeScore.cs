using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
/*
* Program name : LifeScore.cs
* Purpose : to Show the Life on the screen
*           By default, player has 3 life score and it goes down when the play is hit by enemy
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class LifeScore : HudString
    {
        #region variables
        int lifeScore = 3;

        #endregion

        #region ctor
        public LifeScore(Game game, string fontName, HudLocation screenLocation)
    : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<LifeScore>() == null)
            {
                Game.Services.AddService<LifeScore>(this);
            }
        }
        #endregion

        #region overrides
        /// <summary>
        /// show the updated life score on the scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            displayString = $"Life: {lifeScore}";
            base.Update(gameTime);
        }
        #endregion

        #region helper methods
        /// <summary>
        /// remove life
        /// </summary>
        public void RemoveLife()
        {
            lifeScore--;
        }

        /// <summary>
        /// get the life's value
        /// </summary>
        /// <returns></returns>
        public int GetLifeScore()
        {
            return lifeScore;
        }

        /// <summary>
        /// reset the life to 3
        /// </summary>
        internal void ResetLifeScore()
        {
            this.lifeScore = 3;
        } 
        #endregion
    }
}
