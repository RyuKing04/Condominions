using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceReserva : IServiceReserva
    {
        public IEnumerable<Reserva> GetReserva(bool falso)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReserva(falso);
        }

        public Reserva GetReservaByID(int id)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReservaByID(id);
        }

        public IEnumerable<Reserva> GetReservaByUsuario(int idUsuario)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReservaByUsuario(idUsuario);
        }

        public Reserva Save(Reserva reserva)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.Save(reserva);
        }
    }
}
