using Music.Core;
using Music.Core.Repositories;
using Music.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TrackDbContext _context;
        private TrackRepository _trackRepository;
        private ArtistRepository _artistRepository;

        public UnitOfWork(TrackDbContext context)
        {
            _context = context; 
        }

        public ITrackRepository Tracks => _trackRepository = _trackRepository ?? new TrackRepository(_context);

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
