using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestesMariana.Dominio.ModuloDisciplina;

namespace TesteMariana.infra.DataBase.ModuloDisciplina
{
    public class RepositorioDisciplinaDB : IRepositorioDisciplina
    {

        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=TestesMariana;" +
               "Integrated Security=True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBDISCIPLINA] 
                (
                    [NOME]
	            )
	            VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";

        private const string sqlInserirsSemDuplicatas =
            @"IF NOT EXISTS
                (
                    SELECT * FROM [TBDISCIPLINA]
                    WHERE
                        [NOME] = @NOME
                )
            BEGIN
                INSERT INTO [TBDISCIPLINA]
                (
                   [NOME]
                )
            VALUES
                (
                    @NOME
                )
            END
            SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
            @"UPDATE[TBDISCIPLINA]
		        SET
			        [NOME] = @NOME
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBDISCIPLINA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [NOME] 
	            FROM 
		            [TBDISCIPLINA]";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            [NUMERO], 
		            [NOME]
	            FROM 
		            [TBDISCIPLINA]
		        WHERE
                    [NUMERO] = @NUMERO";
        #endregion

        public ValidationResult Inserir(Disciplina novoRegistro)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserirsSemDuplicatas, conexaoComBanco);

            ConfigurarParametrosDisciplina(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            if (id != DBNull.Value)
                novoRegistro.Numero = Convert.ToInt32(id);
            else
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma disciplina com este nome cadastrada no DataBase!"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Disciplina registro)
        {
            var validador = new ValidadorDisciplina();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosDisciplina(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Disciplina registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a disiciplina!"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Disciplina SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            Disciplina discilina = null;
            if (leitorDisciplina.Read())
                discilina = ConverterParaDisciplina(leitorDisciplina);

            conexaoComBanco.Close();

            return discilina;
        }

        public List<Disciplina> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorDisciplina = comandoSelecao.ExecuteReader();

            List<Disciplina> disiciplinas = new List<Disciplina>();

            while (leitorDisciplina.Read())
            {
                Disciplina disciplina = ConverterParaDisciplina(leitorDisciplina);

                disiciplinas.Add(disciplina);
            }

            conexaoComBanco.Close();

            return disiciplinas;
        }

        private static Disciplina ConverterParaDisciplina(SqlDataReader leitorDisciplina)
        {
            int numero = Convert.ToInt32(leitorDisciplina["NUMERO"]);
            string nome = Convert.ToString(leitorDisciplina["NOME"]);

            var disciplina = new Disciplina
            {
                Numero = numero,
                Nome = nome
            };

            return disciplina;
        }

        private static void ConfigurarParametrosDisciplina(Disciplina novaDisciplina, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaDisciplina.Numero);
            comando.Parameters.AddWithValue("NOME", novaDisciplina.Nome);
        }

    }
}
