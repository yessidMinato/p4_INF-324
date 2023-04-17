using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace p4_INF_324
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-AQVJKSE\\SQLEXPRESS;Initial Catalog=mibaseyessid;User ID=yessid;Password=123456;";
            // Crear la conexión
            SqlConnection connection = new SqlConnection(connectionString);
            // Abrir la conexión
            connection.Open();
            // Consulta SQL para obtener la media de notas por departamento
            string sql = "SELECT p.departamento, AVG(CAST(i.notaFinal AS DECIMAL(18,2))) " + "FROM inscripcion i " + "INNER JOIN persona p ON i.ci = p.ci " + "GROUP BY p.departamento";
            SqlCommand command = new SqlCommand(sql, connection);
            // Ejecutar la consulta y guardar los resultados en una lista
            List<string> resultados = new List<string>();

            //Aquí estamos ejecutando la consulta SQL definida en el objeto command y guardando los resultados en el objeto reader. 
            //SqlDataReader es una clase que nos permite leer los resultados de una consulta SQL en forma de un conjunto de filas, una por una.
            SqlDataReader reader = command.ExecuteReader();

            //El objeto reader actúa como un cursor que apunta a la primera fila de resultados. El método Read() nos permite avanzar el cursor a la siguiente fila
            while (reader.Read())
            {
                string departamento = reader.GetString(0);
                decimal media = reader.GetDecimal(1);
                string resultado = String.Format("{0}: {1}", departamento, media);
                resultados.Add(resultado);
            }
            // Llenar el control GridView con los resultados
            GridView1.DataSource = resultados;
            GridView1.DataBind();
            // Cerrar la conexión
            connection.Close();
        }
    }
}







