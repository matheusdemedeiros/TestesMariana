using System;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        #region PROGPS

        public int Numero { get; set; }

        public string Descricao { get; set; }

        public bool Correta { get; set; }
        
        public char Letra { get; set; }

        #endregion

        public Alternativa()
        {
            this.Correta = false;
        }

        #region MÉTODOS PÚBLICOS

        public override string ToString()
        {
            if (Correta)
                return $"{Letra}) {Descricao} - (CORRETA);";

            return $"{Letra}) {Descricao};";
        }

        public override bool Equals(object obj)
        {
            return obj is Alternativa alternativa &&
                   Numero == alternativa.Numero &&
                   Descricao == alternativa.Descricao &&
                   Correta == alternativa.Correta &&
                   Letra == alternativa.Letra;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numero, Descricao, Correta, Letra);
        }
        
        #endregion
    }
}
