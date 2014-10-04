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
    public partial class Form2 : Form
    {
        private int n;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!int.TryParse(textBox1.Text, out n) || n < 2)
            {
                MessageBox.Show("Размер сейфа должен быть натуральным числом > 1. Попробуйте еще раз. ");
                textBox1.Clear();
                textBox1.Focus();
            }
            else
            {
                Number.n = n;
                Close();
            }
        }
    }
}
