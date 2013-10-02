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
    public class MenuItem:Component
    {
        public enum MENU_TYPE
        {
            GAMEPLAY,
            EXIT
        }

        SpriteBatch sprite_batch;
        Texture2D texture;
        public MENU_TYPE type;
        SpriteFont font;
        

        public MenuItem(MENU_TYPE _type):base()
        {
            type = _type;
        }

        public override void load_content()
        {
            font = this.parent_gamescreen.Engine.content_manager.Load<SpriteFont>(@"fonts\amsterdam");
            sprite_batch = new SpriteBatch(this.parent_gamescreen.Engine.graphics_device);
            texture = new Texture2D(this.parent_gamescreen.graphics_device, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[1] { new Color(0, 0, 0, 225) });
            base.load_content();
        }

        public override void Draw(GameTime game_time)
        {
            sprite_batch.Begin();
            sprite_batch.Draw(texture, bounding_box, Color.Black);
            string caption = "";
            switch (this.type)
            {
                case MENU_TYPE.GAMEPLAY:
                    caption = " > Game";
                    break;
                case MENU_TYPE.EXIT:
                    caption = " > Quit";
                    break;
                default:
                    break;
            }
            Color item_color = Color.White;
            if (mouse_over) item_color = Color.Yellow;
            sprite_batch.DrawString(font, caption, new Vector2(this.bounding_box.Location.X,this.bounding_box.Location.Y),item_color);
            sprite_batch.End();
            base.Draw(game_time);
        }
    }
}
