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

            Internals.CRYPT_KEY_STATIC =
            ((char)('K' ^ Internals.VERSION_MAJOR)).ToString() +
            ((char)('E' ^ Internals.VERSION_MINOR)).ToString() +
            ((char)('Y' ^ Internals.VERSION_PATCH)).ToString() +
            "RdQZaXP5iqzKpLZFaxkN";
            Internals.CRYPT_KEY_DYNAMIC = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            Settings.Initialize();

            Main m = new Main();
            Application.Run(m);

            Settings.Uninitialize();
        }
    }
}
