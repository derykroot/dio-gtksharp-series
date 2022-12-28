using System;
using Gtk;

namespace dio_gtksharp_series
{
    class mainw {
        public static Gtk.Window win = new WMain();
    }    
    class Program
    {
        [STAThread]

        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.dio_gtksharp_series", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            //var win = new MainWindow();
            app.AddWindow(mainw.win);

            mainw.win.Show();
            Application.Run();
        }
    }
}
