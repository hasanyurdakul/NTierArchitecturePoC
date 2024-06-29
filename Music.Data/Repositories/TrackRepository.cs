using Microsoft.EntityFrameworkCore;
using Music.Core.Model;
using Music.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Data.Repositories
{
    public class TrackRepository : Repository<Track>, ITrackRepository
    {
        private TrackDbContext _context
        {
            get { return _context as TrackDbContext; }
        }
        public TrackRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Track>> GetAllWithArtistAsync()
        {
            return await _context.Tracks.Include(m => m.Artist).ToListAsync();
        }

        public async Task<IEnumerable<Track>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await _context.Tracks.Include(m => m.Artist).Where(m => m.ArtistId == artistId).ToListAsync();
        }

        public async Task<Track> GetWithArtistByIdAsync(int id)
        {
            return await _context.Tracks.Include(m => m.Artist).SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
