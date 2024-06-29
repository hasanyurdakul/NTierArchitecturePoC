using Music.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITrackRepository Tracks { get; }
        IArtistRepository Artists { get; }
        Task<int> CommitAsync();
    }
}
