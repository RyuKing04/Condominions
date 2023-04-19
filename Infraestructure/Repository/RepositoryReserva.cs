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
    public class RepositoryReserva : IRepositoryReserva
    {
        public IEnumerable<Reserva> GetReserva(bool falso)
        {
            try
            {
                IEnumerable<Reserva> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Reserva.Include(x => x.AreaComun).Include(x => x.Usuario).Where(x => x.Estado == falso).ToList<Reserva>();
                }
                return lista; //true : HISTORIAL        false : NO ACEPTADAS

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

        public Reserva GetReservaByID(int id)
        {
            Reserva oReserva = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oReserva = ctx.Reserva.Find(id);
                }

                return oReserva;
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

        public IEnumerable<Reserva> GetReservaByUsuario(int idUsuario)
        {
            IEnumerable<Reserva> oReserva = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oReserva = ctx.Reserva.Where(x => x.IdUsuario == idUsuario).Include("Usuario").ToList();
                }
                return oReserva;
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
        public void Correo(string correo)
        {

        }

        public Reserva Save(Reserva reserva)
        {
            int retorno = 0;
            Reserva oReserva = null;

            IRepositoryAreaComun _RepositoryArea = new RepositoryAreaComun();

            int tarifaHora = _RepositoryArea.GetAreaComunByID(reserva.idArea).TarifaPorHora;

            DateTime fecha1 = Convert.ToDateTime(reserva.HoraInicio);
            DateTime fecha2 = Convert.ToDateTime(reserva.HoraFin);
            int total = ((int)fecha2.Subtract(fecha1).TotalHours) * tarifaHora;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oReserva = GetReservaByID((int)reserva.Id);
                IRepositoryReserva _RepositoryReserva = new RepositoryReserva();

                if (oReserva == null)
                {
                    reserva.Total = total;

                    ctx.Reserva.Add(reserva);

                    retorno = ctx.SaveChanges();

                }
                else
                {
                    ctx.Reserva.Add(reserva);
                    ctx.Entry(reserva).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }

            if (retorno >= 0)
                oReserva = GetReservaByID((int)reserva.Id);

            return oReserva;
        }
    }
}
