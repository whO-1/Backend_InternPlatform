using System;
using System.Drawing;
using System.Drawing.Imaging;


namespace internPlatform.Application.Helpers
{
    public static class ImageHelper
    {
        public static void CreateThumbnail(string originalImagePath, string thumbnailPath, int thumbnailWidth, int thumbnailHeight)
        {
            using (var image = Image.FromFile(originalImagePath))
            {
                var thumbnail = image.GetThumbnailImage(thumbnailWidth, thumbnailHeight, () => false, IntPtr.Zero);
                thumbnail.Save(thumbnailPath, ImageFormat.Jpeg);
            }
        }
    }
}
