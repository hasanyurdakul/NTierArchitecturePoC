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
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private TrackDbContext _context
        {
            get { return _context as TrackDbContext; }
        }
        public ArtistRepository(TrackDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Artist>> GetAllWithMusicAsync()
        {
            return await _context.Artists.Include(a => a.Tracks).ToListAsync();
        }

        public Task<Artist> GetWithMusicByIdAsync(int id)
        {
            return _context.Artists.Include(a => a.Tracks).SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}
