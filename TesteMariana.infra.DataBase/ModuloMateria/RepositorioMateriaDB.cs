using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;

namespace TesteMariana.infra.DataBase.ModuloMateria
{
    public class RepositorioMateriaDB : IRepositorioMateria
    {

        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=TestesMariana;" +
              "Integrated Security=True;" +
              "Pooling=False";

        #region Sql Queries

        private const string sqlInserir =
            @"INSERT INTO [TBMATERIA] 
                (
                    [TITULO],
                    [SERIE],
                    [DISCIPLINA_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @SERIE,
                    @DISCIPLINA_NUMERO

                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
                @"UPDATE[TBMATERIA]
		        SET
			        [TITULO] = @TITULO,
                    [SERIE] = @SERIE,
                    [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBMATERIA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
	            MT.NUMERO,
	            MT.TITULO,
	            MT.SERIE,

	            D.NUMERO AS DISCIPLINA_NUMERO,
	            D.NOME AS DISCIPLINA_NOME

            FROM
	            TBMATERIA AS MT INNER JOIN TBDISCIPLINA AS D ON
	            MT.DISCIPLINA_NUMERO = D.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
                    MT.[NUMERO],		            
                    MT.[TITULO],
                    MT.[SERIE],
                    
	                D.NUMERO AS DISCIPLINA_NUMERO,
	                D.NOME AS DISCIPLINA_NOME

                FROM
	                TBMATERIA AS MT INNER JOIN TBDISCIPLINA AS D ON
	                MT.DISCIPLINA_NUMERO = D.NUMERO
                WHERE
                    MT.[NUMERO] = @NUMERO";

        #endregion

        public ValidationResult Inserir(Materia novoRegistro)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMateria(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            //Tentando inserir registros sem duplicatas

            //if (id != DBNull.Value)
            //    novoRegistro.Numero = Convert.ToInt32(id);
            //else
            //    resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma Materia com este nome cadastrada no DataBase!"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Materia registro)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMateria(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Materia registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a matéria!"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Materia SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            Materia materia = null;
            if (leitorMateria.Read())
                materia = ConverterParaMateria(leitorMateria);

            conexaoComBanco.Close();

            return materia;
        }

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            List<Materia> disiciplinas = new List<Materia>();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                disiciplinas.Add(materia);
            }

            conexaoComBanco.Close();

            return disiciplinas;
        }

        private static Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["NUMERO"]);
            string titulo = Convert.ToString(leitorMateria["TITULO"]);
            string serie = Convert.ToString(leitorMateria["SERIE"]);

            int numeroDisciplina = Convert.ToInt32(leitorMateria["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorMateria["DISCIPLINA_NOME"]);

            var materia = new Materia
            {
                Numero = numero,
                Titulo = titulo,
                Serie = serie,
                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Nome = nomeDisciplina
                }
            };

            return materia;
        }

        private static void ConfigurarParametrosMateria(Materia novaMateria, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaMateria.Numero);
            comando.Parameters.AddWithValue("TITULO", novaMateria.Titulo);
            comando.Parameters.AddWithValue("SERIE", novaMateria.Serie);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", novaMateria.Disciplina.Numero);

        }
    }
}
