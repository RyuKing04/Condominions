﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class MyContext : CondominionsEntities
    {
        public MyContext(){}

        public System.Data.Entity.DbSet<Infraestructure.Models.AvisosMetadata> AvisosMetadatas { get; set; }
    }
}
