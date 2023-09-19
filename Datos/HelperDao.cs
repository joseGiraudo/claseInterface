using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Datos
{
    public class HelperDao
    {
        // Clase Singleton

        private static HelperDao instancia;

        // creo y configuro la conexion
        private SqlConnection connection;
        string connectionString = @"Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;Password=alumno1w1";
        // string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CARPINTERIA_2023;Integrated Security=True";

        // constructor privado para que no se pueda instanciar desde afuera
        private HelperDao() 
        {
            connection = new SqlConnection(connectionString);
        }

        // si no hay instancia, la crea, sino devuelve la que ya esta creada (asi la crea una sola vez)
        public static HelperDao GetInstance()
        {
            if(instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }

        // Metodos de la clase Helper
        
        public int ConsultarEscalar(string nombreSP, string nombreParamOut)
        {
            // abro la conexion
            connection.Open();

            // creo y configuro el comando con el SP
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = nombreSP;

            // creo y configuro un parametro porque el SP devuelve un parametro
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = nombreParamOut;
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;

            // paso el parametro al comando (para que traiga el resultado del SP)
            command.Parameters.Add(parameter);

            // ejecuto el comando
            command.ExecuteNonQuery();

            connection.Close();

            return (int)parameter.Value;
        }

        public DataTable Consultar(string nombreSP)
        {
            connection.Open();

            // configuro el comando con el SP pasado por parametros
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = nombreSP;

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            connection.Close();

            return table;
        }
    }
}
