using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IRepositoryReserva
    {
        IEnumerable<Reserva> GetReserva(bool falso);
        IEnumerable<Reserva> GetReservaByUsuario(int idUsuario);
        Reserva GetReservaByID(int id);
        Reserva Save(Reserva reserva);
    }
}
