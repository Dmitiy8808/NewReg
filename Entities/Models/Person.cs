using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public string BithPlace { get; set; } = string.Empty;
        public DateTime? BithDate { get; set; } 
        public string Сitizenship { get; set; } = string.Empty;
        public string PassportSeries { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public DateTime? PassportDate { get; set; }
        public string PassportAddon { get; set; } = string.Empty;
        public string PassportUnit { get; set; } = string.Empty;
        public string Inn { get; set; } = string.Empty;
        public string Snils { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileTelephone { get; set; } = string.Empty;
        public int GenderId { get; set; }

    }
}