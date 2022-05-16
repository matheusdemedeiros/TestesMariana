using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
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

        public void GerarPDF(bool gabarito)
        {
            Document doc = CriarDocumento();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(arquivo, FileMode.Create));
                doc.Open();

                doc.Add(GerarTitulo(gabarito));

                doc.Add(GerarDisciplina());

                doc.Add(GerarSerie());

                if (teste.Materia != null)
                    doc.Add(AdicionarMateria());

                foreach (var q in GerarQuestoes(gabarito))
                    doc.Add(q);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                doc.Close();
            }
        }

        private List<Paragraph> GerarQuestoes(bool gabarito)
        {
            List<Paragraph> questoes = new List<Paragraph>();
            Font fontBOLD = FontFactory.GetFont(FontFactory.TIMES_BOLD);

            for (int i = 0; i < teste.Questoes.Count; i++)
            {
                Paragraph questao = new Paragraph();
                questao.Alignment = Element.ALIGN_JUSTIFIED;
                questao.Add($"{i + 1} - {teste.Questoes[i].Enunciado}\n");

                if (gabarito == true)
                {
                    foreach (var alternativa in teste.Questoes[i].Alternativas)
                        if (alternativa.Correta)
                            questao.Add($"{alternativa.Letra}) {alternativa.Descricao}\n");
                }
                else
                {
                    foreach (var alternativa in teste.Questoes[i].Alternativas)
                        questao.Add($"{alternativa.Letra}) {alternativa.Descricao}\n");
                }

                questao.Add("\n");

                questoes.Add(questao);
            }

            return questoes;
        }

        private Paragraph AdicionarMateria()
        {
            Paragraph materia = new Paragraph();
            materia.Alignment = Element.ALIGN_JUSTIFIED;
            materia.Add($"Matéria: {teste.Materia.Titulo}\n\n");
            return materia;
        }

        private Paragraph GerarSerie()
        {
            Paragraph serie = new Paragraph();
            serie.Alignment = Element.ALIGN_JUSTIFIED;
            serie.Add($"Série: {teste.Serie}\n");
            return serie;
        }

        private Paragraph GerarDisciplina()
        {
            Paragraph disciplina = new Paragraph();
            disciplina.Alignment = Element.ALIGN_JUSTIFIED;
            disciplina.Add($"Disciplina: {teste.Disciplina.Nome}\n");
            return disciplina;
        }

        private Paragraph GerarTitulo(bool gabarito)
        {
            Paragraph titulo = new Paragraph();
            titulo.Font.Size = 18;
            titulo.Alignment = Element.ALIGN_CENTER;

            if (gabarito)
                titulo.Add($"{teste.Titulo} - GABARITO\n\n\n");
            else
                titulo.Add($"{teste.Titulo}\n\n\n");

            return titulo;
        }

        private Document CriarDocumento()
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(50, 50, 50, 50);
            return doc;
        }

    }
}
