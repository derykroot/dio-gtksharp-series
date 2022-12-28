using System;

namespace dio_gtksharp_series
{
    public class Serie : EntidadeBase
    {
        // Atributosp
		private string _Titulo;
		private GeneroEnum _Genero;
		private string _Descricao;
		private int _Ano;
        public bool Excluido {get; set;}

        // Métodos
		public Serie(string titulo, GeneroEnum genero, int ano, string descricao)
		{
			this._Genero = genero;
			this._Titulo = titulo;			
			this._Ano = ano;
			this._Descricao = descricao;
            this.Excluido = false;
		}

		public int setId { set { this.Id = value; } }
		public int getId { get { return this.Id;} }

		public string Titulo { get {return this._Titulo;} set {this._Titulo = value;}}
		public GeneroEnum Genero { get {return this._Genero;} set {this._Genero = value;}}
		public string Descricao { get {return this._Descricao;} set {this._Descricao = value;}}
		public int Ano { get {return this._Ano;} set {this._Ano = value;}}
        public void Excluir() {
            this.Excluido = true;
        }
    }
}