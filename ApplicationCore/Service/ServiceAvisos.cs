using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceAvisos : IServiceAvisos
    {
        public IEnumerable<Avisos> GetAvisos(bool active)
        {
            IRepositoryAvisos repository = new RepositoryAvisos();
            return repository.GetAvisos(active);
        }

        public Avisos GetAvisosByID(int id)
        {
            IRepositoryAvisos repository = new RepositoryAvisos();
            return repository.GetAvisosByID(id);
        }

        public IEnumerable<Avisos> GetAvisosByIdUsuario(int idUsuario)
        {
            IRepositoryAvisos repository = new RepositoryAvisos();
            return repository.GetAvisosByIdUsuario(idUsuario);
        }

        public Avisos SaveAvisos(Avisos aviso)
        {
            IRepositoryAvisos repository = new RepositoryAvisos();
            return repository.SaveAvisos(aviso);
        }
    }
}
