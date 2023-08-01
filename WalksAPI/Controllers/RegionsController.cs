using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using WalksAPI.CustomActionFilters;
using WalksAPI.Database.DomainModel;
using WalksAPI.Database.DTO;
using WalksAPI.DataModels;
using WalksAPI.DataModels.Domain;
using WalksAPI.Services.IRepository;
using WalksAPI.Services.Repository;
using System.Text.Json;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        // Get All
        [HttpGet]
        [Route("GetAllRegions")]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<ActionResult<List<RegionDto>>> GetAll()
        {
            try
            {
                logger.LogInformation("GetAll Invoked");

                // Making Custom EXCEPTION For Logger

                var regions = await regionRepository.GetAllAsync();

                logger.LogInformation($"Regions: {JsonSerializer.Serialize(regions)}");

                return Ok(mapper.Map<List<RegionDto>>(regions));

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }

        }


        // Get By Id
        [HttpGet]
        [Route("GetById")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null) return NotFound();

            return Ok(mapper.Map<RegionDto>(region));

        }

        // Create 
        [HttpPost]
        [Route("Create")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var region = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.CreateAsync(region);

            var regionDto = mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = region.Id }, regionDto);
        }

        // UPDATE
        [HttpPut]
        [ValidateModel]
        [Route("Update")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null) return NotFound();

            var regionsDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionsDto);
        }

        // DELETE
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null) return NotFound();

            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }
    }
}
