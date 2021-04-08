using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Test.Entities;

namespace Test.Models.Bet
{
    public class CreateBetModel
    {
        [DisplayName("Número (De 0 a 36)")]
        [Range(0, 36)]
        public int Number { get; set; }
        [DisplayName("Color (Rojo o Negro)")]
        [RegularExpression("Rojo|Negro", ErrorMessage = "Los valores del color son Rojo o Negro.")]
        public String Color { get; set; }
        [DisplayName("Dinero")]
        [Required(ErrorMessage = "El dinero es requerido")]
        public int Money { get; set; }
    }
}