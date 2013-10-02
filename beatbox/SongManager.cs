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
using System.Diagnostics;

namespace beatbox
{
    public class SongManager : Component
    {
        private string[] color_array = new string[5] { "blue", "red", "green", "purple", "yellow" };

        Texture2D texture;
        Texture2D tutorial_texture;
        SpriteBatch sprite_batch;
        SpriteFont font;
        SpriteFont small_font;

        public double started_time;

        public SongManager()
            : base(DRAW_ORDER.MIDDLE)
        {
            this.song_index = 2;
        }

        public SongManager(int _sound_index):base(DRAW_ORDER.MIDDLE)
        {
            song_index = _sound_index;
        }

        public double time_to_start = 4000;
        public double time_to_finish = 0;
        public bool started = false;
        public int total_paths = 0;
        public bool should_finish = false;

        public int song_index;

        public void switch_auto_play()
        {
            foreach (Path path in sub_components)
            {
                if (path.auto_play) path.auto_play = false;
                else path.auto_play = true;
            }
        }


        public override void load_content()
        {
            this.parent_gamescreen.Engine.notify_autoplay += new Engine.autoplay_handler(switch_auto_play);

            int incremental_key = 0;
            int color_index = 0;

            sprite_batch = new SpriteBatch(this.parent_gamescreen.graphics_device);

            tutorial_texture = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("klavye");
            texture = new Texture2D(this.parent_gamescreen.graphics_device, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[1] { new Color(0, 0, 0, 100) });

            Level level = new Level();
            level.LevelName = "level"+song_index;
            level.Load();

            this.total_paths = level.PathCount;

            int manager_width = level.PathCount * 100;
            this.bounding_box = new Rectangle((this.parent_gamescreen.width - manager_width) / 2, 0, manager_width, this.parent_gamescreen.height);
            int incremental_cords = this.bounding_box.Left + 50;

            font = this.parent_gamescreen.Engine.content_manager.Load<SpriteFont>("fonts/mostwasted");
            small_font = this.parent_gamescreen.Engine.content_manager.Load<SpriteFont>("fonts/amsterdam");

            Tab tab = null;
            foreach (LevelPath item in level.GamePaths)
            {
                Path path = new Path();
                path.color = color_array[color_index];
                color_index++;
                path.x_coord = incremental_cords;
                incremental_cords += 100;
                path.sound_file = item.FileName;
                path.path_key = (PATH_KEYS)incremental_key;
                add_sub_component(path);

                foreach (int timer in item.StartTime)
                {
                    tab = new Tab();
                    tab.path = path;
                    tab.act_time = timer;
                    path.add_sub_component(tab);
                }                              
                incremental_key += 1;
            }
        }
        public override void Draw(GameTime game_time)
        {
            sprite_batch.Begin();
            sprite_batch.Draw(texture, bounding_box, Color.Black);
            if(this.time_to_start<100 && time_to_finish==0)
                sprite_batch.DrawString(font, "GO!", new Vector2(this.parent_gamescreen.width - 275, 15), Color.Black);
            if (0 < this.time_to_start && this.time_to_start < 1000)
            {
                sprite_batch.DrawString(font, "1", new Vector2(this.parent_gamescreen.width - 275, 15), Color.Black);
                sprite_batch.Draw(tutorial_texture, new Rectangle(this.parent_gamescreen.width - 400, 100, 300, 250), Color.White);
            }
            if (1000 < this.time_to_start && this.time_to_start < 2000)
            {
                sprite_batch.DrawString(font, "2", new Vector2(this.parent_gamescreen.width - 275, 15), Color.Black);
                sprite_batch.Draw(tutorial_texture, new Rectangle(this.parent_gamescreen.width - 400, 100, 300, 250), Color.White);
            }
            if (2000 < this.time_to_start && this.time_to_start < 3000)
            {
                sprite_batch.DrawString(font, "3", new Vector2(this.parent_gamescreen.width - 275, 15), Color.Black);
                sprite_batch.Draw(tutorial_texture, new Rectangle(this.parent_gamescreen.width - 400, 100, 300, 250), Color.White);
            }
            if(this.time_to_finish>0 )
                sprite_batch.DrawString(small_font, "PREPARE FOR NEXT STAGE!", new Vector2(150, 35), Color.Black);

            sprite_batch.End();
            base.Draw(game_time);
        }

        public override void Update(GameTime game_time)
        {
            if (!this.started)
                this.time_to_start -= game_time.ElapsedGameTime.Milliseconds;
            if (this.time_to_start <= 0 && !this.started)
            {
                this.started = true;
                this.started_time = game_time.TotalGameTime.TotalMilliseconds;
            }

            if (this.time_to_finish > 0)
                this.time_to_finish -= game_time.ElapsedGameTime.Milliseconds;

            if (should_finish)
            {
                if (this.time_to_finish < 0)
                {
                    this.parent_gamescreen.Engine.screen_manager.change_stage();
                    this.parent_gamescreen.Close();
                }
            }
            else
            {
                if (this.song_index == 2)
                {
                    /* check if song finished */
                    bool finished = true;
                    foreach (Component component in sub_components)
                    {
                        Path path = (Path)component;
                        if (path.sub_components.Count > 0)
                            finished = false;
                    }
                    if (finished)
                    {
                        should_finish = true;
                        time_to_finish = 3000;
                    }
                }
            }
            base.Update(game_time);
        }
    }
}
