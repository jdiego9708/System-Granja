namespace SISGranja.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;
    using System.Data;
    using System.ComponentModel.DataAnnotations;

    public class Tipo_corral
    {
        [Key]
        public int Id_tipo_corral { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Nombre_tipo_corral { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Estado_corral { get; set; }
    }
}
