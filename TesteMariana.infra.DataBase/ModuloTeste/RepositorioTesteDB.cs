using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;
using TestesMariana.Dominio.ModuloTeste;

namespace TesteMariana.infra.DataBase.ModuloTeste
{
    public class RepositorioTesteDB : IRepositorioTeste
    {

        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=TestesMariana;" +
              "Integrated Security=True;" +
              "Pooling=False";

        #region SQL QUERIES

        private const string sqlInserir =
            @"INSERT INTO [TBTESTE]
                 (
				    [TITULO],
				    [SERIE],
				    [DATACRIACAO],
				    [DISCIPLINA_NUMERO],
				    [MATERIA_NUMERO]
			    )
            VALUES
                (
				    @TITULO,
				    @SERIE,
				    @DATACRIACAO,
				    @DISCIPLINA_NUMERO,
				    @MATERIA_NUMERO
			); SELECT SCOPE_IDENTITY();";

        //private const string sqlEditar =
        //        @"UPDATE[TBMATERIA]
        //  SET
        //   [TITULO] = @TITULO,
        //            [SERIE] = @SERIE,
        //            [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO
        //  WHERE
        //   [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBTESTE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT
                T.[NUMERO],
	            T.[TITULO],
	            T.[SERIE],
	            T.[DATACRIACAO],

	            D.[NUMERO] AS DISCIPLINA_NUMERO,
                D.[NOME] AS DISCIPLINA_NOME,

	            MT.[NUMERO] AS MATERIA_NUMERO,
	            MT.[TITULO] AS MATERIA_TITULO,
	            MT.[SERIE] AS MATERIA_SERIE
            FROM
                TBTESTE AS T LEFT JOIN TBDISCIPLINA AS D
            ON
                T.DISCIPLINA_NUMERO = D.NUMERO
                LEFT JOIN TBMATERIA AS MT
            ON
                T.MATERIA_NUMERO = MT.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT
                T.[NUMERO],
	            T.[TITULO],
	            T.[SERIE],
	            T.[DATACRIACAO],

	            D.[NUMERO] AS DISCIPLINA_NUMERO,
                D.[NOME] AS DISCIPLINA_NOME,

	            MT.[NUMERO] AS MATERIA_NUMERO,
	            MT.[TITULO] AS MATERIA_TITULO,
	            MT.[SERIE] AS MATERIA_SERIE
            FROM
                TBTESTE AS T LEFT JOIN TBDISCIPLINA AS D
            ON
                T.DISCIPLINA_NUMERO = D.NUMERO
                LEFT JOIN TBMATERIA AS MT
            ON
                T.MATERIA_NUMERO = MT.NUMERO
            WHERE
                T.NUMERO = @NUMERO";

        private const string sqlRemoverQuestaoDoTeste =
            @" DELETE FROM
                TBTESTE_TBQUESTAO
            WHERE
                [TESTE_NUMERO] = @TESTE_NUMERO";

        private const string sqlAdicionarQuestaoNoTeste =
            @"INSERT INTO[TBTESTE_TBQUESTAO]
                (
                    [TESTE_NUMERO],
                    [QUESTAO_NUMERO]
                )
                VALUES
                (
                    @TESTE_NUMERO,
                    @QUESTAO_NUMERO
                )";

        private const string sqlSelecionarAlternativasDaQuestao =
            @"SELECT 
                [NUMERO],
                [DESCRICAO],
                [CORRETA],
                [LETRA],
                [QUESTAO_NUMERO]
            FROM
                [TBALTERNATIVA]
            
            WHERE 
                [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        private const string sqlSelecionarQuestoesDoTeste =
            @"SELECT 
	                QT.[NUMERO], 
	                QT.[ENUNCIADO],
	                QT.[MATERIA_NUMERO],
	                QT.[DISCIPLINA_NUMERO],
	                QT.[SERIE],

	                D.[NOME] AS DISCIPLINA_NOME,
                    
                    MT.[NUMERO] AS MATERIA_NUMERO,
                    MT.[TITULO] AS MATERIA_TITULO,
                    MT.[SERIE] AS MATERIA_SERIE
            FROM 
	                TBQUESTAO AS QT INNER JOIN TBTESTE_TBQUESTAO AS TQ 
                ON 
	                QT.NUMERO = TQ.QUESTAO_NUMERO
                    INNER JOIN TBDISCIPLINA AS D
                ON
                    QT.[DISCIPLINA_NUMERO] = D.NUMERO
                    INNER JOIN TBMATERIA AS MT
                ON
                    MT.[NUMERO] = QT.MATERIA_NUMERO
                WHERE 
	                TQ.TESTE_NUMERO = @NUMERO";

        #endregion

        public ValidationResult Inserir(Teste novoRegistro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosTeste(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Numero = Convert.ToInt32(id);

            foreach (var questao in novoRegistro.Questoes)
                AdicionarQuestao(novoRegistro, questao);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Teste registro)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Teste registro)
        {
            RemoverQuestao(registro);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o Teste!!"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Teste SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            Teste teste = null;

            if (leitorTeste.Read())
                teste = ConverterParaTeste(leitorTeste);

            conexaoComBanco.Close();

            CarregarQuestoesDoTeste(teste);

            return teste;
        }

        public List<Teste> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();

            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            List<Teste> testes = new List<Teste>();

            while (leitorTeste.Read())
            {
                Teste teste = ConverterParaTeste(leitorTeste);

                CarregarQuestoesDoTeste(teste);

                testes.Add(teste);
            }

            conexaoComBanco.Close();

            return testes;
        }

