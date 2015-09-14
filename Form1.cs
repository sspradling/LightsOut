using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lights_Out
{
    public partial class MainForm : Form
    {
        private const int GRID_OFFSET = 25;
        private const int GRID_LENGTH = 200;
        private const int NUM_CELLS = 3;
        private const int CELL_LENGTH = GRID_LENGTH / NUM_CELLS;

        private bool[,] grid;
        private Random rand;
        public int clickCount = 0;



        public MainForm()
        {
            InitializeComponent();

            rand = new Random();
            grid = new bool[NUM_CELLS, NUM_CELLS];

            for(int r = 0; r < NUM_CELLS; r++)
            {
                for(int c = 0; c < NUM_CELLS; c++)
                {
                    grid[r, c] = true;
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for(int r = 0; r < NUM_CELLS; r++)
            {
                for(int c = 0; c < NUM_CELLS; c++)
                {
                    Brush brush;
                    Pen pen;

                    if(grid[r,c])
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = c * CELL_LENGTH + GRID_OFFSET;
                    int y = r * CELL_LENGTH + GRID_OFFSET;

                    g.DrawRectangle(pen, x, y, CELL_LENGTH, CELL_LENGTH);
                    g.FillRectangle(brush, x + 1, y + 1, CELL_LENGTH - 1, CELL_LENGTH - 1);
            
                }
            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.X < GRID_OFFSET || e.X > CELL_LENGTH * NUM_CELLS + GRID_OFFSET || e.Y < GRID_OFFSET || e.Y > CELL_LENGTH * NUM_CELLS + GRID_OFFSET)
            {
                return; 
            }

            clickCount += 1;
            int r = (e.Y - GRID_OFFSET) / CELL_LENGTH;
            int c = (e.X - GRID_OFFSET) / CELL_LENGTH;

            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    if (i >= 0 && i < NUM_CELLS && j >= 0 && j < NUM_CELLS)
                    {
                        grid[i, j] = !grid[i, j];
                    }
                }
            }

            this.Invalidate();

            if(PlayerWon())
            {
                MessageBox.Show(this, "Congratulations! You've won with " + clickCount + " clicks!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }

        private bool PlayerWon()
        {
            bool won = true;
            for( int r = 0; r < NUM_CELLS; r++ )
            {
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    if (grid[r,c] == true)
                    {
                        won = false;
                    }
                }
            }
            return won;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            clickCount = 0;
            for (int r = 0; r < NUM_CELLS; r++)
            {
                for (int c = 0; c < NUM_CELLS; c++)
                {
                    grid[r, c] = rand.Next(2) == 1;
                }
            }
            this.Invalidate();
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exitButton_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }
    }
}
