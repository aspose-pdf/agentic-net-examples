using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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
            {
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
            }
        }
    }
}

namespace AsposePdfFacadeTests
{
    [NUnit.Framework.TestFixture]
    public class PdfMetadataTests
    {
        private const string BaseUrlKey = "BaseUrl";
        private const string CreatorToolKey = "CreatorTool";
        private const string NicknameKey = "Nickname";

        private string CreateSamplePdf()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // ensure the document has at least one page
                doc.Save(tempPath);
            }
            return tempPath;
        }

        [NUnit.Framework.Test]
        public void Verify_BaseUrl_CreatorTool_Nickname_Are_Written_And_Read_Back()
        {
            // Arrange
            string sourcePdf = CreateSamplePdf();
            string expectedBaseUrl = "https://example.com";
            string expectedCreatorTool = "MyCreatorTool";
            string expectedNickname = "TestUser";

            // Act
            string updatedPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            using (PdfFileInfo pdfInfo = new PdfFileInfo(sourcePdf))
            {
                pdfInfo.Creator = expectedCreatorTool;
                pdfInfo.SetMetaInfo(BaseUrlKey, expectedBaseUrl);
                pdfInfo.SetMetaInfo(CreatorToolKey, expectedCreatorTool);
                pdfInfo.SetMetaInfo(NicknameKey, expectedNickname);
                pdfInfo.SaveNewInfo(updatedPdf);
            }

            // Assert
            using (PdfFileInfo pdfInfoRead = new PdfFileInfo(updatedPdf))
            {
                NUnit.Framework.Assert.AreEqual(expectedCreatorTool, pdfInfoRead.Creator, "Creator property mismatch.");

                string actualBaseUrl = pdfInfoRead.GetMetaInfo(BaseUrlKey);
                string actualCreatorTool = pdfInfoRead.GetMetaInfo(CreatorToolKey);
                string actualNickname = pdfInfoRead.GetMetaInfo(NicknameKey);

                NUnit.Framework.Assert.AreEqual(expectedBaseUrl, actualBaseUrl, "BaseUrl metadata mismatch.");
                NUnit.Framework.Assert.AreEqual(expectedCreatorTool, actualCreatorTool, "CreatorTool metadata mismatch.");
                NUnit.Framework.Assert.AreEqual(expectedNickname, actualNickname, "Nickname metadata mismatch.");
            }

            // Cleanup
            File.Delete(sourcePdf);
            File.Delete(updatedPdf);
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    // In a real test project the output type would be a library, but adding a minimal Main
    // method removes the CS5001 error without affecting the test logic.
    public static class Program
    {
        public static void Main()
        {
            // No operation – the tests are discovered and run by the test runner.
        }
    }
}
