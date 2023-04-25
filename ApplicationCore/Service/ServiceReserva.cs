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
using System.Globalization;

namespace ApplicationCore.Service
{
    public class ServiceReserva : IServiceReserva
    {
        public void Correo(string correo , Reserva reserva )
        {
            
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.office365.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("tonysaske@hotmail.com", "Pelusaserrano123");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("tonysaske@hotmail.com");
                message.To.Add(correo);
                message.Subject = "Reserva Aceptada";
                message.Body = "Su Reserva en " + reserva.AreaComun.Descripcion + " para la fecha " + reserva.fecha +  " ha sido aceptada exitosamente.";
                client.Send(message);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ConvertToAscii(string input)
        {
            StringBuilder result = new StringBuilder();
            string[] parts = input.Split('@');
            if (parts.Length == 2)
            {
                result.Append(parts[0]);
                result.Append('@');
                result.Append(new IdnMapping().GetAscii(parts[1]));
            }
            else
            {
                result.Append(input);
            }
            return Uri.EscapeUriString(result.ToString());
        }

        public IEnumerable<Reserva> GetReserva(bool falso)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReserva(falso);
        }

        public  IEnumerable<Reserva> GetReservaByUsuario(bool falso,  int id)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReservaByUsuario(falso, id);
        }

        public Reserva GetReservaByID (int idUsuario)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.GetReservaByID(idUsuario);
        }

        public Reserva Save(Reserva reserva)
        {
            IRepositoryReserva repository = new RepositoryReserva();
            return repository.Save(reserva);
        }

        
    }
}
