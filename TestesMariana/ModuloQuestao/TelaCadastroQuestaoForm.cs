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
using TestesMariana.Dominio.ModuloQuestao;

namespace TestesMariana.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestaoForm : Form
    {
        public TelaCadastroQuestaoForm()
        {
            InitializeComponent();
        }

        public Questao Questao { get;  set; }
        public Func<Questao, ValidationResult> GravarRegistro { get; internal set; }
    }
}
