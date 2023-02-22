using ApplicationCore.Service;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoryAsignacion : IRepositoryAsignacion
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
                      .Include(a => a.Residencia)
                      .Include(a => a.Residencia.Usuario)
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

        public Asignacion GetAsignacionbyId(int id)
        {
            Asignacion oAsignacion = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAsignacion = ctx.Asignacion.Include(x => x.Plan).Include("Residencia").Include(c => c.Residencia.Usuario).Where(c => c.Id == id).FirstOrDefault();
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

        public IEnumerable<Asignacion> GetAsignacionByIdPlan(int idPlan)
        {
            IEnumerable<Asignacion> oAsignacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAsignacion = ctx.Asignacion.Where(x => x.IdPlan == idPlan).Include("Plan").ToList();
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

        public IEnumerable<Asignacion> GetAsignacionByIdResidencia(int idResidencia, bool activo)
        {
            IEnumerable<Asignacion> oAsignacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAsignacion = ctx.Asignacion.Include("Residencia").Where(x => x.IdResidencia == idResidencia && x.Deuda==activo).Include(x=>x.Residencia.Usuario).Include("Plan").ToList();
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

        public IEnumerable<Asignacion> GetAsignacionByIdUsuario(int idUsuario)
        {
            IEnumerable<Asignacion> oAsignacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAsignacion = ctx.Asignacion.Include(x=>x.Residencia).Include(x => x.Residencia.Usuario).Where(X => X.Residencia.idUsuario == idUsuario).ToList();
                    //Puede funcionar o puede que no verificar
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

        public Asignacion SaveAsignacion(Asignacion asignacion)
        {
            throw new NotImplementedException();
        }
    }
}
