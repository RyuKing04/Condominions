using ApplicationCore.Service;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryPlan : IRepositoryPlan
    {
        public IEnumerable<Plan> GetPlan()
        {
            try
            {

                IEnumerable<Plan> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Plan.ToList<Plan>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Plan GetPlanByID(int id)
        {
            Plan oPlan = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oPlan = ctx.Plan.Include("Rubro").Where(c=> c.Id == id).FirstOrDefault();
                }

                return oPlan;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Plan SavePlan(Plan plan, string[] selectedRubros)
        {
            int retorno = 0;
            int total = 0;
            Plan oPlan = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPlan = GetPlanByID((int)plan.Id);
                IRepositoryRubro _RepositoryRubro = new RepositoryRubro();

                if (oPlan == null)
                {
                    //Crear Plan
                    if (selectedRubros != null)
                    {
                        plan.Rubro = new List<Rubro>();
                        foreach (var rubro in selectedRubros)
                        {
                            var rubroToAdd = _RepositoryRubro.GetRubroByID(int.Parse(rubro));
                            ctx.Rubro.Attach(rubroToAdd); 
                            plan.Rubro.Add(rubroToAdd);
                            total += rubroToAdd.Precio; //ACUMULA EL PRECIO DE CADA RUBRO QUE VA A TENER EL PLAN
                        }
                    }
                    plan.Total = total; // ASIGNA EL ACUMULADO AL TOTAL DEL PLAN
                    ctx.Plan.Add(plan);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //Actualizar Plan
                    ctx.Plan.Add(plan);
                    ctx.Entry(plan).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    //Actualizar Rubros
                    var selectedRubrosXid = new HashSet<string>(selectedRubros);
                    if (selectedRubros != null)
                    {
                        ctx.Entry(plan).Collection(p => p.Rubro).Load();
                        var newRubroForPlan = ctx.Rubro.Where(x => selectedRubrosXid.Contains(x.Id.ToString())).ToList();
                        plan.Rubro = newRubroForPlan;

                        foreach (var item in selectedRubros)
                        {
                            var rubroToAdd = _RepositoryRubro.GetRubroByID(int.Parse(item));
                            plan.Total += rubroToAdd.Precio;
                        }

                        ctx.Entry(plan).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();


                    }
                }
            }

            if (retorno >= 0)
                oPlan = GetPlanByID((int)plan.Id);

            return oPlan;
        }

        
    }
}
