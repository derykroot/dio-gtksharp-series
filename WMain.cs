using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dio_gtksharp_series
{
    class WMain : Window
    {   
        [UI] private Gtk.MenuItem _mSobre = null;
        [UI] private Gtk.Label _lbsel = null;
        [UI] private Gtk.Button _btadd = null;
        [UI] private Gtk.Button _btedit = null;
        [UI] private Gtk.Button _btrmv = null;
        [UI] private Gtk.Entry _eFilt = null;

        [UI] private Gtk.Box _clbox= null;
        private CustomTreeView _tv = new CustomTreeView();

        private int codselected;

        Gtk.TreeModelFilter filter;

        public WMain() : this(new Builder("WMain.glade")) { }

        private WMain(Builder builder) : base(builder.GetRawOwnedObject("WMain"))
        {
            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            _mSobre.ButtonReleaseEvent += btAbout_Clicked;
            _btadd.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => (new WCad()).Init());
            _btedit.ButtonReleaseEvent += new ButtonReleaseEventHandler((object sender, ButtonReleaseEventArgs a) => {
                if (codselected<1) {utils.msgbox("Selecione um registro para editar!"); return;}
                (new WCad()).Init(codselected);
            });
            _btrmv.ButtonReleaseEvent += btRmv_Clicked;            
            _tv.CursorChanged += showselected;

            _eFilt.Changed += eFiltChanged;

            _tv.AddCol("Cod").MinWidth = 60;    
            _tv.AddCol("Titulo").MinWidth = 150;
            _tv.AddCol("Gênero").MinWidth = 80;
            _tv.AddCol("Ano").MinWidth = 80;
            _tv.AddCol("Descrição").MinWidth = 200;
            _clbox.PackStart(_tv, true, true, 0);
            _tv.Show();

            this.LoadData();
            this.SetSizeRequest(800, 600);
        }

        private void LoadData() {
            filter = new Gtk.TreeModelFilter (dados.modelStore, null);
            filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
            _tv.Model = filter; // dataStore;

            dados.InsertSerie(new Serie("Friends", GeneroEnum.Comedia,1995, "Serie da vida cotidiana de amigos"));
            dados.InsertSerie(new Serie("The Office", GeneroEnum.Comedia, 2005, "Serie da vida cotidiana no trabalho"));        
        }

        private bool FilterTree (Gtk.ITreeModel model, Gtk.TreeIter iter) {
            if ((bool)model.GetValue(iter, 5) == true) return false;
            if (_eFilt.Text == "") return true;

            object vlr = model.GetValue(iter, 1);
            if (vlr==null) return false;
            string Tit = vlr.ToString().ToLower();        
 
            if (Tit.IndexOf(_eFilt.Text.ToLower()) > -1)
                return true;
            else
                return false;
        }

        private void eFiltChanged(object sender, EventArgs a) {
            filter.Refilter();
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void showselected(object sender, EventArgs a)
        {
            _lbsel.Text = _tv.Selection.GetSelected(out ITreeModel _, out TreeIter it) ? _tv.Model.GetValue(it, 1).ToString() : "";
            codselected = _tv.Selection.GetSelected(out ITreeModel _, out it) ? (int)_tv.Model.GetValue(it, 0) : 0;
        }

        private void btRmv_Clicked(object sender, EventArgs a)
        {
            if (codselected<1) {utils.msgbox("Selecione um registro para remover!"); return;}
            Serie s = dados.GetSerie(codselected);
            s.Excluir();

            dados.updateStore(codselected, new System.Collections.Generic.Dictionary<int, object>() { 
                {5, s.Excluido},
            });                    
        }
        private void btAbout_Clicked(object sender, EventArgs a)
        {
            (new WSobre()).Show();                    
        }
    }
}
