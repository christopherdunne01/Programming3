using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace COM377CATMOUSEGAME
{

    class CatController : Controller
    {
        public delegate void LastMove();

        private Timer t = new Timer();
        private GameBoard f;
        private Random r = new Random();
        private bool IsIntelligent = false;
        private bool pathBlocked = false;

        private MouseController mouse;

        private LastMove lastMove;

        public CatController(Graphics g, int cellSize, Cell[,] map, GameBoard parentForm, MouseController mouse)
        {
            CurPos.pixX += (cellSize / 4 + 1);
            CurPos.pixY += +(cellSize / 4 + 1);

            this.moveDistance = this.cellSize = cellSize;

            this.g = g;
            this.map = map;

            f = parentForm;

            LoadMouseImage();
            DrawCat(g);

            this.mouse = mouse;
            lastMove = MoveUp;

            Collisions += new CollisionCheck(CheckCollision);

            t.Enabled = true;
            t.Interval = 300;
            t.Tick += new System.EventHandler(Move);
            t.Start();
        }

        public void LoadMouseImage()
        {
            image = Image.FromFile("..\\..\\resources\\cat.png");
            //set the starting position...
            SetCurPosition(29, 5);
        }

        public void DrawCat(Graphics g)
        {
            g.DrawImage(image, CurPos.pixX, CurPos.pixY, cellSize, cellSize);

        }

        private void MoveRandomly()
        {
            int num = r.Next(1, 5);

            if (!pathBlocked)
            {
                if (lastMove == MoveUp || lastMove == MoveDown)
                {

                    switch (num % 2)
                    {

                        case 0:
                            if (IsValidCell(map[CurPos.cellX + 1, CurPos.cellY].CellType))
                            {
                                MoveRight();
                                return;
                            }
                            else if (IsValidCell(map[CurPos.cellX - 1, CurPos.cellY].CellType))
                            {
                                MoveLeft();
                                return;
                            }
                            break;

                        case 1:
                            if (IsValidCell(map[CurPos.cellX + 1, CurPos.cellY].CellType))
                            {
                                MoveLeft();
                                return;
                            }
                            else if (IsValidCell(map[CurPos.cellX - 1, CurPos.cellY].CellType))
                            {
                                MoveRight();
                                return;
                            }
                            break;

                        default:
                            break;
                    }
                }
                else if (lastMove == MoveRight || lastMove == MoveLeft)
                {
                    switch (num % 2)
                    {

                        case 0:
                            if (IsValidCell(map[CurPos.cellX, CurPos.cellY + 1].CellType))
                            {
                                MoveDown();
                                return;
                            }
                            else if (IsValidCell(map[CurPos.cellX, CurPos.cellY - 1].CellType))
                            {
                                MoveUp();
                                return;
                            }
                            break;

                        case 1:
                            if (IsValidCell(map[CurPos.cellX, CurPos.cellY + 1].CellType))
                            {
                                MoveUp();
                                return;
                            }
                            else if (IsValidCell(map[CurPos.cellX, CurPos.cellY - 1].CellType))
                            {
                                MoveDown();
                                return;
                            }
                            break;

                        default:
                            break;

                    }
                }

                lastMove();

            }
            else
            {

                pathBlocked = false;

                switch (num)
                {
                    case 1:
                        MoveRight();
                        break;
                    case 2:
                        MoveLeft();
                        break;
                    case 3:
                        MoveUp();
                        break;
                    case 4:
                        MoveDown();
                        break;
                }
            }
        }

        private void MoveIntelligently()
        {
            //TODO
        }

        public void Move(object sender, EventArgs e)
        {
            CheckCollision();

            if (!IsIntelligent)
            {
                MoveRandomly();
            }
            else
            {

            }

            f.Refresh();

            CalculateDistance();
        }

        private void MoveRight()
        {       //check the next cell over is one that can be moved into
            if (IsValidCell(map[CurPos.cellX + 1, CurPos.cellY].CellType))
            {
                CurPos.cellX += 1;
                CurPos.pixX = (CurPos.cellX * cellSize);
                lastMove = MoveRight;
            }
        }

        private void MoveLeft()
        {
            if (IsValidCell(map[CurPos.cellX - 1, CurPos.cellY].CellType))
            {
                CurPos.cellX -= 1;
                CurPos.pixX = (CurPos.cellX * cellSize);
                lastMove = MoveLeft;
            }
        }

        private void MoveUp()
        {
            if (IsValidCell(map[CurPos.cellX, CurPos.cellY - 1].CellType))
            {
                CurPos.cellY -= 1;
                CurPos.pixY = (CurPos.cellY * cellSize);
                lastMove = MoveUp;
            }
        }

        private void MoveDown()
        {
            if (IsValidCell(map[CurPos.cellX, CurPos.cellY + 1].CellType))
            {
                CurPos.cellY += 1;
                CurPos.pixY = (CurPos.cellY * cellSize);
                lastMove = MoveDown;
            }
        }

        private bool IsValidCell(char cellType)
        {
            bool validCell = (cellType == 'o' || cellType == 'd' ||
                cellType == 'c' || cellType == 'g');

            if (validCell)
            {
                pathBlocked = false;
                return true;
            }
            else
            {
                pathBlocked = true;
                return false;
            }
        }

        private void CalculateDistance()
        {
            int mRow, cRow, mCol, cCol;
            double distance = 0;

            mRow = mouse.CurPos.cellY;
            mCol = mouse.CurPos.cellX;

            cRow = CurPos.cellY;
            cCol = CurPos.cellX;

            distance = Math.Sqrt((Math.Pow((cRow - mRow), 2.0)) + (Math.Pow((cCol - mCol), 2.0f)));

            Console.WriteLine(distance.ToString());
        }

        private void CheckCollision()
        {
            if (this.CurPos.cellX == mouse.CurPos.cellX && this.CurPos.cellY == mouse.CurPos.cellY)
            {
                Control[] lifeBox = f.Controls.Find("lifeBox", false);

                lifeBox[0].Text = ((f.Lives--) - 1).ToString();

                if (f.Lives == 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to continue?", "Message", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        mouse.SetCurPosition(21, 10);
                        f.Lives = 3;
                    }
                    else if (result == DialogResult.No)
                    {
                        Application.Exit();
                        
                    }
                }

                mouse.SetCurPosition(21, 10);
            }
        }

    }
}