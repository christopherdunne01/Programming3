using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COM377CATMOUSEGAME
{
    public class CollisionEventArgs : EventArgs
    {
        public Form f { get; set; }

        public CollisionEventArgs()
        {

        }

        public static int lives = 3;

        public CollisionEventArgs(int score)
        {
            lives = score;
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }
    }
}
