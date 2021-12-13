using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBettingRoulette.Dtos.Roulette;
using OnlineBettingRoulette.Services.Roulette;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;

namespace OnlineBettingRoulette.Controllers
{
    [ApiController]
    [Route("/api/roulette")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService _service;

        public RouletteController(IRouletteService service)
        {
            _service = service;
        }

        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRoulette createRequest)
        {
            ReadRoulette result = await _service.Create(createRequest);
            return Created("/api/v1/projects"+ result.Id, new ApiResponse("Roulette created.", result, 201));
        }
    }
}
