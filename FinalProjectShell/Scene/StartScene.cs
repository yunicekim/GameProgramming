using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
/*
* Program name : StartScene.cs
* Purpose : to show menu on the screen using menu component
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public enum MenuSelection
    {
        StartGame,
        Help,
        HighScore,
        About,
        Quit
    }

    public class StartScene : GameScene
    {
        #region variables
        List<string> menuItems = new List<string>(new string[]
                       {"Start Game",
                                "Help",
                                "High Score",
                                "About",
                                "Quit"});

        Song bgMusic; 
        #endregion

        #region ctor
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="game"></param>
        public StartScene(Game game) : base(game)
        {
        } 
        #endregion

        #region overrides

        /// <summary>
        /// Initialize all needed in the main menu
        /// </summary>
        public override void Initialize()
        {
            //Creates and adds any components that belong to this scene
            this.AddSceneComponent(new MenuComponent(Game, menuItems));

            bgMusic = Game.Content.Load<Song>("Sounds/snd_music_victorytheme");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.3f;

            this.Show();

            base.Initialize();
        }

        /// <summary>
        /// Shows start scene
        /// </summary>
        public override void Show()
        {
            MediaPlayer.Play(bgMusic);
            base.Show();
        }

        /// <summary>
        /// Hides start scene
        /// </summary>
        public override void Hide()
        {
            MediaPlayer.Stop();
            base.Hide();
        } 
        #endregion
    }
}
