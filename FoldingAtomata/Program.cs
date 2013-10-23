using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace FoldingAtomata
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (!Options.HandleFlags(args.Length, args))
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            //Console.Read();
        }
    }
}
