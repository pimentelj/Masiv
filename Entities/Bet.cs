using System;
using System.ComponentModel.DataAnnotations;
using Test.Models;

namespace Test.Entities
{
    public class Bet
    {
        [Key]
        public long Id { get; set; }
        public long RouletteId { get; set; }
        public Roulette roulette { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Number { get; set; }
        public TypeColor Color { get; set; }
        public int Money { get; set; }

    }
}