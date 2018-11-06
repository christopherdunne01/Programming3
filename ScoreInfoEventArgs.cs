using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Media;

namespace COM377CATMOUSEGAME
{
    public delegate void ScoreChangeEventHandler(object Source, ScoreInfoEventArgs e);

    public class ScoreInfoEventArgs : EventArgs
    {

        public ScoreInfoEventArgs()
        {
        }

        private static int score = 0;

        public ScoreInfoEventArgs(int score)
        {
            Score = score;
        }

        public int Score
        {
            get{
                return score;
            }
            set
            {
                score = value;
            }
        }
    }
}


