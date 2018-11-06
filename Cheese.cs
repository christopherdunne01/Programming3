using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM377CATMOUSEGAME
{
    class Cheese
    {
        private int cheeseWidth = 20;
        private int cheeseHeight = 10;

        private int cheeseX = 0;
        private int cheeseY = 0;

        private Form1 parent;

        public Cheese(Form1 parent)
        {
            this.parent = parent;
            cheeseX = parent.YardWidth / 2;
            cheeseY = parent.YardHeight / 2;
        }

        public int CheeseX
        {
            get { return cheeseX; }
            set { cheeseX = value; }
        }

        public int CheeseY
        {
            get { return cheeseY; }
            set { cheeseY = value; }
        }

        public int CheeseWidth
        {
            get { return cheeseWidth; }
            set { cheeseWidth = value; }
        }

        public int CheeseHeight
        {
            get { return cheeseHeight; }
            set { cheeseHeight = value; }
        }


    }
}
