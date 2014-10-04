using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pilots
{
    public partial class Form1 : Form
    {
        public static int n = 3;//размер сейфа
        public Button[,] buttons;
        private int size = 50;//размер отдельного рычага сейфа
        private Random rand;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Number.n = n;
            LoadField();
        }

        //метод отрисовки игрового поля
        //изначально все рычаги стоят в одном положении
        void LoadField()
        {
            buttons = new Button[n, n];
            Width = n * size + size / 3;
            Height = n * size + 2 * size;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            rand = new Random();

            for (var i = 0; i < n; ++i)
            {
                for (var j = 0; j < n; ++j)
                {
                    buttons[i, j] = new Button
                                        {
                                            Size = new Size(size, size),
                                            Tag = 0.ToString(),
                                            Location = new Point(i*size, j*size + size),
                                            Name = i.ToString() + ' ' + j.ToString(),
                                            BackColor = Color.White,
                                            Image = Properties.Resources.pilot
                                        };
                    buttons[i,j].Click += new System.EventHandler(button_Click); 
                    Controls.Add(buttons[i, j]);
                }
            }

            MixButtons();
        }

        //метод, создающий игровую комбинацию
        void MixButtons()
        {
            int numberMix;
            int x, y;

            //количество перемешиваний положений рычагов
            numberMix = rand.Next(n, n+20);

            //случайным образом выбираются координаты рычага, в одной строке и в одном столбце с которым
            //меняются положения всех рычагов
            for (var num = 0; num < numberMix; ++num)
            {
                x = rand.Next(0, n);
                for (var j = 0; j < n; ++j)
                    Swap(buttons[x, j]);
                y = rand.Next(0, n);
                for (var i = 0; i < n; ++i)
                    Swap(buttons[i, y]);
                Swap(buttons[x,y]);
            }
        }

        void Swap(Button button)
        {
            //положение рычага ("вверх" или "в сторону") определяется по тегу соответствующей кнопки
            if (button.Tag.ToString() == "0")
            {
                button.Tag = "1";
                button.Image = Properties.Resources.pilotH;
            }
            else
            {
                button.Tag = "0";
                button.Image = Properties.Resources.pilot;
            }
        }

        bool isWin()
        {
            int up = 0;
            int down = 0;
            for(var i = 0; i < n; ++i)
                for (var j = 0; j < n; ++j)
                {
                    if (buttons[i, j].Tag.ToString() == "0")
                        down++;
                    else up++;
                }

            if (down == n*n || up == n*n)
                return true;
            return false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            //в имени кнопки содержатся ее координаты как элемента массива
            string[] strKoord = btn.Name.Split(' ');
            int x = Convert.ToInt32(strKoord[0]);
            int y = Convert.ToInt32(strKoord[1]);

            for (var j = 0; j < n; ++j)
                Swap(buttons[x, j]);
            for (var i = 0; i < n; ++i)
                Swap(buttons[i, y]);
            Swap(buttons[x, y]);

            if (isWin())
            {
                MessageBox.Show("Ура! Победа!");
                for (var i = 0; i < n; ++i)
                    for (var j = 0; j < n; ++j)
                        buttons[i,j].Enabled = false;

                if (MessageBox.Show("Сыграть еще раз?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    NewGame();
                }
            }
        }

        public void NewGame()
        {
            //Form2 - форма для ввода размера сейфа
            Form2 diagForm = new Form2();
            diagForm.ShowDialog();
            diagForm.StartPosition = FormStartPosition.CenterParent;

            for (var i = 0; i < n; ++i)
                for (var j = 0; j < n; ++j)
                {
                    Controls.Remove(buttons[i, j]);
                    buttons[i, j].Dispose();
                }
            n = Number.n;
            LoadField();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}
