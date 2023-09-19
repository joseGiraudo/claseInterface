using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public int ObtenerProximoPresupuesto()
        {
            return HelperDao.GetInstance().ConsultarEscalar("SP_PROXIMO_ID", "@next");
        }
    }
}
