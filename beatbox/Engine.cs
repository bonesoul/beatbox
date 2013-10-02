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
    public class Engine
    {
        // devices & main structures
        public GraphicsDevice graphics_device;
        public GraphicsDeviceManager graphics_manager;
        public ContentManager content_manager;
        public Game game;

        // screen properties
        public int screen_width;
        public int screen_height;

        // events
        public delegate void input_handler(MouseState mouse_state); // mouse & keyboard handler
        public event input_handler notify_input;

        public delegate void autoplay_handler();
        public event autoplay_handler notify_autoplay;


        // engine objects
        List<GameScreen> game_screens = new List<GameScreen>();
        public ScreenManager screen_manager;

        // engine data
        public KeyboardState keyboard_state;
        public MouseState mouse_state;

        public Engine(GraphicsDeviceManager _graphics_manager, ContentManager _content_manager, Game _game) // engine init
        {
            this.graphics_manager = _graphics_manager;
            this.graphics_device = this.graphics_manager.GraphicsDevice;
            this.content_manager = _content_manager;
            this.game = _game;

            this.keyboard_state = Keyboard.GetState();
            this.mouse_state = Mouse.GetState();
        }

        ~Engine()
        { }

        public void pause_the_game(GameTime game_time)
        {
            try
            {
                foreach (GameScreen screen in game_screens)
                {
                    if (screen.GetType().ToString() == "beatbox.MainScreen")
                    {
                        screen_manager.change_screen(ScreenManager.SCREEN_TYPE.mainmenu);
                        this.remove_screen(screen);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public void Update(GameTime game_time)
        {
            this.keyboard_state = Keyboard.GetState();
            this.mouse_state = Mouse.GetState();

            // handle mouse 
            if (notify_input != null) notify_input(Mouse.GetState());

            // check ESC
            if(keyboard_state.IsKeyDown(Keys.Escape))
            {
                switch (screen_manager.active_screen)
                {
                    case ScreenManager.SCREEN_TYPE.loading_screen:
                        break;
                    case ScreenManager.SCREEN_TYPE.mainmenu:
                        break;
                    case ScreenManager.SCREEN_TYPE.gameplay:
                        pause_the_game(game_time);
                        break;
                    case ScreenManager.SCREEN_TYPE.credits:
                        game.Exit();
                        break;
                    default:
                        break;
                }
            }

            if (keyboard_state.IsKeyDown(Keys.F12))
            {
                if (notify_autoplay != null) notify_autoplay();
            }


            // run Update on components
            try
            {
                foreach (GameScreen gamescreen in game_screens)
                {
                    gamescreen.Update(game_time);
                }
            }
            catch (InvalidOperationException exc) // may fire when gamescreens closed
            {
            }
        }

        public void Draw(GameTime game_time)
        {
            graphics_device.Clear(Color.CornflowerBlue);
            try
            {
                foreach (GameScreen gamescreen in game_screens)
                {
                    gamescreen.Draw(game_time);
                }
            }
            catch (InvalidOperationException exc) // may fire when gamescreens closed
            {
            }
        }


        public static float RandomBetween(float min, float max)
        {
            return min + (float)Engine.random.NextDouble() * (max - min);
        }

        public static Random random = new Random();

        public void set_resolution(int width, int height)
        {
            this.screen_width = width;
            this.screen_height = height;
            graphics_manager.PreferredBackBufferWidth = this.screen_width;
            graphics_manager.PreferredBackBufferHeight = this.screen_height;
            graphics_manager.ApplyChanges();
        }

        public void toggle_fullscreen()
        {
            graphics_manager.ToggleFullScreen();
        }

        public void push_screen(GameScreen screen)
        {
            this.push_screen(screen, DRAW_ORDER.TOP_MOST);
        }

        public void push_screen(GameScreen screen,DRAW_ORDER draw_order)
        {
            if (!game_screens.Contains(screen)) // a game screen can only have one instance
            {
                screen.Engine = this;
                screen.load();
                game_screens.Add(screen);
            }
        }

        public void remove_screen(GameScreen _screen)
        {
            foreach (GameScreen screen in game_screens)
            {
                if (screen == _screen)
                {
                    game_screens.Remove(screen);                    
                    break;
                }
            }
        }
    }
}
