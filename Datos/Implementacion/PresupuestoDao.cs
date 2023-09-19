using System;
using System.Collections.Generic;
using System.Data;
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
            throw new NotImplementedException();
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
