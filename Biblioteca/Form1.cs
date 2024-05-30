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
        private void LeerLibros()
        {
            DataTable dt = catalogo.LeerTodos();
            dataGridView.DataSource = dt;
        }
        //------------------------------------------------------------------------------------------
        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtAutor.Text) ||
                string.IsNullOrEmpty(txtGenero.Text) || string.IsNullOrEmpty(txtPrecio.Text))
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
                        Genero = txtGenero.Text,
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
                    Genero = txtGenero.Text,
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
            
                // Verificar si el TextBox de ID está vacío
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Por favor, ingrese un ID para buscar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Obtener el ID ingresado por el usuario
                int idABuscar;
                if (!int.TryParse(txtID.Text, out idABuscar))
                {
                    MessageBox.Show("El ID ingresado no es válido. Por favor, ingrese un número entero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Realizar la búsqueda del libro por ID en la base de datos
                Libro libroEncontrado = catalogo.BuscarPorId(idABuscar);

                // Verificar si se encontró el libro
                if (libroEncontrado != null)
                {
                    // Mostrar el libro encontrado en los controles de tu formulario
                    MostrarLibroEncontrado(libroEncontrado);
                }
                else
                {
                    MessageBox.Show("No se encontró ningún libro con el ID especificado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            // Método para mostrar el libro encontrado en los controles del formulario
             void MostrarLibroEncontrado(Libro libro)
            {
                txtTitulo.Text = libro.Titulo;
                txtAutor.Text = libro.Autor;
                txtGenero.Text = libro.Genero;
                dtpFechaPublicacion.Value = libro.FechaPublicacion;
                txtPrecio.Text = libro.Precio.ToString();
                chkDisponible.Checked = libro.Disponible;
            }
        }
    }
}

