using System;

namespace OnlineBettingRoulette.Models
{
    public class Bet
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdRoulette { get; set; }
        public string User { get; set; }
        public string Value { get; set; }
        public string NumberColor { get; set; }
        public string Estado { get; set; } = "abierta";
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
