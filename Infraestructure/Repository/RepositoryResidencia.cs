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
    public class RepositoryResidencia : IRepositoryResidencia
    {
        public IEnumerable<Residencia> GetResidencia()
        {
            IEnumerable<Residencia> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista=ctx.Residencia.Include("Usuario").ToList();   
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

        public IEnumerable<Residencia> GetResidenciaByFechaAsignacion(DateTime fecha)
        {
            IEnumerable<Residencia> oResidencia = null;

            DateTime primerDiaDelMes = new DateTime(fecha.Year, fecha.Month, 1);
            DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);

            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //oResidencia = ctx.Asignacion.Include("Residencia").Where(x => x.IdResidencia == idResidencia && x.Deuda == activo).Include(x => x.Residencia.Usuario).Include("Plan").ToList();
                    //oResidencia = ctx.Asignacion.Include("Residencia").Where(x => x.FechaPago > primerDiaDelMes).Where(x => x.FechaPago < ultimoDiaDelMes).ToList();
                    var todasLasResidencias = ctx.Residencia.Include("Usuario").ToList();

                    var asignacionesDelMes = ctx.Asignacion
                    .Where(a => a.FechaPago >= primerDiaDelMes && a.FechaPago <= ultimoDiaDelMes)
                    .ToList();

                    var sinAsignacionesDelMes = todasLasResidencias
                        .Where(r => !asignacionesDelMes.Any(a => a.IdResidencia == r.id))
                        .ToList();

                    oResidencia = (IEnumerable<Residencia>)sinAsignacionesDelMes;
                }
                return oResidencia;
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

        public Residencia GetResidenciaByID(int id)
        {
            Residencia residencia = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    residencia = ctx.Residencia.Where(l => l.id == id).Include("Usuario").FirstOrDefault();
                }
                return residencia;
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

        public IEnumerable<Residencia> GetResidenciaByUsuario(int idUsuario)
        {
            IEnumerable<Residencia> oResidencia = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oResidencia = ctx.Residencia.Where(l => l.idUsuario == idUsuario).Include("Usuario").ToList();
                }
                return oResidencia;
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

        public Residencia Save(Residencia residencia)
        {
            int retorno = 0;
            Residencia oResidencia= null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oResidencia = GetResidenciaByID((int)residencia.id);

                if (oResidencia == null)
                {

                    ctx.Residencia.Add(residencia);
                    
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    
                    ctx.Residencia.Add(residencia);
                    ctx.Entry(residencia).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();


                }
            }

            if (retorno >= 0)
                oResidencia = GetResidenciaByID((int)residencia.id);

            return oResidencia;
        }   
    }
}
