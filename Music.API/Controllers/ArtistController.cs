using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.API.DTOs;
using Music.API.Validators;
using Music.Core.Model;
using Music.Core.Services;

namespace Music.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;
        
        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();
            var artistsDTO = _mapper.Map<IEnumerable<ArtistDTO>>(artists);
            return Ok(artistsDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            var artistDTO = _mapper.Map<Artist, ArtistDTO>(artist);
            return Ok(artistDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> CreateArtist([FromBody] SaveArtistDTO saveArtistDTO)
        {
            var validator = new SaveArtistResourcesValidator();
            var validationResult = await validator.ValidateAsync(saveArtistDTO);
            if (!validationResult.IsValid) {
                return BadRequest();
            }
            var artistToCreate = _mapper.Map<SaveArtistDTO, Artist>(saveArtistDTO);
            var newArtist = await _artistService.CreateArtist(artistToCreate);
            var artistResource = _mapper.Map<Artist, ArtistDTO>(newArtist);
            return Ok(artistResource);
        }

        [HttpPut("{oldArtistId}")]
        public async Task<ActionResult<ArtistDTO>> UpdateArtist([FromBody] SaveArtistDTO newSaveArtistDTO, int oldArtistId)
        {
            var newArtist = _mapper.Map<SaveArtistDTO, Artist>(newSaveArtistDTO);
            var oldArtist = await _artistService.GetArtistById(oldArtistId);
            await _artistService.UpdateArtist(oldArtist, newArtist);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            await _artistService.DeleteArtist(artist);
            return NoContent();
        }
    }
}
