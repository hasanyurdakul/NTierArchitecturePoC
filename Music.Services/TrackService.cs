using Music.Core;
using Music.Core.Model;
using Music.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Services
{
    public class TrackService : ITrackService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TrackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Track> CreateTrackAsync(Track newTrack)
        {
            await _unitOfWork.Tracks.AddAsync(newTrack);
            return newTrack;
        }

        public async Task DeleteTrackAsync(Track track)
        {
            _unitOfWork.Tracks.Remove(track);
        }

        public async Task<IEnumerable<Track>> GetAllWithArtistAsync()
        {
            return await _unitOfWork.Tracks.GetAllWithArtistAsync();
        }

        public async Task<Track> GetTrackByIdAsync(int id)
        {
            return await _unitOfWork.Tracks.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Track>> GetTracksByArtistIdAsync(int artistId)
        {
            return await _unitOfWork.Tracks.GetAllWithArtistByArtistIdAsync(artistId);
        }

        public Task UpdateTrackAsync(Track trackToBeUpdated, Track track)
        {
            trackToBeUpdated.Name = track.Name;
            trackToBeUpdated.ArtistId = track.ArtistId;
            return Task.CompletedTask;
        }
    }
}
