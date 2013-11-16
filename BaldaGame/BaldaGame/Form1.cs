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

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Style = notchek;
            cells = new int[0];

            game = new balda();
            dataGridView1.RowCount = dataGridView1.ColumnCount = 6;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Width = 30;
                dataGridView1.Columns[i].Resizable = DataGridViewTriState.False;
                dataGridView1.Rows[i].Resizable = DataGridViewTriState.False;
            }
            var ch = game.CheckCells(new int[] { 0, 0, 0, 1, 1,1});
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int[] buf = new int[cells.Length+2];
            for (int i = 0; i < cells.Length; i++)
                buf[i] = cells[i];
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = chek;
            buf[buf.Length - 2] = e.ColumnIndex;
            buf[buf.Length - 1] = e.RowIndex;
            cells = buf;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!game.CheckCells(cells))
            { 
                ClearData();
                MessageBox.Show("Некорректное выделение.");
            }
            if (e.Button == MouseButtons.Right)
                ClearData();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        dataGridView1.Rows[i].Cells[j].Style = notchek;
            cells = new int[0];
        }

        void ClearData()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Style = notchek;
            cells = new int[0];
        }
    }
}
