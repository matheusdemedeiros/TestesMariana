using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloMateria;
using TestesMariana.Dominio.ModuloQuestao;

namespace TestesMariana.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestaoForm : Form
    {
        private Questao questao;

        public TelaCadastroQuestaoForm()
        {
            InitializeComponent();
        }

        public Questao Questao
        {
            get
            {
                return questao;
            }
            set
            {
                questao = value;
                questao.Enunciado = richTextBoxEnunciado.Text;
                questao.Materia = (Materia)comboBoxMateria.SelectedItem;
                
            }
        }
        public Func<Questao, ValidationResult> GravarRegistro { get; set; }
        
        private void btnAdicionarAlternativa_Click(object sender, EventArgs e)
        {
            AdicionarAlternativa();
        }

        private void AdicionarAlternativa()
        {
            Alternativa alternativa = new Alternativa();
            
            alternativa.Descricao = txtAlternativaDescricao.Text;

            MarcarCorreta(alternativa);

            

        }

        public void MarcarCorreta(Alternativa alternativa)
        {
            if(checkBoxAlternativaCorreta.Checked)
                alternativa.Correta = true;
        }

    }
}
