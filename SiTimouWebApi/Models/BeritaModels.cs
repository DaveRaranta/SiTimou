namespace minahasa.sitimou.webapi.Models
{
    public class DataBerita
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int totalPages { get; set; }
        public List<DaftarBerita> data { get; set; }
    }

    public class DaftarBerita
    {
        public string id_berita { get; set; }
        public string judul_berita { get; set; }
        public string tgl_berita { get; set; }
        public string isi_berita { get; set; }
        public string cover_berita { get; set; }
    }

}

