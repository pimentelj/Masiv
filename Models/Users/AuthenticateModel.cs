using System.ComponentModel.DataAnnotations;

namespace Test.Models.Users
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "El nickname es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo {1} caracteres")]
        public string Nickname { get; set; }        

        [Required(ErrorMessage = "El password es requerido")]
        [StringLength(50, ErrorMessage = "El {0} debe ser máximo {1} y minimo {2} caracteres", MinimumLength = 5)]        
        public string Password { get; set; }
    }
}