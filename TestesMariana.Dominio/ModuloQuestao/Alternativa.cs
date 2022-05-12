using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public class Alternativa
    {
        public string Descricao { get; set; }

        public bool Correta { get; set; }

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
            return $"Descrição: {Descricao}";
        }
    }
}
