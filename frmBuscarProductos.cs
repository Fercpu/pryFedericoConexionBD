using Microsoft.Identity.Client;
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
    public partial class frmBuscarProductos : Form
    {
        public frmBuscarProductos()
        {
            InitializeComponent();
            
        }
        public string Campo;

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            
            clsConexionBD kl = new clsConexionBD(); //Nombre del Objeto kl
            if (Campo == "Nombre") 
            {
                string Car = txtBusqueda.Text;
                kl.BuscarPorNombre(Car,dgvGrilla);
            }

            if (Campo == "CategoriaId")
            {
                int CatId = Convert.ToInt32(cmbCategorias.SelectedValue);
                kl.BuscarPorCategoria(CatId,dgvGrilla);
            }

            if (Campo == "Codigo") 
            {
                int Cod = Convert.ToInt32(txtBusqueda.Text);
                kl.BuscarPorCodigo(Cod,dgvGrilla);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal var = new frmMenuPrincipal();
            this.Hide();
            var.ShowDialog();

        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Tipo = cmbTipo.SelectedIndex;
            if (Tipo == 0)// Nombre 
            {
                cmbCategorias.Visible = false;
                txtBusqueda.Visible = true;
                Campo = "Nombre";
                
            }
            if (Tipo == 1) //Categoria
            {
                cmbCategorias.Visible = true;
                txtBusqueda.Visible = false;
                Campo = "CategoriaId";
            }
            if (Tipo == 2)// Codigo
            {
                cmbCategorias.Visible = false;
                txtBusqueda.Visible = true;
                Campo = "Codigo";
            }
        }

        private void frmBuscarProductos_Load(object sender, EventArgs e)
        {
            clsConexionBD ñl = new clsConexionBD();
            ñl.CargarCategorias(cmbCategorias);
        }
    }
}
