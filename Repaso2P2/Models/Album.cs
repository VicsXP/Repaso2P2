namespace Repaso2P2.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string TituloAlbum { get; set; } = string.Empty;

        public string ArtistaAlbum { get; set; } = string.Empty;

        public DateTime FechaPublicacion { get; set; }

        public List<Cancion> Canciones { get; set; } = new();
    }
}
