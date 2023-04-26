using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IServiceReporte
    {
        IEnumerable<Asignacion> GetAsignacion();
        IEnumerable<Asignacion> GetAsignacionByIdResidencia(int idResidencia);
    }
}
