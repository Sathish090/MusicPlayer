using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Interface;
using MusicPlayer.Model;
using MusicPlayer.Repository;

namespace MusicPlayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicFileRepository musicFileRepository;

        public MusicController(IMusicFileRepository musicFileRepository)
        {
            this.musicFileRepository = musicFileRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var musicFile = await musicFileRepository.AddMusic(file);
            return Ok(musicFile);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var musicFile = await musicFileRepository.GetMusicById(id);
            if (musicFile == null)
                return NotFound();

            return File(musicFile.Data, musicFile.ContentType, musicFile.FileName);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IFormFile file)
        {
            if (id == 0)
                return BadRequest();

            var updatedMusicFile = await musicFileRepository.UpdateMusic(id, file);
            if (updatedMusicFile == null)
                return NotFound();

            return Ok(updatedMusicFile);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var musicFile = await musicFileRepository.RemoveMusic(id);
            if (musicFile == null)
                return NotFound();
            return Ok(musicFile);
        }
        [HttpGet]
        public async Task<ActionResult<MusicMetadata>> GetMusics()
        {
            return await musicFileRepository.GetMusicMetadata();
        }
    }
}
