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

    [MetadataType(typeof(AreaComunMetadata))]

    public partial class AreaComun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AreaComun()
        {
            this.Reserva = new HashSet<Reserva>();
        }
    
        public int id { get; set; }
        public string Descripcion { get; set; }
        public byte[] foto { get; set; }
        public bool Disponibilidad { get; set; }
        public System.DateTime HoraInicio { get; set; }
        public System.DateTime HoraFin { get; set; }
        public int TarifaPorHora { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
