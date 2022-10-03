using Microsoft.AspNetCore.Http;

namespace minahasa.sitimou.webapi.Models;

public class SimpanLaporan
{
    public int IdUser { get; set; }
    public string Perihal { get; set; }
    public string IsiLaporan { get; set; }
    public double GpsLat { get; set; }
    public double GpsLng { get; set; }
    public IFormFile FileFoto { get; set; }
}

public class SimpanPanik
{
    public int IdUser { get; set; }
    public double GpsLat { get; set; }
    public double GpsLng { get; set; }
}


public class SimpanProsesLaporan
{
    public int IdDisposisi { get; set; }
    public int IdUser { get; set; }
    public int IdOpd { get; set; }
    public int IdLaporan { get; set; }
    public string JenisLaporan { get; set; }
    public string Judul { get; set; }
    public string Uraian { get; set; }
    public string Status { get; set; }
    
    public IFormFile FileFoto { get; set; }
}




