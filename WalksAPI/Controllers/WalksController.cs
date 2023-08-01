using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using WalksAPI.CustomActionFilters;
using WalksAPI.Database.DomainModel;
using WalksAPI.Database.DTO;
using WalksAPI.Database.DTOModel;
using WalksAPI.DataModels.Domain;
using WalksAPI.Services.IRepository;
using WalksAPI.Services.Repository;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        public IMapper Mapper { get; }
        public IWalkRepository WalkRepository { get; }

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            Mapper = mapper;
            WalkRepository = walkRepository;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            var walk = Mapper.Map<Walk>(addWalksRequestDto);

            await WalkRepository.CreateAsync(walk);

            return Ok(Mapper.Map<WalkDto>(walk));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
                [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
                [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var walks = await WalkRepository.GetAllAsync(filterOn, filterQuery, sortBy,
                                isAscending ?? true, pageNumber, pageSize);

            return Ok(Mapper.Map<List<WalkDto>>(walks));
        }

        [HttpGet]
        [Route("/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var walk = await WalkRepository.GetByIdAsync(id);

            if (walk == null) return NotFound();

            return Ok(Mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [ValidateModel]
        [Route("/{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var walk = Mapper.Map<Walk>(updateRegionRequestDto);

            var walkToUpdate = await WalkRepository.UpdateAsync(id, walk);

            if (walkToUpdate == null) return NotFound();

            return Ok(Mapper.Map<WalkDto>(walkToUpdate));
        }

        [HttpDelete]
        [Route("/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var walk = await WalkRepository.DeleteAsync(id);

            if(walk == null) return NotFound();

            return Ok(Mapper.Map<WalkDto>(walk));
        }
    }
}
