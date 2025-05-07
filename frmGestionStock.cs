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
    public partial class frmGestionStock : Form
    {
        public frmGestionStock()
        {
            InitializeComponent();
        }

        

        private void frmGestionStock_Load(object sender, EventArgs e)
        {
            clsConexionBD fg = new clsConexionBD();
            fg.Cargarcmbproducto(cmbProductos);
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedIndex <= 0) return;
            int Id = Convert.ToInt32(cmbProductos.SelectedValue);
            clsConexionBD lk = new clsConexionBD();
            lk.BuscarStock(nudStock , Id);
        }

        private void btnActualizarStock_Click(object sender, EventArgs e)
        {
            int Stock = Convert.ToInt32(nudStock.Value);
            int Id = Convert.ToInt32(cmbProductos.SelectedValue);
            clsConexionBD op = new clsConexionBD();
            op.ModificarStock(Id, Stock);
        }

        private void btnReporteInventario_Click(object sender, EventArgs e)
        {
            frmBuscarProductos ventana = new frmBuscarProductos();
            this.Hide();
            ventana.ShowDialog();
        }
    }
}
