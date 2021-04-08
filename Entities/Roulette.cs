using System.Collections.Generic;
using Test.Models;

namespace Test.Entities
{
    public class Roulette
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Open { get; set; }
        public int Number { get; set; }
        public List<Bet> Bets { get; set; }
    }
}