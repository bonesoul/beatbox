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
    public class BottomLine : Component
    {
        SpriteBatch sprite_batch;
        Texture2D mytexture;
        //{ "blue", "red", "green", "purple", "yellow" };
        Texture2D[] colors = new Texture2D[5];

        int numberofPaths;
        int propUPside;

        int how_many_bottom;
        
        public BottomLine(int upside ) : base(DRAW_ORDER.TOP_MOST) {

            this.propUPside = upside;
            //this.how_many_bottom = number;
            

        }

        public override void load_content()

        {
            colors[0] = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("tabs/blue");
            colors[1] = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("tabs/red");
            colors[2] = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("tabs/green");
            colors[3] = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("tabs/purple");
            colors[4] = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("tabs/yellow");
            sprite_batch = new SpriteBatch(this.parent_gamescreen.Engine.graphics_device);
            mytexture =new Texture2D(this.parent_gamescreen.graphics_device, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            mytexture.SetData<Color>(new Color[1] { new Color(49, 79, 79, 250) });
            numberofPaths = ((MainScreen)this.parent_gamescreen).song_manager.total_paths;
            base.load_content();
        }
        public override void Update(Microsoft.Xna.Framework.GameTime game_time)
        {
            base.Update(game_time);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime game_time)
        {
            SongManager song_manager = ((MainScreen)this.parent_gamescreen).song_manager;
            sprite_batch.Begin();
                sprite_batch.Draw(this.mytexture, new Rectangle(song_manager.bounding_box.Left-25,this.propUPside-20,100*(numberofPaths)+50,75), Color.White);
                for (int i = 0; i < this.numberofPaths; i++)
                {
                    sprite_batch.Draw(colors[i], new Rectangle(song_manager.bounding_box.Left+10+100 * i, this.parent_gamescreen.Engine.screen_height - 85, 80, 65), Color.White);
                }
            

            sprite_batch.End();
            base.Draw(game_time);
        }

        
    }

}
