using FluentValidation;
using TesteMariana.infra.DataBase.Compartilhado;
using TestesMariana.Dominio.ModuloMateria;

namespace TesteMariana.infra.DataBase.ModuloMateria
{
    public class RepositorioMateriaDBFilha : RepositorioBDBase<Materia>, IRepositorioMateria
    {
        protected override AbstractValidator<Materia> ObterValidador()
        {
            return new ValidadorMateria();
        }

        protected override string SQLInsercao()
        {
            return @"INSERT INTO [TBMATERIA] 
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
        }

        protected override string SQLEdicao()
        {
            return @"UPDATE[TBMATERIA]
		        SET
			        [TITULO],
                    [SERIE],
                    [DISCIPLINA.NUMERO]
		        WHERE
			        [NUMERO] = @NUMERO";
        }

        protected override string SQLExclusao()
        {
            return @"DELETE FROM [TBMATERIA]
		        WHERE
			        [NUMERO] = @NUMERO";
        }

        protected override string SQLSelecionarRegistroPorNumero()
        {
            return @"SELECT 
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
        }

        protected override string SQLSelecionarTodosOsRegistros()
        {
            return @"SELECT 
	            MT.NUMERO,
	            MT.TITULO,
	            MT.SERIE,

	            D.NUMERO AS DISCIPLINA_NUMERO,
	            D.NOME AS DISCIPLINA_NOME

            FROM
	            TBMATERIA AS MT INNER JOIN TBDISCIPLINA AS D ON
	            MT.DISCIPLINA_NUMERO = D.NUMERO";
        }
    }
}
