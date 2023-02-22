using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IServiceResidencia
    {
        IEnumerable<Residencia> GetResidencia();
        IEnumerable<Residencia> GetResidenciaByUsuario(int idUsuario);
        Residencia GetResidenciaByID(int id);
        Residencia  Save(Residencia residencia);
        
    }
}
