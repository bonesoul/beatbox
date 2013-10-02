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
    public class Menu:Component
    {
        SpriteBatch sprite_batch;
        Texture2D texture;
        
        public Menu()
            : base()
        {

        }

        public override void load_content()
        {
            this.parent_gamescreen.Engine.notify_input += new Engine.input_handler(input_handler);

            int menu_width = 350;
            int menu_height = 300;
            int menu_left = (this.parent_gamescreen.width - menu_width) / 2;
            int menu_top = (this.parent_gamescreen.height - menu_height) / 2;
            bounding_box = new Rectangle(menu_left, menu_top, menu_width, menu_height);
            sprite_batch = new SpriteBatch(this.parent_gamescreen.Engine.graphics_device);
            texture = new Texture2D(this.parent_gamescreen.graphics_device, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[1] { new Color(0, 0, 0, 0) });
            
            MenuItem m2 = new MenuItem(MenuItem.MENU_TYPE.GAMEPLAY);
            m2.bounding_box = new Rectangle(menu_left, menu_top + 100, menu_width, 100);
            add_sub_component(m2);

            MenuItem m3 = new MenuItem(MenuItem.MENU_TYPE.EXIT);
            m3.bounding_box = new Rectangle(menu_left, menu_top + 200, menu_width, 100);
            add_sub_component(m3);

            base.load_content();
        }

        public void input_handler(MouseState mouse_state)
        {
            Component clicked = this.parent_gamescreen.get_clicked_component(mouse_state);
            if (clicked != null)
            {
                MenuItem m = (MenuItem)clicked;
                switch (m.type)
                {
                    case MenuItem.MENU_TYPE.GAMEPLAY:
                        parent_gamescreen.Engine.screen_manager.change_screen(ScreenManager.SCREEN_TYPE.gameplay);
                        break;
                    case MenuItem.MENU_TYPE.EXIT:
                        parent_gamescreen.Engine.screen_manager.change_screen(ScreenManager.SCREEN_TYPE.credits);
                        break;
                    default:
                        break;
                }
                this.parent_gamescreen.Close();
            }
            Component component = this.parent_gamescreen.get_component_mouse_over(mouse_state);
            if (component != null)
            {
                clear_mouse_over();
                component.mouse_over = true;
            }
        }

        public void clear_mouse_over()
        {
            foreach (Component component in sub_components)
            {
                component.mouse_over = false;
            }
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
