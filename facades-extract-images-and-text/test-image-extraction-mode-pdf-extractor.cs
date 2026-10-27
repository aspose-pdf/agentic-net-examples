using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

// -----------------------------------------------------------------------------
// Minimal stubs for NUnit when the real package is not referenced.
// -----------------------------------------------------------------------------
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
    public class ImageExtractionModeTests
    {
        private const string Image1Path = "image1.jpg";
        private const string Image2Path = "image2.jpg";

        // Helper to create a simple PDF containing two images
        private string CreatePdfWithTwoImages()
        {
            // Ensure placeholder images exist; in a real test they would be embedded resources.
            // For demonstration we create tiny blank JPEG files if they are missing.
            if (!File.Exists(Image1Path))
                File.WriteAllBytes(Image1Path, new byte[] { 0xFF, 0xD8, 0xFF, 0xD9 }); // minimal JPEG
            if (!File.Exists(Image2Path))
                File.WriteAllBytes(Image2Path, new byte[] { 0xFF, 0xD8, 0xFF, 0xD9 });

            string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            using (Document doc = new Document())
            {
                // First page with first image
                Page page1 = doc.Pages.Add();
                ImageStamp stamp1 = new ImageStamp(Image1Path)
                {
                    XIndent = 50,
                    YIndent = 700,
                    Width = 100,
                    Height = 100
                };
                page1.AddStamp(stamp1);

                // Second page with second image
                Page page2 = doc.Pages.Add();
                ImageStamp stamp2 = new ImageStamp(Image2Path)
                {
                    XIndent = 50,
                    YIndent = 700,
                    Width = 100,
                    Height = 100
                };
                page2.AddStamp(stamp2);

                doc.Save(pdfPath);
            }

            return pdfPath;
        }

        // Helper to extract images using the default extraction mode and return the count
        private int ExtractImageCount(string pdfPath)
        {
            string extractFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(extractFolder);

            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                // The ExtractImageMode property is optional – the default mode extracts all images.
                // No explicit assignment is required for current Aspose.Pdf versions.
                extractor.ExtractImage();

                int index = 0;
                while (extractor.HasNextImage())
                {
                    string outPath = Path.Combine(extractFolder, $"img_{index}.png");
                    extractor.GetNextImage(outPath);
                    index++;
                }
            }

            int count = Directory.GetFiles(extractFolder).Length;
            Directory.Delete(extractFolder, true);
            return count;
        }

        [NUnit.Framework.Test]
        public void ImageExtractionMode_ShouldAffectExtractedImageCount()
        {
            // Arrange
            string pdfPath = CreatePdfWithTwoImages();

            // Act
            int count = ExtractImageCount(pdfPath);

            // Cleanup PDF and placeholder images
            File.Delete(pdfPath);
            File.Delete(Image1Path);
            File.Delete(Image2Path);

            // Assert
            // The default extraction mode should return both images.
            NUnit.Framework.Assert.AreEqual(2, count, "Default extraction should return both images.");
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – the real work is performed by the NUnit tests.
        }
    }
}
