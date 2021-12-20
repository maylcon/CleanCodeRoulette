namespace OnlineBettingRoulette.Dtos.Bet
{
    public class WinBet
    {
        public decimal Profit { get; set; }
        public int NumberWin { get; set; }
        public ReadBet Bet { get; set; }
    }
}
