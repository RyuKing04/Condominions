using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;


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
        public Reserva Correo (Reserva reserva)
        {
            // Configura el cliente SMTP
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com"; // Cambia esto al servidor SMTP que desees usar
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("tu-correo@gmail.com", "tu-contraseña");

            // Crea el mensaje de correo electrónico
            MailMessage message = new MailMessage();
            message.From = new MailAddress("tu-correo@gmail.com");
            message.To.Add("destinatario@ejemplo.com");
            message.Subject = "Asunto del correo";
            message.Body = "Contenido del correo electrónico.";

            // Envía el correo electrónico
            client.Send(message);
        }
    }
}
