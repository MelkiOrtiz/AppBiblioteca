using Biblioteca.Data.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

public class catalogo_libros
{
    string connectionString = "server=localhost;database=bibliotecacrud;user=root;password=Melki-2004";
    MySqlConnection connection;

    public catalogo_libros()
    {
        connection = new MySqlConnection(connectionString);
    }

    public void Insertar(Libro libro)
    {
        try
        {
            string query = "INSERT INTO Libros (Titulo, Autor, Genero, FechaPublicacion, Precio, Disponible) VALUES (@Titulo, @Autor, @Genero, @FechaPublicacion, @Precio, @Disponible)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@Autor", libro.Autor);
            cmd.Parameters.AddWithValue("@Genero", libro.Genero);
            cmd.Parameters.AddWithValue("@FechaPublicacion", libro.FechaPublicacion);
            cmd.Parameters.AddWithValue("@Precio", libro.Precio);
            cmd.Parameters.AddWithValue("@Disponible", libro.Disponible);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al agregar el registro: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    public void Actualizar(Libro libro)
    {
        try
        {
            string query = "UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, Genero = @Genero, FechaPublicacion = @FechaPublicacion, Precio = @Precio, Disponible = @Disponible WHERE ID = @ID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", libro.ID);
            cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@Autor", libro.Autor);
            cmd.Parameters.AddWithValue("@Genero", libro.Genero);
            cmd.Parameters.AddWithValue("@FechaPublicacion", libro.FechaPublicacion);
            cmd.Parameters.AddWithValue("@Precio", libro.Precio);
            cmd.Parameters.AddWithValue("@Disponible", libro.Disponible);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al actualizar el registro: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    public void Eliminar(int id)
    {
        try
        {
            string query = "DELETE FROM Libros WHERE ID = @ID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al eliminar el registro: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }

    public DataTable LeerTodos()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "SELECT * FROM Libros";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            connection.Open();
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al leer los registros: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return dt;
    }
    public Libro BuscarPorId(int id)
    {
        Libro libroEncontrado = null;
        try
        {
            string query = "SELECT * FROM Libros WHERE ID = @ID";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@ID", id);
            connection.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                libroEncontrado = new Libro
                {
                    ID = reader.GetInt32("ID"),
                    Titulo = reader.GetString("Titulo"),
                    Autor = reader.GetString("Autor"),
                    Genero = reader.GetString("Genero"),
                    FechaPublicacion = reader.GetDateTime("FechaPublicacion"),
                    Precio = reader.GetDecimal("Precio"),
                    Disponible = reader.GetBoolean("Disponible")
                };
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al buscar el libro por ID: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return libroEncontrado;
    }
}
