﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Service
{
    public interface IRepositoryUsuario
    {
        IEnumerable<Usuario> GetUsuario();
        Usuario GetUsuarioByID(int id);
        Usuario Save(Usuario usuario);
        Usuario CambiarEstado(int Id);
        Usuario GetUsuario(string email, string password);
    }
}
