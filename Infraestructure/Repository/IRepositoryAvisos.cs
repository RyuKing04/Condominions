﻿using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Service
{
    public interface IRepositoryAvisos
    {
        Avisos SaveAvisos(Avisos aviso);
        IEnumerable<Avisos> GetAvisos();
        IEnumerable<Avisos> GetAvisosByIdUsuario(int idUsuario);
        Avisos GetAvisosByID(int id);
    }
}
