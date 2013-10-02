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
    public class GameScreen
    {
        public Engine Engine;
        public GraphicsDevice graphics_device;

        public List<Component> components = new List<Component>(); // screen components

        protected bool loaded = false; // loaded?
        public int width;
        public int height;

        public Rectangle bounding_box;

        public DRAW_ORDER draw_order;

        public GameScreen()
        { draw_order = DRAW_ORDER.BOTTOM_MOST; }

        public GameScreen(DRAW_ORDER _draw_order)
        {
            draw_order=_draw_order;
        }

        public virtual void load()
        {
            this.graphics_device = Engine.graphics_device;
            this.width = Engine.screen_width;
            this.height = Engine.screen_height;
            load_components();
            this.loaded = true;
        }

        public virtual void load_components()
        {
        }

        public virtual void Draw(GameTime game_time)
        {
            foreach (Component component in components)
            {
                component.Draw(game_time);
            }
        }

        public virtual void Update(GameTime game_time)
        {
            foreach (Component component in components)
            {
                component.Update(game_time);
            }
        }

        public virtual void Close() // Closes the current gamescreen
        {
            this.Engine.remove_screen(this);
        }

        public void add_component(Component component)
        {
            if (component.unique && components.Contains(component)) // if component needs to be unique and is already on gamescreen
                return; // then don't add it

            component.parent_gamescreen = this;
            component.load();

            // now put component in correct draw order
            int i = 0;
            for (i = 0; i < components.Count; i++)
                if (components[i].draw_order > component.draw_order)
                    break;
            components.Insert(i, component);
        }

        public virtual Component get_clicked_component(MouseState mouse_state) // gets a clicked component
        {
            // we should iterate components in reserve draw order!
            List<Component> reserved = new List<Component>(components.Count); // so create a reserved list first
            for (int i = components.Count - 1; i >= 0; i--)
            {
                reserved.Add(components[i]);
            }

            foreach (Component c in reserved)
            {
                Component t = c.is_clicked(mouse_state);
                if (t != null) return t;
            }
            return null;
        }

        public virtual Component get_component_mouse_over(MouseState mouse_state) 
        {
            // we should iterate components in reserve draw order!
            List<Component> reserved = new List<Component>(components.Count); // so create a reserved list first
            for (int i = components.Count - 1; i >= 0; i--)
            {
                reserved.Add(components[i]);
            }

            foreach (Component c in reserved)
            {
                Component t = c.is_mouse_over(mouse_state);
                if (t != null) return t;
            }
            return null;
        }
    }
}
