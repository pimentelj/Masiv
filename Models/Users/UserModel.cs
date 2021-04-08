using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Nickname { get; set; }
        public long Money { get; set; }
        public List<Test.Entities.Bet> Bets { get; set; }
    }
}