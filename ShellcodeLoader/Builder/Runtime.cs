using System;
using System.Threading;
using System.Windows.Forms;

namespace Builder
{
    static class Runtime
    {
        static Mutex mutex = new Mutex(true, "SHLMTXXXX");
        
        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("The program has already been launched!", "Loader Informer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CMain());
            mutex.ReleaseMutex();
        }
    }
}
