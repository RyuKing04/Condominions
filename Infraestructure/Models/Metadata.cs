using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class ResidenciaMetadata
    {
        [Display(Name = "Cantidad de Personas")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int CantPersonas { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Estado { get; set; }

        [Display(Name = "Cantidad de Carros")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int CantCarros { get; set; }

        public int id { get; set; }

        [Display(Name = "Número de Condominio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int NoCondominio { get; set; }

        [Display(Name = "Año de Inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int AnnoInicio { get; set; }

        [Display(Name = "Nombre de Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int idUsuario { get; set; }


    }
    internal partial class UsuarioMetadata
    {
        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Apellido { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public byte[] Contrasenna { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Contrasenna1 { get; set; }

        [Display(Name = "Cantidad de Personas")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool Estado { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        public string Email { get; set; }

    }
    internal partial class PlanMetadata
    {
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descrpcion { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "solo acepta números, con dos decimales")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Total { get; set; }
    }

    internal partial class RubroMetadata {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descrpicion { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "solo acepta números, con dos decimales")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Precio { get; set; }
    }

    internal partial class AreaComunMetadata
    {
        public int id { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }
        [Display(Name = "Foto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public byte[] foto { get; set; }

        
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool Disponibilidad { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        [Display(Name = "Inicio del Horario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime HoraInicio { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        [Display(Name = "Fin del Horario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime HoraFin { get; set; }

        [Display(Name = "Tarifa por Hora")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "solo acepta números, con dos decimales")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal TarifaPorHora { get; set; }
        
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public virtual ICollection<Reserva> Reserva { get; set; }
    }

    internal partial class AsignacionMetadata
    {
        public int Id { get; set; }

        [Display(Name = "Número del Condominio")]
        public int IdResidencia { get; set; }

        [Display(Name = "Plan")]
        public int IdPlan { get; set; }

        [Display(Name = "Fecha de Pago")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime FechaPago { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public bool Deuda { get; set; }

        public virtual Plan Plan { get; set; }

        public virtual Residencia Residencia { get; set; }
    }

    public partial class AvisosMetadata
    {
        public int id { get; set; }
        
        public bool EstadoTipoInfo { get; set; }

        public int idUsuario { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Tipo de Aviso")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string TipoAviso { get; set; }

        [Display(Name = "Fecha de Publicación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime Fecha { get; set; }

        [Display(Name = "Archivo Cargado")]
        [UIHint("Image")]
        public byte[] Documento { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    public partial class ReservaMetadata
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        [Display(Name = "Fecha de la Reserva")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime fecha { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        [Display(Name = "Hora de Inicio")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime HoraInicio { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        [Display(Name = "Hora Final")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime HoraFin { get; set; }

        public bool Estado { get; set; }

        public int idArea { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "solo acepta números, con dos decimales")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Total { get; set; }

        public virtual AreaComun AreaComun { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public partial class VisitanteMetadata
    {
        public int id { get; set; }
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Apelido { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int Telefono { get; set; }

        [Display(Name = "Fecha de la Visita")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime FechaVisita { get; set; }

        public int idResidencia { get; set; }

        public virtual Residencia Residencia { get; set; }
    }
}
