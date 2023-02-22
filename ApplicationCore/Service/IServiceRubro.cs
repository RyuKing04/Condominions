using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IServiceRubro
    {
        IEnumerable<Rubro> GetRubro();
        Rubro GetRubroByID(int id);
        Rubro SaveRubro(Rubro rubro);
    }
}
