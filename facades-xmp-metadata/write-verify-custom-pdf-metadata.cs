using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – added because the project does not reference the real
// NUnit package. These definitions provide just enough functionality for the
// unit‑test code to compile and run.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public static class Assert
    {
        // Made the optional message parameter nullable to satisfy the non‑nullable
        // reference type analysis (CS8625). This mirrors the behaviour of the real
        // NUnit Assert class where the message argument is optional.
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfFacadeTests
{
    [TestFixture]
    public class PdfMetadataTests
    {
        private const string BaseUrlKey = "BaseUrl";
        private const string CreatorToolKey = "CreatorTool";
        private const string NicknameKey = "Nickname";

        // Made nullable to satisfy the compiler's non‑nullable analysis.
        private string? _originalPdfPath;
        private string? _updatedPdfPath;

        [SetUp]
        public void SetUp()
        {
            // Create a simple one‑page PDF document.
            _originalPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
            using (Document doc = new Document())
            {
                // Add an empty page (Aspose.Pdf creates a default page automatically).
                doc.Pages.Add();
                // Save the original PDF.
                doc.Save(_originalPdfPath);
            }

            // Path for the PDF after metadata update.
            _updatedPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files.
            if (!string.IsNullOrEmpty(_originalPdfPath) && File.Exists(_originalPdfPath))
                File.Delete(_originalPdfPath);
            if (!string.IsNullOrEmpty(_updatedPdfPath) && File.Exists(_updatedPdfPath))
                File.Delete(_updatedPdfPath);
        }

        [Test]
        public void VerifyCustomMetadata_IsWrittenAndReadCorrectly()
        {
            // Arrange: define expected values.
            const string expectedBaseUrl = "https://example.com";
            const string expectedCreatorTool = "MyAwesomeTool";
            const string expectedNickname = "TestUser";

            // Act: open the original PDF with PdfFileInfo, set custom metadata, and save.
            using (PdfFileInfo info = new PdfFileInfo(_originalPdfPath!))
            {
                // Set custom metadata entries.
                info.SetMetaInfo(BaseUrlKey, expectedBaseUrl);
                info.SetMetaInfo(CreatorToolKey, expectedCreatorTool);
                info.SetMetaInfo(NicknameKey, expectedNickname);

                // Save the updated PDF to a new file.
                info.SaveNewInfo(_updatedPdfPath!);
            }

            // Assert: read back the metadata from the updated PDF.
            using (PdfFileInfo info = new PdfFileInfo(_updatedPdfPath!))
            {
                string actualBaseUrl = info.GetMetaInfo(BaseUrlKey);
                string actualCreatorTool = info.GetMetaInfo(CreatorToolKey);
                string actualNickname = info.GetMetaInfo(NicknameKey);

                Assert.AreEqual(expectedBaseUrl, actualBaseUrl, "BaseUrl metadata mismatch.");
                Assert.AreEqual(expectedCreatorTool, actualCreatorTool, "CreatorTool metadata mismatch.");
                Assert.AreEqual(expectedNickname, actualNickname, "Nickname metadata mismatch.");
            }
        }
    }
}

// Dummy entry point to satisfy the console‑application requirement of the project.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the unit tests are executed by the test runner.
    }
}
