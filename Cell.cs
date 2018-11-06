/* Cell.cs
 * 
 * This program is used to construct the game board for COM377 group project.
 * You are not allowed to make any changes to this class.
 * 
 */


using System.Drawing;

namespace COM377CATMOUSEGAME
{
    public class Cell
    {
        const int cellSize = 20; //cell dimension in pixels
        private int x, y;
        private char type;
        private bool isVisible = true;

        //Constructor
        public Cell(int x, int y, char type)
        {
            this.type = type;
            this.x = x;
            this.y = y;
        }

        //Set cell as visited (no pill inside it)
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }

            set
            {
                isVisible = value;
            }
        }

        public char CellType
        {
            get { return type; }
            set { type = value; }
        }

        //draw the cell 
        public virtual void drawBackground(Graphics g)
        {

            int xBase = 0;
            int yBase = 0;

            switch (type)
            {
                case 'e': //Gate of mouse cage
                    g.FillRectangle(Brushes.White, x * cellSize, y * cellSize + cellSize / 2 - 10, cellSize, 3);
                    break;
                case 'a': //horizontal line
                    g.FillRectangle(Brushes.Blue, x * cellSize, y * cellSize + cellSize / 2 - 1, cellSize + 1, 3);
                    break;
                case 'm': //vertical line
                    g.FillRectangle(Brushes.Blue, x * cellSize + cellSize / 2 - 1, y * cellSize, 3, cellSize + 1);
                    break;
                case '1': //northeast corner
                    xBase = x * cellSize;
                    yBase = y * cellSize + cellSize / 2;
                    g.FillRectangle(Brushes.Blue, xBase, yBase - 1, cellSize / 2, 3);
                    g.FillRectangle(Brushes.Blue, xBase + cellSize / 2 - 1, yBase - 1, 3, cellSize / 2 + 2);
                    break;
                case '2': //northwest corner
                    xBase = x * cellSize + cellSize / 2;
                    yBase = y * cellSize + cellSize / 2;
                    g.FillRectangle(Brushes.Blue, xBase - 1, yBase - 1, cellSize / 2 + 2, 3);
                    g.FillRectangle(Brushes.Blue, xBase - 1, yBase - 1, 3, cellSize / 2 + 2);
                    break;
                case '3': //southeast corner
                    xBase = x * cellSize;
                    yBase = y * cellSize;
                    g.FillRectangle(Brushes.Blue, xBase, yBase + cellSize / 2 - 1, cellSize / 2, 3);
                    g.FillRectangle(Brushes.Blue, xBase + cellSize / 2 - 1, yBase, 3, cellSize / 2 + 2);
                    break;
                case '4': //southwest corner
                    xBase = x * cellSize + cellSize / 2;
                    yBase = y * cellSize;
                    g.FillRectangle(Brushes.Blue, xBase - 1, yBase + cellSize / 2 - 1, cellSize / 2 + 2, 3);
                    g.FillRectangle(Brushes.Blue, xBase - 1, yBase, 3, cellSize / 2 + 2);
                    break;
                case 'o':
                    break; //empty navigable cell

                case 'd': //navigable cell with pill
                    if (isVisible)
                    {
                        g.FillRectangle(Brushes.White, x * cellSize + cellSize / 2 - 1, y * cellSize + cellSize / 2 - 1, 3, 3);
                    }
                    break;

                case 'c': //navigable cell with cheese
                    if (isVisible)
                    {
                        g.FillRectangle(new SolidBrush(Color.Yellow), x * cellSize + cellSize / 2 - 7, y * cellSize + cellSize / 2 - 7, 13, 13);

                    }
                    break;
                case 'x': //empty non-navigable cell
                case 'g': //mouse cage
                default:
                    break;
            }
        }
    }

}
