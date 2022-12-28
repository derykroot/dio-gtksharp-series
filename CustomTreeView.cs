using System;
using GLib;
using Gtk;

namespace dio_gtksharp_series
{
    // Classe herdada de TreeView criada para manipular de forma mais simples e diminuir a poluição de código da janela principal
    class CustomTreeView: TreeView {
        private int ctdcol=-1;
        public TreeViewColumn AddCol(string name, bool iscoinfield = false){
            ctdcol+=1;
            Gtk.TreeViewColumn NewColumn = new Gtk.TreeViewColumn();
            NewColumn.Title = name;

            NewColumn.Clickable = true;
            NewColumn.SortColumnId = ctdcol; // Para não necessitar do primeiro click para começar a ordenar
            NewColumn.Clicked += Bt_Clicked_Order;

            this.AppendColumn(NewColumn);            
            
            Gtk.CellRendererText newCell = new Gtk.CellRendererText();
  
            // Add the cell to the column
            NewColumn.PackStart(newCell, true);
            NewColumn.AddAttribute(newCell, "text", ctdcol);

            // NewColumn.Alignment = 0.5F;

            if (iscoinfield) NewColumn.SetCellDataFunc(newCell, new TreeCellDataFunc(RenderCell));

            return NewColumn;
        }

        private void RenderCell (Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.ITreeModel model, Gtk.TreeIter iter)
        {
            var res = (cell as Gtk.CellRendererText).Text;
            if (res==null) res= "0";
            // utils.msgbox((cell as Gtk.CellRendererText).Text );
            (cell as Gtk.CellRendererText).Text = string.Format("{0:#.00}", decimal.Parse(res));

            cell.SetAlignment(0.95F, 0.5F); // Alinhar para Direitra
        }

        public int getColPosition(TreeViewColumn cl) {
            for (var i = 0; i < this.Columns.Length; i++) {
                if (this.Columns[i] == cl) return i;
            }
            return 0;
        }

        private void Bt_Clicked_Order(object sender, EventArgs a)
        {
            int pos = this.getColPosition((TreeViewColumn)sender);
            this.Columns[pos].SortColumnId = pos;
        }
    }
}