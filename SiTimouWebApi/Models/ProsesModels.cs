namespace minahasa.sitimou.webapi.Models;

public class DetailLaporanFr
{
    public string JenisLaporan { get; set; }
    public int IdLaporan { get; set; }
}

public class BatalLaporan
{
    public int IdUser { get; set; }
    public int IdLaporan { get; set; }
}

public class ProsesLaporan
{
    public int IdDisposisi { get; set; }
    public int IdUser { get; set; }
    public int IdLaporan { get; set; }
    public string JenisLaporan { get; set; }
    public string Judul { get; set; }
    public string Uraian { get; set; }
    public string Status { get; set; }
    public IFormFile FileFoto { get; set; }
}

public class RiwayatProsesLaporan
{
    public string  JenisData { get; set; }
    public int IdUser { get; set; }
}