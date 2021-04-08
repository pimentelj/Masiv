using System;
using System.ComponentModel.DataAnnotations;
using Test.Entities;

namespace Test.Models.Bet
{
    public class BetModel
    {
        [Key]
        public long Id { get; set; }
        public long RouletteId { get; set; }
        public Test.Entities.Roulette Roulette { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Number { get; set; }
        public int Color { get; set; }
        public int Money { get; set; }

    }
}