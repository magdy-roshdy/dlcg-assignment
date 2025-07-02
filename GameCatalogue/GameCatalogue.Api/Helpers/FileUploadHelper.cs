namespace GameCatalogue.Api.Helpers
{
    public static class FileUploadHelper
    {
        //2MB
        private const long MaxFileSize = 2 * 1024 * 1024;
        private static readonly string[] AllowedTypes = { "image/png", "image/jpeg" };

        public static (bool IsValid, string? ErrorMessage, Stream? Stream, string? Extension)
        ProcessImageFile(IFormFile? file)
        {
            if (file == null)
                return (true, null, null, null);

            if (file.Length > MaxFileSize)
                return (false, "Image must be ≤ 2 MB.", null, null);

            if (!AllowedTypes.Contains(file.ContentType))
                return (false, "Only PNG or JPEG images are supported.", null, null);

            return (true, null, file.OpenReadStream(), Path.GetExtension(file.FileName));
        }
    }
}
