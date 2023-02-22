using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceRubro : IServiceRubro
    {
        public IEnumerable<Rubro> GetRubro()
        {
            IRepositoryRubro repository = new RepositoryRubro();
            return repository.GetRubro();
        }

        public Rubro GetRubroByID(int id)
        {
            IRepositoryRubro repository = new RepositoryRubro();
            return repository.GetRubroByID(id);
        }

        public Rubro SaveRubro(Rubro rubro)
        {
            IRepositoryRubro repository = new RepositoryRubro();
            return repository.SaveRubro(rubro);
        }
    }
}
