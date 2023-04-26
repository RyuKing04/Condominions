using ApplicationCore.Service;
using Infraestructure.Models;
using Infraestructure.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
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

        public IEnumerable<Asignacion> GetAsignacionByMes(DateTime mes)
        {
            try
            {
                IEnumerable<Asignacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Asignacion
                      .Include(a => a.Residencia)
                      .Include(a => a.Residencia.Usuario)
                      .ToList()
                      .Where(x => x.FechaPago.Month == mes.Month)
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
                    oAsignacion = ctx.Asignacion.Include("Residencia").Where(x => x.IdResidencia == idResidencia && x.Deuda == activo).Include(x => x.Residencia.Usuario).Include("Plan").ToList();
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
                    oAsignacion = ctx.Asignacion.Include(x => x.Plan).Include(x => x.Residencia).Include(x => x.Residencia.Usuario).Where(X => X.Residencia.idUsuario == idUsuario).ToList();
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
            int retorno = 0;
            Asignacion oAsignacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oAsignacion = GetAsignacionbyId((int)asignacion.Id);

                if (oAsignacion == null)
                {
                    ctx.Asignacion.Add(asignacion);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    //ctx.Asignacion.Add(asignacion);
                    //ctx.Entry(asignacion).State = EntityState.Modified;
                    //retorno = ctx.SaveChanges();

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = "(local)";
                    builder.UserID = "sa";
                    builder.Password = "123456";
                    builder.InitialCatalog = "Condominions";

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        String sql = "UPDATE Asignacion SET Deuda = 0 WHERE Id = " + asignacion.Id;

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    retorno = 1;
                                }
                            }
                        }
                    }
                }
            }

            if (retorno >= 0)
                oAsignacion = GetAsignacionbyId((int)asignacion.Id);

            return oAsignacion;
        }

        public void GetIngresosMes(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    int year = DateTime.Now.Year;
                    var resultado = ctx.Asignacion
                    .Where(x => x.FechaPago.Year == year && x.Deuda == false)
                    .GroupBy(x => x.FechaPago.Month)
                    .Select(o => new { Total = o.Sum(x => x.Plan.Total), Month = o.Key })
                    .ToList()
                    .Select(o => new { Total = o.Total, Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(o.Month) });

                    //Crear etiquetas y valores
                    foreach (var item in resultado)
                    {
                        varEtiquetas += item.Month + ",";
                        varValores += item.Total.ToString() + ",";
                    }

                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
                throw new Exception(mensaje);
            }
        }
    }
}
