using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.Models.Users
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El nickname es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "El password es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo {1} y minimo {2} caracteres", MinimumLength = 5)]
        public string Password { get; set; }
        public long Money { get; set; }

        public RegisterModel()
        {
            this.Money = 100;
        }
    }
}