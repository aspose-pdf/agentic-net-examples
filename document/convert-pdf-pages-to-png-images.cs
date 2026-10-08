using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

// Minimal NUnit stubs to allow compilation without the NUnit package
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

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class PdfConversionTests
    {
        [NUnit.Framework.Test]
        public void ConvertPagesToImages_ShouldGenerateImageForEachPage()
        {
            // Arrange: create temporary directories for input PDF and output images
            string tempRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempRoot);
            string pdfPath = Path.Combine(tempRoot, "sample.pdf");
            string imageOutputDir = Path.Combine(tempRoot, "Images");
            Directory.CreateDirectory(imageOutputDir);

            // Create a PDF with a known number of pages (e.g., 3 pages)
            const int pageCount = 3;
            using (Document doc = new Document())
            {
                for (int i = 1; i <= pageCount; i++) // 1‑based indexing per rule
                {
                    doc.Pages.Add();
                }
                doc.Save(pdfPath);
            }

            // Act: convert each PDF page to an image using the core Device API
            using (Document doc = new Document(pdfPath))
            {
                // Use JpegDevice (core API) to render pages as JPEG images
                var resolution = new Resolution(300);
                var jpegDevice = new JpegDevice(resolution);

                int pageNumber = 1;
                foreach (Page page in doc.Pages)
                {
                    string imagePath = Path.Combine(imageOutputDir, $"page_{pageNumber}.jpg");
                    using (FileStream imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        jpegDevice.Process(page, imageStream);
                    }
                    pageNumber++;
                }
            }

            // Assert: verify that an image file exists for each PDF page
            string[] generatedImages = Directory.GetFiles(imageOutputDir, "page_*.jpg");
            NUnit.Framework.Assert.AreEqual(pageCount, generatedImages.Length,
                $"Expected {pageCount} image files, but found {generatedImages.Length}.");

            // Cleanup temporary files
            Directory.Delete(tempRoot, true);
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real test project this class would not be needed, but adding it removes the CS5001 error.
    public static class Program
    {
        public static void Main() { /* No‑op */ }
    }
}
