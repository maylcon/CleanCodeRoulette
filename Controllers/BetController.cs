using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBettingRoulette.Dtos.Bet;
using OnlineBettingRoulette.Services.Bet;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class BetController : ControllerBase
    {
        private readonly IBetService _service;

        public BetController(IBetService service)
        {
            _service = service;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromHeader] string user,[FromBody] CreateBet createRequest)
        {
            if (user == null)
            {
                return Unauthorized("user is required");
            }
            ReadBet result = await _service.Create(createRequest, user);
            return Created("/api/v1/projects" + result.Id, new ApiResponse("Bet created.", result, 201));
        }
    }
}
