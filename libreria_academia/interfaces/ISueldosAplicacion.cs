using libreria_academia.entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace libreria_academia.interfaces
{
    public interface ISueldosAplicacion
    {
        Sueldos Insertar(Sueldos entidad);
        Sueldos Actualizar(Sueldos entidad);
        List<Sueldos> Consultar();
        Sueldos Borrar(Sueldos entidad);
    }
}
