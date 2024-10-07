using Microsoft.Extensions.Logging;
using SpanLeague.BusinessLogic.Helpers.Implementation;
using SpanLeague.BusinessLogic.Helpers;
using Moq;

namespace SpanLeague.Tests.UnitTests.Helpers
{
    public class FileManagerTests
    {
        private readonly Mock<ILogger<IFileManager>> _mockLogger;
        private readonly FileManager _fileManager;

        public FileManagerTests()
        {
            _mockLogger = new Mock<ILogger<IFileManager>>();
            _fileManager = new FileManager(_mockLogger.Object);
        }

        [Fact]
        public void ReadTextFile_ShouldReturnFileContent_WhenFileExists()
        {
            var filePath = "test.txt";
            var expectedContent = new[] { "line1", "line2", "line3" };

            File.WriteAllLines(filePath, expectedContent);

            var result = _fileManager.ReadTextFile(filePath);

            Assert.Equal(expectedContent, result);
        }

        [Fact]
        public void ReadTextFile_ShouldThrowException_WhenFileNotFound()
        {
            var filePath = "nonexistent.txt";

            var exception = Assert.Throws<FileNotFoundException>(() => _fileManager.ReadTextFile(filePath));
        }
    }
}
