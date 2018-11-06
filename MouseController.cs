using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.ComponentModel;

namespace COM377CATMOUSEGAME
{
    class MouseController : Controller
    {
        private SoundPlayer simpleSound;
        private ScoreInfoEventArgs e = new ScoreInfoEventArgs();

        public event ScoreChangeEventHandler ScoreChange;

        private CatController cat;
        private GameBoard f;

        public CatController Cat
        {
            set
            {
                cat = value;
            }
        }

        public void LoadMouseImage()
        {
            image = Properties.Resources.tom;
            //set the starting position...
            SetCurPosition(21, 10);
        }

        public MouseController(Graphics g, int cellSize, Cell[,] map, GameBoard f, CatController cat)
        {
            CurPos.pixX += (cellSize / 4 + 1);
            CurPos.pixY += (cellSize / 4 + 1);

            this.moveDistance = this.cellSize = cellSize;

            this.g = g;
            this.map = map;
            this.cat = cat;
            this.f = f;

            Collisions += new CollisionCheck(CheckCollision);

            LoadMouseImage();
            DrawMouse(g);
            simpleSound = new SoundPlayer(@"..\\..\\res\\cat.wav");

        }

        public void DrawMouse(Graphics g)
        {
            g.DrawImage(image, CurPos.pixX, CurPos.pixY, cellSize, cellSize);

        }


        public void Move(Keys key)
        {
            //IncreaseScore();
            CheckCollision();

            switch (key)
            {
                case Keys.Up:
                    MoveUp();
                    break;
                case Keys.Down:
                    MoveDown();
                    break;
                case Keys.Left:
                    MoveLeft();
                    break;
                case Keys.Right:
                    MoveRight();
                    break;
            }
        }

        private bool isValidCell(char cellType)
        {
            bool validCell = (cellType == 'o' || cellType == 'd' ||
                cellType == 'c' || cellType == 'g' || cellType == 'e');

            if (validCell)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        private void playSimpleSound()
        {
            simpleSound.Play();
        }


     
        
        /*void IncreaseScore()
        {
            e.Score += 5;
            ScoreChange(this, e);
        } */

        private void MoveRight()
        {
            char nextCell = map[CurPos.cellX + 1, CurPos.cellY].CellType;
            //check the next cell over is one that can be moved into
            if (isValidCell(nextCell))
            {
                CurPos.cellX += 1;
                CurPos.pixX = (CurPos.cellX * cellSize);
            }
            if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'd')
            {
                e.Score += 10;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }

            else if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'c')
            {
                e.Score += 50;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }


        }
        private void MoveLeft()
        {
            char nextCell = map[CurPos.cellX - 1, CurPos.cellY].CellType;

            if (isValidCell(nextCell))
            {
                CurPos.cellX -= 1;
                CurPos.pixX = (CurPos.cellX * cellSize);
            }

            if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'd')
            {
                e.Score += 10;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }

            else if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'c')
            {
                e.Score += 50;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }



        }
        private void MoveUp()
        {

            char nextCell = map[CurPos.cellX, CurPos.cellY - 1].CellType;

            if (isValidCell(nextCell))
            {
                CurPos.cellY -= 1;
                CurPos.pixY = (CurPos.cellY * cellSize);
            }
            if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'd')
            {
                e.Score += 10;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }

            else if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'c')
            {
                e.Score += 50;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }



        }
        private void MoveDown()
        {
            char nextCell = map[CurPos.cellX, CurPos.cellY + 1].CellType;

            if (isValidCell(nextCell))
            {
                CurPos.cellY += 1;
                CurPos.pixY = (CurPos.cellY * cellSize);
            }

            if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'd')
            {
                e.Score += 10;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }

            else if (map[CurPos.cellX, CurPos.cellY].IsVisible == true && map[CurPos.cellX, CurPos.cellY].CellType == 'c')
            {
                e.Score += 50;
                ScoreChange(this, e);
                map[CurPos.cellX, CurPos.cellY].IsVisible = false;
                playSimpleSound();
            }
        }

        private void CheckCollision()
        {
            if (cat.CurPos.cellX == this.CurPos.cellX && cat.CurPos.cellY == this.CurPos.cellY) {
                Control[] lifeBox = f.Controls.Find("lifeBox", false);

                lifeBox[0].Text = ((f.Lives--) - 1).ToString();
                if (f.Lives == 0)
                {
                    DialogResult result = MessageBox.Show("Do you want to continue?", "Message", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        SetCurPosition(21, 10);
                        f.Lives = 3;
                    }
                    else if (result == DialogResult.No)
                    {
                        Application.Exit();

                    }
                }
                SetCurPosition(21, 10);
            }
        }
    }
}