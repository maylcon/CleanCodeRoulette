using AutoMapper;
using OnlineBettingRoulette.Dtos.Roulette;
using OnlineBettingRoulette.Repositories.Roulette;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineBettingRoulette.Services.Roulette
{
    public class RouletteService : IRouletteService
    {
        private readonly IRouletteRepository _repository;
        private readonly IMapper _mapper;
        public RouletteService(IRouletteRepository repository, IMapper mapper) { 
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ReadRoulette> Create(CreateRoulette request)
        {
            Models.Roulette roulette = _mapper.Map<CreateRoulette, Models.Roulette>(request);
            roulette = await _repository.Create(roulette);
            ReadRoulette dto = _mapper.Map<Models.Roulette, ReadRoulette>(roulette);
            return dto;
        }

        public async Task<List<ReadRoulette>> GetAll()
        {
            var roulettes = await _repository.GetAll();
            List<ReadRoulette> dto = _mapper.Map<List<Models.Roulette>, List<ReadRoulette>>(roulettes);
            return dto;
        }

        public async Task<ReadRoulette> Open(Guid id)
        {
            var roulette = await _repository.Open(id);
            ReadRoulette dto = _mapper.Map<Models.Roulette, ReadRoulette>(roulette);
            return dto;
        }
    }
}
