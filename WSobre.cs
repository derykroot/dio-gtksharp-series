using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dio_gtksharp_series
{
    class WSobre : Window
    {
        [UI] private Gtk.Button _btok = null;
        public WSobre() : this(new Builder("WSobre.glade")) {}

        private WSobre(Builder builder) : base(builder.GetRawOwnedObject("WSobre"))
        {
            builder.Autoconnect(this);

            _btok.Clicked += Bt_Clicked;
        }

        private void Bt_Clicked(object sender, EventArgs a) {
            this.Destroy();
        }
    }
}
