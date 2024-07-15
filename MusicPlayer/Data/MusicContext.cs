using Microsoft.EntityFrameworkCore;
using MusicPlayer.Model;

namespace MusicPlayer.Data
{
    public class MusicContext : DbContext
    {
        public MusicContext() { }
        public MusicContext(DbContextOptions<MusicContext> option) : base(option)
        {

        }
        public DbSet<MusicFile> MusicFiles { get; set; }        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

             => optionsBuilder.UseSqlServer("Data Source=.;" +
                                             "Initial Catalog=MusicPlayer;" +
                                             "Integrated Security=True;" +
                                             "TrustServerCertificate=True;");
    }
}
