using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IRepositoryAsignacion
    {
        Asignacion GetAsignacionbyId(int id);
        IEnumerable<Asignacion> GetAsignacion();
        IEnumerable<Asignacion> GetAsignacionByMes(DateTime mes);
        Asignacion SaveAsignacion(Asignacion asignacion);
        IEnumerable<Asignacion> GetAsignacionByIdResidencia(int idResidencia, bool activo);
        IEnumerable<Asignacion> GetAsignacionByIdUsuario(int idUsuario);
        IEnumerable<Asignacion> GetAsignacionByIdPlan(int idPlan);
    }
}
