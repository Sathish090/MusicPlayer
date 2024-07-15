using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.Data;
using MusicPlayer.Interface;
using MusicPlayer.Model;
using TagLib;

namespace MusicPlayer.Repository
{
    public class MusicFileRepository : IMusicFileRepository
    {
        private MusicContext _context { get; set; }
        public MusicFileRepository(MusicContext context)
        {

            _context = context;
        }


        public async Task<MusicFile> AddMusic(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");

            var musicFile = new MusicFile
            {
                FileName = file.FileName,
                ContentType = file.ContentType


            };

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                musicFile.Data = memoryStream.ToArray();
            }

            var add = await _context.MusicFiles.AddAsync(musicFile);
            await _context.SaveChangesAsync();

            return add.Entity;
        }


        public Task<MusicFile> GetAllMusic()
        {
            throw new NotImplementedException();
        }

        public async Task<MusicFile> GetMusicById(int id)
        {
            var musicFile = await _context.MusicFiles.FirstOrDefaultAsync(p => p.Id == id);
            if (musicFile == null)
            {
               
                throw new KeyNotFoundException($"Music file with ID {id} not found.");
            }
            return musicFile;
        }
            

        public async Task<MusicFile> RemoveMusic(int id)
        {
            var musicFile = await _context.MusicFiles.FindAsync(id);
            if (musicFile == null)
                return null;

            _context.MusicFiles.Remove(musicFile);
            await _context.SaveChangesAsync();

            return musicFile;
        }

        public async Task<MusicFile> UpdateMusic(int id, IFormFile file)
        {
            var existingMusicFile = await _context.MusicFiles.FindAsync(id);
            if (existingMusicFile == null)
                return null;

            existingMusicFile.FileName = file.FileName;
            existingMusicFile.ContentType = file.ContentType;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                existingMusicFile.Data = memoryStream.ToArray();
            }
            var result = _context.MusicFiles.Update(existingMusicFile);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<MusicMetadata> GetMusicMetadata()
        {
            try
            {
                 var tagFile = TagLib.File.Create(@"C:\Users\sselvam\Downloads\Adangaatha Asuran.mp3");

                    if (tagFile != null && tagFile.Tag != null)
                    {
                        return new MusicMetadata
                        {
                            Title = tagFile.Tag.Title,
                            Artist = tagFile.Tag.FirstAlbumArtist,
                            Album = tagFile.Tag.Album,
                            Duration = tagFile.Properties.Duration.TotalMinutes,
                            
                        };
                    }
                    else
                    {
                        throw new ApplicationException("Failed to read metadata from the file.");
                    }
                
            }
           
            catch (Exception ex)
            {
                throw new ApplicationException("Error reading metadata", ex);
            }
        }
    }
}
