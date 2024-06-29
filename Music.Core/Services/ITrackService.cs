using Music.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<Track>> GetAllWithArtistAsync();
        Task<Track> GetTrackByIdAsync(int id);
        Task<IEnumerable<Track>> GetTracksByArtistIdAsync(int artistId);
        Task<Track> CreateTrackAsync(Track newTrack);
        Task UpdateTrackAsync(Track trackToBeUpdated, Track track);
        Task DeleteTrackAsync(Track track);
    }
}
