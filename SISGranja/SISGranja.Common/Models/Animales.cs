namespace SISGranja.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;
    using System.Data;
    using System.ComponentModel.DataAnnotations;

    public class Animales
    {
        public Animales()
        {

        }

        public Animales(DataRow row)
        {
            try
            {
                this.Id_animal = Convert.ToInt32(row["Id_animal"]);
                this.Nombre_animal = Convert.ToString(row["Nombre_animal"]);
                this.Descripcion_animal = Convert.ToString(row["Descripcion_animal"]);
                this.Estado_animal = Convert.ToString(row["Estado_animal"]);
            }
            catch (Exception)
            {

            }
        }

        [Key]
        public int Id_animal { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Nombre_animal { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener como máximo 200 carácteres.")]
        public string Descripcion_animal { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Estado_animal { get; set; }
    }
}
