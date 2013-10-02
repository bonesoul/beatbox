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
    public class CreditsScreen : GameScreen
    {
        public Texture2D background;
        public SpriteFont myFont;
        public SpriteBatch sprite_batch;
        public Vector2 coords;

         

        
        
        
        public CreditsScreen() : base(){}

        public override void load()
        {

            //yarasa y1 = new yarasa();
            //add_component(y1);
            myFont = this.Engine.content_manager.Load<SpriteFont>(@"fonts\amsterdam");
            coords = new Vector2(this.Engine.screen_width / 2 - 400, this.Engine.screen_height);
            sprite_batch = new SpriteBatch(this.Engine.graphics_device);

            background = this.Engine.content_manager.Load<Texture2D>(@"backgrounds\credits");

            

            
       
            //Engine.push_screen(this);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime game_time)
        {
            
            sprite_batch.Begin();
            sprite_batch.Draw(background,new Rectangle(0,0,this.Engine.screen_width,this.Engine.screen_height),Color.White);
            sprite_batch.DrawString(myFont, "Credits : \n Coding: Eren Yagdiran\n Coding: Huseyin Uslu\n Sound: Nizamettin Tugral\n Arts: Cansu Ozgun Kayacik\n Arts: Sercan Uysal\n", coords, Color.Black);
            
            sprite_batch.End();
            base.Draw(game_time);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime game_time)
        {
            coords -= new Vector2(0,1);

            if (this.Engine.keyboard_state.IsKeyDown(Keys.Escape) || this.Engine.keyboard_state.IsKeyDown(Keys.Space))
            {
                this.Engine.push_screen(new MainScreen());
            }
            base.Update(game_time);
        }

    }
}
