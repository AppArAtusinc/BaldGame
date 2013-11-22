using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaldaGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] cells;
        DataGridViewCellStyle chek, notchek;

        
        balda game;
        private void Form1_Load(object sender, EventArgs e)
        {

            chek = new DataGridViewCellStyle();
            notchek = new DataGridViewCellStyle();
            chek.BackColor = Color.DodgerBlue;
            notchek.BackColor = Color.White;
            notchek.SelectionBackColor = Color.White;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell column in row.Cells)
                {
                    column.Style = notchek;
                }
            }
            //for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //    for (int j = 0; j < dataGridView1.ColumnCount; j++)
            //        dataGridView1.Rows[i].Cells[j].Style = notchek;
            cells = new int[0];

            game = new balda();
            dataGridView1.RowCount = dataGridView1.ColumnCount = 5;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 30;
                dataGridView1.Columns[i].Resizable = DataGridViewTriState.False;
                dataGridView1.Rows[i].Resizable = DataGridViewTriState.False;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int[] buf = new int[cells.Length+2];
            for (int i = 0; i < cells.Length; i++)
                buf[i] = cells[i];
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = chek;
            buf[buf.Length - 1] = e.ColumnIndex;
            buf[buf.Length - 2] = e.RowIndex;
            cells = buf;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            if (!game.CheckCells(cells))
            { 
               Array.Resize<int>(ref cells, cells.Length - 2);
               ClearGridOnly();

                for (int i = 0; i < cells.Length; i += 2)
                    dataGridView1.Rows[cells[i]].Cells[cells[i+1]].Style = chek;
            }
        }


        void ClearGrid()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Style = notchek;
            cells = new int[0];
        }

        void ClearGridOnly()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Style = notchek;
        }
        void ClearData()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style = notchek;
                    dataGridView1.Rows[i].Cells[j].Value = " ";
                }
            cells = new int[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            matrix data = new matrix(dataGridView1.RowCount);


            data.Copy( game.CurrentMatrix());
            int count = 0;
            for (int i = 0; i < data.Length; i++)
                for (int j = 0; j < data.Length; j++)
                {
                    if (data[i][j] == '\0' && dataGridView1.Rows[i].Cells[j].Value.ToString()[0] != '\0')
                        count++;
                    if (count > 1)
                    {

                        for (int ii = 0; ii < data.Length; ii++)
                            for (int jj = 0; jj < data.Length; jj++)
                                dataGridView1.Rows[ii].Cells[jj].Value = (char)data[ii][jj];
                        MessageBox.Show("Вы ввели больше чем 1 символ.");
                        return;

                    }


                }

            for (int i = 0; i < data.Length; i++)
                for (int j = 0; j < data.Length; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Length > 0)
                        data[i][j] = dataGridView1.Rows[i].Cells[j].Value.ToString()[0];
            string word;

            if (data == game.CurrentMatrix())
            {

                for (int i = 0; i < data.Length; i++)
                    for (int j = 0; j < data.Length; j++)
                        dataGridView1.Rows[i].Cells[j].Value = (char)data[i][j];

                MessageBox.Show("Вы не добавили букву или добавили больше одной.");
                return;
            }


            try
            {
                word = game.Move(data, cells);
                if (game.CurentPlayer())
                {

                    label2.Text = game.Rating()[1].ToString();
                    listBox2.Items.Add(word);
                }
                else
                {
                    label1.Text = game.Rating()[0].ToString();
                    listBox1.Items.Add(word);
                }
               
                ClearGrid(); 
            }
            catch (Error ee)
            {
                ClearGrid();
                data = game.CurrentMatrix();
                for (int ii = 0; ii < data.Length; ii++)
                    for (int jj = 0; jj < data.Length; jj++)
                        dataGridView1.Rows[ii].Cells[jj].Value = (char)data[ii][jj];

                MessageBox.Show(ee.Info());
            }

            if (game.EndGame())
            {
                if (game.Rating()[1] > game.Rating()[0])
                    MessageBox.Show("Конец игры. Победил игрок 2.");
                if (game.Rating()[1] < game.Rating()[0])
                    MessageBox.Show("Конец игры. Победил игрок 1.");
                else
                    MessageBox.Show("Конец игры. Ничья!");
            }
            else
            {
                if (!game.CurentPlayer())
                    label7.Text = "2";
                else
                    label7.Text = "1";
            }
            
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearData();
            game.NewGame();
            label6.Visible = true;
            label7.Text = "1";
            matrix buf = new matrix();
            buf.Copy(game.CurrentMatrix());

            for (int i = 0; i < buf.Length; i++)
                for (int j = 0; j < buf.Length; j++)
                   dataGridView1.Rows[i].Cells[j].Value =  (char)buf[i][j];
            label1.Text = game.Rating()[0].ToString();
            label2.Text = game.Rating()[1].ToString();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            label5.Visible = true;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }
    }
}
