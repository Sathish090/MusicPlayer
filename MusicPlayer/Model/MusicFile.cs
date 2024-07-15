namespace MusicPlayer.Model
{
    public class MusicFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public byte[] Data { get; set; }
    }
    public class MusicMetadata
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public double Duration { get; set; }
        
    }

}
