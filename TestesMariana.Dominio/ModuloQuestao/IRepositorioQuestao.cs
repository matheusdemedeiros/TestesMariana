using System;
using System.Collections.Generic;
using TestesMariana.Dominio.Compartilhado;

namespace TestesMariana.Dominio.ModuloQuestao
{
    public interface IRepositorioQuestao : IRepositorio<Questao>
    {
        List<Questao> Filtrar(Predicate<Questao> condicao);
    }
}
