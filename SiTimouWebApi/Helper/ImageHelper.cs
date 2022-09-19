using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace minahasa.sitimou.webapi.Helper
{
    public class ImageHelper
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

        public static Bitmap CropCircle(Image img)
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

        public static Image RoundCorners(Image startImage, float cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            var roundedImage = new Bitmap(startImage.Width, startImage.Height);

            using (var g = Graphics.FromImage(roundedImage))
            {
                g.Clear(backgroundColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (Brush brush = new TextureBrush(startImage))
                {
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddArc(0, -1, cornerRadius, cornerRadius, 180, 90);
                        gp.AddArc(-1 + roundedImage.Width - cornerRadius, -1, cornerRadius, cornerRadius, 270, 90);
                        gp.AddArc(-1 + roundedImage.Width - cornerRadius, -1 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                        gp.AddArc(0, -1 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);

                        g.FillPath(brush, gp);
                    }
                }

                return roundedImage;
            }
        }



        public static Image ScaleImage(Image image, int maxWidth, int maxHeight, bool highQuality = true)
        {

            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            // Convert image
            // Bitmap newImage = new System.Drawing.Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            var newImage = new Bitmap(newWidth, newHeight, image.PixelFormat);

            using (var g = Graphics.FromImage(newImage))
            {
                if (highQuality)
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                }

                // g.DrawImage(image, 0, 0, newWidth, newHeight);
                g.DrawImage(image, ((int)maxWidth - newWidth) / 2, ((int)maxHeight - newHeight) / 2, newWidth, newHeight);
            }

            return newImage;

        }

        // NOT USED?

        public static Image RoundCorners2(Image startImage, int cornerRadius, Color backgroundColor)
        {
            cornerRadius *= 2;
            var roundedImage = new Bitmap(startImage.Width, startImage.Height);

            using (var g = Graphics.FromImage(roundedImage))
            {
                g.Clear(backgroundColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (Brush brush = new TextureBrush(startImage))
                {
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddArc(-1, -1, cornerRadius, cornerRadius, 180, 90);
                        gp.AddArc(0 + roundedImage.Width - cornerRadius, -1, cornerRadius, cornerRadius, 270, 90);
                        gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                        gp.AddArc(-1, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);

                        g.FillPath(brush, gp);
                    }
                }

                return roundedImage;
            }
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

        public static Image ScaleCropImage(Image image, int width, int height, bool needToFill)
        {

            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale;
            double nScaleW;
            double nScaleH;

            nScaleW = ((double)width / (double)sourceWidth);
            nScaleH = ((double)height / (double)sourceHeight);

            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (height - sourceHeight * nScale) / 2;
                destX = (width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);


            Bitmap bmPhoto;
            try
            {
                bmPhoto = new Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"destWidth:{destWidth}, destX:{destX}, destHeight:{destHeight}, desxtY:{destY}, Width:{width}, Height:{height}", ex);
            }
            using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;

                var to = new Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                var from = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }

        public Image ClipToCircle(Image srcImage, PointF center, float radius, Color backGround)
        {
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);

            using (Graphics g = Graphics.FromImage(dstImage))
            {
                RectangleF r = new RectangleF(center.X - radius, center.Y - radius,
                    radius * 2, radius * 2);

                // enables smoothing of the edge of the circle (less pixelated)
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // fills background color
                using (Brush br = new SolidBrush(backGround))
                {
                    g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                }

                // adds the new ellipse & draws the image again 
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(r);
                g.SetClip(path);
                g.DrawImage(srcImage, 0, 0);

                return dstImage;
            }
        }

        public static Image CropToCircle(Image srcImage, Color backGround)
        {
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
            Graphics g = Graphics.FromImage(dstImage);
            using (Brush br = new SolidBrush(backGround))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, dstImage.Width, dstImage.Height);
            g.SetClip(path);
            g.DrawImage(srcImage, 0, 0);

            return dstImage;
        }




        #endregion
    }
}
