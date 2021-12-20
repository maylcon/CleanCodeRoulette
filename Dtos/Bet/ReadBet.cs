using System;

namespace OnlineBettingRoulette.Dtos.Bet
{
    public class ReadBet
    {
        public Guid Id { get; set; }
        public Guid IdRoulette { get; set; }
        public string User { get; set; }
        public string Value { get; set; }
        public string NumberColor { get; set; }
        public string Estado { get; set; }
        public DateTime Date { get; set; }
    }
}
