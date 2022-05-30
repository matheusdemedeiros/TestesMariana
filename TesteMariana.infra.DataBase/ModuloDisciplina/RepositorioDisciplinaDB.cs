using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TestesMariana.Dominio.Compartilhado;
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

        #region SQL QUERIES

        private const string sqlInserir =
            @"INSERT INTO [TBDISCIPLINA] 
                (
                    [NOME]
	            )
	            VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";

        private const string sqlInserirSemDuplicatas =
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

        private const string sqlObterNomesCadastrados =
            @"SELECT [NOME] FROM TBDISCIPLINA;";

        private const string sqlSelecionarPeloNome =
            @"SELECT [NUMERO], [NOME] FROM TBDISCIPLINA WHERE NOME = @NOME;";

        private const string sqlVerificaRelacaoComMateria =
            @"SELECT COUNT (*) FROM TBMATERIA WHERE DISCIPLINA_NUMERO = @NUMERO";

        private const string sqlVerificaRelacaoComQuestao =
           @"SELECT COUNT (*) FROM TBQUESTAO WHERE DISCIPLINA_NUMERO = @NUMERO";

        private const string sqlVerificaRelacaoComTeste =
           @"SELECT COUNT (*) FROM TBTESTE WHERE DISCIPLINA_NUMERO = @NUMERO";

        #endregion

        #region MÉTODOS PÚBLICOS

        public ValidationResult Inserir(Disciplina novoRegistro)
        {
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosDisciplina(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(Disciplina registro)
        {
            var resultadoValidacao = Validar(registro);

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

            ValidationResult resultadoValidacao = VerificaRelacoesParaExclusao(registro);
            
            if (resultadoValidacao.IsValid)
            {
                conexaoComBanco.Open();

                int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

                if (numeroRegistrosExcluidos == 0)
                    resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover a disiciplina!"));

                conexaoComBanco.Close();
            }
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

        public Disciplina SelecionarPorNome(string nome)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPeloNome, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NOME", nome);

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

        #endregion

        #region MÉTODOS PRIVADOS

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

        private ValidationResult Validar(Disciplina registro)
        {
            var validator = new ValidadorDisciplina();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = ValidarNome(registro);

            return resultadoValidacao;
        }

        private ValidationResult ValidarNome(Disciplina registro)
        {
            bool nomeRegistrado = VerificarSeOhNomeJaEstaRegistrado(registro);

            ValidationResult validacaoDeNome = new ValidationResult();

            if (nomeRegistrado)
            {
                if (registro.Numero == 0)
                    validacaoDeNome.Errors.Add(new ValidationFailure("", "Não foi possível inserir, pois já existe uma disciplina com este nome cadastrada no sistema!"));

                else if (SelecionarPorNome(registro.Nome).Numero != registro.Numero)
                    validacaoDeNome.Errors.Add(new ValidationFailure("", "Não foi possível editar, pois já existe uma disciplina com este nome cadastrada no sistema!"));
            }
            return validacaoDeNome;
        }

        private List<string> ObterNomesCadastrados()
        {
            SqlConnection conexao = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sqlObterNomesCadastrados, conexao);

            conexao.Open();

            SqlDataReader leitorNomes = comando.ExecuteReader();

            List<string> nomesCadastrados = new List<string>();

            while (leitorNomes.Read())
            {
                var nome = Convert.ToString(leitorNomes["NOME"]);

                nomesCadastrados.Add(nome);
            }
            conexao.Close();

            return nomesCadastrados;
        }

        private bool VerificarSeOhNomeJaEstaRegistrado(Disciplina registro)
        {
            return ObterNomesCadastrados().Exists(x => x.SaoIguais(registro.Nome));
        }

        private ValidationResult VerificaRelacoesParaExclusao(Disciplina registro)
        {
            var resultadoValidacao = new ValidationResult();

            resultadoValidacao = VerificaSQLDeExclusao(sqlVerificaRelacaoComTeste, registro,
                "Não é possível excluir, pois a disciplina está associada à algum Teste!");

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = VerificaSQLDeExclusao(sqlVerificaRelacaoComQuestao, registro,
                "Não é possível excluir, pois a disciplina está associada à alguma Questão!");

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            resultadoValidacao = VerificaSQLDeExclusao(sqlVerificaRelacaoComMateria, registro,
                            "Não é possível excluir, pois a disciplina está associada à alguma Matéria!");

            return resultadoValidacao;
        }

        private ValidationResult VerificaSQLDeExclusao(string sql, Disciplina registro, string mensagem)
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
