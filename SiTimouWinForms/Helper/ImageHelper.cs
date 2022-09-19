using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace gov.minahasa.sitimou.Helper
{
    internal class ImageHelper
    {
        #region === Image Converter ===

        public byte[] ImageToByte(Image image, ImageFormat imageFormat)
        {
            using var ms = new MemoryStream();
            image.Save(ms, imageFormat);
            var bytes = ms.ToArray();

            return bytes;
        }

        public Bitmap BytesToImage(byte[] blobData)
        {
            using var ms = new MemoryStream();
            ms.Write(blobData, 0, Convert.ToInt32(blobData.Length));
            var bmp = new Bitmap(ms, false);

            return bmp;
        }

        #endregion

        #region === Image Processing ===

        public Bitmap ResizeImage(Image image, int persen)
        {
            var destW = image.Width - (image.Width * ((double)persen / 100));
            var destH = image.Height - (image.Height * ((double)persen / 100));

            var destRect = new Rectangle(0, 0, (int)destW, (int)destH);
            var destImage = new Bitmap((int)destW, (int)destH);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public Image ResizeImage2(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public Image ScaleImage(Image image, int maxWidth, int maxHeight, bool highQuality = true)
        {

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);
            //var ratio = Math.Min(maxWidth / image.Width, maxHeight / image.Height);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            // Convert image
            var newImage = new Bitmap(newWidth, newHeight, image.PixelFormat); //PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                if (highQuality)
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    //g.InterpolationMode = InterpolationMode.High; //.HighQualityBicubic;
                    //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                }

                //g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.DrawImage(image, ((int)maxWidth - newWidth) / 2, ((int)maxHeight - newHeight) / 2, newWidth, newHeight);
            }

            return newImage;

        }

        public Bitmap CropCircle(Image img)
        {
            var roundedImage = new Bitmap(img.Width, img.Height, img.PixelFormat);

            using (var g = Graphics.FromImage(roundedImage))
            using (var gp = new GraphicsPath())
            {
                g.Clear(Color.Transparent);

                g.SmoothingMode = SmoothingMode.AntiAlias;

                Brush brush = new TextureBrush(img);
                gp.AddEllipse(0, 0, img.Width, img.Height);
                g.FillPath(brush, gp);
            }

            return roundedImage;
        }

        public Image RoundCorners(Image image, int cornerRadius)
        {
            cornerRadius *= 2;
            Bitmap roundedImage = new Bitmap(image.Width, image.Height);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddArc(0, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.SetClip(gp);
                g.DrawImage(image, Point.Empty);
            }
            return roundedImage;
        }

        #endregion
    }
}
