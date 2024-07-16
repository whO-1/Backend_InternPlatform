using System.Web;
using System.Web.Mvc;


namespace internPlatform.Application.Extensions
{
    public static class UrlHelperExtensions
    {
        public static IHtmlString ScriptVersioned(this UrlHelper urlHelper, string contentPath)
        {
            var filePath = HttpContext.Current.Server.MapPath(contentPath);
            var version = System.IO.File.GetLastWriteTime(filePath).Ticks.ToString();
            var versionedUrl = $"{urlHelper.Content(contentPath)}?v={version}";
            return new HtmlString($"<script src=\"{versionedUrl}\"></script>");
        }

        public static IHtmlString CssVersioned(this UrlHelper urlHelper, string contentPath)
        {
            var filePath = HttpContext.Current.Server.MapPath(contentPath);
            var version = System.IO.File.GetLastWriteTime(filePath).Ticks.ToString();
            var versionedUrl = $"{urlHelper.Content(contentPath)}?v={version}";
            return new HtmlString($"<link href=\"{versionedUrl}\" rel=\"stylesheet\" />");
        }
    }
}
