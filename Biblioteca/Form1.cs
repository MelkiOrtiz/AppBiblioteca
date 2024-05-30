using Biblioteca.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Biblioteca
{
    public partial class Form1 : Form
    {
        private string[] SeleccionarGenero = {
    "Novela",
    "Clasico",
    "Misterio",
    "Distopia",
    "Fabula"
        };
        catalogo_libros catalogo = new catalogo_libros();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LeerLibros();
        }
        //------------------------------------------------------------------------------------------
        private void LeerLibros(string genero = null)
        {
            DataTable dt = string.IsNullOrEmpty(genero) ? catalogo.LeerTodos() : catalogo.LeerPorGenero(genero);
            dataGridView.DataSource = dt;
        }
        //------------------------------------------------------------------------------------------
        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtAutor.Text) ||
                cmbGenero.SelectedItem == null || string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            return true;
        }
        //----------------------------------------------------------------------------------------
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Libro libro = new Libro
                {
                    Titulo = txtTitulo.Text,
                    Autor = txtAutor.Text,
                    Genero = cmbGenero.SelectedItem.ToString(),
                    FechaPublicacion = dtpFechaPublicacion.Value,
                    Precio = decimal.Parse(txtPrecio.Text),
                    Disponible = chkDisponible.Checked
                };
                try
                {
                    catalogo.Insertar(libro);
                    LeerLibros();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el libro: " + ex.Message);
                }
            }
        }
        //--------------------------------------------------------------------------------------------
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                Libro libro = new Libro
                {
                    ID = int.Parse(txtID.Text),
                    Titulo = txtTitulo.Text,
                    Autor = txtAutor.Text,
                    Genero = cmbGenero.SelectedItem.ToString(),
                    FechaPublicacion = dtpFechaPublicacion.Value,
                    Precio = decimal.Parse(txtPrecio.Text),
                    Disponible = chkDisponible.Checked
                };
                try
                {
                    catalogo.Actualizar(libro);
                    LeerLibros();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el libro: " + ex.Message);
                }
            }
        }
        //--------------------------------------------------------------------------------------
        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            try
            {
                catalogo.Eliminar(id);
                LeerLibros();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el libro: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Por favor, ingrese un ID para buscar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idABuscar;
            if (!int.TryParse(txtID.Text, out idABuscar))
            {
                MessageBox.Show("El ID ingresado no es válido. Por favor, ingrese un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Libro libroEncontrado = catalogo.BuscarPorId(idABuscar);

            if (libroEncontrado != null)
            {
                MostrarLibroEncontrado(libroEncontrado);
            }
            else
            {
                MessageBox.Show("No se encontró ningún libro con el ID especificado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MostrarLibroEncontrado(Libro libro)
        {
            txtTitulo.Text = libro.Titulo;
            txtAutor.Text = libro.Autor;
            cmbGenero.SelectedItem = libro.Genero;
            dtpFechaPublicacion.Value = libro.FechaPublicacion;
            txtPrecio.Text = libro.Precio.ToString();
            chkDisponible.Checked = libro.Disponible;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            cmbGenero.Items.AddRange(SeleccionarGenero);
            dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            LeerLibros();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            
                string generoSeleccionado = cmbGenero.SelectedItem?.ToString();
                LeerLibros(generoSeleccionado);
            
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView.SelectedRows[0];
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtTitulo.Text = row.Cells["Titulo"].Value.ToString();
                txtAutor.Text = row.Cells["Autor"].Value.ToString();
                cmbGenero.SelectedItem = row.Cells["Genero"].Value.ToString();
                dtpFechaPublicacion.Value = Convert.ToDateTime(row.Cells["FechaPublicacion"].Value);
                txtPrecio.Text = row.Cells["Precio"].Value.ToString();
                chkDisponible.Checked = Convert.ToBoolean(row.Cells["Disponible"].Value);
            }
        }

        
    }
}

