using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using TestesMariana.Dominio.Compartilhado;

namespace TesteMariana.infra.DataBase.Compartilhado
{
    public abstract class RepositorioBDBase<T> where T : EntidadeBase<T>
    {
        protected const string connectionString =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=TestesMariana;" +
               "Integrated Security=True;" +
               "Pooling=False";

        protected SqlConnection conexaoComBanco;

        protected SqlCommand comandoAhSerExecutado;

        protected SqlDataReader leitorEntidade;

        protected abstract string SQLInsercao();
        protected abstract string SQLEdicao();
        protected abstract string SQLExclusao();
        protected abstract string SQLSelecionarRegistroPorNumero();
        protected abstract string SQLSelecionarTodosOsRegistros();
        protected abstract AbstractValidator<T> ObterValidador();

        public RepositorioBDBase()
        {
            this.conexaoComBanco = new SqlConnection(connectionString);
        }

        public virtual ValidationResult Inserir(T novoRegistro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            comandoAhSerExecutado = new SqlCommand(SQLInsercao(), conexaoComBanco);

            ConfigurarParametrosEntidade(novoRegistro, comandoAhSerExecutado);

            conexaoComBanco.Open();

            var id = comandoAhSerExecutado.ExecuteScalar();

            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public virtual ValidationResult Editar(T registro)
        {
            var validador = ObterValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            comandoAhSerExecutado = new SqlCommand(SQLEdicao(), conexaoComBanco);

            ConfigurarParametrosEntidade(registro, comandoAhSerExecutado);

            conexaoComBanco.Open();

            comandoAhSerExecutado.ExecuteNonQuery();
            
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public virtual ValidationResult Excluir(T registro)
        {
            comandoAhSerExecutado = new SqlCommand(SQLExclusao(), conexaoComBanco);

            comandoAhSerExecutado.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoAhSerExecutado.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível excluir o registro do DB!"));

            conexaoComBanco.Close();

            return resultadoValidacao;

        }

        public virtual T SelecionarPorNumero(int numero)
        {
            comandoAhSerExecutado = new SqlCommand(SQLSelecionarRegistroPorNumero(), conexaoComBanco);

            comandoAhSerExecutado.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            
            leitorEntidade = comandoAhSerExecutado.ExecuteReader();

            T entidade = null;

            if (leitorEntidade.Read())
                entidade = leitorEntidade.MapearObjeto<T>();

            conexaoComBanco.Close();

            return entidade;
        }

        public virtual List<T> SelecionarTodos()
        {
            comandoAhSerExecutado = new SqlCommand(SQLSelecionarTodosOsRegistros(), conexaoComBanco);

            conexaoComBanco.Open();
            
            leitorEntidade = comandoAhSerExecutado.ExecuteReader();

            List<T> entidades = new List<T>();

            while (leitorEntidade.Read())
            {
                T entidade = leitorEntidade.MapearObjeto<T>();

                entidades.Add(entidade);
            }

            conexaoComBanco.Close();

            return entidades;
        }

        protected virtual void ConfigurarParametrosEntidade(T registro, SqlCommand comandoAhSerExecutado)
        {
            PropertyInfo[] props = registro.GetType().GetFilteredProperties();

            foreach (PropertyInfo prop in props)
                comandoAhSerExecutado.Parameters.AddWithValue(prop.Name.ToUpper(), prop.GetValue(registro));

        }
    }
}
