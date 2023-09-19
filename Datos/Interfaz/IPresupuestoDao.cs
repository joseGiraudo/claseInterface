using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Entidades;

namespace WindowsFormsApp1.Datos.Interfaz
{
    interface IPresupuestoDao
    {
        int ObtenerProximoPresupuesto();
        List<Producto> ObtenerProductos();
        bool Crear(Presupuesto oPresupuesto);
        bool Actualizar(Presupuesto oPresupuesto);
        bool Borrar(int nroPresupuesto);
        List<Presupuesto> ObtenerPresupuestoConFiltro(string cliente, DateTime desde, DateTime hasta);
        Presupuesto ObtenerPresupuestoPorNro(int nroPresupuesto);

    }
}
