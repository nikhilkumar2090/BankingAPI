using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingAPI.Models
{
    public class Member
    {
        public int memberId { get; set; }
        public string givenName { get; set; }
        public string surname { get; set; }
        public int institutionId { get; set; }
        public List<Account> accounts { get; set; }
    }
}