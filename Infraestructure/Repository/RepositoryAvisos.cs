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
    public class RepositoryAvisos : IRepositoryAvisos
    {
        public IEnumerable<Avisos> GetAvisos(bool active)
        {
            try
            {

                IEnumerable<Avisos> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    if (active) //Información
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        lista = ctx.Avisos.Where(x=> x.TipoAviso=="Información").ToList<Avisos>();
                    }
                    else //Incidencia
                    {
                        ctx.Configuration.LazyLoadingEnabled = true;
                        lista = ctx.Avisos.Where(x => x.TipoAviso == "Incidencia").ToList<Avisos>();

                    }
                    return lista;
                }
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

        public Avisos GetAvisosByID(int id)
        {
            Avisos oAvisos = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAvisos = ctx.Avisos.Find(id);
                }

                return oAvisos;
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

        public IEnumerable<Avisos> GetAvisosByIdUsuario(int idUsuario)
        {
            IEnumerable<Avisos> oAviso = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAviso = ctx.Avisos.Where(x => x.idUsuario == idUsuario).Include("Usuario").ToList();
                }
                return oAviso;
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

        public Avisos SaveAvisos(Avisos aviso)
        {
            throw new NotImplementedException();
        }
    }
}
