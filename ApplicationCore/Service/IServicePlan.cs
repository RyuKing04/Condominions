using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IServicePlan
    {
        IEnumerable<Plan> GetPlan();
        Plan GetPlanByID(int id);
        Plan SavePlan(Plan plan, string[] selectedRubros);
    }
}
