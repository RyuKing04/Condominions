using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public class ServiceUsuario : IServiceUsuario
    {
        public IEnumerable<Usuario> GetUsuario()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario();
        }

        public Usuario GetUsuario(string email, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario(email, password);
        }

        public Usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            return oUsuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.Save(usuario);
        }
    }
}
