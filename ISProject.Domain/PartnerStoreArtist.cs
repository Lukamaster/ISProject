using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISProject.Domain
{
    public class PartnerStoreArtist
    {
        public string Name { get; set; }
        public string ArtistImage { get; set; }
        public List<PartnerStoreAlbum> Albums { get; set; }
        public string Id { get; set; }
    }

    public class PartnerStoreAlbum
    {
        public string AlbumName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AlbumImage { get; set; }
        public double Rating { get; set; }
        public decimal Price { get; set; }
        public string ArtistId { get; set; }
        public PartnerStoreArtist Artist { get; set; }
        public List<PartnerStoreTrack> Tracks { get; set; }
        public string Id { get; set; }
    }

    public class PartnerStoreTrack
    {
        public string TrackName { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string AlbumId { get; set; }
        public double Rating { get; set; }
        public PartnerStoreArtist Artist { get; set; }
        public object TracksInPlaylist { get; set; }
        public string Id { get; set; }
    }
}
