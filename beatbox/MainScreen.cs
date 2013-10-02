using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace beatbox
{
    public class MainScreen:GameScreen
    {
        public SongManager song_manager;
        public BottomLine b1;
      //

        public int stage = 2;

        public ScoreBoard skortablosu;

        public MainScreen()
            : base()
        {
        }

        public MainScreen(int _stage):base()
        {
            stage = _stage;
        }

        public MainScreen(bool _auto_play)
            : base()
        {            
        }

       
        public override void load_components()
        {
            song_manager = new SongManager(this.stage);
            this.add_component(song_manager);

            b1 = new BottomLine(this.song_manager.parent_gamescreen.height-75);
            skortablosu = new ScoreBoard();
            add_component(skortablosu);
            add_component(b1);
            Scene scene = new Scene();
            this.add_component(scene);
            base.load_components();
        }
    }
}
