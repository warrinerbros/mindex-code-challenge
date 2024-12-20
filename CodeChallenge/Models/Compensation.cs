using System;
using System.Text.Json.Serialization;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [JsonIgnore]
        public int CompensationId { get; set; }  
        public Employee Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}