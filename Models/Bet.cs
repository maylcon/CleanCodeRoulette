using System;

namespace OnlineBettingRoulette.Models
{
    public class Bet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdRoulette { get; set; }
        public string User { get; set; }
        public string Value { get; set; }
        public string Number  { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; } = "abierta";
        public DateTime Date { get; set; } = new DateTime();
    }
}
