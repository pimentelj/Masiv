using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Test.Entities;

namespace Test.Models.Roulette
{
    public class RouletteModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Open { get; set; }    
        public List<Test.Entities.Bet> Bets { get; set; }
    }
}