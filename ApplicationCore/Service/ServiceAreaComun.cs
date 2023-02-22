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
            IServiceAreaComun repository = new ServiceAreaComun();
            return repository.GetAreaComun();
        }

        public IEnumerable<AreaComun> GetAreaComunByDisponibilidad(bool estado)
        {
            IServiceAreaComun repository = new ServiceAreaComun();
            return repository.GetAreaComunByDisponibilidad(estado);
        }

        public AreaComun GetAreaComunByID(int id)
        {
            IServiceAreaComun repository = new ServiceAreaComun();
            return repository.GetAreaComunByID(id);
        }

        public AreaComun SaveAreaComun(AreaComun areaComun)
        {
            IServiceAreaComun repository = new ServiceAreaComun();
            return repository.SaveAreaComun(areaComun);
        }
    }
}
