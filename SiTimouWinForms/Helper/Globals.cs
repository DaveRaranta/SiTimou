using System.Drawing;
using System.Globalization;

namespace gov.minahasa.sitimou.Helper
{
    internal class Globals
    {
        // APP
        public static string ApiToken;
        public static string ApiAppBaseUrl;

        // Const
        public static Color PrimaryBgColor = Color.FromArgb(255, 245, 245, 245);
        public static Color PrimaryButtonBar = Color.FromArgb(255, 255, 255, 255);
        public static CultureInfo CultureInfo = CultureInfo.CreateSpecificCulture("id-ID");

        // User
        public static int? UserId { get; set; } = null;
        // public static string UserNik { get; set; } = null;
        public static string UserNip { get; set; } = null;
        public static string UserPwd { get; set; } = null;
        public static string UserNamaLengkap { get; set; } = null;
        public static int? UserOpdId { get; set; } = null;
        public static string UserOpdSingkat { get; set; } = null;
        public static string UserOpdLengkap { get; set; } = null;
        public static string UserGrup { get; set; } = null;
        public static string UserJabatan { get; set; } = null;
        public static bool UserOpdAdmin { get; set; } = false;

        public static byte[] UserFotoProfile { get; set; } = null;
    }
}
