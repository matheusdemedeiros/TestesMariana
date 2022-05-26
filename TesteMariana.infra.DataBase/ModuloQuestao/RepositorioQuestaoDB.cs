﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;

namespace TesteMariana.infra.DataBase.ModuloQuestao
{
    public class RepositorioQuestaoDB : IRepositorioQuestao
    {

        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
             "Initial Catalog=TestesMariana;" +
             "Integrated Security=True;" +
             "Pooling=False";

        #region SQL QUERIES

        private const string sqlInserirQuestao =
            @"INSERT INTO [TBQUESTAO]
                (
                    [ENUNCIADO],
                    [SERIE],
                    [MATERIA_NUMERO],
                    [DISCIPLINA_NUMERO]
                )
            VALUES
                (
                    @ENUNCIADO,
                    @SERIE,
                    @MATERIA_NUMERO,
                    @DISCIPLINA_NUMERO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
            @"UPDATE[TBQUESTAO]
		        SET
			        [ENUNCIADO] = @ENUNCIADO,
                    [SERIE] = @SERIE,
                    [MATERIA_NUMERO] = @MATERIA_NUMERO,
                    [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluirQuestao =
            @"DELETE FROM [TBQUESTAO]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodasAsQuestoes =
            @"SELECT
                QT.NUMERO,
                QT.ENUNCIADO,
                QT.SERIE,
	
                D.NUMERO AS DISCIPLINA_NUMERO,
                D.NOME AS DISCIPLINA_NOME,
                                    
                M.NUMERO AS MATERIA_NUMERO,
                M.TITULO AS MATERIA_TITULO,
                M.SERIE AS MATERIA_SERIE
              FROM
                TBQUESTAO AS QT INNER JOIN TBDISCIPLINA AS D ON
                QT.DISCIPLINA_NUMERO = D.NUMERO
                INNER JOIN TBMATERIA AS M ON
                QT.MATERIA_NUMERO = M.NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT
                QT.NUMERO,
                QT.ENUNCIADO,
                QT.SERIE,
	
                D.NUMERO AS DISCIPLINA_NUMERO,
                D.NOME AS DISCIPLINA_NOME,
                                    
                M.NUMERO AS MATERIA_NUMERO,
                M.TITULO AS MATERIA_TITULO,
                M.SERIE AS MATERIA_SERIE
            FROM
                TBQUESTAO AS QT INNER JOIN TBDISCIPLINA AS D ON
                QT.DISCIPLINA_NUMERO = D.NUMERO
                INNER JOIN TBMATERIA AS M ON
                QT.MATERIA_NUMERO = M.NUMERO
            WHERE
                QT.NUMERO = @NUMERO";

        private const string sqlAdicionarAlternativa =
            @"INSERT INTO [TBALTERNATIVA]
                (
                    [DESCRICAO],
                    [CORRETA],
                    [LETRA],
                    [QUESTAO_NUMERO]
                )
            VALUES
                (
                    @DESCRICAO,
                    @CORRETA,
                    @LETRA,
                    @QUESTAO_NUMERO
                ); SELECT SCOPE_IDENTITY();";

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

        private const string sqlExcluirAlternativas =
            @"DELETE FROM [TBALTERNATIVA]
		        WHERE
			        [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        #endregion

        #region MÉTODOS PÚBLICOS

        public ValidationResult Inserir(Questao novoRegistro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserirQuestao, conexaoComBanco);

            ConfigurarParametrosQuestao(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            AdicionarAlternativas(novoRegistro);

            return resultadoValidacao;
        }

        public ValidationResult Editar(Questao registro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosQuestao(registro, comandoEdicao);

            conexaoComBanco.Open();

            comandoEdicao.ExecuteNonQuery();

            conexaoComBanco.Close();

            ExcluirAlternativas(registro);

            AdicionarAlternativas(registro);

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao registro)
        {
            ExcluirAlternativas(registro);

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirQuestao, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", registro.Numero);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível excluir a questão!"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Questao SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            Questao questao = null;

            if (leitorQuestao.Read())
                questao = ConverterParaQuestao(leitorQuestao);

            conexaoComBanco.Close();

            CarregarAlternativas(questao);

            return questao;
        }

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodasAsQuestoes, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);
                
                CarregarAlternativas(questao);

                questoes.Add(questao);
            }

            conexaoComBanco.Close();

            return questoes;
        }
        
        public void AdicionarAlternativas(Questao questaoSelecionada)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            conexaoComBanco.Open();

            SqlCommand comandoInsercao;

            foreach (var item in questaoSelecionada.Alternativas)
            {
                comandoInsercao = new SqlCommand(sqlAdicionarAlternativa, conexaoComBanco);

                comandoInsercao.Parameters.AddWithValue("QUESTAO_NUMERO", questaoSelecionada.Numero);

                ConfigurarParametrosAlternativa(item, comandoInsercao);

                var id = comandoInsercao.ExecuteScalar();

                item.Numero = Convert.ToInt32(id);
            }

            conexaoComBanco.Close();
        }
        
        public void ExcluirAlternativas(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluirAlternativas, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);

            conexaoComBanco.Open();

            comandoExclusao.ExecuteNonQuery();

            conexaoComBanco.Close();
        }
        
        #endregion

        #region MÉTODOS PRIVADOS

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

        private Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {
            var numero = Convert.ToInt32(leitorQuestao["NUMERO"]);
            var enunciado = Convert.ToString(leitorQuestao["ENUNCIADO"]);
            //var serie = Convert.ToString(leitorQuestao["SERIE"]);

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

        private void ConfigurarParametrosQuestao(Questao novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);
            comando.Parameters.AddWithValue("ENUNCIADO", novoRegistro.Enunciado);
            comando.Parameters.AddWithValue("SERIE", novoRegistro.Serie);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novoRegistro.Materia.Numero);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", novoRegistro.Disciplina.Numero);
        }

        private void ConfigurarParametrosAlternativa(Alternativa novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);
            comando.Parameters.AddWithValue("DESCRICAO", novoRegistro.Descricao);
            comando.Parameters.AddWithValue("CORRETA", novoRegistro.Correta);
            comando.Parameters.AddWithValue("LETRA", novoRegistro.Letra);
        }
        
        #endregion
    }
}
