using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OutOfTheBox.Common;

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

            Settings.Initialize();

            Main m = new Main();
            Application.Run(m);

            Settings.Uninitialize();
        }
    }
}
