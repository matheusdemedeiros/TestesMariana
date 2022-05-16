using FluentValidation.Results;
using System;
using System.Drawing;
using System.Windows.Forms;
using TestesMariana.Dominio.ModuloDisciplina;
using TestesMariana.WinApp.Compartilhado;

namespace TestesMariana.WinApp.ModuloDisciplina
{
    public partial class TelaCadastroDisciplinasForm : Form
    {
        private Disciplina disciplina;

        public TelaCadastroDisciplinasForm()
        {
            InitializeComponent();
        }

        public Disciplina Disciplina
        {
            get
            {
                return disciplina;
            }
            set
            {
                disciplina = value;
                txtNumero.Text = disciplina.Numero.ToString();
                txtNomeDisciplina.Text = disciplina.Nome;
            }
        }

        public Func<Disciplina, ValidationResult> GravarRegistro { get; set; }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            TelaPrincipalForm.Instancia.AtualizarRodape("", TipoMensagemRodape.VAZIO);
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            disciplina.Nome = txtNomeDisciplina.Text;

            var resultadoValidacao = GravarRegistro(disciplina);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro, TipoMensagemRodape.ERRO);

                DialogResult = DialogResult.None;
            }
        }

        private void TelaCadastroDisciplinasForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("", TipoMensagemRodape.VAZIO);
        }

        private void TelaCadastroDisciplinasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("", TipoMensagemRodape.VAZIO);
        }

        private void LimparCampos()
        {
            this.txtNomeDisciplina.Clear();
        }

    }
}
