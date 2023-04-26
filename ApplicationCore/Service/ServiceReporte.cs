using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceReporte : IServiceReporte
    {
        public IEnumerable<Asignacion> GetAsignacion()
        {
            IRepositoryReporte repository = new RepositoryReporte();
            return repository.GetAsignacion();
        }

        public IEnumerable<Asignacion> GetAsignacionByIdResidencia(int idResidencia)
        {
            IRepositoryReporte repository= new RepositoryReporte();
            return repository.GetAsignacionByIdResidencia(idResidencia);
        }

        public IEnumerable<Asignacion> GetDeudas()
        {
           IRepositoryReporte repository= new RepositoryReporte();
            return repository.GetDeudas();
        }
    }
}
