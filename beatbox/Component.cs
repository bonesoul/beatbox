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
    public enum DRAW_ORDER
    {
        BOTTOM_MOST = 0,
        MIDDLE = 1,
        TOP_MOST = 2
    }

    public class Component
    {
        // component properties
        public string name; // name
        public GameScreen parent_gamescreen; // parent gamescreen
        public bool unique = true; // is the component needs to be unique in the gamescreen?

        // drawing properties
        public Rectangle bounding_box = new Rectangle(0, 0, 0, 0); // components drawing & selection box        
        public DRAW_ORDER draw_order = DRAW_ORDER.BOTTOM_MOST;

        // sub-components
        public List<Component> sub_components = new List<Component>(); // subcomponents

        // explosin spessific
        public SpriteBatch explosion_sprite_batch;

        public bool mouse_over = false;

        public Component()
        {
            this.draw_order = DRAW_ORDER.BOTTOM_MOST;
        }

        public Component(DRAW_ORDER _draw_order)
        {
            this.draw_order = _draw_order;
        }

        public virtual void load()
        {
            this.load_content();
        }

        public virtual void load_content()
        {
            explosion_sprite_batch = new SpriteBatch(this.parent_gamescreen.graphics_device);
        }

        public virtual void Draw(GameTime game_time)
        {
            foreach (Component sub_component in sub_components)
            {
                sub_component.Draw(game_time);
            }
        }

        public virtual void Update(GameTime game_time)
        {
            foreach (Component sub_component in sub_components)
            {
                sub_component.Update(game_time);
            }
        }

        public void add_sub_component(Component sub_component)
        {
            if (sub_component.unique && sub_components.Contains(sub_component)) // if sub-component needs to be unique and is already in subcomponents
                return;

            sub_component.parent_gamescreen = this.parent_gamescreen;
            sub_component.load();
            sub_components.Add(sub_component);
        }


        public virtual Component is_clicked(MouseState mouse_state) // if component is clicked or one of it's subcomponents, will return the clicked (sub)component
        {
            foreach (Component c in sub_components)
            {
                Component t = c.is_clicked(mouse_state);
                if (t != null)
                    return t;
            }

            if ((((bounding_box.Left <= mouse_state.X) && (bounding_box.Right >= mouse_state.X)) && ((bounding_box.Top <= mouse_state.Y) && (bounding_box.Bottom >= mouse_state.Y))) & (mouse_state.LeftButton== ButtonState.Pressed))
                return this;

            return null;
        }

        public virtual Component is_mouse_over(MouseState mouse_state)
        {
            foreach (Component c in sub_components)
            {
                Component t = c.is_mouse_over(mouse_state);
                if (t != null)
                    return t;
            }

            if (((bounding_box.Left <= mouse_state.X) && (bounding_box.Right >= mouse_state.X)) && ((bounding_box.Top <= mouse_state.Y) && (bounding_box.Bottom >= mouse_state.Y)))
                return this;

            return null;
        }
    }
}
