using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBettingRoulette.Dtos.Roulette;
using OnlineBettingRoulette.Services.Roulette;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using System.Collections.Generic;
using System;

namespace OnlineBettingRoulette.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
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
            return Created("/api/v1/projects" + result.Id, new ApiResponse("Roulette created.", result, 201));
        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            List<ReadRoulette> result = await _service.GetAll();
            return Ok(new ApiResponse("Roulettes list.", result, 200));
        }

        [HttpPut("open/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            ReadRoulette result = await _service.Open(id);
            if(result == null)
            {
                return BadRequest(new ApiResponse("roulette not exists.", result, 400));
            }

            return Ok(new ApiResponse("Roulette open.", result, 200));
        }

        [HttpPut("close/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Close([FromRoute] Guid id)
        {
            ReadRoulette result = await _service.Close(id);
            if (result == null)
            {
                return BadRequest(new ApiResponse("roulette is not open or is close.", result, 400));
            }


            return Ok(new ApiResponse("Roulette open.", result, 200));
        }
    }
}
