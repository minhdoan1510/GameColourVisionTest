using System;

namespace BTH3
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            fMain main = new fMain();
            main.ShowDialog();
        }
    }
}