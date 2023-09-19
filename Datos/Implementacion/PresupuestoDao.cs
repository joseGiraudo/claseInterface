using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Datos.Interfaz;
using WindowsFormsApp1.Entidades;

namespace WindowsFormsApp1.Datos.Implementacion
{
    public class PresupuestoDao : IPresupuestoDao
    {
        public bool Actualizar(Presupuesto oPresupuesto)
        {
            throw new NotImplementedException();
        }

        public bool Borrar(int nroPresupuesto)
        {
            throw new NotImplementedException();
        }

        public bool Crear(Presupuesto oPresupuesto)
        {
            bool result = true;
            SqlConnection connection = HelperDao.GetInstance().GetConnection();

            // objeto transaction propio de ADO
            SqlTransaction transaction = null;

            try
            {
                // abro la conexion y abro una transaction con esa conexion
                connection.Open();
                transaction = connection.BeginTransaction(); // Comienzo de la transaction

                // creo y configuro el comando
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = transaction; // Asigno el objeto transaction al command
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_INSERTAR_MAESTRO"; // procedimientos almacenados creados en la bd

                // configuracion de los parametros de entrada del SP
                command.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
                command.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                command.Parameters.AddWithValue("@total", oPresupuesto.CalcularTotal());

                // creo y configuro el parametro porque el SP devuelve un parametro (parametro de salida): nro de presupuesto
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@presupuesto_nro";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Output;

                // paso el parametro al comando (para que traiga el resultado del SP)
                command.Parameters.Add(parameter);

                // ejecuto el comando
                command.ExecuteNonQuery();

                // ahora debo insertar los detalles del presupueto al presupuesto (del cual ya tengo el numero)
                // el nro de presupuesto es el parametro de salida
                int presupuestoNro = (int)parameter.Value;

                // genero el nro de detalle
                int detalleNro = 1;

                // creo un command para llamar al SP que inserta los detalles al presupuesto
                SqlCommand commandDetalles;

                foreach (DetallePresupuesto detP in nuevoPre.Detalles)
                {
                    commandDetalles = new SqlCommand("SP_INSERTAR_DETALLE", connection, transaction); // paso los valores como parametros
                    commandDetalles.CommandType = CommandType.StoredProcedure;
                    // paso los parametros de entrada del sp de cada detalle que agrego
                    commandDetalles.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
                    commandDetalles.Parameters.AddWithValue("@detalle", detalleNro);
                    commandDetalles.Parameters.AddWithValue("@id_producto", detP.Producto.NroProducto);
                    commandDetalles.Parameters.AddWithValue("@cantidad", detP.Cantidad);
                    commandDetalles.ExecuteNonQuery();
                    detalleNro++;

                }


                // confirmo la transaction
                transaction.Commit();
            }

            catch (Exception ex)
            {
                // si la transaction no es nula quiere decir que comenzo la transaction
                if (transaction != null)
                {
                    transaction.Rollback();
                    result = false;
                }
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    // por las dudas la coneccion quede abierta, la cerramos
                    connection.Close();
                }
            }

            return result;
        }

        public List<Presupuesto> ObtenerPresupuestoConFiltro(string cliente, DateTime desde, DateTime hasta)
        {
            throw new NotImplementedException();
        }

        public Presupuesto ObtenerPresupuestoPorNro(int nroPresupuesto)
        {
            throw new NotImplementedException();
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> lProductos = new List<Producto>();
            DataTable table = HelperDao.GetInstance().Consultar("SP_CONSULTAR_PRODUCTOS");

            foreach(DataRow row in table.Rows)
            {
                Producto p = new Producto(Convert.ToInt32(row[0]), row[1].ToString(), Convert.ToDouble(row[2]));
                lProductos.Add(p);
            }

            return lProductos;
        }

        public int ObtenerProximoPresupuesto()
        {
            return HelperDao.GetInstance().ConsultarEscalar("SP_PROXIMO_ID", "@next");
        }
    }
}
