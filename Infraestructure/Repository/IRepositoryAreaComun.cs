using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IRepositoryAreaComun
    {
        IEnumerable<AreaComun> GetAreaComun();
        AreaComun GetAreaComunByID(int id);
        AreaComun SaveAreaComun(AreaComun areaComun);
        IEnumerable<AreaComun> GetAreaComunByDisponibilidad(Boolean estado);


    }
}
