using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServicePlan : IServicePlan
    {
        public IEnumerable<Plan> GetPlan()
        {
            IRepositoryPlan repository = new RepositoryPlan();
            return repository.GetPlan();
        }

        public Plan GetPlanByID(int id)
        {
            IRepositoryPlan repository = new RepositoryPlan();
            return repository.GetPlanByID(id);
        }

        public Plan SavePlan(Plan plan, string[] selectedRubros)
        {
            IRepositoryPlan repository = new RepositoryPlan();
            return repository.SavePlan(plan, selectedRubros);
        }
    }
}
