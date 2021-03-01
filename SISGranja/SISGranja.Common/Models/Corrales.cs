namespace SISGranja.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;
    using System.Data;
    using System.ComponentModel.DataAnnotations;

    public class Corrales
    {
        public Corrales()
        {

        }

        public Corrales(DataRow row)
        {
            try
            {
                this.Id_corral = Convert.ToInt32(row["Id_corral"]);
                this.Nombre_corral = Convert.ToString(row["Nombre_corral"]);
                this.Descripcion_corral = Convert.ToString(row["Descripcion_corral"]);
                this.Estado_corral = Convert.ToString(row["Estado_corral"]);
            }
            catch (Exception)
            {

            }
        }


        [Key]
        public int Id_corral { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Nombre_corral { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener como máximo 200 carácteres.")]
        public string Descripcion_corral { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Estado_corral { get; set; }
    }
}
