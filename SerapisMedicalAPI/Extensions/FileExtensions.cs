using Microsoft.AspNetCore.StaticFiles;

namespace SerapisMedicalAPI.Extensions
{
    public static class FileExtensions
    {
        private static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

        public static string GetcontentType(this string fileName) 
        {
            if (!Provider.TryGetContentType(subpath: fileName, out var contentType)) 
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
