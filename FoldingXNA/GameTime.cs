using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FoldingXNA
{
    public class GameTime : EventArgs
    {
        public static GameTime Instance
        {
            get
            {
                return _staticReference;
            }
        }

        public delegate void GameTick(object sender, GameTime e);
        public static event GameTick OnGameTick;

        static GameTime _staticReference;
        static Timer game;
        static int oldms = DateTime.Now.Millisecond;
        static GameTime()
        {
            _staticReference = new GameTime();
            game = new Timer();
            game.Interval = 30;
            game.Tick += game_Tick;
        }

        public static void Start()
        {
            game.Start();
        }
        public static void Stop()
        {
            game.Stop();
        }

        static void game_Tick(object sender, EventArgs e)
        {
            if (OnGameTick != null) OnGameTick(sender, Instance);
            oldms = DateTime.Now.Millisecond;
        }

        public int ElapsedMilliseconds
        {
            get
            {
                return (int)(DateTime.Now.Millisecond - oldms);
            }
        }
    }
}
