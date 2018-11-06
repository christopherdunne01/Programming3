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
using COM377CATMOUSEGAME;

namespace COM377CATMOUSEGAME
{
    public partial class GameBoard : Form
    {
        private Cell[,] myMap;
        private const int cellSize = 20;
        private Graphics g;
        private MouseController mouse;
        private CatController cat;
        private ScoreInfoEventArgs su;
        private static int lives = 3;
        public  bool gameRunning { get; set; } 

        public int Lives { 
            set {
                lives = value;  
            } 
            
            get { 
                return lives; 
            } 
        }

        public GameBoard()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Start(); 
           
        }

        private Cell[,] CreateMap(string mapFile)
        {
            int numRows = 0;
            int numColumns = 0;

            try
            {
               StreamReader mapReader = new StreamReader(mapFile);
               String line = mapReader.ReadLine();

                numColumns = line.Length;

                while (line != null)
                {
                    numRows++;
                    line = mapReader.ReadLine();
                }

                mapReader.Close();

            }
            catch (IOException e)
            {

            }

            Cell[,] myMap = new Cell[numColumns, numRows];

            try
            {
                StreamReader mapReader = new StreamReader(mapFile);
                String line = mapReader.ReadLine();

                int rowIndex = 0;

                while (line != null)
                {
                    for(int columnIndex = 0; columnIndex<line.Length; columnIndex++)
                    {
                        myMap[columnIndex, rowIndex] = new Cell(columnIndex, rowIndex, line[columnIndex]);

                    }


                    rowIndex++;
                    line = mapReader.ReadLine();
                     
                }

                mapReader.Close();
            }
            catch (IOException e)
            {

            }
            
            return myMap;
        }

        private void mapBox_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            if (mouse == null) {
                mouse = new MouseController(g, cellSize, myMap, this, cat);
            }
            else {
                mouse.DrawMouse(g);
            }

            if (cat == null) {
                cat = new CatController(g, cellSize, myMap, this, mouse);
            } else {
                cat.DrawCat(g);
            }
            // draw the paving slabs
            foreach (Cell myCell in myMap)
                myCell.drawBackground(g);

            mouse.Cat = cat;
            su = new ScoreInfoEventArgs();
            mouse.ScoreChange += new ScoreChangeEventHandler(ChangeScore);
            gameRunning = true; 
        }

        protected virtual void ChangeScore(object sender, ScoreInfoEventArgs e)
        {
            Control[] scoreBox = Controls.Find("scoreBox", false);

            scoreBox[0].Text = e.score.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void GameBoard_Load(object sender, EventArgs e)
        {
            //map = CreateMap(Properties.Resources.maplevel1);
           // String fileName = "P:/COM377/COM377CATMOUSEGAME/COM377CATMOUSEGAME/res/maplevel1.txt";
            myMap = CreateMap("..\\..\\res\\maplevel1.txt");
            mapBox.Width = myMap.GetLength(0) * cellSize;
            mapBox.Height = myMap.GetLength(1) * cellSize;
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(gameRunning == false)
            {
               gameRunning = true; 
            }else{
                gameRunning = false; 
            }
            timer1.Stop();
            Refresh();
        }

        private void Next_Click(object sender, EventArgs e)
        {
       
            myMap = CreateMap("..\\..\\res\\maplevel2.txt");
            mapBox.Width = myMap.GetLength(0) * cellSize;
            mapBox.Height = myMap.GetLength(1) * cellSize;
            mapBox.Invalidate();
            mouse.Map = myMap;
            mouse.SetCurPosition(19, 12);
            cat.Map = myMap;
            cat.SetCurPosition(29, 5);
        }

        private void CheckCollision()
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (gameRunning == true)
            {


                switch (keyData)
                {
                    case Keys.Down:
                        mouse.Move(keyData);
                        Refresh();
                        break;
                    case Keys.Up:
                        mouse.Move(keyData);
                        Refresh();
                        break;
                    case Keys.Right:
                        mouse.Move(keyData);
                        Refresh();
                        break;
                    case Keys.Left:
                        mouse.Move(keyData);
                        Refresh();
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
            


        }

        
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Tag = "Message";

            // Show a message box with OK button.
            DialogResult r = MessageBox.Show("Welcome! Please get all the cheese before the cat catches you. Watch out, you only have 3 lives!!",
                                              "Help", MessageBoxButtons.OK, 0);
        }


        private void button4_Click(object sender, EventArgs e)
        {
            this.Tag = "Message";

            // Show a message box with OK button.
            DialogResult r = MessageBox.Show("Philip Higgins: B00000000 Elaine Coyle: B0000000 Christopher Dunne: B00684533",
                                              "About", MessageBoxButtons.OK, 0);
        }
            
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
