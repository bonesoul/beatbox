using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace beatbox
{
    public enum PATH_KEYS
    {
        F1,
        F2,
        F3,
        F4,
        F5
    }

    public class Path : Component
    {
        public int x_coord;
        public string color;
        public string sound_file;
        public PATH_KEYS path_key;

        public bool auto_play = false;
        

        public Texture2D tab_texture;
        Texture2D path_texture;
        SpriteBatch sprite_batch;
        SoundEffect sound_effect;

        private int act_height;

        public Path()
            : base(DRAW_ORDER.TOP_MOST)
        {            
        }
       
        public override void load_content()
        {
            this.act_height = this.parent_gamescreen.height-75; //this.parent_gamescreen.height - 200;
            this.bounding_box = new Rectangle(this.x_coord, 1, 5, this.parent_gamescreen.height);
            sprite_batch = new SpriteBatch(this.parent_gamescreen.graphics_device);
            path_texture = new Texture2D(this.parent_gamescreen.graphics_device, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            path_texture.SetData<Color>(new Color[1] { new Color(49, 79, 79, 125) });
            tab_texture = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>(@"tabs\" + color);
            sound_effect = this.parent_gamescreen.Engine.content_manager.Load<SoundEffect>(@"songs\" + sound_file);
        
           
            base.load_content();
        }

        public override void Draw(GameTime game_time)
        {
            sprite_batch.Begin();
            sprite_batch.Draw(path_texture, bounding_box, Color.Blue);
            sprite_batch.End();
            base.Draw(game_time);

        }

        public override void Update(GameTime game_time)
        {

                MainScreen ms = (MainScreen)this.parent_gamescreen;
                if (sub_components.Count > 0 && ms.song_manager.started)
                {
                    Tab tab = (Tab)sub_components[0];

                    bool correct_key = this.auto_play;
                    if ((act_height - 45 <= tab.y_coord) && (tab.y_coord <= act_height + 25))
                    {
                        switch (path_key)
                        {
                            case PATH_KEYS.F1:
                                if (this.parent_gamescreen.Engine.keyboard_state.IsKeyDown(Keys.F1)) correct_key = true;
                                break;
                            case PATH_KEYS.F2:
                                if (this.parent_gamescreen.Engine.keyboard_state.IsKeyDown(Keys.F2)) correct_key = true;
                                break;
                            case PATH_KEYS.F3:
                                if (this.parent_gamescreen.Engine.keyboard_state.IsKeyDown(Keys.F3)) correct_key = true;
                                break;
                            case PATH_KEYS.F4:
                                if (this.parent_gamescreen.Engine.keyboard_state.IsKeyDown(Keys.F4)) correct_key = true;
                                break;
                            case PATH_KEYS.F5:
                                if (this.parent_gamescreen.Engine.keyboard_state.IsKeyDown(Keys.F5)) correct_key = true;
                                break;
                        }
                        if (correct_key && !tab.played)
                        {
                            ms.skortablosu.increment();
                            sound_effect.Play();
                            tab.played = true;
                            tab.explode(game_time);
                        }
                    }
                    else if (tab.y_coord > act_height)
                    {
                        this.sub_components.Remove(tab);
                    }
                }
            base.Update(game_time);
        }
    }
}
