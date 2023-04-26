using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceAsignacion : IServiceAsignacion
    {
        public void GetIngresosMes(out string etiquetas1, out string valores1)
        {
            IRepositoryAsignacion repository = new RepositoryAsignacion();
            repository.GetIngresosMes(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }


        public IEnumerable<Asignacion> GetAsignacion(DateTime fecha)
        {
            if (fecha < DateTime.Now.AddYears(-10))
            {
                IRepositoryAsignacion repository = new RepositoryAsignacion();
                return repository.GetAsignacion();
            }
            else
            {
                IRepositoryAsignacion repository = new RepositoryAsignacion();
                return repository.GetAsignacionByMes(fecha);
            }
        }

        public Asignacion GetAsignacionbyId(int id)
        {
            IRepositoryAsignacion repository = new RepositoryAsignacion();
            return repository.GetAsignacionbyId(id);
        }

        public IEnumerable<Asignacion> GetAsignacionByIdPlan(int idPlan)
        {
            IRepositoryAsignacion repository = new RepositoryAsignacion();
            return repository.GetAsignacionByIdPlan(idPlan);
        }

        public IEnumerable<Asignacion> GetAsignacionByIdResidencia(int idResidencia, bool activo)
        {
            IRepositoryAsignacion repository = new RepositoryAsignacion();
            return repository.GetAsignacionByIdResidencia(idResidencia, activo);
        }

        public IEnumerable<Asignacion> GetAsignacionByIdUsuario(int idUsuario)
        {
            IRepositoryAsignacion repository = new RepositoryAsignacion();
            return repository.GetAsignacionByIdUsuario(idUsuario);
        }

        public Asignacion SaveAsignacion(Asignacion asignacion)
        {
            IRepositoryAsignacion repository = new RepositoryAsignacion();
            return repository.SaveAsignacion(asignacion);
        }
    }
}
