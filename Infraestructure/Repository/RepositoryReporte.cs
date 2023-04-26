using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryReporte : IRepositoryReporte
    {
        public IEnumerable<Asignacion> GetAsignacion()
        {
            try
            {

                IEnumerable<Asignacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //lista = ctx.Asignacion.Include("Residencia").Distinct().Include(c=>c.Residencia.Usuario).ToList();
                    lista = ctx.Asignacion
                      .Include("Residencia").Include("Residencia.Usuario").Include("Plan")
                      .ToList()
                      .GroupBy(a => a.Residencia.id)
                      .Select(g => g.First())
                      .ToList();

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

        public IEnumerable<Asignacion> GetAsignacionByIdResidencia(int idResidencia)
        {
            IEnumerable<Asignacion> oAsignacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAsignacion = ctx.Asignacion.Include("Residencia").Include("Residencia.Usuario").Include("Plan").ToList();
                }
                return oAsignacion;
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
    }
}
