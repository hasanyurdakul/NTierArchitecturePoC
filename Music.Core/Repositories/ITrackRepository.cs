using Music.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<IEnumerable<Track>> GetAllWithArtistAsync();
        Task<IEnumerable<Track>> GetAllWithArtistByArtistIdAsync(int artistId);
        Task<Track> GetWithArtistByIdAsync(int id);
    }
}
