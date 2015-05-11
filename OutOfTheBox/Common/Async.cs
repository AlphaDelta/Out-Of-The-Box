using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OutOfTheBox.Common
{
    public delegate void Action();

    public class Async
    {
        public static BackgroundWorker StartAsync(Action e)
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += delegate { e(); };
            bg.RunWorkerCompleted += delegate { bg.Dispose(); };
            bg.RunWorkerAsync();
            return bg;
        }
    }
}
