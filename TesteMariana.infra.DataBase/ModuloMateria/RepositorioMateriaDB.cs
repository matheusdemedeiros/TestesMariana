using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestesMariana.Dominio.Compartilhado;
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

        private const string sqlObterTitulosCadastrados =
            @"SELECT [TITULO] FROM TBMATERIA;";

        private const string sqlSelecionarPeloTitulo =
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
                    MT.[TITULO] = @TITULO";

        private const string sqlVerificaRelacaoComQuestao =
           @"SELECT COUNT (*) FROM TBQUESTAO WHERE MATERIA_NUMERO = @NUMERO";

        private const string sqlVerificaRelacaoComTeste =
           @"SELECT COUNT (*) FROM TBTESTE WHERE MATERIA_NUMERO = @NUMERO";

        #endregion

        #region MÉTODOS PÚBLICOS

        public ValidationResult Inserir(Materia novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

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
            var resultadoValidacao = Validar(registro);

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

            ValidationResult resultadoValidacao = VerificaRelacoesParaExclusao(registro);

            if (resultadoValidacao.IsValid)
            {
                conexaoComBanco.Open();

                int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

                if (numeroRegistrosExcluidos == 0)
                    resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a matéria!"));

                conexaoComBanco.Close();
            }
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

        #endregion

        #region MÉTODOS PRIVADOS

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

        private ValidationResult Validar(Materia registro)
        {
            var validator = new ValidadorMateria();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarTitulo(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarTitulo(Materia registro)
        {
            bool tituloRegistrado = VerificarSeOhTituloJaEstaRegistrado(registro);

            ValidationResult validacaoDeTitulo = new ValidationResult();

            if (tituloRegistrado)
            {
                if (registro.Numero == 0)
                    validacaoDeTitulo.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma matéria com este título cadastrada no sistema!"));

                else if (SelecionarPorTitulo(registro.Titulo).Numero != registro.Numero)
                    validacaoDeTitulo.Errors.Add(new ValidationFailure("", "Não foi possível editar, pois já existe uma matéria com este título cadastrada no sistema!"));
            }
            return validacaoDeTitulo;
        }

        private Materia SelecionarPorTitulo(string titulo)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPeloTitulo, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("TITULO", titulo);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            Materia materia = null;
            if (leitorMateria.Read())
                materia = ConverterParaMateria(leitorMateria);

            conexaoComBanco.Close();

            return materia;
        }

        private List<string> ObterTitulosCadastrados()
        {
            SqlConnection conexao = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sqlObterTitulosCadastrados, conexao);

            conexao.Open();

            SqlDataReader leitorNomes = comando.ExecuteReader();

            List<string> nomesCadastrados = new List<string>();

            while (leitorNomes.Read())
            {
                var nome = Convert.ToString(leitorNomes["TITULO"]);

                nomesCadastrados.Add(nome);
            }
            conexao.Close();

            return nomesCadastrados;
        }

        private bool VerificarSeOhTituloJaEstaRegistrado(Materia registro)
        {
            return ObterTitulosCadastrados().Exists(x => x.SaoIguais(registro.Titulo));
        }

        private ValidationResult VerificaRelacoesParaExclusao(Materia registro)
        {
            var resultadoValidacao = new ValidationResult();

            resultadoValidacao = VerificaSQLDeExclusao(sqlVerificaRelacaoComTeste, registro,
                "Não é possível excluir, pois a matéria está associada à algum Teste!");

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = VerificaSQLDeExclusao(sqlVerificaRelacaoComQuestao, registro,
                "Não é possível excluir, pois a matéria está associada à alguma Questão!");

            return resultadoValidacao;
        }

        private ValidationResult VerificaSQLDeExclusao(string sql, Materia registro, string mensagem)
        {
            var resultadoValidacao = new ValidationResult();

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);

            comando.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();

            var result = comando.ExecuteScalar();

            conexaoComBanco.Close();

            var qtd = Convert.ToInt32(result);

            if (qtd > 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", mensagem));

            return resultadoValidacao;
        }

        #endregion
    }
}
