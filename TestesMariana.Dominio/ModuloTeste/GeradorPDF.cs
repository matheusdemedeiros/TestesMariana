using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace TestesMariana.Dominio.ModuloTeste
{
    public class GeradorPDF
    {
        private Teste teste;
        private string arquivo;

        public GeradorPDF(Teste teste, string arquivo)
        {
            this.teste = teste;
            this.arquivo = arquivo;
        }

        public void GerarPDF()
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50, 50, 50, 50);
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(arquivo, FileMode.Create));
                doc.Open();


                Paragraph titulo = new Paragraph();
                titulo.Font.Size = 18;
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.Add($"{teste.Titulo}\n\n\n");

                doc.Add(titulo);


                Paragraph disciplina = new Paragraph();
                disciplina.Alignment = Element.ALIGN_JUSTIFIED;
                disciplina.Add($"Disciplina: {teste.Disciplina.Nome}\n");

                doc.Add(disciplina);

                if (teste.Materia != null)
                {
                    Paragraph materia = new Paragraph();
                    materia.Alignment = Element.ALIGN_JUSTIFIED;
                    materia.Add($"Matéria: {teste.Materia.Titulo}\n\n");
                    doc.Add(materia);
                }

                for (int i = 0; i < teste.Questoes.Count; i++)
                {
                    Paragraph questao = new Paragraph();
                    questao.Alignment = Element.ALIGN_JUSTIFIED;
                    questao.Add($"{i + 1} - {teste.Questoes[i].Enunciado}\n");

                    foreach (var alternativa in teste.Questoes[i].Alternativas)
                        questao.Add($"{alternativa.Letra}) {alternativa.Descricao}\n");

                    questao.Add("\n");
                    doc.Add(questao);

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                doc.Close();
            }

        }


    }
}
