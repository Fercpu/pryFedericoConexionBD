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
    public partial class frmModificarProductos : Form
    {
        public frmModificarProductos()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal Ventana = new frmMenuPrincipal();
            this.Hide();
            Ventana.ShowDialog();
        }

        private void frmModificarProductos_Load(object sender, EventArgs e)
        {
            clsConexionBD lk = new clsConexionBD();
            lk.Cargarcmbproducto(cmbProducto);
            lk.CargarCategorias(cmbCategorias);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex <= 0) return;
            int codigo = Convert.ToInt32(cmbProducto.SelectedValue);
            clsConexionBD kl = new clsConexionBD();
            kl.BuscarProductosporcmb(codigo, txtDescripcion, txtPrecio, txtStock, cmbCategorias);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int Codigo = Convert.ToInt32(cmbProducto.SelectedValue);
            string Descripcion = txtDescripcion.Text;
            decimal Precio = Convert.ToDecimal(txtPrecio.Text);
            int Stock = Convert.ToInt32(txtStock.Text);
            int Categoria = Convert.ToInt32(cmbCategorias.SelectedValue);
            clsConexionBD jk = new clsConexionBD();
            jk.ModificarProductos(Codigo, Descripcion,Precio,Stock,Categoria);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
           
            DialogResult Confirmacion = MessageBox.Show("Estas seguro que quieres eliminar este Producto ?","Confirmar Eliminacion",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(Confirmacion == DialogResult.Yes)
            {
                int Codigo = Convert.ToInt32(cmbProducto.SelectedValue);
                clsConexionBD kj = new clsConexionBD();
                kj.EliminarProducto(Codigo);
                txtDescripcion.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                cmbProducto.DataSource = null;
                cmbCategorias.DataSource = null;
                cmbCategorias.Items.Clear();
                cmbProducto.Items.Clear();
                kj.Cargarcmbproducto(cmbProducto);
                kj.CargarCategorias(cmbCategorias);
            }
        }
    }
}
