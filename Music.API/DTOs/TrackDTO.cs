namespace Music.API.DTOs
{
    public class TrackDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistDTO Artist { get; set; }
    }
}
