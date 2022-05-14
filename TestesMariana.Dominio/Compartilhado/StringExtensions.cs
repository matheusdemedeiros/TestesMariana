
namespace TestesMariana.Dominio.Compartilhado
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove todos os espaços em branco de uma string de origem.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>Retorna uma nova string com o texto da string de origem, porém sem os espaços em branco</returns>
        public static string RemoverEspacosEmBranco(this string valor)
        {
            return valor.Replace(" ", "");
        }

        /// <summary>
        /// Compara duas strings ignorando letras maíusculas/minúsculas e os espaços em branco.
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="stringAhSerComparada"></param>
        /// <returns>Retorna true se o texto for o mesmo e false se for diferente.</returns>
       
        public static bool SaoIguais (this string string1, string stringAhSerComparada)
        {
            var str1SemEspacosVazios = RemoverEspacosEmBranco(string1);
            
            var str2SemEspacosVazios = RemoverEspacosEmBranco(stringAhSerComparada);

            if (str1SemEspacosVazios.ToUpper() == str2SemEspacosVazios.ToUpper())
                return true;
            
            return false;
        }

    }
}
