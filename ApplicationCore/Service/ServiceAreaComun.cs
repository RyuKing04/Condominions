using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceAreaComun : IServiceAreaComun
    {
        public IEnumerable<AreaComun> GetAreaComun()
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.GetAreaComun();
        }

        public IEnumerable<AreaComun> GetAreaComunByDisponibilidad(bool estado)
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.GetAreaComunByDisponibilidad(estado);
        }

        public AreaComun GetAreaComunByID(int id)
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.GetAreaComunByID(id);
        }

        public AreaComun SaveAreaComun(AreaComun areaComun)
        {
            IRepositoryAreaComun repository = new RepositoryAreaComun();
            return repository.SaveAreaComun(areaComun);
        }
    }
}
