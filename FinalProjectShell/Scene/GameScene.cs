using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
/*
* Program name : GameScene.cs(abstract class)
* Purpose : to inherit the common attributes of the game scene
* 
* Revision History
*  Dec 5, 2019 created by Yunice Kim
*/
namespace FinalProjectShell
{
    public abstract class GameScene : GameComponent
    {
        #region variables
        const int CLEANUP_INTERVAL = 1;
        double cleanupTimer = 0.0;
        /// <summary>
        /// Used to hold a reference to the components that belong to 
        /// this GameScene instance.  Used to quickly iterate through components 
        /// that belong to the scence to enable and make visible where applicable
        /// </summary>
        List<GameComponent> sceneComponents; 
        #endregion

        #region ctor
        public GameScene(Game game) : base(game)
        {
            sceneComponents = new List<GameComponent>();

            Hide();
        }

        #endregion

        #region overrides
        /// <summary>
        /// As a final step of initialization of the GameScene
        /// we will iterate through all the items that we added to the scene 
        /// and add them to the game components collection
        /// </summary>
        public override void Initialize()
        {
            //Iterates through the list of scene components and
            //Adds them to the game component for the framework to manage
            foreach (GameComponent component in sceneComponents)
            {
                if (Game.Components.Contains(component) == false)
                {
                    Game.Components.Add(component);
                }
            }

            base.Initialize();
        }

        /// <summary>
        /// updates the game's status
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            cleanupTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (cleanupTimer >= CLEANUP_INTERVAL)
            {
                cleanupTimer = 0.0;
                for (int i = 0; i < SceneComponents.Count; i++)
                {
                    if (Game.Components.Contains(SceneComponents[i]) == false)
                    {
                        SceneComponents.Remove(SceneComponents[i]);
                    }
                }
            }

            base.Update(gameTime);
        } 
        #endregion

        #region helper methods
        /// <summary>
        /// return the list of game component
        /// </summary>
        public List<GameComponent> SceneComponents
        {
            get => sceneComponents;
        }

        /// <summary>
        /// Shows this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to true
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Show()
        {
            this.Enabled = true;
            // iterate though all components held by this scene 
            // and set Enabled to true and if it's also a DrawableGameComponent
            // set Visible to true
            foreach (GameComponent component in sceneComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        /// <summary>
        /// Hides this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to false
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            // iterate though all components held by this scene 
            // and set Enabled to false and if it's also a DrawableGameComponent
            // set Visible to false
            foreach (GameComponent component in sceneComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }



        /// <summary>
        /// Add the component to the scene component 
        /// </summary>
        /// <param name="component"></param>
        public void AddSceneComponent(GameComponent component)
        {
            sceneComponents.Add(component);
            Game.Components.Add(component);
        }

        /// <summary>
        /// deletes the component to the scene component 
        /// </summary>
        /// <param name="component"></param>
        public void RemoveSceneComponent(GameComponent component)
        {
            sceneComponents.Remove(component);
            Game.Components.Remove(component);
        } 
        #endregion
    }
}
