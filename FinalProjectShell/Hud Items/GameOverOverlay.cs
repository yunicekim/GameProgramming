using Microsoft.Xna.Framework;
/*
* Program name : GamveOverOverlay.cs
* Purpose : to show string about the ending of the game
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    class GameOverOverlay : HudString
    {
        #region ctor
        public GameOverOverlay(Game game, string fontName, HudLocation screenLocation)
                                                : base(game, fontName, screenLocation)
        {
            displayString = "Game Over";
            gameEnded = true;
        } 
        #endregion
    }
}
