using DonJulioSuper.Forms;
using MetroFramework.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;

namespace DonJulioSuper
{
    public partial class FrmPrincipal : MetroFramework.Forms.MetroForm // Cambia la clase base a MetroForm
    {
        private MetroStyleManager metroStyleManager1; // Declara el metroStyleManager1

        public FrmPrincipal()
        {
            InitializeComponent(); // Cargar los componentes del formulario
            this.IsMdiContainer = true; // Habilitar contenedor MDI

            // Inicializar el MetroStyleManager
            metroStyleManager1 = new MetroFramework.Components.MetroStyleManager();
            metroStyleManager1.Owner = this; // Asignar el propietario
            this.StyleManager = metroStyleManager1;

            // Configurar tema y estilo
            metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark; // Tema oscuro
            metroStyleManager1.Style = MetroFramework.MetroColorStyle.Blue; // Color azul

            // Aplicar los estilos al formulario
            this.Theme = metroStyleManager1.Theme;
            this.Style = metroStyleManager1.Style;
        }
        // Método para abrir un formulario evitando duplicados
        private void AbrirFormulario(Form formulario)
        {
            Form existe = Application.OpenForms[formulario.Name];

            if (existe == null) // Si el formulario no está abierto, abrirlo
            {
                formulario.MdiParent = this; // Establecer el formulario principal como contenedor
                formulario.StartPosition = FormStartPosition.CenterScreen;
                formulario.Show();
            }
            else
            {
                existe.BringToFront(); // Si ya está abierto, traerlo al frente
            }
        }

        // EVENTOS DE LOS MENÚS
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());

        }

        private void facturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmFacturacion());

        }

        private void cajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCaja());

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
