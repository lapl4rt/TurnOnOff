using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pilots
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    static class Number
    {
        public static int n { get; set; }
    }
}
