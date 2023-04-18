using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceResidencia : IServiceResidencia
    {
        public IEnumerable<Residencia> GetResidenciaByFechaAsignacion(DateTime fecha)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidenciaByFechaAsignacion(fecha);
        }

        IEnumerable<Residencia> IServiceResidencia.GetResidencia()
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidencia();
        }

        Residencia IServiceResidencia.GetResidenciaByID(int id)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidenciaByID(id);
        }

        IEnumerable<Residencia> IServiceResidencia.GetResidenciaByUsuario(int idUsuario)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.GetResidenciaByUsuario(idUsuario);
        }

        Residencia IServiceResidencia.Save(Residencia residencia)
        {
            IRepositoryResidencia repository = new RepositoryResidencia();
            return repository.Save(residencia);
        }
    }
}
