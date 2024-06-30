using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.API.DTOs;
using Music.Core.Model;
using Music.Core.Services;

namespace Music.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;
        public TrackController(ITrackService trackService,IMapper mapper)
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
        public async Task<ActionResult<TrackDTO>> GetMusicById (int id){
            var track = await _trackService.GetTrackByIdAsync(id);
            var trackDTO = _mapper.Map<Track,TrackDTO>(track);
            return Ok(trackDTO);
        }



    }
}
