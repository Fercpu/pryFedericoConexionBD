using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryApellidoConexionBD
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
           // clsConexionBD BD = new clsConexionBD();
            //BD.Main(lblConexion);
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            clsConexionBD BD = new clsConexionBD();
            //BD.BuscarProducto();
            BD.ConectarAccess(lblConexion);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmGestionInventario Ventana = new frmGestionInventario();
            this.Hide();
            Ventana.ShowDialog();
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            frmBuscarProductos ventana = new frmBuscarProductos();
            this.Hide(); 
            ventana.ShowDialog();
        }

        private void btnGestionStock_Click(object sender, EventArgs e)
        {
            frmGestionStock df = new frmGestionStock();
            this.Hide();
            df.ShowDialog();
        }
    }
}
