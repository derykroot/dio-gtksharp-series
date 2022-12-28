using Gtk;

namespace dio_gtksharp_series
{
    // Classe Auxiliadora para simplificar a chamada de algumas funções
    static class utils {
        // Exibir Msgs de aviso em apenas uma função simples
        public static void msgbox(string msg, Gtk.MessageType msgType = MessageType.Info, Gtk.ButtonsType btnType = ButtonsType.Ok, Window _Win = null) {
            var w = _Win==null ? mainw.win : _Win;
            MessageDialog md = new MessageDialog(w, DialogFlags.DestroyWithParent,
                msgType, btnType, msg);
            md.Run();
            md.Destroy();
        }
    }
    
}