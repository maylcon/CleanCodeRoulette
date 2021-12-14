using System;

namespace OnlineBettingRoulette.Models
{
    public class Roulette
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public string Estado { get; set; } = "cerrada";
    }
}
