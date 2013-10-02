using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;

namespace beatbox
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics_manager;
        Engine Engine;

        public Game1()
        {
            Debug.WriteLine(        System.IO.Directory.GetCurrentDirectory());
            Window.Title = "Beatbox!";
            graphics_manager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Engine = new Engine(this.graphics_manager, this.Content, this);
            //Engine.set_resolution(1024, 768);
            Engine.set_resolution(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            //Engine.toggle_fullscreen();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Engine.screen_manager = new ScreenManager(this.Engine);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime); // let engine update 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw(gameTime); // let engine draw the scene
            base.Draw(gameTime);
        }
    }
}
