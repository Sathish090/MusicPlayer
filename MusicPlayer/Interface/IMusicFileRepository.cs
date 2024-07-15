using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MusicPlayer.Model;

namespace MusicPlayer.Interface
{
    public interface IMusicFileRepository
    {
        Task<MusicFile> AddMusic(IFormFile file);
        Task<MusicFile> GetMusicById(int id);
        Task<MusicFile> UpdateMusic(int id, IFormFile file);
        Task<MusicFile> RemoveMusic(int id);

        Task<MusicMetadata> GetMusicMetadata();
    }
}
