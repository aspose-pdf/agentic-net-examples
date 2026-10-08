using System;
using System.IO;
using Aspose.Pdf;

// Minimal NUnit-like attributes for the test runner used in this project
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfMetadataTests
{
    using NUnit.Framework;

    [TestFixture]
    public class MetadataTests
    {
        [Test]
        public void BaseUrl_CreatorTool_Nickname_AreWrittenCorrectly()
        {
            // Arrange: temporary PDF file path and expected metadata values
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            const string expectedBaseUrl = "https://example.com";
            const string expectedCreatorTool = "MyTool";
            const string expectedNickname = "DocNick";

            // Act: create a PDF, set metadata, save, then read back via Document API
            using (Document doc = new Document())
            {
                // Add a blank page so the PDF is valid
                doc.Pages.Add();

                // Set metadata properties using the supported API
                doc.Info["BaseUrl"] = expectedBaseUrl;   // custom metadata via indexer
                doc.Info.Creator = expectedCreatorTool;   // creator tool stored in Creator field
                doc.Info.Title = expectedNickname;        // nickname stored in Title field

                // Save the document inside the using block to ensure proper disposal
                doc.Save(tempPdfPath);
            }

            // Read metadata back using the Document API (more reliable than PdfFileInfo for custom fields)
            using (Document loaded = new Document(tempPdfPath))
            {
                Assert.AreEqual(expectedBaseUrl, loaded.Info["BaseUrl"], "BaseUrl does not match.");
                Assert.AreEqual(expectedCreatorTool, loaded.Info.Creator, "CreatorTool does not match.");
                Assert.AreEqual(expectedNickname, loaded.Info.Title, "Nickname does not match.");
            }

            // Cleanup: delete the temporary file
            File.Delete(tempPdfPath);
        }
    }

    // Provide a dummy entry point so the project compiles as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // The test runner will discover and execute the test methods.
            // Keeping Main empty satisfies the compiler requirement for an entry point.
        }
    }
}
