using AutoMapper;
using FluentValidation;
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
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;
        public TrackController(ITrackService trackService, IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDTO>>> GetAllTracks()
        {
            var tracks = await _trackService.GetAllWithArtistAsync();
            var tracksDTO = _mapper.Map<IEnumerable<TrackDTO>>(tracks);
            return Ok(tracksDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDTO>> GetMusicById(int id)
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            var trackDTO = _mapper.Map<Track, TrackDTO>(track);
            return Ok(trackDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TrackDTO>> CreateTrack([FromBody]SaveTrackDTO saveTrackDTO)
        {
            var validator = new SaveTrackResourcesValidator();
            var validationResult = await validator.ValidateAsync(saveTrackDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var trackToCreate = _mapper.Map<SaveTrackDTO, Track>(saveTrackDTO);
            var newTrack = await _trackService.CreateTrackAsync(trackToCreate);
            var trackResource = _mapper.Map<Track, TrackDTO>(newTrack);
            return Ok(trackResource);
        }

        [HttpPut("{oldTrackId}")]
        public async Task<ActionResult<TrackDTO>> UpdateTrack([FromBody]SaveTrackDTO newSaveTrackDTO, int oldTrackId)
        {
            var validator = new SaveTrackResourcesValidator();
            var validationResult = await validator.ValidateAsync(newSaveTrackDTO);
            var requestIsInvalid = oldTrackId == 0 || !validationResult.IsValid;
            if (requestIsInvalid)
            {
                return BadRequest(validationResult.Errors);
            }
            var oldTrack = await _trackService.GetTrackByIdAsync(oldTrackId);
            if (oldTrack == null)
            {
                return NotFound();
            }
            var newTrack = _mapper.Map<SaveTrackDTO, Track>(newSaveTrackDTO);
            await _trackService.UpdateTrackAsync(oldTrack,newTrack);
            var updatedTrack = await _trackService.GetTrackByIdAsync(oldTrackId);
            var updatedTrackDTO = _mapper.Map<Track, TrackDTO>(updatedTrack);
            return Ok(updatedTrackDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var trackToDelete = await _trackService.GetTrackByIdAsync(id);
            if (trackToDelete == null)
            {
                return NotFound();
            }
            await _trackService.DeleteTrackAsync(trackToDelete);
            return NoContent();
        }

    }
}
