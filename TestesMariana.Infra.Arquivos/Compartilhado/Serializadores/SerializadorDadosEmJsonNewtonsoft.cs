using Newtonsoft.Json;
using System.IO;

namespace TestesMariana.Infra.Arquivos.Compartilhado.Serializadores
{
    public class SerializadorDadosEmJsonNewtonsoft : ISerializador
    {
        private const string pasta = @"C:\temp";

        private const string arquivo = pasta + @"\dadosMariana.json";

        public DataContext CarregarDadosDoArquivo()
        {
            if (Directory.Exists(pasta) == false)
                Directory.CreateDirectory(pasta);

            if (File.Exists(arquivo) == false)
                return new DataContext();

            string arquivoJson = File.ReadAllText(arquivo);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            return JsonConvert.DeserializeObject<DataContext>(arquivoJson, settings);
        }

        public void GravarDadosEmArquivo(DataContext dados)
        {
            if (Directory.Exists(pasta) == false)
                Directory.CreateDirectory(pasta);

            JsonSerializerSettings settings = new JsonSerializerSettings();

            settings.Formatting = Formatting.Indented;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            string arquivoJson = JsonConvert.SerializeObject(dados, settings);

            File.WriteAllText(arquivo, arquivoJson);
        }
    }
}
