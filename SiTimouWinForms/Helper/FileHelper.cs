using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace gov.minahasa.sitimou.Helper
{
    internal class FileHelper
    {

        // Convert Byte to Stream
        // (Untuk umum, seperti PDF dan data byte lainnya)
        public Stream BytesToStream(byte[] blobData)
        {
            var ms = new MemoryStream();
            ms.Write(blobData, 0, Convert.ToInt32(blobData.Length));

            return ms;
        }

        // Convert File ke Byte
        public byte[] FileToBytes(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var br = new BinaryReader(fs))
                {
                    var bytes = br.ReadBytes((int)fs.Length);
                    return bytes;
                }
            }
        }

        public bool BytesToFile(byte[] bytes, string namaFile)
        {
            try
            {
                using (var fs = new FileStream(namaFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var bw = new BinaryWriter(fs))
                    {
                        bw.Write(bytes);
                        bw.Flush();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("FileHelper", @"FileHelper", MethodBase.GetCurrentMethod()?.Name, e);

                return false;
            }
        }

        public bool BytesToFile2(byte[] blobData, string namaFile)
        {
            try
            {
                using var fs = new FileStream(namaFile, FileMode.Create, FileAccess.Write);
                var arrSize = new int();
                arrSize = blobData.GetUpperBound(0);

                fs.Write(blobData, 0, arrSize);

                Debug.WriteLine($"[Helper] > BytesToFile [RESULT] == {namaFile}");


                return true;
            }
            catch (Exception e)
            {
                DebugHelper.ShowError("FileHelper", @"FileHelper", MethodBase.GetCurrentMethod()?.Name, e);

                return false;
            }

        }

        public string FileSize(string file)
        {
            var value = new FileInfo(file).Length;

            string[] sizeSuffixes =
                { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

            if (value < 0) { return "-"; }

            var i = 0;
            var dValue = (decimal)value;
            while (Math.Round(dValue / 1024) >= 1)
            {
                dValue /= 1024;
                i++;
            }

            //return string.Format("{0:n1} {1}", dValue, SizeSuffixes[i]);
            return $"{dValue:n1} {sizeSuffixes[i]}";
        }

        public static string TipeFile(string fileExt)
        {
            switch (fileExt.ToLower())
            {
                case "doc":
                case "docx":
                    return $"Word Document|*.{fileExt}";

                case "xls":
                case "xlsx":
                    return $"Excel Document|*.{fileExt}";

                case "pdf":
                    return $"PDF Document|*.{fileExt}";

                case "jpg":
                case "jpeg":
                    return $"JPEG Image|*.{fileExt}";

                case "png":
                    return $"PNG Image|*.{fileExt}";

                default:
                    return null;
            }
        }
    }
}
