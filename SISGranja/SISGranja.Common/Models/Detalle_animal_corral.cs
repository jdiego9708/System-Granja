namespace SISGranja.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;
    using System.Data;
    using System.ComponentModel.DataAnnotations;

    public class Detalle_animal_corral
    {
        [Key]
        public int Id_detalle_animal { get; set; }

        public int Id_animal { get; set; }

        [JsonIgnore]
        public Animales Animal { get; set; }

        public int Id_corral { get; set; }

        [JsonIgnore]
        public Corrales Corral { get; set; }
    }
}
