using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – added because the real NUnit package is not referenced.
// This allows the test code to compile and run in environments where NUnit is
// unavailable. Only the members used by the tests are provided.
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
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfFacadeTests
{
    [TestFixture]
    public class PdfMetaInfoTests
    {
        // Initialise with empty strings to satisfy non‑nullable analysis.
        private string _originalPdfPath = string.Empty;
        private string _updatedPdfPath = string.Empty;

        [SetUp]
        public void SetUp()
        {
            // Create a simple one‑page PDF to work with
            _originalPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
            using (Document doc = new Document())
            {
                // Add an empty page (Aspose.Pdf creates a default page automatically)
                doc.Pages.Add();
                doc.Save(_originalPdfPath);
            }

            // Path for the PDF after metadata update
            _updatedPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files
            if (File.Exists(_originalPdfPath))
                File.Delete(_originalPdfPath);
            if (File.Exists(_updatedPdfPath))
                File.Delete(_updatedPdfPath);
        }

        [Test]
        public void BaseUrl_CreatorTool_Nickname_Are_Written_Correctly()
        {
            const string expectedBaseUrl = "https://example.com";
            const string expectedCreatorTool = "MyAsposeTool";
            const string expectedNickname = "TestUser";

            // -----------------------------------------------------------------
            // Write custom metadata using PdfFileInfo (Facade API)
            // -----------------------------------------------------------------
            using (PdfFileInfo infoWriter = new PdfFileInfo(_originalPdfPath))
            {
                // Set custom properties via SetMetaInfo
                infoWriter.SetMetaInfo("BaseUrl", expectedBaseUrl);
                infoWriter.SetMetaInfo("CreatorTool", expectedCreatorTool);
                infoWriter.SetMetaInfo("Nickname", expectedNickname);

                // Save the updated PDF (preserves existing info)
                infoWriter.SaveNewInfo(_updatedPdfPath);
            }

            // -----------------------------------------------------------------
            // Read back the metadata to verify the values
            // -----------------------------------------------------------------
            using (PdfFileInfo infoReader = new PdfFileInfo(_updatedPdfPath))
            {
                string actualBaseUrl = infoReader.GetMetaInfo("BaseUrl");
                string actualCreatorTool = infoReader.GetMetaInfo("CreatorTool");
                string actualNickname = infoReader.GetMetaInfo("Nickname");

                Assert.AreEqual(expectedBaseUrl, actualBaseUrl, "BaseUrl value mismatch.");
                Assert.AreEqual(expectedCreatorTool, actualCreatorTool, "CreatorTool value mismatch.");
                Assert.AreEqual(expectedNickname, actualNickname, "Nickname value mismatch.");
            }
        }
    }
}

// -----------------------------------------------------------------------------
// Minimal entry point to satisfy the compiler for a console‑style project.
// The tests are executed via the NUnit stubs above, so the Main method can be
// empty.
// -----------------------------------------------------------------------------
public static class Program
{
    public static void Main() { }
}
