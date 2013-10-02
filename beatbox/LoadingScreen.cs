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
    class LoadingScreen:GameScreen
    {
        SpriteBatch sprite_batch;
        Texture2D texture;

        public LoadingScreen()
            : base()
        {
        }

        public override void load_components()
        {
            bounding_box = new Rectangle(0, 0, this.width, this.height);
            sprite_batch = new SpriteBatch(this.Engine.graphics_device);
            texture = this.Engine.content_manager.Load<Texture2D>(@"backgrounds\loading");
            base.load_components();
        }

        public override void Update(GameTime game_time)
        {
            if (game_time.TotalGameTime.Seconds >=5) // let loading_screen only stay for 3 seconds
            {
                Engine.screen_manager.change_screen(ScreenManager.SCREEN_TYPE.mainmenu);
                this.Close();
            }
            base.Update(game_time);
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