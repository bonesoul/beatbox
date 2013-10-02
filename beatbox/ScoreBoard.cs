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
    public class ScoreBoard:Component
    {

        public int score = 0;
        SpriteBatch sprite_batch;
        SpriteFont scoreFont;
        Texture2D layer;

        int width;
        int height;
        int left;
        int top;

        public ScoreBoard() : base(DRAW_ORDER.TOP_MOST) 
        {           
        }
                
        public override void load_content()
        {
            this.left = this.parent_gamescreen.width - 400;
            this.top = this.parent_gamescreen.height - 400;
            this.width = 400;
            this.height = 400;

            this.bounding_box = new Rectangle(left, top, width, height);
            layer = this.parent_gamescreen.Engine.content_manager.Load<Texture2D>("backgrounds/score");
            sprite_batch = new SpriteBatch(this.parent_gamescreen.graphics_device);
            scoreFont = this.parent_gamescreen.Engine.content_manager.Load<SpriteFont>("fonts/mostwasted");
            base.load_content();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime game_time)
        {
            sprite_batch.Begin();
            sprite_batch.Draw(layer, bounding_box, Color.White);
            sprite_batch.DrawString(scoreFont, this.getPoints().ToString(), new Vector2(this.left+(this.width/2)-50, this.top+125), Color.White);
            sprite_batch.End();
            base.Draw(game_time);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime game_time)
        {
            base.Update(game_time);
        }

        

        public int getPoints()
        {
            return score;
        }
        public int resetCounter()
        {
            return 0;
        }
        public void increment()
        {
            score += 10;
        }
        
        
        
    }
}
