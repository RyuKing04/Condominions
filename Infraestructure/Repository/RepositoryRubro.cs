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
    public class RepositoryRubro : IRepositoryRubro
    {
        public IEnumerable<Rubro> GetRubro()
        {
            try
            {

                IEnumerable<Rubro> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Rubro.ToList<Rubro>();
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

        public Rubro GetRubroByID(int id)
        {
            Rubro rubro = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    rubro = ctx.Rubro.Find(id);
                }

                return rubro;
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

        public Rubro SaveRubro(Rubro rubro)
        {
            int retorno = 0;
            Rubro oRubro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRubro = GetRubroByID((int)rubro.Id);
                IRepositoryRubro _RepositoryRubro = new RepositoryRubro();

                if (oRubro == null)
                {
                    //Insertar Libro
                    ctx.Rubro.Add(rubro);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Libro
                    ctx.Rubro.Add(rubro);
                    ctx.Entry(rubro).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                   
                }
            }

            if (retorno >= 0)
                oRubro = GetRubroByID((int)rubro.Id);

            return oRubro;
        }
    }
}
