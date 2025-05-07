using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // libreria sql la llama
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace pryApellidoConexionBD
{
    internal class clsConexionBD
    {

        public void Main(Label lbl)
        {
            string connectionString = "Server=localhost;Database=Comercio;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    lbl.Text = "Conectado";

                }
                catch (Exception ex)
                {
                    lbl.Text = ex.Message;
                    MessageBox.Show("Error al Conectar : " + ex.Message);
                }

            }
        }
        public void BuscarProducto()
        {
            try
            {
                string query = "SELECT Nombre FROM Categorias ";
                String connectionString = "Server=localhost;Database=Comercio;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            MessageBox.Show($"{reader["Nombre"]} ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al Conectar : " + ex.Message);
            }



        }

        public void ConectarAccess(Label lbl)
        {

            try
            {
                SqlConnection objConexion = new SqlConnection();
                String CadenadeConexion = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";
                objConexion.ConnectionString = CadenadeConexion;
                objConexion.Open();
                lbl.Text = "Conectado";
                objConexion.Close();


            }
            catch (Exception ex) //cuando hay error
            {

                lbl.Text = ex.Message;
                MessageBox.Show("Error al Conectar : " + ex.Message);
            }
        }
        public void CargarProducto(String Nombre, String Descripcion, int Precio, int Stock, int Categoria)
        {
            try
            {
                string cadenaConexion = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                SqlConnection conexion = new SqlConnection(cadenaConexion);
                
                    string query = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaId) " +
                                   "VALUES (@Nombre, @Descripcion, @Precio, @Stock, @Categoria)";

                SqlCommand comando = new SqlCommand(query, conexion);
                    
                        // Asignar los valores a los parámetros
                        comando.Parameters.AddWithValue("@Nombre", Nombre);
                        comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                        comando.Parameters.AddWithValue("@Precio", Precio);
                        comando.Parameters.AddWithValue("@Stock", Stock);
                        comando.Parameters.AddWithValue("@Categoria", Categoria);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        conexion.Close();

                        MessageBox.Show("Producto cargado correctamente en SQL Server.");
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message);
            }



        }
        public void CargarCategorias(ComboBox cmbCategoria)
        {
            try
            {
                string cadenaConexion = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string query = "SELECT Id, Categoria FROM Categorias";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        SqlDataAdapter Adaptador = new SqlDataAdapter(comando);
                        DataTable Tabla = new DataTable();
                        Adaptador.Fill(Tabla);

                        cmbCategoria.DataSource = Tabla;
                        cmbCategoria.DisplayMember = "Categoria";
                        cmbCategoria.ValueMember = "Id";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar: " + ex.Message);
            }

        }
        public void Cargarcmbproducto(ComboBox cmbProductos)
        {
            try
            {
                string cadenaConexion = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    string query = "SELECT Codigo, Nombre FROM Productos";

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        SqlDataAdapter Adaptador = new SqlDataAdapter(comando);
                        DataTable Tabla = new DataTable();
                        Adaptador.Fill(Tabla);

                        DataRow fila = Tabla.NewRow();
                        fila["Codigo"] = 0;
                        fila["Nombre"] = "(Seleccione un Producto)";
                        Tabla.Rows.InsertAt(fila, 0);

                        cmbProductos.DataSource = Tabla;
                        cmbProductos.DisplayMember = "Nombre";
                        cmbProductos.ValueMember = "Codigo";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Cargar: " + ex.Message);
            }
        }
        public void BuscarProductosporcmb(int codigo, TextBox txtDescripcion, TextBox txtPrecio, TextBox txtStock, ComboBox cmbCategoria)
        {
            try
            {

                String connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Descripcion,Precio,Stock,CategoriaId FROM Productos " +
                        "WHERE Codigo = @Codigo";

                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        comando.Parameters.AddWithValue("@Codigo", codigo);
                        connection.Open();
                        using (SqlDataReader Lector = comando.ExecuteReader()) //Funciones                                                       
                        {
                            if (Lector.Read())
                            {
                                txtDescripcion.Text = Lector["Descripcion"].ToString();
                                txtPrecio.Text = Lector["Precio"].ToString();
                                txtStock.Text = Lector["Stock"].ToString();
                                cmbCategoria.SelectedValue = Convert.ToInt32(Lector["CategoriaId"]);

                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al Conectar : " + ex.Message);
            }
        }
        public void ModificarProductos(int codigo, string Descripcion, decimal Precio, int Stock, int Categoria)
        {
            try
            {
                String connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Productos SET " +
                        "Descripcion = @Descripcion," +
                        "Precio = @Precio," +
                        "Stock = @Stock," +
                        "CategoriaId = @CategoriaId " +
                        "WHERE Codigo = @Codigo";
                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        // Asignar los valores a los parámetros
                        comando.Parameters.AddWithValue("@Codigo", codigo);
                        comando.Parameters.AddWithValue("@Descripcion", Descripcion);
                        comando.Parameters.AddWithValue("@Precio", Precio);
                        comando.Parameters.AddWithValue("@Stock", Stock);
                        comando.Parameters.AddWithValue("@CategoriaId", Categoria);

                        connection.Open();
                        int FilasAfectadas = comando.ExecuteNonQuery();
                        if (FilasAfectadas > 0)
                        {
                            MessageBox.Show("Producto Modificado Correctamente");

                        }
                        else
                        {
                            MessageBox.Show("No se pudo Modificar el Producto");
                        }
                        connection.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar :" + ex.Message);
            }
        }
        public void EliminarProducto(int Codigo)
        {
            try
            {
                String connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Productos " +
                        "WHERE Codigo = @Codigo";
                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        comando.Parameters.AddWithValue("@Codigo", Codigo);

                        connection.Open();
                        int FilasAfectadas = comando.ExecuteNonQuery();
                        if (FilasAfectadas > 0)
                        {
                            MessageBox.Show("Producto Eliminado Correctamente");

                        }
                        else
                        {
                            MessageBox.Show("No se pudo Eliminar el Producto");
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar :" + ex.Message);
            }

        }
        public void BuscarPorNombre(string Nombre, DataGridView dgv)
        {
            try
            {
                string connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "  SELECT P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, C.Categoria FROM Productos AS P\r\nINNER JOIN Categorias AS C ON P.CategoriaId = C.Id WHERE Nombre LIKE @Nombre";

                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        comando.Parameters.AddWithValue("@Nombre", "%" + Nombre + "%");//Busca por Nombres Similares

                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);

                        dgv.DataSource = tabla;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el producto: " + ex.Message);
            }
        }
            public void BuscarPorCodigo(int codigo,  DataGridView dgv)
            {
                try
                {
                    string connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "SELECT P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, C.Categoria FROM Productos AS P\r\nINNER JOIN Categorias AS C ON P.CategoriaId = C.Id WHERE Codigo = @Codigo";

                        using (SqlCommand comando = new SqlCommand(query, connection))
                        {
                            comando.Parameters.AddWithValue("@Codigo", codigo);

                            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                            DataTable tabla = new DataTable();
                            adaptador.Fill(tabla);

                            dgv.DataSource = tabla;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el producto: " + ex.Message);
                }
            }
        public void BuscarPorCategoria(int CatId , DataGridView dgv) 
        {
            try
            {
                string connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT P.Codigo, P.Nombre, P.Descripcion, P.Precio, P.Stock, C.Categoria FROM Productos AS P\r\nINNER JOIN Categorias AS C ON P.CategoriaId = C.Id WHERE CategoriaId = @CategoriaId";

                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        comando.Parameters.AddWithValue("@CategoriaId", CatId);

                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);

                        dgv.DataSource = tabla;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el producto: " + ex.Message);
            }
        }
        public void BuscarStock(NumericUpDown nudStock, int codigo) 
        {
            try
            {

                String connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Stock FROM Productos " +
                        "WHERE Codigo = @Codigo";

                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        comando.Parameters.AddWithValue("@Codigo", codigo);
                        connection.Open();
                        using (SqlDataReader Lector = comando.ExecuteReader()) //Funciones                                                       
                        {
                            if (Lector.Read())
                            {

                                nudStock.Value = Convert.ToInt32(Lector["Stock"]); 
                               

                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al Conectar : " + ex.Message);
            }
        }
        public void ModificarStock(int Codigo , int Stock) 
        {
            try
            {
                String connectionString = "Server=localhost;Database=GestionInv;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Productos SET " +
                        
                        "Stock = @Stock " +
                        
                        "WHERE Codigo = @Codigo";
                    using (SqlCommand comando = new SqlCommand(query, connection))
                    {
                        // Asignar los valores a los parámetros
                        comando.Parameters.AddWithValue("@Codigo", Codigo);
                       
                        comando.Parameters.AddWithValue("@Stock", Stock);
                       

                        connection.Open();
                        int FilasAfectadas = comando.ExecuteNonQuery();
                        if (FilasAfectadas > 0)
                        {
                            MessageBox.Show("Producto Modificado Correctamente");

                        }
                        else
                        {
                            MessageBox.Show("No se pudo Modificar el Producto");
                        }
                        connection.Close();


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Conectar :" + ex.Message);
            }
        }
    }

}

