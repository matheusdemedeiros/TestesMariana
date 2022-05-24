using FluentValidation;
using TesteMariana.infra.DataBase.Compartilhado;
using TestesMariana.Dominio.ModuloDisciplina;

namespace TesteMariana.infra.DataBase.ModuloDisciplina
{
    public class RepositorioDisciplinaDBFilha : RepositorioBDBase<Disciplina>, IRepositorioDisciplina
    {
        protected override AbstractValidator<Disciplina> ObterValidador()
        {
            return new ValidadorDisciplina();
        }

        protected override string SQLInsercao()
        {
            return @"INSERT INTO [TBDISCIPLINA] 
                (
                    [NOME]
	            )
	            VALUES
                (
                    @NOME
                );SELECT SCOPE_IDENTITY();";
        }

        protected override string SQLEdicao()
        {
            return @"UPDATE[TBDISCIPLINA]
		        SET
			        [NOME] = @NOME
		        WHERE
			        [NUMERO] = @NUMERO";
        }

        protected override string SQLExclusao()
        {
            return @"DELETE FROM [TBDISCIPLINA]
		        WHERE
			        [NUMERO] = @NUMERO";
        }

        protected override string SQLSelecionarTodosOsRegistros()
        {
            return @"SELECT 
		            [NUMERO], 
		            [NOME] 
	            FROM 
		            [TBDISCIPLINA]";
        }

        protected override string SQLSelecionarRegistroPorNumero()
        {
            return @"SELECT 
		            [NUMERO], 
		            [NOME]
	            FROM 
		            [TBDISCIPLINA]
		        WHERE
                    [NUMERO] = @NUMERO";
        }
    }
}
