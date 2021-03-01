namespace SISGranja.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Runtime.Serialization;
    using System.Data;
    using System.ComponentModel.DataAnnotations;

    public class Detalle_tipo_animal
    {
        [Key]
        public int Id_detalle_animal { get; set; }

        public int Id_animal { get; set; }

        [JsonIgnore]
        public Animales Animal { get; set; }

        public int Id_tipo_animal { get; set; }

        [JsonIgnore]
        public Tipo_animal Tipo_animal { get; set; }
    }
}
