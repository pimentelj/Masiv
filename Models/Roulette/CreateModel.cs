using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Test.Models.Roulette
{
    public class CreateModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Name { get; set; }
        [JsonIgnore]
        public bool Open { get; set; }
        [JsonIgnore]
        public int Number { get; set; }

        public CreateModel()
        {
            this.Open = false;
        }
    }
}