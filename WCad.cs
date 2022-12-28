using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using static dio_gtksharp_series.utils;

namespace dio_gtksharp_series
{
    class WCad : Window
    {
        [UI] private Gtk.Label _lbId = null;
        [UI] private Gtk.Entry _eTitle= null;
        [UI] private Gtk.Entry _eAno= null;
        [UI] private Gtk.ComboBoxText _cbtGen = null;
        [UI] private Gtk.TextView _tDescr = null;
        // [UI] private Gtk.RadioButton _rbJur = null;
        [UI] private Gtk.Button _btsv = null;
        public WCad() : this(new Builder("WCad.glade")) {}

        private WCad(Builder builder) : base(builder.GetRawOwnedObject("WCad"))
        {
            builder.Autoconnect(this);

            _btsv.Clicked += Bt_Clicked;
        }

        private int? cod;
        public void Init(int? _cod = null) {
            cod = _cod;
            foreach(var g in Enum.GetValues(typeof(GeneroEnum))) {
                _cbtGen.AppendText(g.ToString());
            }
            this.SetSizeRequest(400, 400);

            if (cod != null) {
                CarregaItem((int)cod);
            }
            this.Show();
        }

        private void CarregaItem(int cod) {
            Serie s = dados.GetSerie(cod);
            _lbId.Text = cod.ToString();
            _eTitle.Text = s.Titulo;
            _cbtGen.Active = ((int)s.Genero) -1;
            _eAno.Text = s.Ano.ToString();
            _tDescr.Buffer.Text = s.Descricao;
        }

        private bool conds(){
            if (_eTitle.Text.Trim().Length<1)  {msgbox("É necessário preencher o título!", _Win: this); _eTitle.GrabFocus(); return false;}
            if (!double.TryParse(_eAno.Text, out var _)) {msgbox("Valor inválido!", _Win: this); _eAno.GrabFocus(); return false;}
            if (_cbtGen.Active<0) {msgbox("Valor inválido, indique o gênero!", _Win: this); _cbtGen.GrabFocus(); return false;}
            return true;
        }

        private string getTvText(Gtk.TextView tv) {
            tv.Buffer.GetBounds(out TextIter _start, out TextIter _end);
            return tv.Buffer.GetText(_start,_end, true);
        }
        private void Bt_Clicked(object sender, EventArgs a) {
            if (!conds()) return;
            if (cod == null) Inserir(); else Alterar();

            //utils.msgbox(getTview(_tDescr).ToString());
            this.Destroy();
        }

        private void Inserir() {
            var sr = new Serie(_eTitle.Text,
                    (GeneroEnum)Enum.ToObject(typeof(GeneroEnum), _cbtGen.Active +1),
                    int.Parse(_eAno.Text),
                    _tDescr.Buffer.Text); //getTvText(_tDescr))
            dados.InsertSerie(sr);
        }

        private void Alterar() {
            Serie s = dados.GetSerie((int)this.cod);
            s.Titulo = _eTitle.Text;
            s.Genero = (GeneroEnum) _cbtGen.Active + 1; // Forma mais simples de converter numero idx em Enum
            s.Ano = int.Parse(_eAno.Text);
            s.Descricao = _tDescr.Buffer.Text;

            dados.updateStore((int)this.cod, new System.Collections.Generic.Dictionary<int, object>() {
                {1, s.Titulo},
                {2, _cbtGen.ActiveText},
                {3, s.Ano},
                {4, s.Descricao},
            });
        }
    }
}