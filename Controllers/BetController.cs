using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBettingRoulette.Dtos.Bet;
using OnlineBettingRoulette.Services.Bet;
using OnlineBettingRoulette.Services.Roulette;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class BetController : ControllerBase
    {
        private readonly IBetService _service;
        private readonly IRouletteService _serviceRoulette;

        public BetController(IBetService service, IRouletteService serviceRoulette)
        {
            _service = service;
            _serviceRoulette = serviceRoulette;
        }

        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromHeader] string user,[FromBody] CreateBet createRequest)
        {
            if (user == null)
            {
                return Unauthorized(new ApiResponse("unauthenticated user", null, 401));
            }

            bool rouletteOpenOrExits = await _serviceRoulette.Exist(createRequest.IdRoulette);

            if (rouletteOpenOrExits)
            {
                ReadBet result = await _service.Create(createRequest, user);
                return Created("/api/v1/projects" + result.Id, new ApiResponse("Bet created.", result, 201));
            }

            return BadRequest(new ApiResponse("unrealized bet.", null, 400));

        }
    }
}
