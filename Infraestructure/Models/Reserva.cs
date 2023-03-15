//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infraestructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ReservaMetadata))]
    public partial class Reserva
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime fecha { get; set; }
        public System.DateTime HoraInicio { get; set; }
        public System.DateTime HoraFin { get; set; }
        public bool Estado { get; set; }
        public int idArea { get; set; }
        public int Total { get; set; }
    
        public virtual AreaComun AreaComun { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
