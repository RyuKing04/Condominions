using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;


namespace ApplicationCore.Service
{
    public class ServiceReserva : IServiceReserva
    {
        public void Correo(string correo )
        {
            var password = Environment.GetEnvironmentVariable("gmail", EnvironmentVariableTarget.User);
            try
            {
                var cliente = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("caribbeancondominions@gmail.com", password)

                };
                MailMessage message = new MailMessage();
                message.From = new MailAddress("caribbeancondominions@gmail.com");
                message.To.Add(correo);
                message.Subject = "Asunto del correo";
                message.Body = "Contenido del correo electrónico.";
                cliente.Send(message);

            }
            catch (Exception)
            {
                throw;
            }
        }

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
