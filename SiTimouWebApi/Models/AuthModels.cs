namespace minahasa.sitimou.webapi.Models
{
    public class RegistrasiPengguna
    {
        public string UserNik { get; set; } = null!;
        public string UserNama { get; set; } = null!;
        public string UserPwd { get; set; } = null!;
    }

    public class MasukPenduduk
    {
        public string UserNik { get; set; } = null!;
        public string UserPwd { get; set; } = null!;
    }
    
    public  class LoginResult
    {
        public int IdUser { get; set; }
        public string Nik { get; set; } = null!;
        public byte[] PwdHash { get; set; } = null!;
        public byte[] PwdSalt { get; set; } = null!;
        public string Flg { get; set; } = null!;
    }
    
    public class MasukPegawai
    {
        public string UserLogin { get; set; } = null!;
        public string UserPwd { get; set; } = null!;
    }

    
    public  class LoginPegawaiResult
    {
        public int IdUser { get; set; }
        public string Login { get; set; } = null!;
        public byte[] PwdHash { get; set; } = null!;
        public byte[] PwdSalt { get; set; } = null!;
        public string Flg { get; set; } = null!;
    }
}

