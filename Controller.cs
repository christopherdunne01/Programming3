using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;

namespace COM377CATMOUSEGAME
{
    public delegate void CollisionCheck();

    abstract class Controller
    {
        public struct Position
        {   /* cellX and cellY represent the poistion within the 2D array
               and are used primarily to calculate interactions between
               the cat and mouse. pixX and pixY represent the pixel values
               associated with the position and are primarily used for drawing
            */
            public int cellX { get; set; }
            public int cellY { get; set; }
            public int pixX { get; set; }
            public int pixY { get; set; }
        }


        protected int moveDistance = 0;
        protected int cellSize = 20;
        protected Cell[,] map;
        public Position CurPos;
        public event CollisionCheck Collisions;

        public Cell[,] Map
        {
            set
            {
                map = value;
            }
        }

        protected Graphics g;
        protected Image image;

        public void SetCurPosition(int cellX, int cellY)
        {
            CurPos.pixX = (cellX * cellSize);
            CurPos.pixY = (cellY * cellSize);

            CurPos.cellX = cellX;
            CurPos.cellY = cellY;
        }
    }
}
