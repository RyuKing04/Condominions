using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IServiceReserva
    {
        IEnumerable<Reserva> GetReserva(bool falso);
        IEnumerable<Reserva> GetReservaByUsuario(bool falso, int idUsuario);
        Reserva GetReservaByID(int id);
        Reserva Save(Reserva reserva);
        void Correo(string correo, Reserva reserva);
    }
}
