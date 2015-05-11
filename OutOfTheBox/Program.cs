using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OutOfTheBox
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Main m = new Main();
            Application.Run(m);
        }
    }
}
