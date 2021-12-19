using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBettingRoulette.Dtos.Bet
{
    public class CreateBet
    {
        public Guid IdRoulette { get; set; }
        [Range(typeof(decimal), "1", "10000", ErrorMessage = "bet outside the allowed range")]
        public decimal Value { get; set; }
        [RegularExpression(@"^(rojo|negro|([0-9]|[1-2]\d|3[0-6]))$", ErrorMessage = "the bet only allows the color 'rojo' or 'negro' and numbers between 0 and 36")]
        public string NumberColor { get; set; }
      
    }
}
