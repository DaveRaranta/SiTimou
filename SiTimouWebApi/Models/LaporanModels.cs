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