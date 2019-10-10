using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B3
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            fMain fMain = new fMain();
            fMain.ShowDialog();
        }
    }
}
