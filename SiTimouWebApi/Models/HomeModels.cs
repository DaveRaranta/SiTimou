namespace minahasa.sitimou.webapi.Models;

public class UpdateFotoPengguna
{
    public int IdUser { get; set; }
    public IFormFile FileFoto { get; set; }
}

public class UpdateProfil
{
    public int IdUser { get; set; }
    public string Nik { get; set; }
    public string NamaLengkap { get; set; }
    public string TanggalLahir { get; set; }
    public string JenisKelamin { get; set; }
    public string Alamat { get; set; }
    public int IdDesa { get; set; }
    public int IdKecamatan { get; set; }
    public string NoTelp { get; set; }
}