using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Devices;            // For image devices (not used directly here)
using NUnit.Framework;               // Stubbed NUnit framework for compilation

// Minimal NUnit stubs to allow compilation without the real NUnit package.
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

    public delegate void TestDelegate();

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void AreEqual(object expected, object actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void Fail(string message)
        {
            throw new Exception(message);
        }

        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex);
            }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class ConvertPagesToImagesTests
    {
        // Path to a sample PDF used for the test. Adjust as needed.
        private const string SamplePdfPath = "sample.pdf";

        // Temporary directory for test artifacts.
        private string _tempDir = string.Empty;

        [SetUp]
        public void SetUp()
        {
            // Create a unique temporary directory for each test run.
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempDir);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up temporary files.
            if (Directory.Exists(_tempDir))
            {
                Directory.Delete(_tempDir, true);
            }
        }

        [Test]
        public void ConvertPagesToImages_FlagCreatesImagesForAllPages()
        {
            // Ensure the sample PDF exists.
            if (!File.Exists(SamplePdfPath))
            {
                Assert.Fail($"Sample PDF not found at path: {SamplePdfPath}");
            }

            // Load the PDF document (lifecycle rule: use using block).
            using (Document pdfDoc = new Document(SamplePdfPath))
            {
                int pageCount = pdfDoc.Pages.Count; // 1‑based page count

                // Prepare the output PPTX path.
                string pptxPath = Path.Combine(_tempDir, "output.pptx");

                // Configure PPTX save options to generate slides as images.
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true // Convert each PDF page to an image slide.
                };

                // Save the PDF as PPTX using explicit SaveOptions (lifecycle rule).
                pdfDoc.Save(pptxPath, pptxOptions);

                // Verify that the PPTX file was created.
                Assert.IsTrue(File.Exists(pptxPath), "PPTX file was not created.");

                // Open the PPTX (which is a ZIP archive) and count image entries.
                int imageCount;
                using (ZipArchive zip = ZipFile.OpenRead(pptxPath))
                {
                    // Images are stored under the "ppt/media" folder.
                    imageCount = zip.Entries
                                   .Where(e => e.FullName.StartsWith("ppt/media/", StringComparison.OrdinalIgnoreCase) &&
                                               (e.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                                e.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                e.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                                e.Name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                                                e.Name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase)))
                                   .Count();
                }

                // The number of extracted images should match the number of PDF pages.
                Assert.AreEqual(pageCount, imageCount,
                    $"Expected {pageCount} images in the PPTX, but found {imageCount}.");
            }
        }
    }
}

// Minimal entry point to satisfy the compiler for a console‑type project.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are executed via the test runner.
    }
}
