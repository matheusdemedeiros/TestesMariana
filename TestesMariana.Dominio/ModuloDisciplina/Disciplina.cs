using System;
using TestesMariana.Dominio.Compartilhado;

namespace TestesMariana.Dominio.ModuloDisciplina
{

    [Serializable]
    public class Disciplina : EntidadeBase<Disciplina>
    {
        public Disciplina()
        {

        }

        public Disciplina(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

        public bool PodeExcluir => QtdMateriasRelacionadas == 0 ? true : false;

        public int QtdMateriasRelacionadas = 0;
        
        public void IncrementarQtdMateriasRelacionadas()
        {
            QtdMateriasRelacionadas++;
        }

        public void DecrementarQtdMateriasRelacionadas()
        {
            QtdMateriasRelacionadas--;
        }

        public override void Atualizar(Disciplina registro)
        {
            this.Nome = registro.Nome;
        }

        public override string ToString()
        {
            return $"Número: {Numero} - Nome: {Nome}";
        }

        public Disciplina Clone()
        {
            return MemberwiseClone() as Disciplina;
        }
    }
}
