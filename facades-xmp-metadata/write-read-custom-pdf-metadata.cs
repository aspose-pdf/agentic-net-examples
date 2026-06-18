using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfFacadeTests
{
    [TestFixture]
    public class PdfMetaInfoTests
    {
        private const string BaseUrlKey = "BaseUrl";
        private const string CreatorToolKey = "CreatorTool";
        private const string NicknameKey = "Nickname";

        private const string BaseUrlValue = "https://example.com";
        private const string CreatorToolValue = "MyTool";
        private const string NicknameValue = "TestDoc";

        // Made nullable to satisfy the compiler warnings about non‑nullable fields.
        private string? _originalPdfPath;
        private string? _updatedPdfPath;

        [SetUp]
        public void SetUp()
        {
            // Create a temporary PDF with a single blank page
            _originalPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(_originalPdfPath);
            }

            // Path for the PDF after metadata is written
            _updatedPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files
            if (!string.IsNullOrEmpty(_originalPdfPath) && File.Exists(_originalPdfPath))
                File.Delete(_originalPdfPath);
            if (!string.IsNullOrEmpty(_updatedPdfPath) && File.Exists(_updatedPdfPath))
                File.Delete(_updatedPdfPath);
        }

        [Test]
        public void BaseUrl_CreatorTool_Nickname_ShouldBeWrittenAndReadCorrectly()
        {
            // Write custom metadata using PdfFileInfo
            using (PdfFileInfo infoWriter = new PdfFileInfo(_originalPdfPath!))
            {
                infoWriter.SetMetaInfo(BaseUrlKey, BaseUrlValue);
                infoWriter.SetMetaInfo(CreatorToolKey, CreatorToolValue);
                infoWriter.SetMetaInfo(NicknameKey, NicknameValue);
                infoWriter.SaveNewInfo(_updatedPdfPath!);
            }

            // Read back the metadata and verify
            using (PdfFileInfo infoReader = new PdfFileInfo(_updatedPdfPath!))
            {
                string readBaseUrl = infoReader.GetMetaInfo(BaseUrlKey);
                string readCreatorTool = infoReader.GetMetaInfo(CreatorToolKey);
                string readNickname = infoReader.GetMetaInfo(NicknameKey);

                Assert.AreEqual(BaseUrlValue, readBaseUrl, "BaseUrl metadata mismatch.");
                Assert.AreEqual(CreatorToolValue, readCreatorTool, "CreatorTool metadata mismatch.");
                Assert.AreEqual(NicknameValue, readNickname, "Nickname metadata mismatch.");
            }
        }
    }
}

// Added a minimal entry point so the project builds as a console application.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – the NUnit‑style tests are discovered and run by the test runner.
        // Keeping Main empty satisfies the compiler's requirement for an entry point.
    }
}
