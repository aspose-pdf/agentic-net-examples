using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

// Minimal NUnit stubs to allow compilation without the real NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

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

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class ConvertPagesToImagesTests
    {
        private string? _tempPdfPath;
        private string? _outputDir;

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            // Create a temporary directory for test artifacts
            _outputDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_outputDir);

            // Create a simple PDF with a known number of pages (e.g., 3 pages)
            _tempPdfPath = Path.Combine(_outputDir, "sample.pdf");
            using (Document doc = new Document())
            {
                // Add three blank pages
                doc.Pages.Add();
                doc.Pages.Add();
                doc.Pages.Add();

                // Save the PDF to the temporary path
                doc.Save(_tempPdfPath);
            }
        }

        [NUnit.Framework.TearDown]
        public void TearDown()
        {
            // Clean up temporary files and directories
            if (!string.IsNullOrEmpty(_tempPdfPath) && File.Exists(_tempPdfPath))
                File.Delete(_tempPdfPath);

            if (!string.IsNullOrEmpty(_outputDir) && Directory.Exists(_outputDir))
                Directory.Delete(_outputDir, true);
        }

        [NUnit.Framework.Test]
        public void ConvertPagesToImages_ShouldGenerateOneImagePerPage()
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(_tempPdfPath!))
            {
                // Prepare an image device (PNG) with default resolution
                Resolution resolution = new Resolution(150);
                PngDevice pngDevice = new PngDevice(resolution);

                // Convert each page to an image file
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string imagePath = Path.Combine(_outputDir!, $"page_{pageNumber}.png");
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                    }
                }

                // Verify that the number of generated image files matches the page count
                string[] generatedImages = Directory.GetFiles(_outputDir!, "page_*.png");
                NUnit.Framework.Assert.AreEqual(pdfDoc.Pages.Count, generatedImages.Length,
                    $"Expected {pdfDoc.Pages.Count} image files, but found {generatedImages.Length}.");
            }
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑style project.
public static class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required – tests are executed by the test runner.
    }
}
