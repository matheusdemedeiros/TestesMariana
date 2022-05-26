namespace TestesMariana.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        public int Numero { get; set; }

        public string Descricao { get; set; }

        public bool Correta { get; set; }
        
        public char Letra { get; set; }

        public Alternativa()
        {
            this.Correta = false;
        }

        public Alternativa(string descricao, bool correta) : this()
        {
            Descricao = descricao;
            Correta = correta;
        }

        public override string ToString()
        {
            if (Correta)
                return $"{Letra}) {Descricao} - (CORRETA);";

            return $"{Letra}) {Descricao};";
        }
    }
}
