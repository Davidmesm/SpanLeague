using Microsoft.Extensions.Logging;

namespace SpanLeague.BusinessLogic.Helpers.Implementation
{
    public class FileManager : IFileManager
    {
        private readonly ILogger<IFileManager> _logger;

        public FileManager(ILogger<IFileManager> logger)
        {
            _logger = logger;
        }

        public string[] ReadTextFile(string path)
        {
            try
            {
                _logger.LogInformation($"Processing: {path}");
                return File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading input file.");
                throw;
            }
        }
    }
}
