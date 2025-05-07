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
    public partial class frmGestionInventario : Form
    {
        public frmGestionInventario()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            int Precio = Convert.ToInt32(txtPrecio.Text);
            int Stock = Convert.ToInt32(txtStock.Text);
            int Categoria = Convert.ToInt32(cmbCategorias.SelectedValue);
            clsConexionBD  clase = new clsConexionBD();
            clase.CargarProducto(Nombre,Descripcion,Precio,Stock,Categoria);
        }

        private void frmGestionInventario_Load(object sender, EventArgs e)
        {
            clsConexionBD Clase = new clsConexionBD();
            Clase.CargarCategorias(cmbCategorias);
        }

        private void btnModificaroEliminar_Click(object sender, EventArgs e)
        {
            frmModificarProductos Ventana = new frmModificarProductos();
            this.Hide();
            Ventana.ShowDialog();
            
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal Ventana = new frmMenuPrincipal();
            this.Hide();
            Ventana.ShowDialog();
        }
    }
}
