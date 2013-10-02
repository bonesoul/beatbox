using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace beatbox
{
    /// <summary>
    /// USAGE:
    ///     
    ///     Level level = new Level();
    ///     level.LevelName = level1;
    ///     level.Load();

    ///     foreach (LevelPath item in level.GamePaths)
    ///     {
    ///          MessageBox.Show(item.Color);
    ///          foreach (int timer in item.StartTime)
    ///          {
    ///            MessageBox.Show(timer.ToString());
    ///          }
    ///     }        

    /// </summary>
    public class LevelPath
    {
        public string FileName { get; set; }
        public int TimeCount { get; set; }
        public List<int> StartTime { get; set; }

        public LevelPath()
        {
            StartTime = new List<int>();
        }
    }
    public class Level
    {
        public string LevelName { get; set; }
        public int PathCount { get; set; }
        public List<LevelPath> GamePaths { get; set; }

        //TODO: Renew for new computer 
        //public const string LevelPath = @"C:\Users\Giray\Desktop\ggjbeat\trunk\beatbox\beatbox\maps\";
        //public string LevelPath = @"\maps\";
        public string LevelPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\maps\\";
        

        public Level()
        {
            GamePaths = new List<LevelPath>();
        }
        /// <summary>
        /// Level yükle bebeğim
        /// </summary>
        public void Load()
        {
            Settings gameSettings = new Settings();
            gameSettings.FilePath = LevelPath + LevelName + ".map";

            PathCount = DataConvert.SafeInt(gameSettings.Read(LevelName, "path_count"));

            LevelPath path = null;
            for (int pathCounts = 1; pathCounts <= PathCount; pathCounts++)
            {
                path = new LevelPath();
                path.FileName = gameSettings.Read(string.Format("path{0}", pathCounts), "filename");
                path.TimeCount = DataConvert.SafeInt(gameSettings.Read(string.Format("path{0}", pathCounts), "time_count"));

                for (int timeCounts = 1; timeCounts <= path.TimeCount; timeCounts++)
                {
                    path.StartTime.Add(DataConvert.SafeInt(gameSettings.Read(string.Format("path{0}", pathCounts), string.Format("start_time{0}", timeCounts))));
                }

                GamePaths.Add(path);
            }
        }

    }
}
