using Repaso2P2.Models;
using System.Text.Json;

namespace Repaso2P2.Services
{
    public class AlbumService
    {
        private readonly string filePath =
            Path.Combine("wwwroot", "data", "albums.json");

        // Todos los albums
        public List<Album> ObtenerAlbums()
        {
            if (!File.Exists(filePath))
            {
                return new List<Album>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Album>();
            }

            return JsonSerializer.Deserialize<List<Album>>(json)
                   ?? new List<Album>();
        }

        // Guardar new album
        public void GuardarAlbum(Album album)
        {
            List<Album> albums = ObtenerAlbums();
            if (albums.Count > 0)
            {
                album.Id = albums.Max(a => a.Id) + 1;
            }
            else
            {
                album.Id = 1;
            }

            albums.Add(album);

            GuardarTodos(albums);
        }

        // Editar
        public void EditarAlbum(Album albumEditado)
        {
            List<Album> albums = ObtenerAlbums();

            Album? album =
                albums.FirstOrDefault(a => a.Id == albumEditado.Id);

            if (album != null)
            {
                album.TituloAlbum = albumEditado.TituloAlbum;
                album.ArtistaAlbum = albumEditado.ArtistaAlbum;
                album.FechaPublicacion = albumEditado.FechaPublicacion;
                album.Canciones = albumEditado.Canciones;

                GuardarTodos(albums);
            }
        }

        // Buscar por Artista
        public List<Album> BuscarPorArtista(string artista)
        {
            List<Album> albums = ObtenerAlbums();

            return albums
                .Where(a =>
                    a.ArtistaAlbum.Contains(
                        artista,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Guardar Json
        private void GuardarTodos(List<Album> albums)
        {
            string json = JsonSerializer.Serialize(
                albums,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
        }
    }
}