        #region MÉTODOS PRIVADOS

        #region MÉTODOS DOS TESTES

        private void ConfigurarParametrosTeste(Teste teste, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("TITULO", teste.Titulo);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", teste.Disciplina.Numero);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", teste.Materia != null ? teste.Materia.Numero : DBNull.Value);
            comando.Parameters.AddWithValue("SERIE", teste.Serie);
            comando.Parameters.AddWithValue("DATACRIACAO", teste.DataCriacao);
        }

        private Teste ConverterParaTeste(SqlDataReader leitorTeste)
        {
            var numeroTeste = Convert.ToInt32(leitorTeste["NUMERO"]);
            var titulo = Convert.ToString(leitorTeste["TITULO"]);
            var serie = Convert.ToString(leitorTeste["SERIE"]);
            var dataCriacao = Convert.ToDateTime(leitorTeste["DATACRIACAO"]);

            var numeroDisciplina = Convert.ToInt32(leitorTeste["DISCIPLINA_NUMERO"]);
            var nomeDisciplina = Convert.ToString(leitorTeste["DISCIPLINA_NOME"]);

            Disciplina disciplina = new Disciplina();
            disciplina.Numero = numeroDisciplina;
            disciplina.Nome = nomeDisciplina;

            var teste = new Teste
            {
                Numero = numeroTeste,
                Titulo = titulo,
                Serie = serie,
                DataCriacao = dataCriacao,
                Disciplina = disciplina
            };

            if (leitorTeste["MATERIA_NUMERO"] != DBNull.Value)
            {
                teste.Materia = new Materia();
                teste.Materia.Numero = Convert.ToInt32(leitorTeste["MATERIA_NUMERO"]);
                teste.Materia.Disciplina = disciplina;
                teste.Materia.Titulo = Convert.ToString(leitorTeste["MATERIA_TITULO"]);
                teste.Materia.Serie = Convert.ToString(leitorTeste["MATERIA_SERIE"]);
            }

            return teste;
        }

        #endregion

        #region MÉTODOS DAS QUESTOES

        private void CarregarQuestoesDoTeste(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestoesDoTeste, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", teste.Numero);

            conexaoComBanco.Open();

            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);

                CarregarAlternativas(questao);

                teste.Questoes.Add(questao);
            }

            conexaoComBanco.Close();
        }

        private void AdicionarQuestao(Teste teste, Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlAdicionarQuestaoNoTeste, conexaoComBanco);

            comandoInsercao.Parameters.AddWithValue("TESTE_NUMERO", teste.Numero);
            comandoInsercao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            conexaoComBanco.Open();
            comandoInsercao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        private void RemoverQuestao(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlRemoverQuestaoDoTeste, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("TESTE_NUMERO", teste.Numero);

            conexaoComBanco.Open();
            comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();
        }

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            var numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            var enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);

            var numeroDisciplina = Convert.ToInt32(leitorQuestao["DISCIPLINA_NUMERO"]);
            var nomeDisciplina = Convert.ToString(leitorQuestao["DISCIPLINA_NOME"]);

            var numeroMateria = Convert.ToInt32(leitorQuestao["MATERIA_NUMERO"]);
            var tituloMateria = Convert.ToString(leitorQuestao["MATERIA_TITULO"]);
            var serieMateria = Convert.ToString(leitorQuestao["MATERIA_SERIE"]);

            Disciplina disciplina = new Disciplina();
            disciplina.Numero = numeroDisciplina;
            disciplina.Nome = nomeDisciplina;

            Materia materia = new Materia();
            materia.Numero = numeroMateria;
            materia.Titulo = tituloMateria;
            materia.Serie = serieMateria;
            materia.Disciplina = disciplina;

            var questao = new Questao
            {
                Numero = numero,
                Enunciado = enunciado,
                Materia = materia
            };

            return questao;
        }

        #endregion

        #region MÉTODOS DAS ALTERNATIVAS

        private void CarregarAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarAlternativasDaQuestao, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            conexaoComBanco.Open();

            SqlDataReader leitorAlternativa = comandoSelecao.ExecuteReader();

            while (leitorAlternativa.Read())
            {
                Alternativa alternativa = ConverterParaAlternativa(leitorAlternativa);

                questao.AdicionarAlternativa(alternativa);
            }

            conexaoComBanco.Close();
        }

        private Alternativa ConverterParaAlternativa(SqlDataReader leitorAlternativa)
        {
            var numero = Convert.ToInt32(leitorAlternativa["NUMERO"]);
            var descricao = Convert.ToString(leitorAlternativa["DESCRICAO"]);
            var correta = Convert.ToBoolean(leitorAlternativa["CORRETA"]);
            var letra = Convert.ToChar(leitorAlternativa["LETRA"]);

            var alternativa = new Alternativa
            {
                Numero = numero,
                Descricao = descricao,
                Correta = correta,
                Letra = letra
            };

            return alternativa;
        }

        #endregion

        #endregion
    }
}
