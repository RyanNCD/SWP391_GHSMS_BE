namespace Repository.Util
{
    public static class CommonUtil
    {
        private const string BaseUrl = "https://meet.jit.si/";
        public static async Task<string> CreateMeetingUrl()
        {
            var url = BaseUrl + Guid.NewGuid();
            return url;
        }

        public static string HTMLLoading(string fileName)
        {
            var htmlAssembly = typeof(HTMLEmail.HTMLEmail.EmailTemplateMarker).Assembly;

            var resourceName = htmlAssembly.GetManifestResourceNames()
                .FirstOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new FileNotFoundException($"Không tìm thấy resource: {fileName}");

            using var stream = htmlAssembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException("Stream is null"));
            return reader.ReadToEnd();
        }
    }
}
