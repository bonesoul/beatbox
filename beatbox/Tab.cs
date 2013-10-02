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
    public class Tab:Component
    {
        public Path path;
        public float act_time;
        public bool on_stage = false;
        public bool played = false;
       

        ExplosionParticleSystem explosion;

        public int y_coord=-250;
        public SpriteBatch sprite_batch;

        // explosion 

        float timeTillExplosion = 0.0f;


        public Tab()
            : base()
        {
        }

        public override void  load_content()
        {
            act_time = (float)(act_time * 1.7);
            sprite_batch = new SpriteBatch(this.parent_gamescreen.graphics_device);
            explosion = new ExplosionParticleSystem(this.parent_gamescreen.Engine.game, 1);
            explosion.parent = this;
            add_sub_component(explosion);
 	        base.load_content();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime game_time)
        {
            int button_width = 88;
            int button_height = 66;
            bounding_box = new Rectangle(this.path.x_coord - button_width/2, this.y_coord-button_height/2, button_width, button_height);
            sprite_batch.Begin();
            sprite_batch.Draw(this.path.tab_texture, bounding_box, Color.White);
            sprite_batch.End();
            base.Draw(game_time);
        }

        public override void Update(GameTime game_time)
        {
                MainScreen ms = (MainScreen)this.parent_gamescreen;
                if (ms.song_manager.started)
                {
                    if (on_stage)
                        this.y_coord += 8;// (int)500 / (int)game_time.ElapsedGameTime.TotalMilliseconds;
                    else
                    {
                        if ((int)game_time.TotalGameTime.TotalMilliseconds - ms.song_manager.started_time >= this.act_time)
                        {
                            on_stage = true;
                            this.y_coord = 0;
                        }
                    }
                }
            base.Update(game_time);
        }

        public void explode(GameTime game_time)
        {
            float dt = (float)game_time.ElapsedGameTime.TotalSeconds;
            UpdateExplosions(dt);
        }

        private void UpdateExplosions(float dt)
        {
            timeTillExplosion -= dt;
            const float TimeBetweenExplosions = 2.0f;
            if (timeTillExplosion < 0)
            {
                Vector2 where = Vector2.Zero;
                // create the explosion at some random point on the screen.
                where.X = Engine.RandomBetween(this.bounding_box.Left-5, this.bounding_box.Right+5);
                where.Y = Engine.RandomBetween(this.bounding_box.Top - 5, this.bounding_box.Bottom + 5);

                // the overall explosion effect is actually comprised of two particle
                // systems: the fiery bit, and the smoke behind it. add particles to
                // both of those systems.
                explosion.AddParticles(where);
                //smoke.AddParticles(where);

                // reset the timer.
                timeTillExplosion = TimeBetweenExplosions;
            }
        }
    }
}
