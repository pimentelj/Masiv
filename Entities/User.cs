using System.Collections.Generic;
using Test.Models;

namespace Test.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public long Money { get; set; }
        public List<Bet> Bets { get; set; }
    }
}