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
    public class ScreenManager
    {
        public enum SCREEN_TYPE
        {
            loading_screen,
            mainmenu,
            gameplay,
            credits
        }
        private Engine Engine;
        SoundEffect sound_effect;
        public SCREEN_TYPE active_screen;


        public ScreenManager(Engine _engine)
        {
            Engine = _engine;
            sound_effect = Engine.content_manager.Load<SoundEffect>(@"sounds\intro");
            change_screen(SCREEN_TYPE.loading_screen);
        }

        public void change_stage()
        {
            change_screen(SCREEN_TYPE.gameplay);
        }

        public void change_screen(SCREEN_TYPE type)
        {
            switch (type)
            {
                case SCREEN_TYPE.loading_screen:
                    sound_effect.Play();
                    active_screen = SCREEN_TYPE.loading_screen;
                    Engine.push_screen(new LoadingScreen());
                    break;
                case SCREEN_TYPE.mainmenu:
                    active_screen = SCREEN_TYPE.mainmenu;
                    Engine.push_screen(new MenuScreen());
                    break;
                case SCREEN_TYPE.gameplay:
                    sound_effect.Dispose();
                    if (active_screen == SCREEN_TYPE.gameplay)
                        Engine.push_screen(new MainScreen(3));
                    else
                        Engine.push_screen(new MainScreen(2));
                    active_screen = SCREEN_TYPE.gameplay;
                    break;
                case SCREEN_TYPE.credits:
                    sound_effect.Dispose();
                    SoundEffect sound_effect2 = Engine.content_manager.Load<SoundEffect>(@"sounds\credits");
                    sound_effect2.Play();
                    active_screen = SCREEN_TYPE.credits;
                    Engine.push_screen(new CreditsScreen());
                    break;
                default:
                    break;
            }
        }
    }
}
