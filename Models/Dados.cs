using System;
using System.Collections.Generic;
using Gtk;

namespace dio_gtksharp_series
{
    public static class dados {
        private static int codctd = 0;
        public static List<Serie> SerieRepo = new List<Serie>(); 
        public static Gtk.ListStore modelStore = new Gtk.ListStore(typeof (int),
                                                        typeof (string),
                                                        typeof (string),
                                                        typeof (int),
                                                        typeof (string),
                                                        typeof (bool));
            

        public static void InsertSerie(Serie sr) {
            sr.setId = codctd+=1;
            SerieRepo.Add(sr);
            modelStore.AppendValues(sr.getId, sr.Titulo, sr.Genero.ToString(), sr.Ano, sr.Descricao);            
        }

        public static void updateStore(int cod, Dictionary<int, object> fields) {
            dados.modelStore.Foreach(new TreeModelForeachFunc((ITreeModel model, TreePath path, TreeIter iter) => {
                if ((int)dados.modelStore.GetValue(iter, 0) == cod) {                    
                    foreach(var f in fields) {
                        dados.modelStore.SetValue(iter, f.Key, f.Value);
                    }
                    return true;
                }
                return false;
            }));
        }

        public static Serie GetSerie(int cod) {
            return SerieRepo.Find(s => s.Id == cod);
        }
    }

}