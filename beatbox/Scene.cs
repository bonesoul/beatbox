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
    class Scene:Component
    {
        SpriteBatch sprite_batch;
        Texture2D texture;
        public Scene()
            : base(DRAW_ORDER.BOTTOM_MOST)
        {
        }

        public override void load_content()
        {
            this.bounding_box = new Rectangle(0,0,this.parent_gamescreen.width, this.parent_gamescreen.height);
            sprite_batch = new SpriteBatch(this.parent_gamescreen.graphics_device);
            texture = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>(@"backgrounds\back");
            base.load_content();
        }

        public override void Draw(GameTime game_time)
        {
            sprite_batch.Begin();
            sprite_batch.Draw(texture, bounding_box, Color.White);
            sprite_batch.End();
            base.Draw(game_time);
        }
    }
}
