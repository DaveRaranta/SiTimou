using Microsoft.AspNetCore.Http;
namespace minahasa.sitimou.webapi.Models;

public class SimpanLokasi
{
    public string JenisLokasi { get; set; }
    public string NamaLokasi { get; set; }
    public string Alamat { get; set; }
    public int IdDesa { get; set; }
    public int IdKecamatan { get; set; }
    public string? NoTelp { get; set; } = null;
    public double? GpsLat { get; set; } = null;
    public double? GpsLng { get; set; } = null;
    public string? Keterangan { get; set; } = null;
    public IFormFile FileFoto { get; set; }
}